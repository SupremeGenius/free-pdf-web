using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient;

namespace BusinessLogicLayer
{
    [Table(Name="Preferences")]
    public class PreferencesModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int PreferencesID { get; set; }

        [Column(DbType="VARCHAR(45)", CanBeNull=false)]
        public String Name { get; set; }

        [Column(DbType="VARCHAR(200)", CanBeNull=false)]
        public String Value { get; set; }

        public PreferencesModel GetPreferencesById(int PreferencesID)
        {
            return LinqAdapter.GetTable<PreferencesModel>().SingleOrDefault(p => p.PreferencesID == PreferencesID);
        }        

        public PreferencesModel GetPreferencesByName(String Name)
        {
            return LinqAdapter.GetTable<PreferencesModel>().SingleOrDefault(p => p.Name == Name);
        }

        public void UpdatePreferences(int PreferencesID, String Name, String Value)
        {
            PreferencesModel preferencesToUpdate = GetPreferencesById(PreferencesID);
            preferencesToUpdate.Name = Name;
            preferencesToUpdate.Value = Value;

            LinqAdapter.SubmitChanges();
        }

        public void UpdatePreferences(String Name, String Value)
        {
            PreferencesModel preferencesToUpdate = GetPreferencesByName(Name);
            preferencesToUpdate.Value = Value;

            LinqAdapter.SubmitChanges();
        }
    }
}
