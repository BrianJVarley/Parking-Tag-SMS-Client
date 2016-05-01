using Parking_Tag_Picker_WRT.Helpers;
using Parking_Tag_Picker_WRT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Chat;

namespace Parking_Tag_Picker_WRT.Extensions
{
    public static class SMSTaskExtensions
    {

        public async static Task SendParkingTagSMSRequest(ZoneInfo selectedZone, string regNumber, TimeSpan? parkingDuration)
        {
            var SMSBookingRecipient = AppConstants.ServiceProviderNumber;
            double parkingDurationMinutes;

          
            if(parkingDuration.HasValue)
            {
                parkingDurationMinutes = parkingDuration.Value.TotalMinutes;         
            }
            else
            {
                parkingDurationMinutes = 00;
            }

           ChatMessage chat = new ChatMessage();

           if(selectedZone.ZoneName != null && parkingDuration != null && regNumber != null)
           {
               chat.Body = "Park" + " " + selectedZone.ZoneName + " " + parkingDurationMinutes + " " + regNumber;
           }
           chat.Recipients.Add(SMSBookingRecipient);
           await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chat);

        }
    }
}
