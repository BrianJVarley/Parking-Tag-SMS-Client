using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Tag_Picker_WRT.Helpers
{
    public sealed class TimeSpanConverter 
    {
        public static string GetTimeSpanAsString(TimeSpan input)
        {
            return input.ToString(@"hh\:mm");
        }
    }
}
