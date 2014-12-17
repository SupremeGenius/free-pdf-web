using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace FreePDF.UserControl
{
    public partial class DataPaging : System.Web.UI.UserControl
    {
        /// <summary>
        /// Get First Page Number
        /// </summary>
        public int FirstPageNumber
        {
            get { return 1; }
        }

        /// <summary>
        /// Get Last Page Number
        /// </summary>
        public int LastPageNumber
        {
            get { return TotalPage; }
        }

        /// <summary>
        /// Get/Set Current Page
        /// </summary>        
        public int CurrentPage { get; set; }

        /// <summary>
        /// Get Next Page Number
        /// </summary>
        public int NextPage
        {
            get { return CurrentPage + 1 > TotalPage ? TotalPage : CurrentPage + 1; }
        }

        /// <summary>
        /// Get Previous Page Number
        /// </summary>
        public int PreviousPage
        {
            get { return CurrentPage - 1 <= 0 ? 1 : CurrentPage - 1; }
        }

        /// <summary>
        /// Get/Set Page Size
        /// </summary>
        [Category("Custom Setting")]
        [Description("Get/Set Page Size")]
        public int PageSize { get; set; }

        /// <summary>
        /// Get/Set Total Use To Paged
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Get Total Page
        /// </summary>
        public int TotalPage
        {            
            get
            {
                int _TotalPage = (int)Math.Ceiling((decimal)TotalRecord / (decimal)PageSize);
                return _TotalPage == 0 ? 1 : _TotalPage;
            }
        }

        /// <summary>
        /// Get/Set Target Control ID
        /// </summary>
        [Category("Custom Setting")]
        [Description("Get/Set Control ID To Paged")]
        public String TargetControlID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageNumberGenerate();
        }

        private void PageNumberGenerate()
        {
            CurrentPage = CurrentPage == 0 ? 1 : CurrentPage;
            List<int> lstPage = PageList(CurrentPage);
            foreach (int i in lstPage)
            {                
                LinkButton lnkPage = new LinkButton();
                if (i == CurrentPage)
                    lnkPage.CssClass = "activepage";
                lnkPage.Command += new CommandEventHandler(PageNumber_Command);
                lnkPage.CommandArgument = i.ToString();
                lnkPage.Text = i.ToString();                
                pageNumberHolder.Controls.Add(lnkPage);
            }
        }

        private String LinkGenerate(int PageNumber)
        {
            if (String.IsNullOrEmpty(Page.Request.Url.Query))
                return Page.Request.Url.OriginalString + "?page=" + PageNumber;

            Regex rg = new Regex(@"(page=)\w+");

            if (Page.Request.QueryString["page"] != null)
                return rg.Replace(Page.Request.Url.OriginalString, "page=" + PageNumber);            

            return Page.Request.Url.OriginalString + "&page=" + PageNumber;
        }

        /// <summary>
        /// Get Page List
        /// </summary>
        /// <param name="startPageIndex">Start Page Index</param>
        /// <returns></returns>
        public List<int> PageList(int startPageIndex)
        {
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

            //List<int> lstPage = new List<int>();
            //int firstPageIndex = 0, lastPageIndex = 0;

            //firstPageIndex = startPageIndex <= PageSize / 2 ? 1 : startPageIndex - (PageSize / 2);
            //lastPageIndex = startPageIndex <= PageSize / 2 ? PageSize : startPageIndex + (PageSize / 2) - 1;

            //for (int i = firstPageIndex; i <= lastPageIndex && i <= TotalPage; i++)
            //    lstPage.Add(i);

            //return lstPage;
        }

        protected void PageNumber_Command(object sender, CommandEventArgs e)
        {
            int PageNum = Convert.ToInt32(e.CommandArgument);

            Response.Redirect(LinkGenerate(PageNum));           
        }

        protected void lnkFirstPage_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkGenerate(FirstPageNumber));
        }

        protected void lnkPreviousPage_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkGenerate(PreviousPage));
        }

        protected void lnkNextPage_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkGenerate(NextPage));
        }

        protected void lnkLastPage_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkGenerate(LastPageNumber));
        }
    }
}