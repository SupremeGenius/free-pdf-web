using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace FreePDF.AdminCP
{
    public partial class usergroup : System.Web.UI.Page
    {
        private GroupModel usergroupLogic = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            usergroupLogic = new GroupModel();
            grvGroupList.DataSource = usergroupLogic.GetGroupList();
            grvGroupList.DataBind();
        }

        protected void grvGroupList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String Key = grvGroupList.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString();

            if (e.CommandName == "EditRow")
            {
                Response.Redirect("edit.aspx?type=2&id=" + Key);
            }
            else if (e.CommandName == "DeleteRow")
            {
                usergroupLogic = new GroupModel();
                usergroupLogic.RemoveGroup(Int32.Parse(Key));
                LoadData();
            }
        }
    }
}