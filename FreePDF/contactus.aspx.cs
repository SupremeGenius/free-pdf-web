using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace FreePDF
{
    public partial class contactus : System.Web.UI.Page
    {
        private FeedbackModel feedbackLogic = null;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    feedbackLogic = new FeedbackModel();
                    feedbackLogic.AddFeedback(txtFullName.Text, txtEmail.Text, txtMessage.Text);

                    blInfo.Items.Add("Gửi thành công");
                }
                catch (ApplicationException ex)
                {
                    blInfo.Items.Clear();
                    blInfo.Items.Add(ex.Message);
                }
            }
        }
    }
}