
using System;
using System.ComponentModel.Design;
using System.IO;

using System.Linq;

using Carbon.Services;
using EnvDTE;
using EnvDTE80;
using Microsoft.CodeAnalysis;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.VisualStudio.Shell;
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

            CommandID menuCommandID = new CommandID(CommandSet, CommandId);

            OleMenuCommand command = new OleMenuCommand(Execute, menuCommandID)
            {
                Supported = true
            };
            command.BeforeQueryStatus += Command_BeforeQueryStatus;

            //var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(command);
        }

        private void Command_BeforeQueryStatus(object sender, EventArgs e)
        {
            MenuCommand button = (MenuCommand)sender;
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


        private async void Execute(object sender, EventArgs e)
        {
            object service = await ServiceProvider.GetServiceAsync(typeof(SVsTextManager));
            IVsTextManager2 textManager = service as IVsTextManager2;
            _ = textManager.GetActiveView2(1, null, (uint)_VIEWFRAMETYPE.vftCodeWindow, out IVsTextView view);

            view.GetSelection(out _, out _, out _, out _); //end could be before beginning
            view.GetSelectedText(out string selectedText);

            StatementSyntax syntax = SyntaxFactory.ParseStatement(selectedText);

            SyntaxNode formattedResult = Formatter.Format(syntax, new AdhocWorkspace());
            SyntaxSenderService.Send(formattedResult, Language);
        }


        string[] supportedFiles = new[] { ".cs", ".vb", ".fs", ".xaml", 
                                            ".html", ".json" };


        private CarbonTypes.Language Language
        {
            get
            {
                DTE2 dte = ServiceProvider.GetServiceAsync(typeof(DTE)).Result as DTE2;
                ProjectItem item = dte.SelectedItems.Item(1)?.ProjectItem;

                if (item != null)
                {
                    string fileExtension = Path.GetExtension(item.Name).ToLowerInvariant();

                    if (fileExtension == ".cs")
                        return CarbonTypes.Language.CSharp;

                    if(fileExtension == ".vb")
                        return CarbonTypes.Language.VBNet;

                    if (fileExtension == ".fs")
                        return CarbonTypes.Language.FSharp;

                    if (fileExtension == ".xaml")
                        return CarbonTypes.Language.XAML;

                    if (fileExtension == ".html")
                        return CarbonTypes.Language.HTML;

                    if (fileExtension == ".json")
                        return CarbonTypes.Language.JSON;


                    return CarbonTypes.Language.Unknown;

                }
                return CarbonTypes.Language.Unknown;
            }
        }


        private bool IsSupportedFileType
        {
            get
            {
                DTE2 dte = ServiceProvider.GetServiceAsync(typeof(DTE)).Result as DTE2;
                ProjectItem item = dte.SelectedItems.Item(1)?.ProjectItem;

                if(item != null)
                {
                    string fileExtension = Path.GetExtension(item.Name).ToLowerInvariant();
                    // Show the button only if a supported file is selected
                    return supportedFiles.Contains(fileExtension);
                }
                return false;
            }
        }

        private string SelectedText
        {
            get
            {
                object service = ServiceProvider.GetServiceAsync(typeof(SVsTextManager)).Result;
                IVsTextManager2 textManager = service as IVsTextManager2;
                _ = textManager.GetActiveView2(1, null, (uint)_VIEWFRAMETYPE.vftCodeWindow, out IVsTextView view);

                view.GetSelection(out _, out _, out _, out _); //end could be before beginning
                view.GetSelectedText(out string selectedText);

                return selectedText;
            }
        }


        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider => package;


        public static async Task InitializeAsync(AsyncPackage package, IMenuCommandService commandService)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            Instance = new SendMethodCommand(package, commandService);
        }


        public static SendMethodCommand Instance { get; private set; }

    }
}
