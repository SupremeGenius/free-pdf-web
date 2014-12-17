using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class UserGroupBLL
    {
        public int GroupID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Boolean IsDefault { get; set; }
        public Boolean IsAdmin { get; set; }

        public UserGroupBLL() { }

        public UserGroupBLL(int GroupID, String Name, String Description, bool IsDefault = false, bool IsAdmin = false)
        {
            this.GroupID = GroupID;
            this.Name = Name;
            this.Description = Description;
            this.IsDefault = IsDefault;
            this.IsAdmin = IsAdmin;
        }

        public List<UserGroupBLL> GetGroupList()
        {
            List<UserGroupBLL> groupLogic = new List<UserGroupBLL>();

            DataSet groupDS = DAL.CallProcedureReturnDataset("GetGroupList");
            DataTable groupTable = groupDS.Tables[0];
            DataRowCollection groupRow = groupTable.Rows;

            foreach (DataRow row in groupRow)
            {
                groupLogic.Add(new UserGroupBLL(
                    Int32.Parse(row["GroupID"].ToString()),
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    Boolean.Parse(row["IsDefault"].ToString()),
                    Boolean.Parse(row["IsAdmin"].ToString())
                    ));
            }

            return groupLogic;
        }

        public UserGroupBLL GetGroupById(int GroupID)
        {
            UserGroupBLL groupLogic;

            DataSet groupDS = DAL.CallProcedureReturnDataset("GetGroupById", "@groupid=" + GroupID);
            DataTable groupTable = groupDS.Tables[0];
            DataRow row = groupTable.Rows[0];

            groupLogic = new UserGroupBLL(
                Int32.Parse(row["GroupID"].ToString()),
                row["Name"].ToString(),
                row["Description"].ToString(),
                Boolean.Parse(row["IsDefault"].ToString()),
                Boolean.Parse(row["IsAdmin"].ToString())
                );

            return groupLogic;
        }

        public int GetDefaultGroupID()
        {
            return Int32.Parse(DAL.CallProcedure("GetDefaultGroupID")[0]["GroupID"]);
        }

        public bool IsAdminUser(String Username)
        {
            return Int32.Parse(DAL.CallProcedure("IsAdminUser", "@username=" + Username)[0]["Total"]) != 0;
        }

        public bool AddGroup(String Name, String Description)
        {
            int rowAffected = DAL.CallUpdateProcedure("AddGroup", "@name=" + Name, "@description=" + Description);

            return rowAffected == 1;
        }

        public bool UpdateGroup(String Name, String Description, int GroupID, bool IsDefault = false, bool IsAdmin = false)
        {
            int rowAffected = DAL.CallUpdateProcedure("UpdateGroup", "@name=" + Name, "@description=" + Description, "@groupid=" + GroupID, "@isdefault=" + IsDefault, "@isadmin=" + IsAdmin);

            return rowAffected == 1;
        }

        public bool RemoveGroup(int GroupID)
        {
            int rowAffected = DAL.CallUpdateProcedure("RemoveGroup", "@groupid=" + GroupID);

            return rowAffected == 1;
        }
    }
}
