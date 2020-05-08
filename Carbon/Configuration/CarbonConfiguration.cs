using Carbon.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Configuration
{
    //To handle importing / exporting configurations
    public class CarbonConfiguration
    {
        public static explicit operator CarbonConfiguration(LocalConfiguration addInConfiguration) => InternalConvert(addInConfiguration);

        internal static CarbonConfiguration InternalConvert(LocalConfiguration addInConfiguration, string name = "", int firstLineNumber = 1)
        {
            var carbonConfiguration = new CarbonConfiguration();

            carbonConfiguration.WidthAdjustment = addInConfiguration.AutoAdjustWidth;
            carbonConfiguration.WindowControls = addInConfiguration.WindowControls;
            carbonConfiguration.WindowTheme = addInConfiguration.WindowTheme.GetId();
            carbonConfiguration.Theme = addInConfiguration.Theme.GetId();


            carbonConfiguration.BackgroundColor = $"rgba({addInConfiguration.BackgroundColor.R}, {addInConfiguration.BackgroundColor.G}, {addInConfiguration.BackgroundColor.B}, {addInConfiguration.BackgroundColor.A})";
            carbonConfiguration.BackgroundMode = "color";
            carbonConfiguration.BackgroundImage = null;
            carbonConfiguration.BackgroundImageSelection = null;


            carbonConfiguration.DropShadow = addInConfiguration.DropShadow;
            carbonConfiguration.DropShadowOffsetY = $"{addInConfiguration.DropShadowOffsetY}px";
            carbonConfiguration.DropShadowBlurRadius = $"{addInConfiguration.DropShadowBlurRadius}px";

            carbonConfiguration.FontFamily = addInConfiguration.FontFamily.GetId();
            carbonConfiguration.FontSize = $"{addInConfiguration.FontSize}px";

            carbonConfiguration.LineNumbers = addInConfiguration.LineNumbers;
            carbonConfiguration.LineHeight = $"{addInConfiguration.LineHeight}%";
            carbonConfiguration.FirstLineNumber = firstLineNumber;

            carbonConfiguration.ExportSize = addInConfiguration.ExportSize.GetId();
            carbonConfiguration.Watermark = addInConfiguration.ShowWaterMark;

            carbonConfiguration.PaddingHorizontal = $"{addInConfiguration.PaddingHorizontal}px";
            carbonConfiguration.PaddingVertical = $"{addInConfiguration.PaddingVertical}px";


            //Misc defaults;
            carbonConfiguration.SquaredImage = false;
            carbonConfiguration.HiddenCharacters = false;
            carbonConfiguration.Loading = false;
            carbonConfiguration.IsVisible = true;
            carbonConfiguration.Icon = "/static/presets/6.png";

            return carbonConfiguration;
        }



        [JsonProperty("paddingVertical")]
        public string PaddingVertical { get; set; }

        [JsonProperty("paddingHorizontal")]
        public string PaddingHorizontal { get; set; }

        [JsonProperty("backgroundImage")]
        public object BackgroundImage { get; set; }

        [JsonProperty("backgroundImageSelection")]
        public object BackgroundImageSelection { get; set; }

        [JsonProperty("backgroundMode")]
        public string BackgroundMode { get; set; }

        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("dropShadow")]
        public bool DropShadow { get; set; }

        [JsonProperty("dropShadowOffsetY")]
        public string DropShadowOffsetY { get; set; }

        [JsonProperty("dropShadowBlurRadius")]
        public string DropShadowBlurRadius { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }

        [JsonProperty("windowTheme")]
        public string WindowTheme { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("fontFamily")]
        public string FontFamily { get; set; }

        [JsonProperty("fontSize")]
        public string FontSize { get; set; }

        [JsonProperty("lineHeight")]
        public string LineHeight { get; set; }

        [JsonProperty("windowControls")]
        public bool WindowControls { get; set; }

        [JsonProperty("widthAdjustment")]
        public bool WidthAdjustment { get; set; }

        [JsonProperty("lineNumbers")]
        public bool LineNumbers { get; set; }

        [JsonProperty("firstLineNumber")]
        public long FirstLineNumber { get; set; }

        [JsonProperty("exportSize")]
        public string ExportSize { get; set; }

        [JsonProperty("watermark")]
        public bool Watermark { get; set; }

        [JsonProperty("squaredImage")]
        public bool SquaredImage { get; set; }

        [JsonProperty("hiddenCharacters")]
        public bool HiddenCharacters { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("loading")]
        public bool Loading { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("isVisible")]
        public bool IsVisible { get; set; }
    }
}
