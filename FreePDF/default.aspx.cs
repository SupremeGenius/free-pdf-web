using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace FreePDF
{
    public partial class _default : System.Web.UI.Page
    {
        private DocumentModel docLogic;
        private UsersModel userLogic;
        private CategoryModel categoryLogic;
        private TagsModel tagsLogic;

        public int CurrentPage
        {
            get { return Convert.ToInt32(Request.QueryString["page"] ?? "1"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(Request.QueryString["q"]))
                    LoadDocument();
                else
                    SearchDocumentByTagName();
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
            //docLogic = new DocumentModel();
            //categoryLogic = new CategoryModel();
            //IEnumerable<DocumentModel>[] lstCachedData = (IEnumerable<DocumentModel>[])Cache["documentData"];

            dataPaging.CurrentPage = CurrentPage;
            dataPaging.TotalRecord = DocumentModel.TotalRecord;

            int StartRowIndex = (CurrentPage * dataPaging.PageSize) - (dataPaging.PageSize - 1) - 1;
            int Amount = dataPaging.PageSize;
            //int TotalCached = 0;
            //int CacheDuration = 15;

            //bool IsCacheComplete = TotalCached == dataPaging.TotalPage;
            //bool IsCurrentPageCached = lstCachedData == null ? false : lstCachedData[CurrentPage - 1] != null;
            //bool IsCachedListEmpty = lstCachedData == null;

            //int i = 0;
            //while (!IsCachedListEmpty && i < lstCachedData.Length && lstCachedData[i] != null)
            //{
            //    TotalCached++;
            //    i++;
            //}
                        
            //if ((IsCachedListEmpty || (!IsCurrentPageCached && !IsCacheComplete)))
            //{
            //    if (IsCachedListEmpty)
            //        lstCachedData = new IEnumerable<DocumentModel>[dataPaging.TotalPage];
            //    lstCachedData[CurrentPage - 1] = docLogic.GetDocumentList(StartRowIndex, Amount);
            //    Cache.Add("documentData", lstCachedData, null, DateTime.Now.AddMinutes(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);                
            //}

            CacheProcess cacheProcess = new CacheProcess();

            //IEnumerable<dynamic>[] lstCachedData = cacheProcess.SuperCacheData("BusinessLogicLayer.DocumentModel", "documentData", 15, CurrentPage, StartRowIndex, Amount, dataPaging.TotalPage, "GetDocumentList"); 
            Hashtable lstCachedData = cacheProcess.FragmentDataCache("BusinessLogicLayer.DocumentModel", "documentData", 15, CurrentPage, StartRowIndex, Amount, dataPaging.TotalPage, "GetDocumentList");

            lstDocument.DataSource = lstCachedData[String.Format("{0}_Part{1}", "documentData", CurrentPage)];
            lstDocument.DataBind();
        }  

        public void SearchDocumentByTagName()
        {
            String TagName = Request.QueryString["q"].ToString();

            tagsLogic = new TagsModel();
            docLogic = new DocumentModel();

            IEnumerable<TagsModel> tagtList = tagsLogic.SearchByTagName(TagName);
            IEnumerable<DocumentModel> docList = docLogic.GetDocumentList();

            IEnumerable<DocumentModel> Result = from t in tagtList
                                                join d in docList
                                                on t.DocumentID equals d.DocumentID
                                                select d;

            lstDocument.DataSource = Result;
            lstDocument.DataBind();
        }

        public void SearchDocument(IEnumerable<DocumentModel> Result)
        {
            //
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