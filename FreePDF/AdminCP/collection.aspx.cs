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
    public partial class collection : System.Web.UI.Page
    {
        private DataPaging pagingControl;
        private CollectionModel collectionLogic;
        private UsersModel userLogic;
        private String Filter;
        private int KeyID = 0;

        public int OrderNo { get; set; }

        public int CurrentPage
        {
            get { return Convert.ToInt32(Request.QueryString["page"] ?? "1"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["filter"] != null)
            {
                Filter = Request.QueryString["filter"];
                if(Filter == "details")
                    KeyID = Int32.Parse(Request.QueryString["id"]);
            }

            if (Request.QueryString["removeid"] != null)
            {
                int Key = Convert.ToInt32(Request.QueryString["removeid"]);

                collectionLogic = new CollectionModel();
                collectionLogic.RemoveCollection(Key);
                Response.Redirect(Session["PreviousPage"].ToString());
            }

            Session["PreviousPage"] = Request.Url.OriginalString;

            LoadData();
        }

        private void LoadData()
        {
            collectionLogic = new CollectionModel();

            if (Filter == "details")
            {
                pnlDetails.Visible = true;
                CollectionDetails(KeyID);

                return;
            }
            //else
            pnlView.Visible = true;
            if (Filter == "haserror")
                LoadCollectionHasError();
            else
                LoadAllCollection();
        }

        private void CollectionDetails(int CollectionID)
        {
            collectionLogic = collectionLogic.GetCollectionById(CollectionID);

            List<CollectionModel> collectionDetails = new List<CollectionModel>();
            collectionDetails.Add(collectionLogic);

            dvCollection.DataSource = collectionDetails;
            dvCollection.DataBind();
        }

        private void LoadAllCollection()
        {
            collectionLogic = new CollectionModel();
            int CountCollection = CollectionModel.TotalRecord;
            int StartRowIndex = 0;

            LoadPagingControl(CountCollection);

            StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;

            lstCollection.DataSource = collectionLogic.GetCollectionList(StartRowIndex, pagingControl.PageSize).ToList();
            lstCollection.DataBind();
        }

        private void LoadCollectionHasError()
        {
            int CountCollection = collectionLogic.GetCollectionHasError().Count();
            int StartRowIndex = 0;

            LoadPagingControl(CountCollection);

            StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;

            lstCollection.DataSource = collectionLogic.GetCollectionHasError().Skip(StartRowIndex).Take(pagingControl.PageSize).ToList();
            lstCollection.DataBind();
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

        public String GetUsername(int UserID)
        {
            userLogic = new UsersModel();
            return userLogic.GetUserById(UserID).Username;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int SearchType = ddlSearchType.SelectedIndex + 1;
            int Count = 0;
            int StartRowIndex = 0;
            String SearchText = txtSearch.Text;

            if (SearchText.Length >= 5)
            {
                collectionLogic = new CollectionModel();

                switch (SearchType)
                {
                    case 1:
                        Count = collectionLogic.SearchCollectionByName(SearchText, false).Count();
                        LoadPagingControl(Count);
                        StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;
                        lstCollection.DataSource = collectionLogic.SearchCollectionByName(SearchText, false).Skip(StartRowIndex).Take(pagingControl.PageSize).ToList();
                        break;
                    case 2:
                        Count = collectionLogic.SearchCollectionByUsername(SearchText).Count();
                        LoadPagingControl(Count);
                        StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;
                        lstCollection.DataSource = collectionLogic.SearchCollectionByUsername(SearchText).Skip(StartRowIndex).Take(pagingControl.PageSize);
                        break;
                }

                lstCollection.DataBind();
            }
        }
    }
}