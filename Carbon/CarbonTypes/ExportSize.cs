using Carbon.Helpers;
using System.ComponentModel;

namespace Carbon.CarbonTypes
{

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


}
