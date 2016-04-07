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


            //ChatMessage msg = new ChatMessage();
            //msg.Body = SelectedZone.ZoneName + RegNumber + ParkingDuration;
            //msg.Recipients.Add(SMSBookingRecipient);
            //ChatMessageStore cms = await ChatMessageManager.RequestStoreAsync();
            //await cms.SendMessageAsync(msg);

           ChatMessage chat = new ChatMessage();
           chat.Body = "Park" + " " + selectedZone.ZoneName + " " + parkingDurationMinutes + " " + regNumber;
           chat.Recipients.Add(SMSBookingRecipient);
           await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chat);

        }
    }
}
