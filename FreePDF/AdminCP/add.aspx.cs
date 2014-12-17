using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessLogicLayer;

namespace FreePDF.AdminCP
{
    public partial class add : System.Web.UI.Page
    {
        private UsersModel userLogic = null;
        private GroupModel usergroupLogic = null;
        private CategoryModel categoryLogic = null;
        private int Type;

        protected void Page_Load(object sender, EventArgs e)
        {
            Type = Int32.Parse(Request.QueryString["type"]);

            pnlAddUser.Visible = Type == 1;
            pnlAddGroup.Visible = Type == 2;
            pnlAddCategory.Visible = Type == 3;

            if (!Page.IsPostBack && Type == 1)
            {
                usergroupLogic = new GroupModel();

                ddlGroup.DataSource = usergroupLogic.GetGroupList();
                ddlGroup.DataTextField = "Name";
                ddlGroup.DataValueField = "GroupID";
                ddlGroup.DataBind();                
            }
        }

        protected void btnAddpUser_Click(object sender, EventArgs e)
        {
            userLogic = new UsersModel();
            userLogic.AddUser(txtUsername.Text, txtEmail.Text, txtPassword.Text, Int32.Parse(ddlGroup.SelectedValue));

            Response.Redirect("users.aspx");
        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            usergroupLogic = new GroupModel();
            usergroupLogic.AddGroup(txtName.Text, txtDescription.Text, cbIsLimit.Checked, Int32.Parse(txtDownLimit.Text));            
            
            Response.Redirect("usergroup.aspx");
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            categoryLogic = new CategoryModel();
            categoryLogic.AddCategory(txtCategoryName.Text);

            Response.Redirect("category.aspx");
        }

        protected void btnCancelAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("users.aspx");
        }

        protected void btnCancelAddGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("usergroup.aspx");
        }

        protected void btnCancelAddCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("category.aspx");
        }
    }
}