using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace TagTimerTask
{
    public sealed class UpdateLiveTile : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            UpdateLiveTile();

        }

        private static void UpdateLiveTile()
        {
            TimeSpan timerStartTime = TimeSpan.Zero;
            string selectedZoneName = string.Empty;
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan remainingTimeSpan = TimeSpan.Zero;
            TimeSpan? userParkingDuration = TimeSpan.Zero;
            TimeSpan elapsedTimeSpan = TimeSpan.Zero;

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            //Code to retrieve local settings key values ->
            if (localSettings.Values.ContainsKey("timerStartTime")
                && localSettings.Values.ContainsKey("userParkingDuration") && localSettings.Values.ContainsKey("userParkZone"))
            {
                timerStartTime = (TimeSpan)localSettings.Values["timerStartTime"];
                userParkingDuration = (TimeSpan?)localSettings.Values["userParkingDuration"];
                selectedZoneName = (string)localSettings.Values["userParkZone"];

            }

            elapsedTimeSpan = timerStartTime - currentTime;
            remainingTimeSpan = (userParkingDuration ?? elapsedTimeSpan) - elapsedTimeSpan;

            var remainingTimeString = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                remainingTimeSpan.Hours, remainingTimeSpan.Minutes, remainingTimeSpan.Seconds,
                remainingTimeSpan.Milliseconds / 10);

            var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText01);

            var tileImage = tileXml.GetElementsByTagName("image")[0] as XmlElement;
            tileImage.SetAttribute("src", "ms-appx:///Assets/Logo.scale-140.png");

            var tileText = tileXml.GetElementsByTagName("text");
            (tileText[0] as XmlElement).InnerText = "Zone:";
            (tileText[1] as XmlElement).InnerText = " " + selectedZoneName;
            (tileText[2] as XmlElement).InnerText = "Remining Time:";
            (tileText[3] as XmlElement).InnerText = " " + remainingTimeString;

            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}
