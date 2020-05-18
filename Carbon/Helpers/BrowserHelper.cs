using System;
using System.Diagnostics;

namespace Carbon.Helpers
{
    public static class BrowserHelper
    {

        public static void OpenUrl(Uri uri) =>
 //string browserPath = GetBrowserPath();
 //if (browserPath == string.Empty)
 //    browserPath = "iexplore";
            //Process process = new Process();
            //process.StartInfo = new ProcessStartInfo(browserPath);
            //process.StartInfo.Arguments = "\"" + uri.ToString() + "\"";
            //process.Start();

            Process.Start(uri.ToString());

    }
}
