using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using FreePDF.UserControl;

namespace FreePDF.AdminCP
{
    public partial class tags : System.Web.UI.Page
    {
        private DataPaging pagingControl;
        private TagsModel tagsLogic;

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

                tagsLogic = new TagsModel();
                tagsLogic.RemoveTag(Key);
                Response.Redirect(Session["PreviousPage"].ToString());
            }

            Session["PreviousPage"] = Request.Url.OriginalString;

            LoadAllTags();
        }

        private void LoadAllTags()
        {
            tagsLogic = new TagsModel();
            int CountDocument = DocumentModel.TotalRecord;
            int StartRowIndex = 0;

            LoadPagingControl(CountDocument);

            StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;

            lstTags.DataSource = tagsLogic.GetTagsList(StartRowIndex, pagingControl.PageSize);
            lstTags.DataBind();
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