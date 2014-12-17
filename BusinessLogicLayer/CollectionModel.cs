using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using ConvertLetterAccent;

namespace BusinessLogicLayer
{
    [Table(Name="DocumentCollection")]
    public class CollectionModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int CollectionID { get; set; }

        [Column(DbType = "INT", CanBeNull = false)]
        public int UserID { get; set; }

        [Column(DbType = "NVARCHAR(45)", CanBeNull = false)]
        public String Name { get; set; }

        [Column(DbType = "VARCHAR(45)", CanBeNull = false)]
        public String Alias { get; set; }

        [Column(DbType = "NVARCHAR(500)", CanBeNull = true)]
        public String Description { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public int TotalView { get; set; }

        [Column(DbType = "DATETIME", CanBeNull = true)]
        public DateTime CreatedDate { get; set; }

        [Column(DbType = "BIT", CanBeNull = true)]
        public Boolean IsError { get; set; }

        public static int TotalRecord
        {
            get { return LinqAdapter.GetTable<CollectionModel>().Count(); }
        }

        public IEnumerable<CollectionModel> GetCollectionList()
        {
            return LinqAdapter.GetTable<CollectionModel>().ToList();
        }

        public IEnumerable<CollectionModel> GetCollectionList(int startRowIndex, int maximumRows)
        {
            return LinqAdapter.GetTable<CollectionModel>().Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public CollectionModel GetCollectionById(int CollectionID)
        {
            return LinqAdapter.GetTable<CollectionModel>().SingleOrDefault(c => c.CollectionID == CollectionID);
        }

        public IEnumerable<CollectionModel> GetCollectionByUserId(int UserID)
        {
            return LinqAdapter.GetTable<CollectionModel>().Where(c => c.UserID == UserID).ToList();
        }        

        public IEnumerable<CollectionModel> GetCollectionHasError()
        {
            return LinqAdapter.GetTable<CollectionModel>().Where(c => c.IsError == true).ToList();
        }

        public IEnumerable<CollectionModel> SearchCollectionByName(String CollectionName, bool Absolute)
        {
            String SearchPattern = Absolute == true ? CollectionName : "%" + CollectionName + "%";
            return LinqAdapter.GetTable<CollectionModel>().Where(c => SqlMethods.Like(c.Name, SearchPattern)).ToList();
        }

        public IEnumerable<CollectionModel> SearchCollectionByUsername(String Username)
        {
            UsersModel userObj = new UsersModel();
            CollectionModel collectionObj = new CollectionModel();

            IEnumerable<UsersModel> user = userObj.GetUserList();
            IEnumerable<CollectionModel> collection = collectionObj.GetCollectionList();

            IEnumerable<CollectionModel> result = from u in user
                                                  join c in collection
                                                  on u.UserID equals c.UserID
                                                  where SqlMethods.Like(u.Username, "%" + Username + "%")
                                                  select c;

            return result;
        }

        public int AddCollection(String Name, String Description, int UserID)
        {
            ConvertLetter cvLetter = new ConvertLetter();
            String Alias = cvLetter.ClearAccent(Name).ToTitleCase().Replace(" ", "_");

            CollectionModel collectionToInsert = new CollectionModel();
            collectionToInsert.UserID = UserID;
            collectionToInsert.Name = Name;
            collectionToInsert.Alias = Alias;
            collectionToInsert.Description = Description;
            collectionToInsert.TotalView = 0;
            collectionToInsert.CreatedDate = DateTime.Now;
            collectionToInsert.IsError = false;

            LinqAdapter.GetTable<CollectionModel>().InsertOnSubmit(collectionToInsert);
            LinqAdapter.SubmitChanges();

            return collectionToInsert.CollectionID;
        }

        public void UpdateCollection(String Name, String Description, bool IsError, int CollectionID)
        {
            ConvertLetter cvLetter = new ConvertLetter();
            String Alias = cvLetter.ClearAccent(Name).ToTitleCase().Replace(" ", "_");

            CollectionModel collection = GetCollectionById(CollectionID);
            collection.Name = Name;
            collection.Alias = Alias;
            collection.Description = Description;
            collection.IsError = IsError;

            LinqAdapter.SubmitChanges();
        }

        public void RemoveCollection(int CollectionID)
        {
            CollectionModel collectionToRemove = GetCollectionById(CollectionID);
            LinqAdapter.GetTable<CollectionModel>().DeleteOnSubmit(collectionToRemove);
            LinqAdapter.SubmitChanges();
        }
    }
}
