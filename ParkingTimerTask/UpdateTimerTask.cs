using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.ApplicationModel.Background;

namespace ParkingTimerTask
{
    public sealed class UpdateTimerTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //Somehow update the live tile elapsed time after app is closed..
            

        }

    }
}
