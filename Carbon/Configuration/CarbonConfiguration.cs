
using Carbon.Helpers.Extensions;
using Newtonsoft.Json;

namespace Carbon.Configuration
{

    //To handle importing / exporting configurations
    public class CarbonConfiguration
    {
        public static explicit operator CarbonConfiguration(LocalConfiguration addInConfiguration) => InternalConvert(addInConfiguration);

        internal static CarbonConfiguration InternalConvert(LocalConfiguration addInConfiguration, string name = "", int firstLineNumber = 1)
        {
            CarbonConfiguration carbonConfiguration = new CarbonConfiguration
            {
                WidthAdjustment = addInConfiguration.AutoAdjustWidth,
                WindowControls = addInConfiguration.WindowControls,
                WindowTheme = addInConfiguration.WindowTheme.GetId(),
                Theme = addInConfiguration.Theme.GetId(),

                BackgroundColor = $"rgba({addInConfiguration.BackgroundColor.R}, {addInConfiguration.BackgroundColor.G}, {addInConfiguration.BackgroundColor.B}, {addInConfiguration.BackgroundColor.A})",
                BackgroundMode = "color",
                BackgroundImage = null,
                BackgroundImageSelection = null,

                DropShadow = addInConfiguration.DropShadow,
                DropShadowOffsetY = $"{addInConfiguration.DropShadowOffsetY}px",
                DropShadowBlurRadius = $"{addInConfiguration.DropShadowBlurRadius}px",

                FontFamily = addInConfiguration.FontFamily.GetId(),
                FontSize = $"{addInConfiguration.FontSize}px",

                LineNumbers = addInConfiguration.LineNumbers,
                LineHeight = $"{addInConfiguration.LineHeight}%",
                FirstLineNumber = firstLineNumber,

                ExportSize = addInConfiguration.ExportSize.GetId(),
                Watermark = addInConfiguration.ShowWaterMark,

                PaddingHorizontal = $"{addInConfiguration.PaddingHorizontal}px",
                PaddingVertical = $"{addInConfiguration.PaddingVertical}px",

                //Misc defaults;
                SquaredImage = false,
                HiddenCharacters = false,
                Loading = false,
                IsVisible = true,
                Icon = "/static/presets/6.png"
            };

            return carbonConfiguration;
        }

        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("backgroundImage")]
        public object BackgroundImage { get; set; }

        [JsonProperty("backgroundImageSelection")]
        public object BackgroundImageSelection { get; set; }

        [JsonProperty("backgroundMode")]
        public string BackgroundMode { get; set; }

        [JsonProperty("dropShadow")]
        public bool DropShadow { get; set; }

        [JsonProperty("dropShadowBlurRadius")]
        public string DropShadowBlurRadius { get; set; }

        [JsonProperty("dropShadowOffsetY")]
        public string DropShadowOffsetY { get; set; }

        [JsonProperty("exportSize")]
        public string ExportSize { get; set; }

        [JsonProperty("firstLineNumber")]
        public long FirstLineNumber { get; set; }

        [JsonProperty("fontFamily")]
        public string FontFamily { get; set; }

        [JsonProperty("fontSize")]
        public string FontSize { get; set; }

        [JsonProperty("hiddenCharacters")]
        public bool HiddenCharacters { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("isVisible")]
        public bool IsVisible { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("lineHeight")]
        public string LineHeight { get; set; }

        [JsonProperty("lineNumbers")]
        public bool LineNumbers { get; set; }

        [JsonProperty("loading")]
        public bool Loading { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("paddingHorizontal")]
        public string PaddingHorizontal { get; set; }


        [JsonProperty("paddingVertical")]
        public string PaddingVertical { get; set; }

        [JsonProperty("squaredImage")]
        public bool SquaredImage { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }

        [JsonProperty("watermark")]
        public bool Watermark { get; set; }

        [JsonProperty("widthAdjustment")]
        public bool WidthAdjustment { get; set; }

        [JsonProperty("windowControls")]
        public bool WindowControls { get; set; }

        [JsonProperty("windowTheme")]
        public string WindowTheme { get; set; }

    }
}
