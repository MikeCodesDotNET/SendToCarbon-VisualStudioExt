using Microsoft.VisualStudio.Text;

namespace Carbon.Helpers
{
    internal static class BufferExt
    {
        public static string GetFileName(this ITextBuffer textBuffer)
        {
            textBuffer.Properties.TryGetProperty(typeof(ITextDocument), out ITextDocument document);
            return document == null ? null : document.FilePath;
        }
    }
}
