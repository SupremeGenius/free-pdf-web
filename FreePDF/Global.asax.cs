using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.IO;
using BusinessLogicLayer;

namespace FreePDF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            InittializeRoutes(RouteTable.Routes);            
            SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);            
        }

        private SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            if (SiteMap.CurrentNode == null)
            {
                String url = e.Context.Request.RawUrl;

                if(url.Contains("TheLoai/"))
                {
                    CategoryModel categoryLogic = new CategoryModel();
                    String parentNodeTitle = "Thể Loại";
                    String subNodeAlias = url.Substring(url.IndexOf("TheLoai") + 8);
                    String subNodeTitle = categoryLogic.GetCategoryNameByAlias(subNodeAlias).Name;
                    
                    SiteMapNode parentNode = new SiteMapNode(e.Provider, url.Substring(0, 8), null, parentNodeTitle);
                    SiteMapNode childNode = new SiteMapNode(e.Provider, url, null, subNodeTitle);
                    parentNode.ParentNode = SiteMap.RootNode;
                    childNode.ParentNode = parentNode;

                    return childNode;
                }
                else if (url.Contains("TaiLieu/"))
                {
                    DocumentModel docLogic = new DocumentModel();
                    String parentNodeTitle = "Tài Liệu";
                    String[] DocFullName = url.Substring(url.IndexOf("TaiLieu") + 8).Split('-');
                    String subNodeAlias = DocFullName[1];
                    String subNodeTitle = docLogic.GetDocumentById(Int32.Parse(DocFullName[0])).Name;

                    SiteMapNode parentNode = new SiteMapNode(e.Provider, url.Substring(0, 8), null, parentNodeTitle);
                    SiteMapNode childNode = new SiteMapNode(e.Provider, url, null, subNodeTitle);
                    parentNode.ParentNode = SiteMap.RootNode;
                    childNode.ParentNode = parentNode;

                    return childNode;
                }

                bool IsHomePage = url.Contains("default.aspx") || url == "/";

                if (IsHomePage)
                {
                    SiteMapNode node = new SiteMapNode(e.Provider, url, url, "Trang Chủ");
                    return node;
                }
            }
            return SiteMap.CurrentNode;
        }

        private void InittializeRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("category", "TheLoai/{Alias}", "~/category.aspx");            
            routes.MapPageRoute("default", "Home", "~/default.aspx");
            routes.MapPageRoute("login", "Member/Login", "~/login.aspx");
            routes.MapPageRoute("register", "Register", "~/register.aspx");
            routes.MapPageRoute("contactus", "ContactUs", "~/contactus.aspx");
            routes.MapPageRoute("upload", "Member/Upload", "~/upload.aspx");
            routes.MapPageRoute("document", "TaiLieu/{DocAlias}", "~/document.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }        
    }
}