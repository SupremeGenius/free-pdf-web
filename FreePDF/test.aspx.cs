using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using BusinessLogicLayer;
using System.Text.RegularExpressions;

namespace FreePDF
{
    public class Employee
    {
        public int EmpID { get; set; }
        public String Name { get; set; }
    }

    public partial class test : System.Web.UI.Page
    {
        public int CurrentPage
        {
            get { return Convert.ToInt32(Request.QueryString["page"]) == 0 ? 1 : Convert.ToInt32(Request.QueryString["page"]); }
        }

        public void Testing(Type t, object o)
        {
            System.Reflection.MemberInfo medInfo = t.GetMethod("GetDocumentById");
            object ob = t.GetMethod("GetDocumentById").Invoke(o, new object[] { 2 });

            //IEnumerable<DocumentModel> d = (IEnumerable<DocumentModel>)t.GetMethod("GetDocumentById").Invoke(this, new Object[] { 1 });
        }

        public dynamic CallMethodByText(dynamic TypeObject, String MethodName, Object[] ParamList)
        {
            Type type = TypeObject.GetType();            
            return type.GetMethod(MethodName).Invoke(TypeObject, ParamList);
        }

        public dynamic CallMethodByString(String TypeName, String MethodName, Object[] ParameterList = null)
        {
            Type type = Type.GetType("BusinessLogicLayer.DocumentModel, BusinessLogicLayer");
            Object typeObj = Activator.CreateInstance(type);

            return type.GetMethod(MethodName).Invoke(typeObj, ParameterList);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //DocumentRatingModel rating = new DocumentRatingModel();
            //rating.DocumentAverageRate(2);

            ErrorLoging.WriteLog("test phat", System.Diagnostics.EventLogEntryType.Information);

            //DocumentModel docModel = new DocumentModel();
            //CallMethodByText(docModel, "GetDocumentById", new object[] { 2 });
            //Testing(docModel.GetType(), docModel);

            //dynamic dym = CallMethodByString("BusinessLogicLayer.DocumentModel", "GetDocumentList");

            //String path = new Uri("http://localhost:4358/Upload").AbsolutePath;
            HumanResource dataObject = new HumanResource();
            
            dataPaging.CurrentPage = CurrentPage;
            dataPaging.TotalRecord = HumanResource.TotalRecord;

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
            //        lstCachedData = new IEnumerable<HumanResource>[dataPaging.TotalPage];
            //    lstCachedData[CurrentPage - 1] = dataObject.GetEmployeeList(StartRowIndex, Amount);
            //    Cache.Add("tmpData", lstCachedData, null, DateTime.Now.AddMinutes(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
            //    Trace.Write("Get data from server");
            //}
            //else
            //{
            //    Trace.Write("Get data from cache");
            //}
            IEnumerable<dynamic>[] lstCachedData = new CacheProcess().SuperCacheData("BusinessLogicLayer.HumanResource", "humanData", 5, CurrentPage, StartRowIndex, Amount, dataPaging.PageSize, "GetEmployeeList");
            lstTest.DataSource = lstCachedData[CurrentPage - 1];
            lstTest.DataBind();

            //HumanResource h = new HumanResource();
            //h.GetEmployeeById(774);

            //CategoryModel c = new CategoryModel();
            //int count = c.CountDocumentInCategory(1);
            //PageListTest(1);
            Regex rg = new Regex(@"(page=)\d{1,9}");
            String s = rg.Replace(Request.Url.OriginalString, "page=4");
            
        }

        public int GetStartPageIndex(int CurrentPage, int PageSize)
        {
            return (CurrentPage * PageSize) - (PageSize - 1) - 1;
        }

        public List<int> PageList(int startPageIndex)
        {
            int TotalPage = 23;
            int Lower, Upper, AVG;
            int Remainder;
            List<int> lstPageNum = new List<int>();

            if (startPageIndex == 1)
                startPageIndex++;
            else if (startPageIndex == TotalPage)
                startPageIndex--;

            Lower = startPageIndex - 4 > 0 ? startPageIndex - 4 : 1;
            Upper = startPageIndex + 4 > TotalPage ? TotalPage : startPageIndex + 4;

            AVG = (Upper - Lower) / 2;

            while (Upper - startPageIndex != AVG)
            {                
                Upper = startPageIndex + AVG;

                if (Upper > TotalPage)
                {
                    Lower++;
                    Upper = TotalPage;
                    break;
                }
            }

            while(startPageIndex - Lower != AVG)
            {
                if (startPageIndex - AVG > 0)
                    Lower = startPageIndex - AVG;
                else
                {
                    Lower = 1;
                    break;
                }                
            }

            Remainder = ((startPageIndex - Lower) - (Upper - startPageIndex)) > 1 ? 1 : ((startPageIndex - Lower) - (Upper - startPageIndex));
            Lower += Remainder;

            if (Lower == 0)
            {
                Lower = 1;
                Upper--;
            }

            for (int i = Lower; i <= Upper; i++)
            {               
                lstPageNum.Add(i);
            }

            return lstPageNum;
        }

        public List<int> PageListTest(int startPageIndex)
        {            
            int TotalPage = 10;
            int Lower, Upper;
            List<int> lstPageNum = new List<int>();

            Lower = startPageIndex - 4 > 0 ? startPageIndex - 4 : 1;
            Upper = startPageIndex + 4 > TotalPage ? TotalPage : startPageIndex + 4;

            if (startPageIndex <= 5)
            {
                Lower = 1;
                Upper = 9;

                if (Upper > TotalPage)
                    Upper = TotalPage;
            }           

            do
            {
                if (Lower == Upper)
                {
                    lstPageNum.Add(Upper);
                    break;
                }
                lstPageNum.Add(Lower);
                lstPageNum.Add(Upper);
                Lower++; Upper--;
            } while (Lower <= Upper);

            lstPageNum.Sort();

            return lstPageNum;
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            
        }
    }
}