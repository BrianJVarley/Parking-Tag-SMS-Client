using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Tag_Picker_WRT.Models
{
    public class ZoneInfo
    {

        //The ObjectId property is marked as the Primary Key  
        [SQLite.PrimaryKey]
        [Column("objectId")]
        public string ObjectId { get; set; }

        [Column("zone")]
        public string ZoneName { get; set; }

        [Column("tariff_ph")]
        public float TariffPH  { get; set; }

        [Column("tariff_pd")]
        public float TariffPD { get; set; }

        [Column("restrictions")]
        public string Restrictions { get; set; }

        [Column("days_of_operation")]
        public string DaysOpen { get; set; }

        [Column("hours_of_operation")]
        public string HoursOpen { get; set; }

        
        
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
