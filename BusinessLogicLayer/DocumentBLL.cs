using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DatabaseAccessLayer;

namespace BusinessLogicLayer
{
    public class DocumentBLL
    {
        public int DocumentID { get; set; }
        public int UserID { get; set; }
        public int CollectionID { get; set; }
        public int CategoryID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int TotalView { get; set; }
        public int TotalDownload { get; set; }
        public DateTime UploadDate { get; set; }
        public int FileSize { get; set; }
        public String Link { get; set; }
        public Boolean IsError { get; set; }

        public DocumentBLL() { }

        public DocumentBLL(int DocumentID, int UserID, int CollectionID, int CategoryID, String Name, String Descirption, int TotalView, int TotalDownload, DateTime UploadDate, int FileSize, String Link, bool IsError)
        {
            this.DocumentID = DocumentID;
            this.UserID = UserID;
            this.CollectionID = CollectionID;
            this.CategoryID = CategoryID;
            this.Name = Name;
            this.Description = Descirption;
            this.TotalView = TotalView;
            this.TotalDownload = TotalDownload;
            this.UploadDate = UploadDate;
            this.FileSize = FileSize;
            this.Link = Link;
            this.IsError = IsError;
        }

        public List<DocumentBLL> GetDocumentList()
        {
            List<DocumentBLL> documentLogic = new List<DocumentBLL>();
            DataSet documentDS = DAL.CallProcedureReturnDataset("GetDocumentList");
            DataTable documentTable = documentDS.Tables[0];
            DataRowCollection documentRow = documentTable.Rows;

            foreach (DataRow row in documentRow)
            {
                documentLogic.Add(new DocumentBLL(
                    Int32.Parse(row["DocumentID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["CategoryID"].ToString()),
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    Int32.Parse(row["TotalDownload"].ToString()),
                    DateTime.Parse(row["UploadDate"].ToString()),
                    Int32.Parse(row["FileSize"].ToString()),
                    row["Link"].ToString(),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return documentLogic;
        }

        public List<DocumentBLL> GetRangeDocument(int startRowIndex, int maximumRows)
        {
            List<DocumentBLL> documentLogic = new List<DocumentBLL>();
            DataSet documentDS = DAL.CallProcedureReturnDataset("GetPagedDocument", "@StartRowIndex=" + startRowIndex, "@MaximumRows=" + maximumRows);
            DataTable documentTable = documentDS.Tables[0];
            DataRowCollection documentRow = documentTable.Rows;

            foreach (DataRow row in documentRow)
            {
                documentLogic.Add(new DocumentBLL(
                    Int32.Parse(row["DocumentID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["CategoryID"].ToString()),
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    Int32.Parse(row["TotalDownload"].ToString()),
                    DateTime.Parse(row["UploadDate"].ToString()),
                    Int32.Parse(row["FileSize"].ToString()),
                    row["Link"].ToString(),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return documentLogic;
        }

        public DocumentBLL GetDocumentById(int DocumentID)
        {
            DocumentBLL documentLogic;
            DataSet documentDS = DAL.CallProcedureReturnDataset("GetDocumentById", "@docid=" + DocumentID);
            DataTable documentTable = documentDS.Tables[0];
            DataRow documentRow = documentTable.Rows[0];

            documentLogic = new DocumentBLL(
                Int32.Parse(documentRow["DocumentID"].ToString()),
                Int32.Parse(documentRow["UserID"].ToString()),
                Int32.Parse(documentRow["CollectionID"].ToString()),
                Int32.Parse(documentRow["CategoryID"].ToString()),
                documentRow["Name"].ToString(),
                documentRow["Description"].ToString(),
                Int32.Parse(documentRow["TotalView"].ToString()),
                Int32.Parse(documentRow["TotalDownload"].ToString()),
                DateTime.Parse(documentRow["UploadDate"].ToString()),
                Int32.Parse(documentRow["FileSize"].ToString()),
                documentRow["Link"].ToString(),
                Boolean.Parse(documentRow["IsError"].ToString())
                );

            return documentLogic;
        }

        public List<DocumentBLL> GetDocumentByUserId(int UserID)
        {
            List<DocumentBLL> documentLogic = new List<DocumentBLL>();
            DataSet documentDS = DAL.CallProcedureReturnDataset("GetDocumentById", "@userid=" + UserID);
            DataTable documentTable = documentDS.Tables[0];
            DataRowCollection documentRow = documentTable.Rows;

            foreach (DataRow row in documentRow)
            {
                documentLogic.Add(new DocumentBLL(
                    Int32.Parse(row["DocumentID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["CategoryID"].ToString()),
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    Int32.Parse(row["TotalDownload"].ToString()),
                    DateTime.Parse(row["UploadDate"].ToString()),
                    Int32.Parse(row["FileSize"].ToString()),
                    row["Link"].ToString(),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return documentLogic;
        }

        public List<DocumentBLL> GetDocumentHasError()
        {
            List<DocumentBLL> documentLogic = new List<DocumentBLL>();
            DataSet documentDS = DAL.CallProcedureReturnDataset("GetDocumentHasError");
            DataTable documentTable = documentDS.Tables[0];
            DataRowCollection documentRow = documentTable.Rows;

            foreach (DataRow row in documentRow)
            {
                documentLogic.Add(new DocumentBLL(
                    Int32.Parse(row["DocumentID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["CategoryID"].ToString()),
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    Int32.Parse(row["TotalDownload"].ToString()),
                    DateTime.Parse(row["UploadDate"].ToString()),
                    Int32.Parse(row["FileSize"].ToString()),
                    row["Link"].ToString(),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return documentLogic;
        }

        public List<DocumentBLL> GetDocumentByCategoryID(int CategoryID)
        {
            List<DocumentBLL> documentLogic = new List<DocumentBLL>();
            DataSet documentDS = DAL.CallProcedureReturnDataset("GetDocumentByCategoryID", "@categoryid=" + CategoryID);
            DataTable documentTable = documentDS.Tables[0];
            DataRowCollection documentRow = documentTable.Rows;

            foreach (DataRow row in documentRow)
            {
                documentLogic.Add(new DocumentBLL(
                    Int32.Parse(row["DocumentID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["CategoryID"].ToString()),
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    Int32.Parse(row["TotalDownload"].ToString()),
                    DateTime.Parse(row["UploadDate"].ToString()),
                    Int32.Parse(row["FileSize"].ToString()),
                    row["Link"].ToString(),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return documentLogic;
        }

        public List<DocumentBLL> SearchDocumentByName(String Name)
        {
            List<DocumentBLL> documentLogic = new List<DocumentBLL>();
            DataSet documentDS = DAL.CallProcedureReturnDataset("SearchDocumentByName", "@name=" + Name);
            DataTable documentTable = documentDS.Tables[0];
            DataRowCollection documentRow = documentTable.Rows;

            foreach (DataRow row in documentRow)
            {
                documentLogic.Add(new DocumentBLL(
                    Int32.Parse(row["DocumentID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["CategoryID"].ToString()),
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    Int32.Parse(row["TotalDownload"].ToString()),
                    DateTime.Parse(row["UploadDate"].ToString()),
                    Int32.Parse(row["FileSize"].ToString()),
                    row["Link"].ToString(),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return documentLogic;
        }

        public List<DocumentBLL> SearchDocumentByUsername(String Username)
        {
            List<DocumentBLL> documentLogic = new List<DocumentBLL>();
            DataSet documentDS = DAL.CallProcedureReturnDataset("SearchDocumentByUsername", "@username=" + Username);
            DataTable documentTable = documentDS.Tables[0];
            DataRowCollection documentRow = documentTable.Rows;

            foreach (DataRow row in documentRow)
            {
                documentLogic.Add(new DocumentBLL(
                    Int32.Parse(row["DocumentID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["CategoryID"].ToString()),
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    Int32.Parse(row["TotalDownload"].ToString()),
                    DateTime.Parse(row["UploadDate"].ToString()),
                    Int32.Parse(row["FileSize"].ToString()),
                    row["Link"].ToString(),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return documentLogic;
        }

        public List<DocumentBLL> SearchDocumentByCollectionName(String Name)
        {
            List<DocumentBLL> documentLogic = new List<DocumentBLL>();
            DataSet documentDS = DAL.CallProcedureReturnDataset("SearchDocumentByCollectionName", "@name=" + Name);
            DataTable documentTable = documentDS.Tables[0];
            DataRowCollection documentRow = documentTable.Rows;

            foreach (DataRow row in documentRow)
            {
                documentLogic.Add(new DocumentBLL(
                    Int32.Parse(row["DocumentID"].ToString()),
                    Int32.Parse(row["UserID"].ToString()),
                    Int32.Parse(row["CollectionID"].ToString()),
                    Int32.Parse(row["CategoryID"].ToString()),
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    Int32.Parse(row["TotalView"].ToString()),
                    Int32.Parse(row["TotalDownload"].ToString()),
                    DateTime.Parse(row["UploadDate"].ToString()),
                    Int32.Parse(row["FileSize"].ToString()),
                    row["Link"].ToString(),
                    Boolean.Parse(row["IsError"].ToString())
                    ));
            }

            return documentLogic;
        }

        public static int GetTotalDocument()
        {
            return Convert.ToInt32(DAL.CallProcedure("GetTotalDocument")[0]["Total"]);
        }

        public bool AddDocument(String Name, String Description, String Link, int FileSize, int UserID, int CategoryID, int CollectionID)
        {
            int rowAffected = DAL.CallUpdateProcedure("AddDocument", "@name=" + Name, "@description=" + Description, "@link=" + Link,
                "@filesize=" + FileSize, "@userid=" + UserID, "@categoryid=" + CategoryID, "@collectionid=" + CollectionID);

            return rowAffected == 1;
        }

        public bool UpdateDocument(String Name, String Description, String Link, bool IsError, int DocumentID, int CategoryID, int CollectionID)
        {
            int rowAffected = DAL.CallUpdateProcedure("UpdateDocument", "@name=" + Name, "@description=" + Description, "@link=" + Link,
                "@iserror=" + IsError, "@docid=" + DocumentID, "@categoryid=" + CategoryID, "@collectionid=" + CollectionID);

            return rowAffected == 1;
        }

        public bool RemoveDocument(int DocumentID)
        {
            int rowAffected = DAL.CallUpdateProcedure("RemoveDocument", "@docid=" + DocumentID);

            return rowAffected == 1;
        }
    }
}
