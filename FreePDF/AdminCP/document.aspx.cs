using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FreePDF.UserControl;
using BusinessLogicLayer;

namespace FreePDF.AdminCP
{
    public partial class document : System.Web.UI.Page
    {
        private DataPaging pagingControl;
        private DocumentModel docLogic;
        private UsersModel userLogic;
        private CategoryModel categoryLogic;
        private CollectionModel collectionLogic;
        private String Filter = String.Empty;
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
                if (Filter == "details")
                    KeyID = Int32.Parse(Request.QueryString["id"]);
            }
            
            if (Request.QueryString["removeid"] != null)
            {
                int Key = Convert.ToInt32(Request.QueryString["removeid"]);

                docLogic = new DocumentModel();
                docLogic.RemoveDocument(Key);
                Response.Redirect(Session["PreviousPage"].ToString());
            }

            Session["PreviousPage"] = Request.Url.OriginalString;
                        
            LoadData();
        }
                
        private void LoadData()
        {
            docLogic = new DocumentModel();
            categoryLogic = new CategoryModel();

            if (Filter == "details")
            {
                pnlDetails.Visible = true;
                DocumentDetails(KeyID);

                return;
            }
            //else
            pnlView.Visible = true;

            LoadCategory();

            if (Filter == "haserror")
                LoadDocumentHasError();
            else
                LoadAllDocument();
        }

        private void LoadCategory()
        {
            categoryLogic = new CategoryModel();
            if (!Page.IsPostBack)
            {
                ddlFilter.DataSource = categoryLogic.GetCategoryList();
                ddlFilter.DataTextField = "Name";
                ddlFilter.DataValueField = "CategoryID";
                ddlFilter.DataBind();
            }
        }

        private void LoadDocumentHasError()
        {
            docLogic = new DocumentModel();
            int CountDocument = docLogic.GetDocumentHasError().Count();
            int StartRowIndex = 0;

            LoadPagingControl(CountDocument);

            StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;

            lstDocument.DataSource = docLogic.GetDocumentHasError().Skip(StartRowIndex).Take(pagingControl.PageSize).ToList();
            lstDocument.DataBind();
        }

        private void LoadAllDocument()
        {
            docLogic = new DocumentModel();
            int CountDocument = DocumentModel.TotalRecord;
            int StartRowIndex = 0;

            LoadPagingControl(CountDocument);

            StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;

            lstDocument.DataSource = docLogic.GetDocumentListPartByPart(StartRowIndex, pagingControl.PageSize);
            lstDocument.DataBind();
        }

        private void DocumentDetails(int DocumentID)
        {
            docLogic = new DocumentModel();
            docLogic = docLogic.GetDocumentById(DocumentID);

            List<DocumentModel> documentDetails = new List<DocumentModel>();
            documentDetails.Add(docLogic);

            dvDocument.DataSource = documentDetails;
            dvDocument.DataBind();
        }

        private void LoadPagingControl(int Count)
        {            
            pagingControl = Page.LoadControl("~/UserControl/DataPaging.ascx") as DataPaging;
            pagingControl.PageSize = 5;
            pagingControl.CurrentPage = CurrentPage;
            pagingControl.TotalRecord = Count;
            if(Count != 0)
                pageHolder.Controls.Add(pagingControl);            
        }

        public String GetCategoryName(int CategoryID)
        {
            categoryLogic = new CategoryModel();
            return categoryLogic.GetCategoryById(CategoryID).Name;
        }

        public String GetUsername(int UserID)
        {
            userLogic = new UsersModel();
            return userLogic.GetUserById(UserID).Username;
        }

        public String GetCollectionName(int CollectionID)
        {
            collectionLogic = new CollectionModel();
            return collectionLogic.GetCollectionById(CollectionID).Name;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int SearchType = ddlSearchType.SelectedIndex + 1;
            int Count = 0;
            int StartRowIndex = 0;
            String SearchText = txtSearch.Text;

            if (SearchText.Length >= 5)
            {
                docLogic = new DocumentModel();

                switch (SearchType)
                {
                    case 1:
                        Count = docLogic.SearchDocumentByName(SearchText).Count();
                        LoadPagingControl(Count);
                        StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;
                        lstDocument.DataSource = docLogic.SearchDocumentByName(SearchText).Skip(StartRowIndex).Take(pagingControl.PageSize).ToList();
                        break;
                    case 2:
                        Count = docLogic.SearchDocumentByUsername(SearchText).Count();
                        LoadPagingControl(Count);
                        StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;
                        lstDocument.DataSource = docLogic.SearchDocumentByUsername(SearchText).Skip(StartRowIndex).Take(pagingControl.PageSize).ToList();
                        break;
                    case 3:
                        Count = docLogic.SearchDocumentByCollectionName(SearchText).Count();
                        LoadPagingControl(Count);
                        StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;
                        lstDocument.DataSource = docLogic.SearchDocumentByCollectionName(SearchText).Skip(StartRowIndex).Take(pagingControl.PageSize).ToList();
                        break;
                }

                lstDocument.DataBind();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            int CategoryID = Convert.ToInt32(ddlFilter.SelectedValue);
            int CountDocumentInCategory = docLogic.GetDocumentByCategoryID(CategoryID).Count();

            if (pagingControl != null)
                pageHolder.Controls.Clear();

            LoadPagingControl(CountDocumentInCategory);

            int StartRowIndex = (CurrentPage * pagingControl.PageSize) - (pagingControl.PageSize - 1) - 1;

            if(Filter == "haserror")
                lstDocument.DataSource = docLogic.GetDocumentByCategoryID(CategoryID).Where(d => d.IsError == true).Skip(StartRowIndex).Take(pagingControl.PageSize).ToList();
            else
                lstDocument.DataSource = docLogic.GetDocumentByCategoryID(CategoryID).Skip(StartRowIndex).Take(pagingControl.PageSize).ToList();
            lstDocument.DataBind();
        }
    }
}