using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace FreePDF.AdminCP
{
    public partial class _default : System.Web.UI.Page
    {
        private UsersModel userLogic;
        private GroupModel groupLogic;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUser"] != null)
                Response.Redirect("administrator.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    userLogic = new UsersModel();
                    groupLogic = new GroupModel();

                    if (userLogic.IsUserAvailable(txtUsername.Text, txtPassword.Text) && groupLogic.IsAdminUser(txtUsername.Text)) //GroupID: 1 => Administrator
                    {
                        Session["AdminUser"] = txtUsername.Text;
                        Response.Redirect("administrator.aspx");
                    }
                    else
                    {
                        blInfo.Items.Clear();
                        blInfo.Items.Add("Tài khoản hoặc mật khẩu không đúng");
                    }
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