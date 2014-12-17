using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using System.Collections;

namespace FreePDF
{
    public partial class category : System.Web.UI.Page
    {
        private DocumentModel docLogic;
        private UsersModel userLogic;
        private CategoryModel categoryLogic;

        public String CategoryAlias
        {
            get { return RouteData.Values["Alias"].ToString(); }
        }

        public int CurrentPage
        {
            get { return Convert.ToInt32(Request.QueryString["page"]) == 0 ? 1 : Convert.ToInt32(Request.QueryString["page"]); }
        }

        public int CategoryID
        {
            get
            {
                categoryLogic = new CategoryModel();
                return categoryLogic.GetCategoryNameByAlias(CategoryAlias).CategoryID; 
            }
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDocument();
            }
            catch (IndexOutOfRangeException ex)
            {
                ErrorLoging.WriteLog(String.Format("Message: {0}\nTarget: {1}\nDetails: {2}", ex.Message, ex.TargetSite, ex.StackTrace), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (NullReferenceException ex)
            {
                ErrorLoging.WriteLog(String.Format("Message: {0}\nTarget: {1}\nDetails: {2}", ex.Message, ex.TargetSite, ex.StackTrace), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                ErrorLoging.WriteLog(String.Format("Message: {0}\nTarget: {1}\nDetails: {2}", ex.Message, ex.TargetSite, ex.StackTrace), System.Diagnostics.EventLogEntryType.Error);
                Response.Redirect("error.aspx");
            }
        }

        private void LoadDocument()
        {
            categoryLogic = new CategoryModel();

            dataPaging.CurrentPage = CurrentPage;
            dataPaging.TotalRecord = categoryLogic.CountDocumentInCategory(CategoryID);

            int StartRowIndex = (CurrentPage * dataPaging.PageSize) - (dataPaging.PageSize - 1) - 1;
            int Amount = dataPaging.PageSize;

            CacheProcess cacheProcess = new CacheProcess();

            String CacheName = CategoryAlias + "_CacheData";
            bool IsCurrentCategoryCached = Cache[String.Format("{0}_Part{1}", CacheName, CurrentPage)] != null;

            Hashtable lstCachedData = cacheProcess.FragmentDataCache("BusinessLogicLayer.DocumentModel", CacheName, 15, 
                CurrentPage, StartRowIndex, Amount, dataPaging.TotalPage, 
                "GetDocumentByCategoryID", new Object[] { CategoryID }, IsCurrentCategoryCached);

            lstDocument.DataSource = lstCachedData[String.Format("{0}_Part{1}", CacheName, CurrentPage)];
            lstDocument.DataBind();
        }

        public String GetThumbnailsPath()
        {
            return new PreferencesModel().GetPreferencesByName("ThumbnailPath").Value;
        }

        public String GetUsername(int UserID)
        {
            return new UsersModel().GetUserById(UserID).Username;
        }

        public string GetCategoryName(int CategoryID)
        {
            return new CategoryModel().GetCategoryById(CategoryID).Name;
        }
    }
}