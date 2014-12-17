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
    [Table(Name = "DocumentRequest")]
    public class RequestModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int RequestID { get; set; }

        [Column(DbType = "INT", CanBeNull = false)]
        public int UserID { get; set; }

        [Column(DbType = "NVARCHAR(500)", CanBeNull = false)]
        public String RequestContent { get; set; }

        [Column(DbType = "DATETIME", CanBeNull = true)]
        public DateTime RequestDate { get; set; }

        public static int TotalRecord
        {
            get { return LinqAdapter.GetTable<RequestModel>().Count(); }
        }

        public IEnumerable<RequestModel> GetRequestList()
        {
            return LinqAdapter.GetTable<RequestModel>().ToList();
        }

        public IEnumerable<RequestModel> GetRequestList(int startRowIndex, int maximumRows)
        {
            return LinqAdapter.GetTable<RequestModel>().Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public RequestModel GetRequestById(int RequestID)
        {
            return LinqAdapter.GetTable<RequestModel>().SingleOrDefault(r => r.RequestID == RequestID);
        }

        public void AddRequest(String Content, int UserID)
        {
            RequestModel requestToInsert = new RequestModel();
            requestToInsert.RequestContent = Content;
            requestToInsert.UserID = UserID;
            requestToInsert.RequestDate = DateTime.Now;

            LinqAdapter.GetTable<RequestModel>().InsertOnSubmit(requestToInsert);
            LinqAdapter.SubmitChanges();
        }

        public void RemoveRequest(int RequestID)
        {
            RequestModel requestToRemove = GetRequestById(RequestID);
            LinqAdapter.GetTable<RequestModel>().DeleteOnSubmit(requestToRemove);
            LinqAdapter.SubmitChanges();
        }
    }
}
