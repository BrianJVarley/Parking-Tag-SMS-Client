using Parking_Tag_Picker_WRT.Models;
using SQLite;
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
        public async Task<bool> Init()
        {


            bool isDatabaseExisting = false;

            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(AppDBPath);
                isDatabaseExisting = true;
            }
            catch
            {
                isDatabaseExisting = false;
            }

            if (!isDatabaseExisting)
            {

                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync(PackageDBPath);
                await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
            }

            return true;          
        }

        

        /// <summary>
        ///  Retrieve the specific zone info from the database.   
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public ZoneInfo ReadZone(int zoneId, string tableName)
        {
            using (var dbConn = new SQLiteConnection(tableName))
            {
                var existingZone = dbConn.Query<ZoneInfo>("select * from {0} where ObjectId ={1}", tableName, zoneId).FirstOrDefault();
                return existingZone;
            }
        }


        /// <summary>
        ///  Retrieve zone info list from the database.   
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ZoneInfo> ReadZones(string tableName)
        {
            using (var dbConn = new SQLiteConnection(Path.Combine(ApplicationData.Current.LocalFolder.Path, AppDBPath), true))
            {
                List<ZoneInfo> zoneInfo = dbConn.Query<ZoneInfo>("select * from " + tableName).ToList<ZoneInfo>();
                ObservableCollection<ZoneInfo> zoneInfoCollection = new ObservableCollection<ZoneInfo>(zoneInfo);
                return zoneInfoCollection;
            }
        }
    
        
    }
}
