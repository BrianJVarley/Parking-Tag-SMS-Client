using GalaSoft.MvvmLight.Command;
using Parking_Tag_Picker_WRT.Extensions;
using Parking_Tag_Picker_WRT.Helpers;
using Parking_Tag_Picker_WRT.Interfaces;
using Parking_Tag_Picker_WRT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.Serialization;

namespace Parking_Tag_Picker_WRT.ViewModel
{
    [DataContract]
    public class TagRequestViewModel : INotifyPropertyChanged
    {


        private DatabaseHelper _dbHelper;
        private Dictionary<int, string> TableNameDictionary = new Dictionary<int, string>();
        private Dictionary<int, string> CouncilDisplayNameDictionary = new Dictionary<int, string>();
        private int CouncilId; 

        
        public TagRequestViewModel(DatabaseHelper dbHelper)
        {
            this._dbHelper = dbHelper;
            TableNameInit();
            CouncilDisplayNameInit();
            LoadCommands();

            IsValidTagRequest = false;
        }

        
        public RelayCommand TagRequestCommand
        {
            get;
            private set;
        }

        private void LoadCommands()
        {
            TagRequestCommand = new RelayCommand(async () => { await SendParkingTagSMSRequest(); });
        }


    
        private bool _isValidTagRequest = false;
        public bool IsValidTagRequest
        {
            get { return _isValidTagRequest; }
            set
            {
               if (value != _isValidTagRequest)
               {              
                    _isValidTagRequest = value;
                    RaisePropertyChanged("IsValidTagRequest");
               }

            }
              
        }



        private string _councilHeaderDisplayName;
        public string CouncilHeaderDisplayName
        {
            get { return _councilHeaderDisplayName; }
            set
            {
                if (value != _councilHeaderDisplayName)
                {
                    _councilHeaderDisplayName = value;
                    RaisePropertyChanged("CouncilHeaderDisplayName");
                }

            }

        }



        private string _selectedCouncilId;
        public string SelectedCouncilId
        {
            get
            {
                return _selectedCouncilId;
            }
            set
            {
                if (_selectedCouncilId != value)
                {
                    _selectedCouncilId = value;
                    RaisePropertyChanged("SelectedCouncilId");

                }
            }
        }


        private ObservableCollection<ZoneInfo> _zoneInfoCollection;
        public ObservableCollection<ZoneInfo> ZoneInfoCollection
        {
            get
            {
                return this._zoneInfoCollection;
            }

            set
            {
                if (_zoneInfoCollection != value)
                {
                    _zoneInfoCollection = value;
                    RaisePropertyChanged("ZoneInfoCollection");
                }
            }
        }



        private string _selectedRegNumber;
        [DataMember]
        public string SelectedRegNumber
        {
            get
            {
                return this._selectedRegNumber;
            }

            set
            {
                if (_selectedRegNumber != value)
                {
                    _selectedRegNumber = value;
                    RaisePropertyChanged("SelectedRegNumber");
                    CheckValidTagRequest();
                }
            }
        }

        private void CheckValidTagRequest()
        {
            if (SelectedParkDuration != null && SelectedZone != null
                && SelectedRegNumber != string.Empty)
                IsValidTagRequest = true;

        }

        private ZoneInfo _selectedZone;
        public ZoneInfo SelectedZone
        {
            get
            {
                return this._selectedZone;
            }

            set
            {
                if (_selectedZone != value)
                {
                    _selectedZone = value;
                    RaisePropertyChanged("SelectedZone");
                    CheckValidTagRequest();
                }
            }
        }


        private TimeSpan? _selectedParkDuration = TimeSpan.Parse("00:00");
        public TimeSpan? SelectedParkDuration
        {
            get
            {
                return this._selectedParkDuration;
            }

            set
            {
                if (_selectedParkDuration != value)
                {
                    _selectedParkDuration = value;
                    RaisePropertyChanged("SelectedParkDuration");
                    CheckValidTagRequest();
                }
            }
        }




        private void TableNameInit()
        {

            TableNameDictionary.Add(0,"DublinCityCouncilTable");
            TableNameDictionary.Add(1,"DunLaoghaireCityCouncilTable");
            TableNameDictionary.Add(2,"FingalCouncilTable");
            TableNameDictionary.Add(3,"SouthDublinCouncilTable");
            TableNameDictionary.Add(4,"ArklowCouncilTable");
            TableNameDictionary.Add(5,"DLRCouncilTable");
            TableNameDictionary.Add(6,"WicklowCouncilTable");
            TableNameDictionary.Add(7,"TallaghtCouncilTable");
            TableNameDictionary.Add(8,"GreystonesCouncilTable");
        }


        private void CouncilDisplayNameInit()
        {

            CouncilDisplayNameDictionary.Add(0, "Dublin City");
            CouncilDisplayNameDictionary.Add(1, "Dun Laoghaire");
            CouncilDisplayNameDictionary.Add(2, "Fingal");
            CouncilDisplayNameDictionary.Add(3, "South Dublin");
            CouncilDisplayNameDictionary.Add(4, "Arklow");
            CouncilDisplayNameDictionary.Add(5, "DLH");
            CouncilDisplayNameDictionary.Add(6, "Wicklow");
            CouncilDisplayNameDictionary.Add(7, "Tallaght");
            CouncilDisplayNameDictionary.Add(8, "Greystones");
        }



        public void InitZoneInfoAsync()
        {
            string tableName;
            CouncilId = Int32.Parse(SelectedCouncilId);
            tableName = TableNameDictionary[CouncilId];

            CouncilHeaderDisplayName = CouncilDisplayNameDictionary[CouncilId];

            var result = _dbHelper.Init();

            ////Return zone database records
            var zoneResult = _dbHelper.ReadZones(tableName);
            ZoneInfoCollection = zoneResult;
        }


        public async Task SendParkingTagSMSRequest()
        {
            await SMSTaskExtensions.SendParkingTagSMSRequest(SelectedZone, SelectedRegNumber, SelectedParkDuration);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }


    }

}
