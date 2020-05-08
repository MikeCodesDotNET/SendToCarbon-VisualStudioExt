using Carbon.Services;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace Carbon.Helpers
{
    public static class TextViewHelper
    {
        public static IWpfTextView GetCurrentViewHost()
        {
            var textManager = (IVsTextManager)ServiceProvider.GlobalProvider.GetService(typeof(SVsTextManager));
            var componentModel = (IComponentModel)ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
            var editor = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            textManager.GetActiveView(1, null, out IVsTextView textViewCurrent);
            return editor.GetWpfTextView(textViewCurrent);
        }

        public static ITextDocument GetTextDocumentForView(IWpfTextViewHost viewHost)
        {
            ITextDocument document;
            viewHost.TextView.TextDataModel.DocumentBuffer.Properties.TryGetProperty(typeof(ITextDocument), out document);
            return document;
        }


        public static ITextSelection GetSelection(IWpfTextViewHost viewHost)
        {
            return viewHost.TextView.Selection;
        }

    }
}
