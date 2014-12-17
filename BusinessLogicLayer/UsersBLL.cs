using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class UsersBLL
    {
        public int UserID { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public int Point { get; set; }
        public int TotalUpload { get; set; }
        public int GroupID { get; set; }

        public UsersBLL() { }

        public UsersBLL(int UserID, String Username, String Password, String Email, int Point, int TotalUpload, int GroupID)
        {
            this.UserID = UserID;
            this.Username = Username;
            this.Password = Password;
            this.Email = Email;
            this.Point = Point;
            this.TotalUpload = TotalUpload;
            this.GroupID = GroupID;                 
        }

        public List<UsersBLL> GetUserList()
        {
            List<UsersBLL> userLogic = new List<UsersBLL>();

            DataSet UserDS = DAL.CallProcedureReturnDataset("GetUserList");
            DataTable userTable = UserDS.Tables[0];
            DataRowCollection userRow = userTable.Rows;

            foreach(DataRow row in userRow)
            {
                userLogic.Add(new UsersBLL(
                    Int32.Parse(row["UserID"].ToString()),
                    row["Username"].ToString(),
                    row["Password"].ToString(),
                    row["Email"].ToString(),
                    Int32.Parse(row["Point"].ToString()),
                    Int32.Parse(row["TotalUpload"].ToString()),
                    Int32.Parse(row["GroupID"].ToString())
                    ));
            }

            return userLogic;
        }

        public UsersBLL GetUserById(int UserID)
        {
            UsersBLL userLogic;

            DataSet UserDS = DAL.CallProcedureReturnDataset("GetUserById", "@userid=" + UserID);
            DataTable userTable = UserDS.Tables[0];
            DataRow userRow = userTable.Rows[0];

            userLogic = new UsersBLL(
                Int32.Parse(userRow["UserID"].ToString()),
                userRow["Username"].ToString(),
                userRow["Password"].ToString(),
                userRow["Email"].ToString(),
                Int32.Parse(userRow["Point"].ToString()),
                Int32.Parse(userRow["TotalUpload"].ToString()),
                Int32.Parse(userRow["GroupID"].ToString())
                );

            return userLogic;
        }

        public List<UsersBLL> SearchUserByUsername(String Username, bool Absolute)
        {
            List<UsersBLL> userLogic = new List<UsersBLL>();
            DataSet userDS;

            if(Absolute)
                userDS = DAL.CallProcedureReturnDataset("SearchUserByUsername", "@username=" + Username, "@absolute=1");
            else
                userDS = DAL.CallProcedureReturnDataset("SearchUserByUsername", "@username=" + Username, "@absolute=0");

            DataTable userTable = userDS.Tables[0];
            DataRowCollection userRow = userTable.Rows;

            foreach (DataRow row in userRow)
            {
                userLogic.Add(new UsersBLL(
                    Int32.Parse(row["UserID"].ToString()),
                    row["Username"].ToString(),
                    row["Password"].ToString(),
                    row["Email"].ToString(),
                    Int32.Parse(row["Point"].ToString()),
                    Int32.Parse(row["TotalUpload"].ToString()),
                    Int32.Parse(row["GroupID"].ToString())
                    ));
            }            

            return userLogic;
        }

        public List<UsersBLL> SeachUserByEmail(String Email)
        {

            List<UsersBLL> userLogic = new List<UsersBLL>();

            DataSet UserDS = DAL.CallProcedureReturnDataset("SearchUserByEmail", "@email=" + Email);
            DataTable userTable = UserDS.Tables[0];
            DataRowCollection userRow = userTable.Rows;

            foreach (DataRow row in userRow)
            {
                userLogic.Add(new UsersBLL(
                    Int32.Parse(row["UserID"].ToString()),
                    row["Username"].ToString(),
                    row["Password"].ToString(),
                    row["Email"].ToString(),
                    Int32.Parse(row["Point"].ToString()),
                    Int32.Parse(row["TotalUpload"].ToString()),
                    Int32.Parse(row["GroupID"].ToString())
                    ));
            }

            return userLogic;
        }

        public int GetUserGroupByUsername(String Username)
        {
            int groupID = Int32.Parse(DAL.CallProcedure("GetUserGroupByUsername", "@username=" + Username)[0]["GroupID"]);

            return groupID;
        }

        public bool IsUserExist(String Username)
        {            
            return DAL.CallProcedure("IsUserExist", "@username=" + Username).Count != 0;
        }

        public bool IsEmailExist(String Email)
        {
            return DAL.CallProcedure("IsEmailExist", "@email=" + Email).Count != 0;
        }

        public bool IsUserAvailable(String Username, String Password)
        {
            List<SList> row = DAL.CallProcedure("IsUserAvailable", "@username=" + Username, "@password=" + Password);

            return row.Count != 0;
        }

        public bool AddUser(String Username, String Password, String Email, int GroupID)
        {
            int rowAffected = DAL.CallUpdateProcedure("AddUser", "@username=" + Username, "@password=" + Password, "@email=" + Email, "@groupid=" + GroupID);

            return rowAffected == 1;
        }

        public bool UpdateUser(String Username, String Password, String Email, int Point, int TotalUpload, int GroupID, int UserID)
        {
            int rowAffected = DAL.CallUpdateProcedure("UpdateUser", "@username=" + Username, "@password=" + Password, "@email=" + Email,
                "@point=" + Point, "@totalupload=" + TotalUpload, "@groupid=" + GroupID, "@userid=" + UserID);

            return rowAffected == 1;
        }

        public bool RemoveUser(int UserID)
        {
            int rowAffected = DAL.CallUpdateProcedure("RemoveUser", "@userid=" + UserID);

            return rowAffected == 1;
        }
    }
}
