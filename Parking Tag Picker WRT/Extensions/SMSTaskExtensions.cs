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

        public async static Task SendParkingTagSMSRequest(ZoneInfo SelectedZone, string RegNumber, string ParkingDuration)
        {
            string SMSBookingRecipient = "53311";

            ChatMessage msg = new ChatMessage();
            msg.Body = SelectedZone + RegNumber + ParkingDuration;
            msg.Recipients.Add(SMSBookingRecipient);
            ChatMessageStore cms = await ChatMessageManager.RequestStoreAsync();
            await cms.SendMessageAsync(msg);


        }
    }
}
