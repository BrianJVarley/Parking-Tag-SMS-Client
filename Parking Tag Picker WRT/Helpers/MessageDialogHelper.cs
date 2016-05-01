using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Parking_Tag_Picker_WRT.Helpers
{
    public class MessageDialogHelper
    {
        public static async Task<IUICommand> Show(string content, string title)
        {
            MessageDialog messageDialog = new MessageDialog(content, title);
            messageDialog.Commands.Add(new UICommand("OK"));
            messageDialog.Commands.Add(new UICommand("Cancel"));

            var result = await messageDialog.ShowAsync();
            return result;
        }
    }
}
