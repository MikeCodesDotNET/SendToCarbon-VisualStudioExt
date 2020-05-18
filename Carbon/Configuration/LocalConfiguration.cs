
using System;
using System.Drawing;
using Carbon.CarbonTypes;
using Carbon.Helpers;
using Carbon.Helpers.Extensions;

namespace Carbon.Configuration
{
    public class LocalConfiguration
    {

        public static LocalConfiguration Default = new LocalConfiguration();

        public LocalConfiguration() => RestoreDefaults();


        public static explicit operator LocalConfiguration(CarbonConfiguration carbonConfiguration) => InternalConvert(carbonConfiguration);


        private Color DefaultBackground => Color.FromArgb(100, 171, 184, 195);

        internal static LocalConfiguration InternalConvert(CarbonConfiguration carbonConfiguration)
        {
            LocalConfiguration addInConfiguration = new LocalConfiguration
            {
                AutoAdjustWidth = carbonConfiguration.WidthAdjustment,
                WindowControls = carbonConfiguration.WindowControls,
                WindowTheme = EnumExt.GetValueFromDescription<WindowTheme>(carbonConfiguration.WindowTheme),
                Theme = EnumExt.GetValueFromDescription<Theme>(carbonConfiguration.Theme)
            };

            string[] splitString = carbonConfiguration.BackgroundColor.Replace("rgba(", string.Empty).Replace(")", string.Empty).Split(',');
            addInConfiguration.BackgroundColor = Color.FromArgb(int.Parse(splitString[3]), int.Parse(splitString[0]), int.Parse(splitString[1]), int.Parse(splitString[2]));

            addInConfiguration.DropShadow = carbonConfiguration.DropShadow;
            addInConfiguration.DropShadowBlurRadius = int.Parse(carbonConfiguration.DropShadowOffsetY.Replace("px", string.Empty));
            addInConfiguration.DropShadowOffsetY = int.Parse(carbonConfiguration.DropShadowBlurRadius.Replace("px", string.Empty));

            addInConfiguration.FontFamily = EnumExt.GetValueFromDescription<CarbonTypes.FontFamily>(carbonConfiguration.FontFamily);
            addInConfiguration.FontSize = int.Parse(carbonConfiguration.FontSize.Replace("px", string.Empty));
            addInConfiguration.LineNumbers = carbonConfiguration.LineNumbers;
            addInConfiguration.LineHeight = int.Parse(carbonConfiguration.LineHeight.Replace("%", string.Empty));
            addInConfiguration.ExportSize = EnumExt.GetValueFromDescription<ExportSize>(carbonConfiguration.ExportSize);
            addInConfiguration.ShowWaterMark = carbonConfiguration.Watermark;

            addInConfiguration.PaddingHorizontal = int.Parse(carbonConfiguration.PaddingHorizontal.Replace("px", string.Empty));
            addInConfiguration.PaddingVertical = int.Parse(carbonConfiguration.PaddingVertical.Replace("px", string.Empty));

            return addInConfiguration;
        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if(val.CompareTo(min) < 0)
            {
                return min;
            }
            else if(val.CompareTo(max) > 0)
            {
                return max;
            }
            else
            {
                return val;
            }
        }

        public void RestoreDefaults()
        {
            AutoAdjustWidth = true;
            BackgroundColor = DefaultBackground;
            DropShadow = true;
            FontFamily = CarbonTypes.FontFamily.Hack;
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



        #region Properties 

        [ToolTip("Automatically adjust the width of the window.")]
        public bool AutoAdjustWidth { get; set; }


        [ToolTip("Color to use as the background behind the window.")]
        public Color BackgroundColor { get; set; }


        [ToolTip("Render a drop shadow on the window.")]
        public bool DropShadow { get; set; }

        [ToolTip("The font family to use.")]
        public CarbonTypes.FontFamily FontFamily { get; set; }

        private int fontSize;
        [ToolTip("The font size. Min: 10, Max: 18")]
        public int FontSize
        {
            get => fontSize;
            set => fontSize = Clamp<int>(value, 10, 18);
        }


        [ToolTip("Show line numbers.")]
        public bool LineNumbers { get; set; }

        private int paddingHorizontal;
        [ToolTip("The amount of horizontal padding applied to the window.  Min: 0, Max: 100")]
        public int PaddingHorizontal
        {
            get => paddingHorizontal;
            set => paddingHorizontal = Clamp<int>(value, 0, 100);
        }


        private int paddingVertical;
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

        private int lineHeight;
        [ToolTip("The line height to use. Min: 90, Max: 250")]
        public int LineHeight
        {
            get => lineHeight;
            set => lineHeight = Clamp<int>(value, 90, 250);
        }


        private int dropShadowOffsetY;
        [ToolTip("The drop shadow offset. Min: 0, Max: 100")]
        public int DropShadowOffsetY
        {
            get => dropShadowOffsetY;
            set => dropShadowOffsetY = Clamp<int>(value, 0, 100);
        }


        private int dropShadowBlurRadius;
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


    }

}
