using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreePDF.UserControl;
using BusinessLogicLayer;

namespace FreePDF.AdminCP
{
    public partial class feedback : System.Web.UI.Page
    {
        private DataPaging pagingControl;
        private FeedbackModel feedbackLogic;
        private String Filter = null;
        private int KeyID = 0;

        public int OrderNo { get; set; }

        public int CurrentPage
        {
            get { return Convert.ToInt32(Request.QueryString["page"] ?? "1"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["filter"] != null)
            {
                Filter = Request.QueryString["filter"];                
                KeyID = Int32.Parse(Request.QueryString["id"]);
            }

            if (Request.QueryString["removeid"] != null)
            {
                int Key = Convert.ToInt32(Request.QueryString["removeid"]);

                feedbackLogic = new FeedbackModel();
                feedbackLogic.RemoveFeedback(Key);
                Response.Redirect(Session["PreviousPage"].ToString());
            }

            Session["PreviousPage"] = Request.Url.OriginalString;

            LoadData();            
        }

        private void LoadData()
        {
            if (Filter == "details")
            {
                pnlDetails.Visible = true;
                FeedbackDetails(KeyID);
            }
            else
            {
                pnlView.Visible = true;
                LoadAllFeedback();
            }
        }

        private void LoadAllFeedback()
        {
            feedbackLogic = new FeedbackModel();
            int CountFeedback = FeedbackModel.TotalRecord;
            int StartRowIndex = 0;

            LoadPagingControl(CountFeedback);

            StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;

            lstFeedback.DataSource = feedbackLogic.GetFeedbackList(StartRowIndex, pagingControl.PageSize);
            lstFeedback.DataBind();
        }

        private void FeedbackDetails(int FeedbackID)
        {
            feedbackLogic = new FeedbackModel();
            feedbackLogic = feedbackLogic.GetFeedbackById(FeedbackID);

            List<FeedbackModel> feedbackDetails = new List<FeedbackModel>();
            feedbackDetails.Add(feedbackLogic);

            dvFeedback.DataSource = feedbackDetails;
            dvFeedback.DataBind();
        }

        private void LoadPagingControl(int Count)
        {
            pagingControl = Page.LoadControl("~/UserControl/DataPaging.ascx") as DataPaging;
            pagingControl.PageSize = 5;
            pagingControl.CurrentPage = CurrentPage;
            pagingControl.TotalRecord = Count;
            if (Count != 0)
                pageHolder.Controls.Add(pagingControl);
        }
    }
}