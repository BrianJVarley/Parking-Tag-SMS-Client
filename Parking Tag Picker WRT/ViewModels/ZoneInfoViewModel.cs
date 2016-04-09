using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Tag_Picker_WRT.ViewModels
{
    public class ZoneInfoViewModel : INotifyPropertyChanged
    {

        

        public ZoneInfoViewModel()
        {     

        }

       
        private ZoneInfoPage _selectedZone;
        public ZoneInfoPage SelectedZone
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


        
      
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }


    }
}
