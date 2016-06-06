using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Tag_Picker_WRT.Models
{
    public class ZoneInfo
    {

        public string DaysOpen { get; set; }

        public string HoursOpen { get; set; }

        public string ObjectId { get; set; }

        public string Restrictions { get; set; }

        public float TariffPD { get; set; }

        public float TariffPH  { get; set; }

        public string ZoneName { get; set; }

       

        

        
        
        public ZoneInfo() 
        {

        }


        public ZoneInfo(string objectId, string zoneName, int tariffPH, int tariffPD, 
            string restrictions, string daysOpen, string hoursOpen )
        {

            ObjectId = objectId;
            ZoneName = zoneName;
            TariffPH = tariffPH;
            TariffPD = tariffPD;
            Restrictions = restrictions;
            DaysOpen = daysOpen;
            HoursOpen = hoursOpen;
        }

     
        
    }
}
