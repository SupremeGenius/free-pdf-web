using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient;
using System.Text;

namespace BusinessLogicLayer
{
    [Table(Name="Users")]
    public class UsersModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int UserID { get; set; }

        [Column(DbType="VARCHAR(45)", CanBeNull=false)]
        public String Username { get; set; }

        [Column(DbType="VARCHAR(45)", CanBeNull=false)]
        public String Password { get; set; }

        [Column(DbType="VARCHAR(45)", CanBeNull=false)]
        public String Email { get; set; }

        [Column(DbType="INT", CanBeNull=true)]
        public int Point { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public int TotalUpload { get; set; }

        [Column(DbType = "INT", CanBeNull = false)]
        public int GroupID { get; set; }

        public static int TotalRecord
        {
            get { return LinqAdapter.GetTable<UsersModel>().Count(); }
        }

        public IEnumerable<UsersModel> GetUserList()
        {
            return LinqAdapter.GetTable<UsersModel>().ToList();
        }

        public IEnumerable<UsersModel> GetUserList(int startRowIndex, int maximumRows)
        {
            return LinqAdapter.GetTable<UsersModel>().Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public UsersModel GetUserById(int UserID)
        {
            return LinqAdapter.GetTable<UsersModel>().SingleOrDefault(u => u.UserID == UserID);
        }

        public IEnumerable<UsersModel> SearchUserByUsername(String Username, bool Absolute)
        {
            String SearchPatern = Absolute == true ? Username : "%" + Username + "%";
            return LinqAdapter.GetTable<UsersModel>().Where(u => SqlMethods.Like(u.Username, SearchPatern)).ToList();
        }

        public IEnumerable<UsersModel> SearchUserByEmail(String Email)
        {
            return LinqAdapter.GetTable<UsersModel>().Where(u => SqlMethods.Like(u.Email, "%" + Email + "%")).ToList();
        }

        public int GetUserGroupByUsername(String Username)
        {
            return LinqAdapter.GetTable<UsersModel>().SingleOrDefault(u => u.Username == Username).GroupID;
        }

        public bool IsUserExist(String Username)
        {
            return SearchUserByUsername(Username, true).Count() != 0;
        }

        public bool IsEmailExist(String Email)
        {
            return SearchUserByEmail(Email).Count() != 0;
        }

        public bool IsUserAvailable(String Username, String Password)
        {
            return LinqAdapter.GetTable<UsersModel>().Where(u => u.Username == Username && u.Password == Password).Count() != 0;
        }

        public void AddUser(String Username, String Password, String Email, int GroupID)
        {
            UsersModel userToInsert = new UsersModel();
            userToInsert.Username = Username;
            userToInsert.Password = Password;
            userToInsert.Email = Email;
            userToInsert.Point = 0;
            userToInsert.TotalUpload = 0;
            userToInsert.GroupID = GroupID;

            LinqAdapter.GetTable<UsersModel>().InsertOnSubmit(userToInsert);
            LinqAdapter.SubmitChanges();
        }

        public void UpdateUser(String Username, String Password, String Email, int Point, int TotalUpload, int GroupID, int UserID)
        {
            UsersModel user = GetUserById(UserID);
            user.Username = Username;
            user.Email = Email;
            user.Password = Password;
            user.Point = Point;
            user.TotalUpload = TotalUpload;
            user.GroupID = GroupID;

            LinqAdapter.SubmitChanges();
        }

        public void RemoveUser(int UserID)
        {
            UsersModel userToRemove = GetUserById(UserID);
            LinqAdapter.GetTable<UsersModel>().DeleteOnSubmit(userToRemove);
            LinqAdapter.SubmitChanges();
        }
    }
}
