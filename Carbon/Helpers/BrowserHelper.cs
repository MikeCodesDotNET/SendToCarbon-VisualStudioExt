using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Helpers
{
    public static class BrowserHelper
    {

        public static void OpenUrl(Uri uri)
        {
            //string browserPath = GetBrowserPath();
            //if (browserPath == string.Empty)
            //    browserPath = "iexplore";
            //Process process = new Process();
            //process.StartInfo = new ProcessStartInfo(browserPath);
            //process.StartInfo.Arguments = "\"" + uri.ToString() + "\"";
            //process.Start();

            Process.Start(uri.ToString());
        }

        private static string GetBrowserPath()
        {
            string browser = string.Empty;
            RegistryKey key = null;

            try
            {
                // try location of default browser path in XP
                key = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                // try location of default browser path in Vista
                if (key == null)
                {
                    key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http", false); ;
                }

                if (key != null)
                {
                    //trim off quotes
                    browser = key.GetValue(null).ToString().ToLower().Replace("\"", "");
                    if (!browser.EndsWith("exe"))
                    {
                        //get rid of everything after the ".exe"
                        browser = browser.Substring(0, browser.LastIndexOf(".exe") + 4);
                    }

                    key.Close();
                }
            }
            catch
            {
                return string.Empty;
            }

            return browser;
        }
    }
}
