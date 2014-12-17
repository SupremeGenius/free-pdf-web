using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient;
using ConvertLetterAccent;

namespace BusinessLogicLayer
{
    [Table(Name="Document")]
    public class DocumentModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int DocumentID { get; set; }

        [Column(DbType = "INT", CanBeNull = false)]
        public int UserID { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public Nullable<int> CollectionID { get; set; }

        [Column(DbType = "INT", CanBeNull = false)]
        public int CategoryID { get; set; }

        [Column(DbType = "NVARCHAR(100)", CanBeNull = false)]
        public String Name { get; set; }

        [Column(DbType = "VARCHAR(100)", CanBeNull = false)]
        public String Alias { get; set; }

        [Column(DbType = "NVARCHAR(1000)", CanBeNull = true)]
        public String Description { get; set; }

        [Column(DbType="VARCHAR(200)", CanBeNull = true)]
        public String Thumbnails { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public int TotalView { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public int TotalDownload { get; set; }

        [Column(DbType = "DATETIME", CanBeNull = true)]
        public DateTime UploadDate { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public int FileSize { get; set; }

        [Column(DbType = "VARCHAR(100)", CanBeNull = false)]
        public String Link { get; set; }

        [Column(DbType = "BIT", CanBeNull = true)]
        public Boolean IsError { get; set; }

        [Column(DbType="INT", CanBeNull=true)]
        public int ErrorReportCount { get; set; }

        public static int TotalRecord
        {
            get { return LinqAdapter.GetTable<DocumentModel>().Count(); }
        }

        public IEnumerable<DocumentModel> GetDocumentList()
        {
            return LinqAdapter.GetTable<DocumentModel>().ToList();
        }

        public IEnumerable<DocumentModel> GetDocumentListPartByPart(int startRowIndex, int maximumRows)
        {
            return LinqAdapter.GetTable<DocumentModel>().Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public DocumentModel GetDocumentById(int DocumentID)
        {
            return LinqAdapter.GetTable<DocumentModel>().SingleOrDefault(d => d.DocumentID == DocumentID);
        }

        public IEnumerable<DocumentModel> GetDocumentByUserId(int UserID)
        {
            return LinqAdapter.GetTable<DocumentModel>().Where(d => d.UserID == UserID);
        }
        
        public IEnumerable<DocumentModel> GetDocumentHasError()
        {
            return LinqAdapter.GetTable<DocumentModel>().Where(d => d.IsError == true);
        }

        public IEnumerable<DocumentModel> GetDocumentByCategoryID(int CategoryID)
        {
            return LinqAdapter.GetTable<DocumentModel>().Where(d => d.CategoryID == CategoryID).ToList();
        }

        public IEnumerable<DocumentModel> SearchDocumentByName(String Name)
        {
            return LinqAdapter.GetTable<DocumentModel>().Where(d => SqlMethods.Like(d.Name, "%" + Name + "%")).ToList();
        }

        public IEnumerable<DocumentModel> SearchDocumentByUsername(String Username)
        {
            IEnumerable<UsersModel> user = LinqAdapter.GetTable<UsersModel>();
            IEnumerable<DocumentModel> doc = LinqAdapter.GetTable<DocumentModel>();

            IEnumerable<DocumentModel> result = from u in user
                                                join d in doc
                                                on u.UserID equals d.UserID
                                                //where SqlMethods.Like(u.Username, "%" + Username + "%")
                                                where u.Username.Like("*" + Username + "*")
                                                select d;

            return result.ToList();
        }

        public IEnumerable<DocumentModel> SearchDocumentByCollectionName(String CollectionName)
        {
            IEnumerable<CollectionModel> collection = LinqAdapter.GetTable<CollectionModel>();
            IEnumerable<DocumentModel> doc = LinqAdapter.GetTable<DocumentModel>();

            IEnumerable<DocumentModel> result = from c in collection
                                               join d in doc
                                               on c.CollectionID equals d.UserID
                                               //where SqlMethods.Like(c.Name, "%" + CollectionName + "%")
                                               where c.Name.Like("*" + CollectionName + "*")
                                               select d;

            return result.ToList();
        }

        public void DocumentReportError(int DocumentID)
        {
            DocumentModel docToReportError = GetDocumentById(DocumentID);
            docToReportError.ErrorReportCount += 1;
            docToReportError.IsError = true;

            LinqAdapter.SubmitChanges();
        }

        public int AddDocument(String Name, String Description, String Thumbnails, String Link, int FileSize, int UserID, int CategoryID, Nullable<int> CollectionID)
        {
            ConvertLetter cvLetter = new ConvertLetter();
            String Alias = cvLetter.ClearAccent(Name).ToTitleCase().Replace(" ", "");

            DocumentModel documentToInsert = new DocumentModel();
            
            documentToInsert.UserID = UserID;
            if (CollectionID == 0)
                documentToInsert.CollectionID = null;
            else
                documentToInsert.CollectionID = CollectionID;
            documentToInsert.CategoryID = CategoryID;
            documentToInsert.Name = Name;
            documentToInsert.Alias = Alias;
            documentToInsert.Description = Description;
            documentToInsert.Thumbnails = Thumbnails;
            documentToInsert.TotalView = 0;
            documentToInsert.TotalDownload = 0;
            documentToInsert.UploadDate = DateTime.Now;
            documentToInsert.Link = Link;
            documentToInsert.FileSize = FileSize;
            documentToInsert.IsError = false;
            documentToInsert.ErrorReportCount = 0;

            LinqAdapter.GetTable<DocumentModel>().InsertOnSubmit(documentToInsert);
            LinqAdapter.SubmitChanges();

            return documentToInsert.DocumentID;
        }

        public void UpdateDocument(String Name, String Description, String Thumbnails, String Link, bool IsError, int DocumentID, int CategoryID, Nullable<int> CollectionID, int ErrorReportCount)
        {
            DocumentModel doc = GetDocumentById(DocumentID);
            doc.Name = Name;
            doc.Description = Description;
            doc.Thumbnails = Thumbnails;
            doc.Link = Link;
            doc.IsError = IsError;
            doc.ErrorReportCount = ErrorReportCount;
            doc.CategoryID = CategoryID;
            if (CollectionID == 0)
                doc.CollectionID = null;
            else
                doc.CollectionID = CollectionID;

            LinqAdapter.SubmitChanges();
        }

        public void RemoveDocument(int DocumentID)
        {
            DocumentModel documentToRemove = GetDocumentById(DocumentID);
            LinqAdapter.GetTable<DocumentModel>().DeleteOnSubmit(documentToRemove);
            LinqAdapter.SubmitChanges();
        }
    }
}
