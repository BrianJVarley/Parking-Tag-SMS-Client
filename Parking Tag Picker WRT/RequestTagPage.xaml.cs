using Parking_Tag_Picker_WRT.Common;
using Parking_Tag_Picker_WRT.Helpers;
using Parking_Tag_Picker_WRT.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Parking_Tag_Picker_WRT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RequestTagPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private TagRequestViewModel _vm;
        private DatabaseHelper _dbHelper;

        private bool _isNewPageInstance = false;

        public RequestTagPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            //init data context
            this.NavigationCacheMode = NavigationCacheMode.Required;
            _dbHelper = new DatabaseHelper();

            if(this.DataContext == null)
            {
                _vm = new TagRequestViewModel(_dbHelper);
                this.DataContext = _vm;
                _isNewPageInstance = true;  
            }
    
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // If _isNewPageInstance is true, the page constuctor has been called, so
            // state may need to be restored.
            if (_isNewPageInstance)
            {
                if (_vm == null)
                {
                    if (e.PageState.Count > 0)
                    {
                        _vm = (TagRequestViewModel)e.PageState["ViewModel"];
                    }
                    else
                    {
                        _vm = new TagRequestViewModel(_dbHelper);
                    }
                }
                DataContext = _vm;
            }
            // Set _isNewPageInstance to false. If the user navigates back to this page
            // and it has remained in memory, this value will continue to be false.
            _isNewPageInstance = false;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {         
            // Save the ViewModel variable in the page's State dictionary.
            e.PageState["ViewModel"] = _vm;     
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            ((TagRequestViewModel)this.DataContext).SelectedCouncilId = (string)e.Parameter;
            _vm.InitZoneInfoAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);

           
        }

        #endregion

        private void ZoneInfoAppBarBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ZoneInfoPage), _vm.SelectedZone);
        }

        private void regNumberTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.Equals(VirtualKey.Enter))
            {
                FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
            }

        }

        //private void regNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (regNumberTextBox.Text != string.Empty)
        //    regNumberTextBox.Text = regNumberTextBox.Text.ToUpper();
        //}
    }
}
