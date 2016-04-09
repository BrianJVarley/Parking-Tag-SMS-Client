using Parking_Tag_Picker_WRT.Helpers;
using Parking_Tag_Picker_WRT.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Parking_Tag_Picker_WRT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TagRequestPage : Page
    {
        TagRequestViewModel vm;
        DatabaseHelper dbHelper;

        
        public TagRequestPage()
        {
            //init data context
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.InitializeComponent();
            dbHelper = new DatabaseHelper();
            vm = new TagRequestViewModel(dbHelper);
            this.DataContext = vm;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((TagRequestViewModel)this.DataContext).SelectedCouncilId = (string)e.Parameter;
            vm.InitZoneInfoAsync();

        }

        private void ZoneInfoAppBarBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ZoneInfoPage), vm.SelectedZone);
        }

    }
}
