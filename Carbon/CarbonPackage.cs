using Carbon.Services;
using Carbon.UI;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;

using Task = System.Threading.Tasks.Task;

namespace Carbon
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(CarbonPackage.guidCarbonPackageString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    //[ProvideUIContextRule(uiContextSupportedFiles,
    //    name: "Supported Files",
    //    expression: "CSharp | VisualBasic",
    //    termNames: new[] { "CSharp", "VisualBasic" },
    //    termValues: new[] { "HierSingleSelectionName:.cs$", "HierSingleSelectionName:.vb$" })]
    [ProvideOptionPage(typeof(OptionsDialogPage), "Send to Carbon", "General", 0, 0, true)]
    public sealed class CarbonPackage : AsyncPackage
    {
        /// <summary>
        /// CarbonPackage GUID string.
        /// </summary>
        public const string guidCarbonPackageString = "e46db443-e97d-4021-8366-84b752023426";
        public static Guid guidCarbonPackage = new Guid(guidCarbonPackageString);

        public const string guidCarbonPackageCmdSetString = "590024c4-edbb-48b4-b482-8e0c6f6ff3a4";
        public static Guid guidCarbonPackageCmdSet = new Guid(guidCarbonPackageCmdSetString);

        private const string uiContextSupportedFiles = "24551deb-f034-43e9-a279-0e541241687e"; // Must match guid in VsCommandTable.vsct


        public const string guidImagesString = "c562d224-03b6-4f55-ba0f-34ddf862bf2d";
        public static Guid guidImages = new Guid(guidImagesString);

        public const string guidImages1String = "9865b00d-e515-41d3-8952-316efac05c45";
        public static Guid guidImages1 = new Guid(guidImages1String);

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);
            var commandService = await GetServiceAsync((typeof(IMenuCommandService))) as IMenuCommandService;

            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await Commands.SendMethodCommand.InitializeAsync(this, commandService);

            await Task.Factory.StartNew(() => VisualStudioServices.ComponentModel = GetService(typeof(SComponentModel)) as IComponentModel);
        }

        #endregion


    }
}
