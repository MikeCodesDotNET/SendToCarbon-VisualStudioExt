using System.ComponentModel;

namespace Carbon.Helpers
{
    public class ToolTipAttribute : DescriptionAttribute
    {

        public ToolTipAttribute(string description) : base(description) { }

    }


    public class IdAttribute : DescriptionAttribute
    {

        public IdAttribute(string Id) : base(Id) { }

    }
}
