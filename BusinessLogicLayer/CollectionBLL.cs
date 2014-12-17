using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccessLayer;
using ConvertLetterAccent;
using System.Data;

namespace BusinessLogicLayer
{
    public class CollectionBLL
    {
        public int CollectionID { get; set; }
        public int UserID { get; set; }
        public String Name { get; set; }
        public String Alias { get; set; }
        public String Description { get; set; }
        public int TotalView { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsError { get; set; }

        public CollectionBLL() { }

        public CollectionBLL(int CollectionID, int UserID, String Name, String Alias, String Description, int TotalView, DateTime CreatedDate, bool IsError)
        {
            this.CollectionID = CollectionID;
            this.UserID = UserID;
            this.Name = Name;
            this.Alias = Alias;
            this.Description = Description;
            this.TotalView = TotalView;
            this.CreatedDate = CreatedDate;
            this.IsError = IsError;
        }

        public List<CollectionBLL> GetCollectionList()
        {
            List<CollectionBLL> collectionLogic = new List<CollectionBLL>();

            DataSet collectionDS = DAL.CallProcedureReturnDataset("GetCollectionList");
            DataTable collectionTable = collectionDS.Tables[0];
            DataRowCollection collectionRow = collectionTable.Rows;

            foreach (DataRow row in collectionRow)
            {
                collectionLogic.Add(new CollectionBLL(
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    row["Name"].ToString(),
                    "",
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    DateTime.Parse(row["CreatedDate"].ToString()),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }            

            return collectionLogic;
        }

        public CollectionBLL GetCollectionById(int CollectionID)
        {
            CollectionBLL collectionLogic;

            DataSet collectionDS = DAL.CallProcedureReturnDataset("GetCollectionById", "@collectionid=" + CollectionID);
            DataTable collectionTable = collectionDS.Tables[0];
            DataRow collectionRow = collectionTable.Rows[0];

            collectionLogic = new CollectionBLL(
                Int32.Parse(collectionRow["CollectionID"].ToString()),
                Int32.Parse(collectionRow["UserID"].ToString()),
                collectionRow["Name"].ToString(),
                "",
                collectionRow["Description"].ToString(),
                Int32.Parse(collectionRow["TotalView"].ToString()),
                DateTime.Parse(collectionRow["CreatedDate"].ToString()),
                Boolean.Parse(collectionRow["IsError"].ToString())
                );
            
            return collectionLogic;
        }

        public List<CollectionBLL> GetCollectionByUserId(int UserID)
        {
            List<CollectionBLL> collectionLogic = new List<CollectionBLL>();

            DataSet collectionDS = DAL.CallProcedureReturnDataset("GetCollectionByUserId", "@userid=" + UserID);
            DataTable collectionTable = collectionDS.Tables[0];
            DataRowCollection collectionRow = collectionTable.Rows;

            foreach (DataRow row in collectionRow)
            {
                collectionLogic.Add(new CollectionBLL(
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    row["Name"].ToString(),
                    "",
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    DateTime.Parse(row["CreatedDate"].ToString()),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return collectionLogic;
        }

        public List<CollectionBLL> GetCollectionHasError()
        {
            List<CollectionBLL> collectionLogic = new List<CollectionBLL>();

            DataSet collectionDS = DAL.CallProcedureReturnDataset("GetCollectionHasError");
            DataTable collectionTable = collectionDS.Tables[0];
            DataRowCollection collectionRow = collectionTable.Rows;

            foreach (DataRow row in collectionRow)
            {
                collectionLogic.Add(new CollectionBLL(
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    row["Name"].ToString(),
                    "",
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    DateTime.Parse(row["CreatedDate"].ToString()),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }
           
            return collectionLogic;
        }

        public List<CollectionBLL> SearchCollectionByName(String Name, bool Absolute)
        {
            List<CollectionBLL> collectionLogic = new List<CollectionBLL>();
            DataSet collectionDS;

            if(Absolute)
                collectionDS = DAL.CallProcedureReturnDataset("SearchCollectionByName", "@name=" + Name, "@absolute=1");
            else
                collectionDS = DAL.CallProcedureReturnDataset("SearchCollectionByName", "@name=" + Name, "@absolute=0");

            DataTable collectionTable = collectionDS.Tables[0];
            DataRowCollection collectionRow = collectionTable.Rows;

            foreach (DataRow row in collectionRow)
            {
                collectionLogic.Add(new CollectionBLL(
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    row["Name"].ToString(),
                    "",
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    DateTime.Parse(row["CreatedDate"].ToString()),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return collectionLogic;
        }

        public List<CollectionBLL> SearchCollectionByUsername(String Username)
        {
            List<CollectionBLL> collectionLogic = new List<CollectionBLL>();

            DataSet collectionDS = DAL.CallProcedureReturnDataset("SearchCollectionByUsername", "@username=" + Username);
            DataTable collectionTable = collectionDS.Tables[0];
            DataRowCollection collectionRow = collectionTable.Rows;

            foreach (DataRow row in collectionRow)
            {
                collectionLogic.Add(new CollectionBLL(
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    row["Name"].ToString(),
                    "",
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    DateTime.Parse(row["CreatedDate"].ToString()),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return collectionLogic;
        }

        public int AddCollection(String Name, String Description, int UserID)
        {
            ConvertLetter cvLetter = new ConvertLetter();
            String alias = cvLetter.ClearAccent(Name).ToTitleCase().Replace(" ", "");
            int rowAffected = DAL.CallUpdateProcedure("AddCollection", "@name=" + Name, "@alias=" + alias, "@description=" + Description, "@userid=" + UserID);

            int tmpCollectionID =  SearchCollectionByName(Name, true)[0].CollectionID;

            return tmpCollectionID;
        }

        public bool UpdateCollection(String Name, String Description, bool IsError, int CollectionID)
        {
            ConvertLetter cvLetter = new ConvertLetter();
            String alias = cvLetter.ClearAccent(Name).ToTitleCase().Replace(" ", "");
            int rowAffected = DAL.CallUpdateProcedure("UpdateCollection", "@name=" + Name, "@alias=" + alias, "@description=" + Description,
                "@iserror=" + IsError, "@collectionid=" + CollectionID);

            return rowAffected == 1;
        }

        public bool RemoveCollection(int CollectionID)
        {
            int rowAffected = DAL.CallUpdateProcedure("RemoveCollection", "@collectionid=" + CollectionID);

            return rowAffected == 1;
        }
    }
}
