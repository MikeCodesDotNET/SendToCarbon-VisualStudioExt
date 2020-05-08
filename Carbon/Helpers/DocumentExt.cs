using Carbon.Configuration;
using Microsoft.CodeAnalysis;
using System.IO;

namespace Carbon.Helpers
{

    public static class DocumentExt
    {
        public static Language GetLanguage(this Document document)
        {
            var fileInfo = new FileInfo(document.FilePath);
            var fileExtension = fileInfo.Extension;

            switch (fileExtension)
            {
                case ".cs":
                    return Language.CSharp;
                case ".fs":
                    return Language.FSharp;
                case "vb":
                    return Language.VBNet;
                default:
                    return Language.Unknown;
            }
        }

        public static string GetName(this Document document, bool includeExtension = false)
        {
            var fileInfo = new FileInfo(document.FilePath);

            if(!includeExtension)
                return Path.GetFileNameWithoutExtension(fileInfo.FullName);

            return fileInfo.Name;
        }

        public static Document GetCurrentDocument()
        {
            var textView = TextViewHelper.GetCurrentViewHost();
            var caretPosition = textView.Caret.Position.BufferPosition;
            return caretPosition.GetDocument();
        }


    }
}
