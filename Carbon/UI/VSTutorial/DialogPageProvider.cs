using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Options
{
    internal class DialogPageProvider
    {
        public class General : BaseOptionPage<GeneralOptions> { }
        public class Configuration : BaseOptionPage<ConfigurationOptions> { }
    }
}
