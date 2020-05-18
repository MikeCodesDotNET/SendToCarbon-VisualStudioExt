using Carbon.Helpers;
using System.ComponentModel;

namespace Carbon.CarbonTypes
{


    public enum Language
    {
        [Id("text")]
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


        [Description("XAML")]
        [Id("htmlmixed")]
        XAML,

        [Description("HTML")]
        [Id("htmlmixed")]
        HTML,


        [Description("JSON")]
        [Id("application/json")]
        JSON,

        Auto,

    }


}
