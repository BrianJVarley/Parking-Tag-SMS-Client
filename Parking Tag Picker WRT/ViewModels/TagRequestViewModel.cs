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

namespace Parking_Tag_Picker_WRT.ViewModel
{
    public class TagRequestViewModel : INotifyPropertyChanged
    {


        private DatabaseHelper _dbHelper;
        Dictionary<int, string> TableNameDictonary = new Dictionary<int, string>();

        public RelayCommand TagRequestCommand
        {
            get;
            private set;
        }


        public TagRequestViewModel(DatabaseHelper dbHelper)
        {
            this._dbHelper = dbHelper;
            InitTableNameDictionary();

            LoadCommands();

        }



        private void LoadCommands()
        {
            TagRequestCommand = new RelayCommand(async () =>
            {
                await SendParkingTagSMSRequest();
            }  );
        }



        //private bool CanSendCommand()
        //{
        //    if(SelectedParkDuration != null && RegNumber != string.Empty && SelectedZone != null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
        



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


        private ObservableCollection<ZoneInfoPage> _zoneInfoCollection;
        public ObservableCollection<ZoneInfoPage> ZoneInfoCollection
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

        private string _regNumber;
        public string RegNumber
        {
            get
            {
                return this._regNumber;
            }

            set
            {
                if (_regNumber != value)
                {
                    _regNumber = value;
                    RaisePropertyChanged("RegNumber");
                }
            }
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
                }
            }
        }


        private TimeSpan? _selectedParkDuration;
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
                }
            }
        }




        private void InitTableNameDictionary()
        {

            TableNameDictonary.Add(0,"DublinCityCouncilTable");
            TableNameDictonary.Add(1,"DunLaoghaireCityCouncilTable");
            TableNameDictonary.Add(2,"FingalCouncilTable");
            TableNameDictonary.Add(3,"SouthDublinCouncilTable");
            TableNameDictonary.Add(4,"ArklowCouncilTable");
            TableNameDictonary.Add(5,"DLRCouncilTable");
            TableNameDictonary.Add(6,"WicklowCouncilTable");
            TableNameDictonary.Add(7,"TallaghtCouncilTable");
            TableNameDictonary.Add(8,"GreystonesCouncilTable");
        }



        public void InitZoneInfoAsync()
        {
            string tableName;
            int CouncilId = Int32.Parse(SelectedCouncilId);
            tableName = TableNameDictonary[CouncilId];
            var result = _dbHelper.Init();

            ////Return zone database records
            var zoneResult = _dbHelper.ReadZones(tableName);
            ZoneInfoCollection = zoneResult;
        }


        public async Task SendParkingTagSMSRequest()
        {
            SMSTaskExtensions.SendParkingTagSMSRequest(SelectedZone, RegNumber, SelectedParkDuration);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }


    }

}
