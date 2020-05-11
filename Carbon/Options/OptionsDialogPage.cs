using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.VisualStudio.ComponentModelHost;

namespace Carbon.Options
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false)]
    [ComVisible(true)]
    [Guid("1D9ECCF3-5D2F-4112-9B25-264596873DC9")]
    public class OptionsDialogPage : UIElementDialogPage
    {
        OptionsView optionsView;

        protected override UIElement Child
        {
            get { return optionsView ?? (optionsView = new OptionsView()); }
        }

        protected override void OnActivate(CancelEventArgs e)
        {
            base.OnActivate(e);

          
        }

        protected override void OnApply(PageApplyEventArgs args)
        {
            if (args.ApplyBehavior == ApplyKind.Apply)
            {

            }

            base.OnApply(args);
        }

    }
}
