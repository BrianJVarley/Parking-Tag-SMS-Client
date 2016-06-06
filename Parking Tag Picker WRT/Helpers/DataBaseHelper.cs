using CsvHelper;
using Parking_Tag_Picker_WRT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Parking_Tag_Picker_WRT.Helpers
{
    public class DatabaseHelper
    {

        public const string AppDBPath = @"ParkingZoneDatabase.db";
        public const string PackageDBPath = @"Databases\ParkingZoneDatabase.db";


        /// <summary>
        /// Load SQL_LiteTable from Solution   
        /// </summary>
        /// <param name="DBPATH"></param>
        /// <returns></returns>
        public async Task<bool> Init(string tableName)
        {
           
            bool isTableExisting = false;

            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(string.Format("{0}.csv", tableName));
                isTableExisting = true;
            }
            catch
            {
                isTableExisting = false;
            }

            if (!isTableExisting)
            {

                StorageFile packagefile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(string.Format(@"ms-appx:///Databases/{0}.csv", tableName)));
                await packagefile.CopyAsync(ApplicationData.Current.LocalFolder);
               
            }

            return true;          
        }

        


        /// <summary>
        ///  Retrieve zone info list from the database.   
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<ZoneInfo>> ReadZones(string tableName)
        {
            ObservableCollection<ZoneInfo> zoneInfoCollection = new ObservableCollection<ZoneInfo>();

            try 
            {
                string fileName = string.Format("{0}.csv", tableName);

                StorageFile localFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(string.Format(@"ms-appx:///Databases/{0}.csv", tableName)));
                Stream fileStream = await localFile.OpenStreamForReadAsync();

                using (var textReader = new StreamReader(fileStream))
                {

                    var csvReader = new CsvReader(textReader);
                    //csvReader.Read();
                    List<ZoneInfo> zoneInfo = csvReader.GetRecords<ZoneInfo>().ToList();
                    zoneInfoCollection = new ObservableCollection<ZoneInfo>(zoneInfo);

                    return zoneInfoCollection;

                }
            
            }
            catch(Exception ex)
            {
                return zoneInfoCollection;
            }


        }
    
        
    }
}
