using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessLogicLayer;

namespace FreePDF.AdminCP
{
    public partial class edit : System.Web.UI.Page
    {
        private UsersModel userLogic = null;
        private GroupModel usergroupLogic = null;
        private CategoryModel categoryLogic = null;
        private DocumentModel docLogic = null;
        private CollectionModel collectionLogic = null;
        
        private int Type, UserID, GroupID, CategoryID, DocID, CollectionID;

        protected void Page_Load(object sender, EventArgs e)
        {
            Type = Int32.Parse(Request.QueryString["type"]);
            CollectionID = DocID = CategoryID = GroupID = UserID = Int32.Parse(Request.QueryString["id"]);

            pnlEditUser.Visible = Type == 1;
            pnlEditGroup.Visible = Type == 2;
            pnlEditCategory.Visible = Type == 3;
            pnlEditDocument.Visible = Type == 4;
            pnlEditCollection.Visible = Type == 5;

            if (!Page.IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            if (Type == 1)
            {
                userLogic = new UsersModel();
                userLogic = userLogic.GetUserById(UserID);
                
                usergroupLogic = new GroupModel();

                txtUsername.Text = userLogic.Username;
                txtEmail.Text = userLogic.Email;
                txtPassword.Text = userLogic.Password;
                txtPoint.Text = userLogic.Point.ToString();
                ddlGroup.DataSource = usergroupLogic.GetGroupList();
                ddlGroup.DataTextField = "Name";
                ddlGroup.DataValueField = "GroupID";                
                ddlGroup.DataBind();
                ddlGroup.SelectedIndex = userLogic.GroupID - 1;
            }
            else if (Type == 2)
            {
                usergroupLogic = new GroupModel();
                usergroupLogic = usergroupLogic.GetGroupById(GroupID);

                txtName.Text = usergroupLogic.Name;
                txtDescription.Text = usergroupLogic.Description;
                cbIsLimit.Checked = usergroupLogic.IsLimit;
                txtDownLimit.Text = usergroupLogic.DownloadLimit.ToString();
            }
            else if (Type == 3)
            {
                categoryLogic = new CategoryModel();
                categoryLogic = categoryLogic.GetCategoryById(CategoryID);

                txtCategoryName.Text = categoryLogic.Name;                
            }
            else if (Type == 4)
            {
                categoryLogic = new CategoryModel();

                docLogic = new DocumentModel();
                docLogic = docLogic.GetDocumentById(DocID);

                txtDocName.Text = docLogic.Name;
                txtDocLink.Text = docLogic.Link;
                cbIsError.Checked = docLogic.IsError;
                ddlDocCategory.DataSource = categoryLogic.GetCategoryList();
                ddlDocCategory.DataTextField = "Name";
                ddlDocCategory.DataValueField = "CategoryID";
                ddlDocCategory.DataBind();
                ddlDocCategory.SelectedIndex = docLogic.CategoryID - 1;
                txtDocDescription.Text = docLogic.Description;
            }
            else if (Type == 5)
            {
                collectionLogic = new CollectionModel();
                collectionLogic = collectionLogic.GetCollectionById(CollectionID);

                txtCollectionName.Text = collectionLogic.Name;
                txtCollectionDesc.Text = collectionLogic.Description;
                cbIsCollectionError.Checked = collectionLogic.IsError;
            }
        }

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            userLogic = new UsersModel();
            userLogic = userLogic.GetUserById(UserID);                
            userLogic.UpdateUser(
                txtUsername.Text,                
                txtPassword.Text,
                txtEmail.Text,
                Int32.Parse(txtPoint.Text),
                userLogic.TotalUpload,
                Int32.Parse(ddlGroup.SelectedValue),
                UserID
                );

            Response.Redirect("users.aspx");
        }

        protected void btnEditGroup_Click(object sender, EventArgs e)
        {
            usergroupLogic = new GroupModel();
            usergroupLogic.UpdateGroup(txtName.Text, txtDescription.Text, GroupID);

            Response.Redirect("usergroup.aspx");
        }

        protected void btnEditCategory_Click(object sender, EventArgs e)
        {
            categoryLogic = new CategoryModel();
            categoryLogic.UpdateCategory(txtCategoryName.Text, CategoryID);

            Response.Redirect("category.aspx");
        }


        protected void btnEditDocument_Click(object sender, EventArgs e)
        {
            docLogic = new DocumentModel();
            docLogic = docLogic.GetDocumentById(DocID);                

            docLogic.UpdateDocument(
                txtDocName.Text,
                txtDocDescription.Text,
                null,
                txtDocLink.Text,
                cbIsError.Checked,
                DocID,
                Int32.Parse(ddlDocCategory.SelectedValue),
                docLogic.CollectionID,
                docLogic.ErrorReportCount
                );

            Response.Redirect("document.aspx");
        }

        protected void btnEditCollection_Click(object sender, EventArgs e)
        {
            collectionLogic = new CollectionModel();
            collectionLogic.UpdateCollection(txtCollectionName.Text, txtCollectionDesc.Text, cbIsCollectionError.Checked, CollectionID);

            Response.Redirect("collection.aspx");
        }

        protected void btnCancelEditUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("users.aspx");
        }

        protected void btnCancelEditGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("usergroup.aspx");
        }

        protected void btnCancelEditCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("category.aspx");
        }
        
        protected void btnCancelEditDocument_Click(object sender, EventArgs e)
        {
            Response.Redirect("document.aspx");
        }

        protected void btnCancelEditCollection_Click(object sender, EventArgs e)
        {
            Response.Redirect("collection.aspx");
        }
    }
}