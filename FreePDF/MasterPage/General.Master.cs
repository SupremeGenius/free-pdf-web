using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using System.Text.RegularExpressions;

namespace FreePDF
{
    public partial class General : System.Web.UI.MasterPage
    {
        public delegate void SearchDelegate(IEnumerable<DocumentModel> Result);
        //public static SearchDelegate SearchAction { get; set; }

        private CategoryModel categoryLogic;
        private UsersModel userLogic;
        private TagsModel tagsLogic;
        private DocumentModel docLogic;

        public String PreviousPage
        {
            get { return Session["previousPage"].ToString(); }
            set { Session["previousPage"] = value; }
        }

        public Boolean IsLogin
        {
            get { return Session["Username"] != null; }
        }

        public Boolean SearchModuleEnabled
        {
            get { return Boolean.Parse(new PreferencesModel().GetPreferencesByName("SearchModule").Value); }
        }

        public Boolean LoginModuleEnabled
        {
            get { return Boolean.Parse(new PreferencesModel().GetPreferencesByName("LoginModule").Value); }
        }

        public Boolean MostViewMostRateModuleEnabled
        {
            get { return Boolean.Parse(new PreferencesModel().GetPreferencesByName("MostViewMostRateModule").Value); }
        }

        public Boolean RandomMostDownloadModuleEnabled
        {
            get { return Boolean.Parse(new PreferencesModel().GetPreferencesByName("RandomMostDownloadModule").Value); }
        }

        public Boolean CategoryModuleEnabled
        {
            get { return Boolean.Parse(new PreferencesModel().GetPreferencesByName("CategoryModule").Value); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowHideElement();

            PreviousPage = this.Request.RawUrl;
            String currentPage = Request.Path;            
            String[] arrPage = { "/Member/Login", "/Register" };

            if (arrPage.Contains<String>(currentPage) && IsLogin)
            {
                //String msg = @"alert('Bạn đã đăng nhập vào tài khoản và không thể thực hiện thao tác này. Hệ thống sẽ chuyển về trang chủ')";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertForm", msg, true);
                Response.Redirect("default.aspx");
            }

            if (!Page.IsPostBack)
            {
                LoadCategory();                
            }
        }

        private void LoadCategory()
        {
            categoryLogic = new CategoryModel();
            rpCategory.DataSource = categoryLogic.GetCategoryList();
            rpCategory.DataBind();
        }

        public int CategoryCount(int CategoryID)
        {
            categoryLogic = new CategoryModel();
            return categoryLogic.CountDocumentInCategory(CategoryID);
        }

        private bool IsUserAvaliable(String Username, String Password)
        {
            userLogic = new UsersModel();
            if (userLogic.IsUserAvailable(Username, Password))
            {
                var result = userLogic.SearchUserByUsername(Username, true);
                int UserID = 0;

                foreach(UsersModel u in result)
                {
                    UserID = u.UserID;
                    break;
                }

                Session["Username"] = Username;
                Session["UserID"] = UserID;
                return true;
            }

            return false;
        }

        private void ShowHideElement()
        {            
            pnlLogin.Visible = Session["Username"] == null;
            pnlUserMenu.Visible = Session["username"] != null;
        }

        public String GenerateQueryStringLink(String QueryKeyword, String QueryString)
        {
            if (String.IsNullOrEmpty(Page.Request.Url.Query))
                return String.Format("{0}?{1}={2}", Page.Request.Url.OriginalString, QueryKeyword, QueryString);

            Regex rg = new Regex("(" + QueryKeyword + @"=)\w+");

            if (Page.Request.QueryString[QueryKeyword] != null)
                return rg.Replace(Page.Request.Url.OriginalString, QueryKeyword + "=" + QueryString);

            return String.Format("{0}&{1}={2}", Page.Request.Url.OriginalString, QueryKeyword, QueryString);
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Session["UserID"] = null;
            Response.Redirect("~/default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsUserAvaliable(txtUsername.Text, txtPassword.Text))
            {
                ShowHideElement();
                if (PreviousPage != null)
                    Response.Redirect(PreviousPage);
            }
            else
            {
                blInfo.Items.Add("Vui lòng kiểm tra lại mật khẩu và tài khoản");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            String SearchText = txtSearchContent.Text;

            if (SearchText.Length < 4)
                return;

            String url = Request.Url.OriginalString;

            if (Request.Path == "/default.aspx" || Request.Path == "/" || Request.Path == "/Home")
                Response.Redirect("default.aspx?q=" + SearchText);
            else if (url.Contains("/TheLoai"))
                Response.Redirect(GenerateQueryStringLink("q", SearchText));

            //tagsLogic = new TagsModel();
            //docLogic = new DocumentModel();

            //IEnumerable<TagsModel> tagtList = tagsLogic.SearchByTagName(SearchText);
            //IEnumerable<DocumentModel> docList = docLogic.GetDocumentList();

            //IEnumerable<DocumentModel> Result = from t in tagtList
            //                                    join d in docList
            //                                    on t.DocumentID equals d.DocumentID
            //                                    select d;

            //SearchDelegate SearchAction = new SearchDelegate(new _default().SearchDocument);
            //SearchAction(Result.ToList());
        }
    }
}