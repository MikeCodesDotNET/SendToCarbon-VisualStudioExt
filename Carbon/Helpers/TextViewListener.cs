using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Windows;
using System.Composition;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Editor;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Linq;
using Carbon.Commands;

namespace Carbon.Helpers
{
	[Export(typeof(IWpfTextViewConnectionListener))]
	[Export(typeof(TextViewListener))]      // To let unit tests modify the instance
	[ContentType("CSharp")]
	public class TextViewListener : IWpfTextViewConnectionListener
	{
		public TextViewListener()
		{
			}


		[Import]
		public SVsServiceProvider ServiceProvider { get; set; }


		[Import]
		public IVsEditorAdaptersFactoryService EditorAdaptersFactoryService { get; set; }

		[Import]
		public ITextDocumentFactoryService TextDocumentFactoryService { get; set; }

		public async void SubjectBuffersConnected(IWpfTextView textView, ConnectionReason reason, Collection<ITextBuffer> subjectBuffers)
		{
			if(!subjectBuffers.Any(b => b.ContentType.IsOfType("CSharp") || b.ContentType.IsOfType("Basic")))
				return;

			// VS2010 only creates TextViewAdapters later; wait for it to exist.
			await Dispatcher.Yield();

			var textViewAdapter = EditorAdaptersFactoryService.GetViewAdapter(textView);
			if(textViewAdapter == null)
				return;
			ITextDocument document;
			if(!TextDocumentFactoryService.TryGetTextDocument(textView.TextDataModel.DocumentBuffer, out document))
				return;

			// Register the native command first, so that it ends up earlier in
			// the command chain than our interceptor. This prevents the native
			// comand from being intercepted too.

			//textView.Properties.GetOrCreateSingletonProperty(() => new SendMethodCommand(textViewAdapter, textView));
		}

		public void SubjectBuffersDisconnected(IWpfTextView textView, ConnectionReason reason, Collection<ITextBuffer> subjectBuffers) { }
	}
}
