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
    public partial class category : System.Web.UI.Page
    {
        private DataPaging pagingControl;
        private CategoryModel categoryLogic;

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

                categoryLogic = new CategoryModel();
                categoryLogic.RemoveCategory(Key);
                Response.Redirect(Session["PreviousPage"].ToString());
            }

            Session["PreviousPage"] = Request.Url.OriginalString;

            LoadData();
        }

        public void LoadData()
        {
            categoryLogic = new CategoryModel();
            int CountCategory = CategoryModel.TotalRecord;
            int StartRowIndex = 0;

            LoadPagingControl(CountCategory);

            StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;

            lstCategory.DataSource = categoryLogic.GetCategoryList(StartRowIndex, pagingControl.PageSize);
            lstCategory.DataBind();
        }

        public void LoadPagingControl(int Count)
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