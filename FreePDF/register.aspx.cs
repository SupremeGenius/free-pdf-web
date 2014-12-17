using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace FreePDF
{
    public partial class register : System.Web.UI.Page
    {
        private UsersModel userLogic;
        private GroupModel groupLogic;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    userLogic = new UsersModel();
                    groupLogic = new GroupModel();

                    if (userLogic.IsUserExist(txtUsername_reg.Text))
                    {                        
                        blInfo.Items.Add("Tên tài khoản đã tồn tại trong hệ thống");
                    }
                    else if (userLogic.IsEmailExist(txtEmail_reg.Text))
                    {                        
                        blInfo.Items.Add("Email đã tồn tại trong hệ thống");
                    }
                    else
                    {
                        userLogic.AddUser(txtUsername_reg.Text, txtPassword_reg.Text, txtEmail_reg.Text, groupLogic.GetDefaultGroupID());
                            
                        blInfo.Items.Add("Đăng kí tài khoản thành công. Bạn hãy kiểm tra Email để kích hoạt tài khoản");              
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