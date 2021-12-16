using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.SeleniumHelpers
{
    public class ByHelpers
    {
        public enum ByHelper
        {
            Id,
            xpath,
            classname,
            cssSelector,
            LINKTEXT,
            PartialLinkText,
            tagname
        };
    }
}
