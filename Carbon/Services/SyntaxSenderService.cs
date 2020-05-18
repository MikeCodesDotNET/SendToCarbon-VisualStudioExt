using Carbon.CarbonTypes;
using Carbon.Configuration;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Specialized;
using System.Web;
using System.Windows.Forms;
using Carbon.Helpers;
using Carbon.Helpers.Extensions;

namespace Carbon.Services
{
    public static class SyntaxSenderService
    {
        private static int MaxLength = 1000;

        private static string RootUrl => "https://carbon.now.sh";


        public static bool Send(SyntaxNode syntax, Language language)
        {
            string sourceCode = string.Empty;
            if (GeneralSettings.Default.IncludeTrivia)
            {
                sourceCode = syntax.ToFullString();
            }
            else
            {
                sourceCode = syntax.ToString();
            }

            if (sourceCode.Length > MaxLength)
            {
                MessageBox.Show($"Snippet too long", "The snippet provided had a length of {syntax.Length}. Max is {MaxLength}", MessageBoxButtons.OK);
                return false;
            }



            var configuration = LocalConfiguration.Default;

            var carbonConfiguration = (CarbonConfiguration)configuration;
            carbonConfiguration.Language = language.GetId();
            carbonConfiguration.FirstLineNumber = syntax.GetLocation().GetMappedLineSpan().StartLinePosition.Line + 1;

            UriBuilder uriBuilder;
            NameValueCollection parameters;

            if (GeneralSettings.Default.UseBrowserCache)
            {
                uriBuilder = new UriBuilder(RootUrl);
                parameters = HttpUtility.ParseQueryString(string.Empty);
                parameters["code"] = @sourceCode;
                uriBuilder.Query = parameters.ToString();
                BrowserHelper.OpenUrl(uriBuilder.Uri);
                return true;
            }

            uriBuilder = new UriBuilder(RootUrl);
            parameters = HttpUtility.ParseQueryString(string.Empty);

            parameters["bg"] = carbonConfiguration.BackgroundColor;
            parameters["t"] = carbonConfiguration.Theme;
            parameters["wt"] = carbonConfiguration.WindowTheme;
            parameters["l"] = carbonConfiguration.Language;
            parameters["ds"] = carbonConfiguration.DropShadow.ToString().ToLower();
            parameters["dsyoff"] = carbonConfiguration.DropShadowOffsetY;
            parameters["dsblur"] = carbonConfiguration.DropShadowBlurRadius;
            parameters["wc"] = carbonConfiguration.WindowControls.ToString().ToLower();
            parameters["wa"] = carbonConfiguration.WidthAdjustment.ToString().ToLower();
            parameters["pv"] = carbonConfiguration.PaddingVertical;
            parameters["ph"] = carbonConfiguration.PaddingHorizontal;
            parameters["ln"] = carbonConfiguration.LineNumbers.ToString().ToLower();
            parameters["f"] = carbonConfiguration.FontFamily;
            parameters["fs"] = carbonConfiguration.FontSize;
            parameters["lh"] = carbonConfiguration.LineHeight;
            parameters["wm"] = carbonConfiguration.Watermark.ToString().ToLower();
            parameters["ts"] = true.ToString().ToLower();
            parameters["es"] = carbonConfiguration.ExportSize;
            parameters["code"] = @sourceCode;
            uriBuilder.Query = parameters.ToString();
            BrowserHelper.OpenUrl(uriBuilder.Uri);

            return true;
        }
    }

}
