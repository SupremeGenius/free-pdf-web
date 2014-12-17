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
    public partial class users : System.Web.UI.Page
    {
        private DataPaging pagingControl;
        private UsersModel userLogic;
        private GroupModel usergroupLogic;

        public int OrderNo { get; set; }

        public int CurrentPage
        {
            get { return Convert.ToInt32(Request.QueryString["page"] ?? "1"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["removeid"] != null)
            {
                int Key = Convert.ToInt32(Request.QueryString["removeid"]);

                userLogic = new UsersModel();
                userLogic.RemoveUser(Key);
                Response.Redirect(Session["PreviousPage"].ToString());
            }

            Session["PreviousPage"] = Request.Url.OriginalString;

            LoadData();
        }

        private void LoadData()
        {
            userLogic = new UsersModel();
            int CountUser = UsersModel.TotalRecord;
            int StartRowIndex = 0;

            LoadPagingControl(CountUser);

            StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;

            lstUser.DataSource = userLogic.GetUserList(StartRowIndex, pagingControl.PageSize);
            lstUser.DataBind();
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

        public String GetGroupName(int GroupID)
        {
            usergroupLogic = new GroupModel();

            return usergroupLogic.GetGroupById(GroupID).Name;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int searchType = ddlSearchType.SelectedIndex + 1;

            if (txtSearch.Text.Length >= 5)
            {
                userLogic = new UsersModel();

                switch (searchType)
                {
                    case 1:
                        lstUser.DataSource = userLogic.SearchUserByUsername(txtSearch.Text, false);
                        break;
                    case 2:
                        lstUser.DataSource = userLogic.SearchUserByEmail(txtSearch.Text);
                        break;
                }
                
                lstUser.DataBind();
            }
        }
    }
}