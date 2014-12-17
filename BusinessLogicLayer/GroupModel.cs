using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient;

namespace BusinessLogicLayer
{
    [Table(Name="UserGroup")]
    public class GroupModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int GroupID { get; set; }

        [Column(DbType="NVARCHAR(45)", CanBeNull=false)]
        public String Name { get; set; }

        [Column(DbType="NVARCHAR(100)", CanBeNull=true)]
        public String Description { get; set; }

        [Column(DbType="BIT", CanBeNull=true)]
        public Boolean IsDefault { get; set; }

        [Column(DbType="BIT", CanBeNull=true)]
        public Boolean IsAdmin { get; set; }

        [Column(DbType = "BIT", CanBeNull = true)]
        public Boolean IsLimit { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public int DownloadLimit { get; set; }

        public static int TotalRecord
        {
            get { return LinqAdapter.GetTable<GroupModel>().Count(); }
        }

        public IEnumerable<GroupModel> GetGroupList()
        {
            return LinqAdapter.GetTable<GroupModel>().ToList();
        }

        public IEnumerable<GroupModel> GetGroupList(int startRowIndex, int maximumRows)
        {
            return LinqAdapter.GetTable<GroupModel>().Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public GroupModel GetGroupById(int GroupID)
        {
            return LinqAdapter.GetTable<GroupModel>().SingleOrDefault(g => g.GroupID == GroupID);
        }

        public int GetDefaultGroupID()
        {
            return LinqAdapter.GetTable<GroupModel>().Single(g => g.IsDefault == true).GroupID;
        }

        public bool IsAdminUser(String Username)
        {
            UsersModel userObj = new UsersModel();
            int GroupID = userObj.GetUserGroupByUsername(Username);

            return GetGroupById(GroupID).IsAdmin;
        }

        public void AddGroup(String Name, String Description, bool IsLimt, int DownloadLimit)
        {
            GroupModel groupToInsert = new GroupModel();
            groupToInsert.Name = Name;
            groupToInsert.Description = Description;
            groupToInsert.IsAdmin = false;
            groupToInsert.IsDefault = false;
            groupToInsert.IsLimit = IsLimit;
            groupToInsert.DownloadLimit = DownloadLimit;

            LinqAdapter.GetTable<GroupModel>().InsertOnSubmit(groupToInsert);
            LinqAdapter.SubmitChanges();
        }

        public void UpdateGroup(String Name, String Description, int GroupID, bool IsDefault = false, bool IsAdmin = false, bool IsLimit = true, int DownloadLimit = 3)
        {
            GroupModel groupToUpdate = GetGroupById(GroupID);
            if(!String.IsNullOrEmpty(Name))
                groupToUpdate.Name = Name;
            if(!String.IsNullOrEmpty(Description))
                groupToUpdate.Description = Description;
            groupToUpdate.IsDefault = IsDefault;
            groupToUpdate.IsAdmin = IsAdmin;
            groupToUpdate.IsLimit = IsLimit;
            groupToUpdate.DownloadLimit = DownloadLimit;

            LinqAdapter.SubmitChanges();
        }

        public void RemoveGroup(int GroupID)
        {
            GroupModel groupToRemove = GetGroupById(GroupID);
            LinqAdapter.GetTable<GroupModel>().DeleteOnSubmit(groupToRemove);
            LinqAdapter.SubmitChanges();
        }
    }
}
