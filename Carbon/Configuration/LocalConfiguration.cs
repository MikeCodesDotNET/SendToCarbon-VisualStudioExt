using Carbon.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Configuration
{
    public class LocalConfiguration
    {
        Color DefaultBackground => Color.FromArgb(100, 171, 184, 195);

        public static LocalConfiguration Default = new LocalConfiguration();

        public LocalConfiguration()
        {
            RestoreDefaults();
        }

        public void RestoreDefaults()
        {
            AutoAdjustWidth = true;
            BackgroundColor = DefaultBackground;
            DropShadow = true;
            FontFamily = FontFamily.Hack;
            FontSize = 13;
            LineNumbers = false;
            PaddingHorizontal = 32;
            PaddingVertical = 48;
            Theme = Theme.Seti;
            WindowControls = true;
            UseBrowserCache = false;
            WindowTheme = WindowTheme.None;
            LineHeight = 133;
            DropShadowOffsetY = 20;
            DropShadowBlurRadius = 68;
            ShowWaterMark = false;
            Timestamp = false;
            ExportSize = ExportSize.TwoX;
        }



        public static explicit operator LocalConfiguration(CarbonConfiguration carbonConfiguration) => InternalConvert(carbonConfiguration);

        internal static LocalConfiguration InternalConvert(CarbonConfiguration carbonConfiguration)
        {
            var addInConfiguration = new LocalConfiguration();

            addInConfiguration.AutoAdjustWidth = carbonConfiguration.WidthAdjustment;
            addInConfiguration.WindowControls = carbonConfiguration.WindowControls;
            addInConfiguration.WindowTheme = EnumExt.GetValueFromDescription<WindowTheme>(carbonConfiguration.WindowTheme);
            addInConfiguration.Theme = EnumExt.GetValueFromDescription<Theme>(carbonConfiguration.Theme);

            var splitString = carbonConfiguration.BackgroundColor.Replace("rgba(", "").Replace(")", "").Split(',');
            addInConfiguration.BackgroundColor = Color.FromArgb(int.Parse(splitString[3]), int.Parse(splitString[0]), int.Parse(splitString[1]), int.Parse(splitString[2]));

            addInConfiguration.DropShadow = carbonConfiguration.DropShadow;
            addInConfiguration.DropShadowBlurRadius = int.Parse(carbonConfiguration.DropShadowOffsetY.Replace("px", ""));
            addInConfiguration.DropShadowOffsetY = int.Parse(carbonConfiguration.DropShadowBlurRadius.Replace("px", ""));

            addInConfiguration.FontFamily = EnumExt.GetValueFromDescription<FontFamily>(carbonConfiguration.FontFamily);
            addInConfiguration.FontSize = int.Parse(carbonConfiguration.FontSize.Replace("px", ""));
            addInConfiguration.LineNumbers = carbonConfiguration.LineNumbers;
            addInConfiguration.LineHeight = int.Parse(carbonConfiguration.LineHeight.Replace("%", ""));
            addInConfiguration.ExportSize = EnumExt.GetValueFromDescription<ExportSize>(carbonConfiguration.ExportSize);
            addInConfiguration.ShowWaterMark = carbonConfiguration.Watermark;

            addInConfiguration.PaddingHorizontal = int.Parse(carbonConfiguration.PaddingHorizontal.Replace("px", ""));
            addInConfiguration.PaddingVertical = int.Parse(carbonConfiguration.PaddingVertical.Replace("px", ""));

            return addInConfiguration;
        }



        #region Properties 

        [ToolTip("Automatically adjust the width of the window.")]
        public bool AutoAdjustWidth { get; set; }


        [ToolTip("Color to use as the background behind the window.")]
        public Color BackgroundColor { get; set; }


        [ToolTip("Render a drop shadow on the window.")]
        public bool DropShadow { get; set; }

        [ToolTip("The font family to use.")]
        public FontFamily FontFamily { get; set; }

        int fontSize;
        [ToolTip("The font size. Min: 10, Max: 18")]
        public int FontSize
        {
            get => fontSize;
            set => fontSize = Clamp<int>(value, 10, 18);
        }


        [ToolTip("Show line numbers.")]
        public bool LineNumbers { get; set; }

        int paddingHorizontal;
        [ToolTip("The amount of horizontal padding applied to the window.  Min: 0, Max: 100")]
        public int PaddingHorizontal
        {
            get => paddingHorizontal;
            set => paddingHorizontal = Clamp<int>(value, 0, 100);
        }


        int paddingVertical;
        [ToolTip("The amount of vertical padding applied to the window. Min: 0, Max: 200")]
        public int PaddingVertical
        {
            get => paddingVertical;
            set => paddingVertical = Clamp<int>(value, 0, 200);
        }

        [ToolTip("Pick the theme to use.")]
        public Theme Theme { get; set; }


        [ToolTip("Show window controls.")]
        public bool WindowControls { get; set; }

        [ToolTip("Use your browsers already configured Carbon settings.")]
        public bool UseBrowserCache { get; set; }

        [ToolTip("The window theme to use. Options: none, sharp, bw.")]
        public WindowTheme WindowTheme { get; set; }

        int lineHeight;
        [ToolTip("The line height to use. Min: 90, Max: 250")]
        public int LineHeight
        {
            get => lineHeight;
            set => lineHeight = Clamp<int>(value, 90, 250);
        }



        int dropShadowOffsetY;
        [ToolTip("The drop shadow offset. Min: 0, Max: 100")]
        public int DropShadowOffsetY
        {
            get => dropShadowOffsetY;
            set => dropShadowOffsetY = Clamp<int>(value, 0, 100);
        }


        int dropShadowBlurRadius;
        [ToolTip("The drop shadow blur radius. Min: 0, Max: 100")]
        public int DropShadowBlurRadius
        {
            get => dropShadowBlurRadius;
            set => dropShadowBlurRadius = Clamp<int>(value, 0, 100);
        }

        [ToolTip("Show a watermark.")]
        public bool ShowWaterMark { get; set; }


        [ToolTip("Timestamp file name.")]
        public bool Timestamp { get; set; }

        [ToolTip("Export size.")]
        public ExportSize ExportSize { get; set; }


        #endregion

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }

    public enum ExportSize
    {
        //Default
        [Description("2x")]
        [Id("1x")]
        TwoX = 1,

        [Description("1x")]
        [Id("2x")]
        OneX = 2,

        [Description("4x")]
        [Id("4x")]
        FourX = 4,
    }


    public enum Language
    {
        Unknown,

        [Description("C#")]
        [Id("text/x-csharp")]
        CSharp,

        [Description("F#")]
        [Id("mllike")]

        FSharp,
        [Description("VB.NET")]
        [Id("vb")]
        VBNet,

    }


    public enum FontFamily
    {
        [Description("Anonymous Pro")]
        [Id("Anonymous Pro")]
        AnonymousPro,
        [Description("Droid Sans Mono")]
        [Id("Droid Sans Mono")]
        DroidSansMono,
        [Description("Fantasque San Mono")]
        [Id("Fantasque San Mono")]
        FantasqueSanMono,
        [Description("Fira Code")]
        [Id("Fira Code")]
        FiraCode,
        [Description("Hack")]
        [Id("Hack")]
        Hack,
        [Description("IBM Plex Mono")]
        [Id("IBM Plex Mono")]
        IBMPlexMono,
        [Description("Inconsolata")]
        [Id("Inconsolata")]
        Inconsolata,
        [Description("Iosevka")]
        [Id("Iosevka")]
        Iosevka,
        [Description("JetBrains Mono")]
        [Id("JetBrains Mono")]
        JetBrainsMono,
        [Description("Monoid")]
        [Id("Monoid")]
        Monoid,
        [Description("Source Code Pro")]
        [Id("Source Code Pro")]
        SourceCodePro,
        [Description("Space Mono")]
        [Id("Space Mono")]
        SpaceMono,
        [Description("Ubunutu Mono")]
        [Id("Ubunutu Mono")]
        UbuntuMono,
    }


    public enum Theme
    {
        [Description("3024 Night")]
        [Id("3024-night")]
        Night,

        [Description("A11y Dark")]
        [Id("a11y-dark")]
        A11yDark,

        [Description("Blackboard")]
        [Id("blackboard")]
        Blackboard,

        [Description("Base 16 (Dark)")]
        [Id("base16-dark")]
        Base16Dark,

        [Description("Base 16 (Light)")]
        [Id("base16-light")]
        Base16Light,

        [Description("Cobalt")]
        [Id("cobalt")]
        Cobalt,

        [Description("Dracula")]
        [Id("dracula")]
        Dracula,

        [Description("Duotone")]
        [Id("duotone-dark")]
        Duotone,

        [Description("Hopscotch")]
        [Id("hopscotch")]
        Hopscotch,

        [Description("Lucario")]
        [Id("lucario")]
        Lucario,

        [Description("Material")]
        [Id("material")]
        Material,

        [Description("Monokai")]
        [Id("monokai")]
        Monokai,

        [Description("Night Owl")]
        [Id("night-owl")]
        NightOwl,

        [Description("Nord")]
        [Id("nord")]
        Nord,

        [Description("Oceanic Next")]
        [Id("oceanic-next")]
        OceanicNext,

        [Description("One Light")]
        [Id("one-light")]
        OneLight,

        [Description("One Dark")]
        [Id("one-dark")]
        OneDark,

        [Description("Panda")]
        [Id("panda-syntax")]
        Panda,

        [Description("Paraiso")]
        [Id("paraiso-dark")]
        Paraiso,

        [Description("Seti")]
        [Id("seti")]
        Seti,

        [Description("Shades of Purple")]
        [Id("shades-of-purple")]
        ShadesOfPurple,

        [Description("Solarized (Dark)")]
        [Id("solarized dark")]
        SolarizedDark,

        [Description("Solarized (Light)")]
        [Id("solarized light")]
        SolarizedLight,

        [Description("SynthWave '84")]
        [Id("synthwave-84")]
        SynthWave84,

        [Description("Twilight")]
        [Id("twilight")]
        Twilight,

        [Description("Verminal")]
        [Id("verminal")]
        Verminal,

        [Description("VSCode")]
        [Id("vscode")]
        VSCode,

        [Description("Yeti")]
        [Id("yeti")]
        Yeti,

        [Description("Zenburn")]
        [Id("zenburn")]
        Zenburn,
    }

    public enum WindowTheme
    {
        [Description("Rounded Corners")]
        None,

        [Description("Sharp Corners")]
        Sharp,

        [Description("Black & White")]
        Bw,
    }
}
