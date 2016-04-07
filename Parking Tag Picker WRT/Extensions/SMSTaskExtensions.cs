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

            if(parkingDuration.HasValue)
            {
                var parkingDurationMinutes = parkingDuration.Value.TotalMinutes;         
            }
            else
            {
                parkingDuration = TimeSpan.Zero;
            }


            //ChatMessage msg = new ChatMessage();
            //msg.Body = SelectedZone.ZoneName + RegNumber + ParkingDuration;
            //msg.Recipients.Add(SMSBookingRecipient);
            //ChatMessageStore cms = await ChatMessageManager.RequestStoreAsync();
            //await cms.SendMessageAsync(msg);

           ChatMessage chat = new ChatMessage();
           chat.Body = "Park" + " " + selectedZone.ZoneName + " " + parkingDuration + " " + regNumber;
           chat.Recipients.Add(SMSBookingRecipient);
           await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chat);

        }
    }
}
