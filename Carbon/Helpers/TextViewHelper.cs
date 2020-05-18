using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Carbon.Helpers
{
    public static class TextViewHelper
    {

        public static IWpfTextView GetCurrentViewHost()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IVsTextManager textManager = (IVsTextManager)ServiceProvider.GlobalProvider.GetService(typeof(SVsTextManager));
            IComponentModel componentModel = (IComponentModel)ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
            IVsEditorAdaptersFactoryService editor = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            textManager.GetActiveView(1, null, out IVsTextView textViewCurrent);
            return editor.GetWpfTextView(textViewCurrent);
        }


        public static ITextSelection GetSelection(IWpfTextViewHost viewHost) => viewHost.TextView.Selection;

        public static ITextDocument GetTextDocumentForView(IWpfTextViewHost viewHost)
        {
            viewHost.TextView.TextDataModel.DocumentBuffer.Properties.TryGetProperty(typeof(ITextDocument), out ITextDocument document);
            return document;
        }

    }
}
