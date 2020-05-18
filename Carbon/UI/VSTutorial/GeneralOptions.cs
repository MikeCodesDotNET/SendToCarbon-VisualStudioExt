using Carbon.Configuration;
using Carbon.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Options
{
    internal class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        [Category("Browser")]
        [DisplayName("Use Cached settings")]
        [Description("Use your existing cached Carbon settings.")]
        [DefaultValue(false)]
        public bool UseBrowserCache { get; set; } = false;

        [Category("Syntax")]
        [DisplayName("Remove comments")]
        [Description("Remove comments from snippets")]
        [DefaultValue(false)]
        public bool RemoveCommentsChoice { get; set; } = false;

        [Category("Syntax")]
        [DisplayName("Include Trivia")]
        [Description("Keeps the leading and trailing trivia.")]
        [DefaultValue(true)]
        public bool IncludeTriviaChoice { get; set; } = true;
    }
}
