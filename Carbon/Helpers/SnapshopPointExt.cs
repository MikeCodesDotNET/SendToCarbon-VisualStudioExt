using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Carbon.Helpers
{
    internal static class SnapshopPointExt
    {
        public static Document GetDocument(this SnapshotPoint caretPosition)
        {
            return caretPosition.Snapshot.GetOpenDocumentInCurrentContextWithChanges();
        }

        public static async Task<SyntaxNode> GetSyntaxNodeAsync(this SnapshotPoint caretPosition)
        {
            var document = caretPosition.GetDocument();
            var root = await document.GetSyntaxRootAsync();
            var declarationSyntax = root?.FindToken(caretPosition).Parent.AncestorsAndSelf().OfType<SyntaxNode>().FirstOrDefault();

            return declarationSyntax;
        }

        public static async Task<SyntaxNode> GetSyntaxNodeAsync()
        {
            var textView = TextViewHelper.GetCurrentViewHost();
            SnapshotPoint caretPosition = textView.Caret.Position.BufferPosition;
            return await caretPosition.GetSyntaxNodeAsync();        
        }

    }
}
