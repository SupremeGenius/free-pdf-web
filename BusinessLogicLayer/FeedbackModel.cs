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
    [Table(Name="Feedback")]
    public class FeedbackModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int FeedbackID { get; set; }

        [Column(DbType = "NVARCHAR(45)", CanBeNull = false)]
        public String Sender { get; set; }

        [Column(DbType = "NVARCHAR(500)", CanBeNull = false)]
        public String FeedbackContent { get; set; }

        [Column(DbType = "NVARCHAR(45)", CanBeNull = true)]
        public String Email { get; set; }

        [Column(DbType = "DATETIME", CanBeNull = true)]
        public DateTime FeedbackDate { get; set; }

        public static int TotalRecord
        {
            get { return LinqAdapter.GetTable<FeedbackModel>().Count(); }
        }

        public IEnumerable<FeedbackModel> GetFeedbackList()
        {
            return LinqAdapter.GetTable<FeedbackModel>().ToList();
        }

        public IEnumerable<FeedbackModel> GetFeedbackList(int startRowIndex, int maximumRows)
        {
            return LinqAdapter.GetTable<FeedbackModel>().Skip(startRowIndex).Take(maximumRows).ToList();                 
        }

        public FeedbackModel GetFeedbackById(int FeedbackID)
        {
            return LinqAdapter.GetTable<FeedbackModel>().SingleOrDefault(f => f.FeedbackID == FeedbackID);
        }

        public void AddFeedback(String Sender, String Email, String Content)
        {
            FeedbackModel feedbackToIsert = new FeedbackModel();
            feedbackToIsert.Sender = Sender;
            feedbackToIsert.Email = Email;
            feedbackToIsert.FeedbackContent = Content;
            feedbackToIsert.FeedbackDate = DateTime.Now;

            LinqAdapter.GetTable<FeedbackModel>().InsertOnSubmit(feedbackToIsert);
            LinqAdapter.SubmitChanges();
        }

        public void RemoveFeedback(int FeedbackID)
        {
            FeedbackModel feedbackToRemove = GetFeedbackById(FeedbackID);
            LinqAdapter.GetTable<FeedbackModel>().DeleteOnSubmit(feedbackToRemove);
            LinqAdapter.SubmitChanges();                
        }
    }
}
