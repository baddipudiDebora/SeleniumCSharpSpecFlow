using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.SeleniumHelpers
{
    public class CommonMethods
    {
        public static string GetCurrentTimestamp()
        {
            var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss").Replace(":", "-");
            return timeStamp;
        }
    }
}
