using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class FeedbackBLL
    {
        public int FeedbackID { get; set; }
        public String Sender { get; set; }
        public String Content { get; set; }
        public String Email { get; set; }
        public DateTime FeedbackDate { get; set; }

        public FeedbackBLL() { }

        public FeedbackBLL(int FeedbackID, String Sender, String Content, String Email, DateTime FeedbackDate)
        {
            this.FeedbackID = FeedbackID;
            this.Sender = Sender;
            this.Content = Content;
            this.Email = Email;
            this.FeedbackDate = FeedbackDate;
        }

        public List<FeedbackBLL> GetFeedbackList()
        {
            List<FeedbackBLL> feedbackLogic = new List<FeedbackBLL>();

            DataSet feedbackDS = DAL.CallProcedureReturnDataset("GetFeedbackList");
            DataTable feedbackTable = feedbackDS.Tables[0];
            DataRowCollection feedbackRow = feedbackTable.Rows;

            foreach (DataRow row in feedbackRow)
            {
                feedbackLogic.Add(new FeedbackBLL(
                    Int32.Parse(row["FeedbackID"].ToString()),
                    row["Sender"].ToString(),
                    row["FeedbackContent"].ToString(),
                    row["Email"].ToString(),
                    DateTime.Parse(row["FeedbackDate"].ToString())
                    ));
            }
            
            return feedbackLogic;
        }

        public FeedbackBLL GetFeedbackById(int FeedbackID)
        {
            FeedbackBLL feedbackLogic;

            DataSet feedbackDS = DAL.CallProcedureReturnDataset("GetFeedbackById", "@feedbackid=" + FeedbackID);
            DataTable feedbackTable = feedbackDS.Tables[0];
            DataRow feedbackRow = feedbackTable.Rows[0];

            feedbackLogic = new FeedbackBLL(
                Int32.Parse(feedbackRow["FeedbackID"].ToString()),
                feedbackRow["Sender"].ToString(),
                feedbackRow["FeedbackContent"].ToString(),
                feedbackRow["Email"].ToString(),
                DateTime.Parse(feedbackRow["FeedbackDate"].ToString())
                );

            return feedbackLogic;
        }

        public bool AddFeedback(String Sender, String Email, String Content)
        {
            int rowAffected = DAL.CallUpdateProcedure("AddFeedback", "@sender=" + Sender, "@email=" + Email, "@content=" + Content);

            return rowAffected == 1;
        }

        public bool RemoveFeedback(int FeedbackId)
        {
            int rowAffected = DAL.CallUpdateProcedure("RemoveFeedback", "@feedbackid=" + FeedbackId);

            return rowAffected == 1;
        }
    }
}
