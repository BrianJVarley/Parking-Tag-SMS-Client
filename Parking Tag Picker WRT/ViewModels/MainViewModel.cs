using Parking_Tag_Picker_WRT.Interfaces;
using Parking_Tag_Picker_WRT.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Parking_Tag_Picker_WRT.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {

        private INavigationCallback _navCallBack { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationCallback navCallBack)
        {
            this._navCallBack = navCallBack;
            this.CouncilNameItems = new ObservableCollection<CouncilName>();
        }


        

        /// <summary>
        /// Creates and adds council name data.
        /// </summary>
        public void LoadCouncilNamesData()
        {
            //Load Council Names
            this.CouncilNameItems.Add(new CouncilName() { ID = "0", CouncilAcronym = "DCC", CouncilFullName = "Dublin City Council" });
            this.CouncilNameItems.Add(new CouncilName() { ID = "1", CouncilAcronym = "DLR", CouncilFullName = "Dún Laoghaire-Rathdown County Council" });
            this.CouncilNameItems.Add(new CouncilName() { ID = "2", CouncilAcronym = "FCC", CouncilFullName = "Fingal County Council" });
            this.CouncilNameItems.Add(new CouncilName() { ID = "3", CouncilAcronym = "SDC", CouncilFullName = "South Dublin County Council" });
            this.CouncilNameItems.Add(new CouncilName() { ID = "4", CouncilAcronym = "ATC", CouncilFullName = "Arklow Town Council" });
            this.CouncilNameItems.Add(new CouncilName() { ID = "5", CouncilAcronym = "DLH", CouncilFullName = "Dún Laoghaire Harbour Company" });
            this.CouncilNameItems.Add(new CouncilName() { ID = "6", CouncilAcronym = "WTC", CouncilFullName = "Wicklow Town Council" });
            this.CouncilNameItems.Add(new CouncilName() { ID = "7", CouncilAcronym = "TS", CouncilFullName = "Tallaght Stadium" });
            this.CouncilNameItems.Add(new CouncilName() { ID = "8", CouncilAcronym = "GS", CouncilFullName = "Greystones" });
           
            this.IsDataLoaded = true;
        }



        public ObservableCollection<CouncilName> CouncilNameItems { get;  set; }

        public bool IsDataLoaded { get; private set; }


        private CouncilName _selectedCouncilName;
        public CouncilName SelectedCouncilName
        {
            get
            {
                return _selectedCouncilName;
            }
            set
            {
                if (_selectedCouncilName != value)
                {
                    _selectedCouncilName = value;
                    RaisePropertyChanged("SelectedCouncilName");
                    _navCallBack.NavigateTo(_selectedCouncilName.ID);            
                }
            }

        }
        
        
  
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
    }
    
}