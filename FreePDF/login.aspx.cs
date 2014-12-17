using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace FreePDF
{
    public partial class login : System.Web.UI.Page
    {
        private UsersModel userLogic = null;
        public String PreviousPage
        {
            get
            {
                return Request.QueryString["redirect"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["Username"] != null)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertForm", @"alert('Ban ko duoc phep vao trang nay khi dang su dung tai khoan')", true);                
            //    Response.Redirect("default.aspx");
            //}               
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    userLogic = new UsersModel();

                    if (userLogic.IsUserAvailable(txtUsername_lg.Text, txtPassword_lg.Text))
                    {
                        var result = userLogic.SearchUserByUsername(txtUsername_lg.Text, true);
                        int UserID = 0;

                        foreach (UsersModel u in result)
                        {
                            UserID = u.UserID;
                            break;
                        }

                        Session["Username"] = txtUsername_lg.Text;
                        Session["UserID"] = UserID;

                        if (PreviousPage == null)
                            Response.Redirect("/default.aspx");
                        else
                            Response.Redirect(PreviousPage);
                    }
                    else
                    {                        
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