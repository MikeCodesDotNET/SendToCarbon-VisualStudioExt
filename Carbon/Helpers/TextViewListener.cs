
using System.Collections.ObjectModel;
using System.Composition;
using System.Linq;
using System.Windows.Threading;
using Microsoft.VisualStudio.Editor;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace Carbon.Helpers
{
    [Export(typeof(IWpfTextViewConnectionListener))]
    [Export(typeof(TextViewListener))]      // To let unit tests modify the instance
    [ContentType("CSharp")]
    public class TextViewListener : IWpfTextViewConnectionListener
    {
        public async void SubjectBuffersConnected(IWpfTextView textView, ConnectionReason reason, Collection<ITextBuffer> subjectBuffers)
        {
            if(!subjectBuffers.Any(b => b.ContentType.IsOfType("CSharp") || b.ContentType.IsOfType("Basic")))
            {
                return;
            }

            await Dispatcher.Yield();

            IVsTextView textViewAdapter = EditorAdaptersFactoryService.GetViewAdapter(textView);
            if(textViewAdapter == null)
            {
                return;
            }

            if(!TextDocumentFactoryService.TryGetTextDocument(textView.TextDataModel.DocumentBuffer, out _))
            {
                return;
            }
        }

        public void SubjectBuffersDisconnected(IWpfTextView textView, ConnectionReason reason, Collection<ITextBuffer> subjectBuffers) { }


        [Import]
        public IVsEditorAdaptersFactoryService EditorAdaptersFactoryService { get; set; }


        [Import]
        public SVsServiceProvider ServiceProvider { get; set; }

        [Import]
        public ITextDocumentFactoryService TextDocumentFactoryService { get; set; }

    }
}
