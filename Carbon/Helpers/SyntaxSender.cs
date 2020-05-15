using Carbon.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Carbon.Helpers
{
    public static class SyntaxSender
    {
        static int MaxLength = 1000;
        static string RootUrl => "https://carbon.now.sh";


        public static bool Send(SyntaxNode syntax, Language language)
        {
            if (syntax.ToFullString().Length > MaxLength)
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

            if (configuration.UseBrowserCache)
            {
                uriBuilder = new UriBuilder(RootUrl);
                parameters = HttpUtility.ParseQueryString(string.Empty);
                parameters["code"] = syntax.ToFullString();
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
            parameters["code"] = syntax.ToFullString();
            uriBuilder.Query = parameters.ToString();
            BrowserHelper.OpenUrl(uriBuilder.Uri);

            return true;
        }
    }
    
}
