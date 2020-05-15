using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Carbon.Helpers;
using EnvDTE;
using EnvDTE80;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Task = System.Threading.Tasks.Task;

namespace Carbon.Commands
{
    internal sealed class SendMethodCommand
    {
        public const int CommandId = 256;

        public static readonly Guid CommandSet = new Guid("d5d8efc6-dc17-4229-9088-dddf76ac0ae4");

        private readonly AsyncPackage package;


        private SendMethodCommand(AsyncPackage package, IMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);

            var command = new OleMenuCommand(Execute, menuCommandID)
            {
                Supported = true
            };
            command.BeforeQueryStatus += Command_BeforeQueryStatus;

            //var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(command);
        }

        private void Command_BeforeQueryStatus(object sender, EventArgs e)
        {
            var button = (MenuCommand)sender;
            button.Visible = false;

            try
            {    
                if(!string.IsNullOrEmpty(SelectedText) && IsSupportedFileType)
                {
                    button.Visible = true;
                }
            }
            catch
            {
                button.Visible = false;
            }
        }

        bool IsSupportedFileType
        {
            get
            {
                var dte = ServiceProvider.GetServiceAsync(typeof(DTE)).Result as DTE2;
                ProjectItem item = dte.SelectedItems.Item(1)?.ProjectItem;

                if (item != null)
                {
                    string fileExtension = Path.GetExtension(item.Name).ToLowerInvariant();
                    string[] supportedFiles = new[] { ".cs", ".vb" };

                    // Show the button only if a supported file is selected
                    return supportedFiles.Contains(fileExtension);
                }
                return false;
            }
        }

        string SelectedText
        {
            get
            {
                var service = ServiceProvider.GetServiceAsync(typeof(SVsTextManager)).Result;
                var textManager = service as IVsTextManager2;
                IVsTextView view;
                int result = textManager.GetActiveView2(1, null, (uint)_VIEWFRAMETYPE.vftCodeWindow, out view);

                view.GetSelection(out int startLine, out int startColumn, out int endLine, out int endColumn); //end could be before beginning
                view.GetSelectedText(out string selectedText);

                return selectedText;
            }
        }


    
        public static SendMethodCommand Instance
        {
            get;
            private set;
        }

    
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }


        public static async Task InitializeAsync(AsyncPackage package, IMenuCommandService commandService)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            Instance = new SendMethodCommand(package, commandService);
        }


        private async void Execute(object sender, EventArgs e)
        {   
            var service = await ServiceProvider.GetServiceAsync(typeof(SVsTextManager));
            var textManager = service as IVsTextManager2;
            IVsTextView view;
            int result = textManager.GetActiveView2(1, null, (uint)_VIEWFRAMETYPE.vftCodeWindow, out view);

            view.GetSelection(out int startLine, out int startColumn, out int endLine, out int endColumn); //end could be before beginning
            view.GetSelectedText(out string selectedText);

            var language = DocumentExt.GetLanguage(DocumentExt.GetCurrentDocument());
            var syntax = SyntaxFactory.ParseStatement(selectedText);

            var formattedResult = Formatter.Format(syntax, new AdhocWorkspace());
            SyntaxSender.Send(formattedResult, language);
        }
    }
}
