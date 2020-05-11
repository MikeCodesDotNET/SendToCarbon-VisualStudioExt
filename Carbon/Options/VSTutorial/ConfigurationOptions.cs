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
    internal class ConfigurationOptions : BaseOptionModel<ConfigurationOptions>
    {
        [Category("Style")]
        [DisplayName("Theme")]
        [Description("The theme used to create the image.")]
        [DefaultValue(Theme.Seti)]
        [TypeConverter(typeof(EnumConverter))] // This will make use of enums more resilient
        public Theme ThemeChoice { get; set; } = Theme.Seti;
              

        [Category("Style")]
        [DisplayName("Background Color")]
        [Description("The background color of the image.")]
        [TypeConverter(typeof(ColorConverter))]
        public Color BackgroundColorChoice { get; set; } = ColorTranslator.FromHtml("#400A6F");

        [Category("Window")]
        [DisplayName("Theme")]
        [Description("The style of the containing window.")]
        [DefaultValue(WindowTheme.None)]
        [TypeConverter(typeof(EnumConverter))]
        public WindowTheme WindowThemeChoice { get; set; } = WindowTheme.None;

        [Category("Window")]
        [DisplayName("Show Controls")]
        [Description("Show the window interaction controls")]
        [DefaultValue(true)]
        public bool ShowWindowControlsChoice { get; set; } = true;



        [Category("Drop Shadow")]
        [DisplayName("Enabled")]
        [Description("Include a drop shadow to window.")]
        [DefaultValue(true)]
        public bool AddDropShadowChoice { get; set; } = true;


        [Category("Drop Shadow")]
        [DisplayName("Y Offset (px)")]
        [Description("The amount of pixels the shadow is offset from the window.")]
        [DefaultValue(20)]
        public int DropShadowOffsetYChoice { get; set; } = 20;


        [Category("Drop Shadow")]
        [DisplayName("Blur Radius (px)")]
        [Description("The radious of the drop shadow blur.")]
        [DefaultValue(68)]
        public int DropShadowBlurRadiusChoice { get; set; } = 68;



    }
}
