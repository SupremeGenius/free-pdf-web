using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BusinessLogicLayer;

namespace FreePDF.WebServices
{
    /// <summary>
    /// Summary description for UserProcessSv
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserProcessSv : System.Web.Services.WebService
    {
        private UsersBLL userLogic = null;

        [WebMethod (EnableSession=true)]
        public bool IsUserAvaliable(String Username, String Password)
        {
            userLogic = new UsersBLL();

            if (userLogic.IsUserAvailable(Username, Password))
            {
                String UserID = userLogic.SearchUserByUsername(Username, true)[0].UserID.ToString();
                Session["Username"] = Username;
                Session["UserID"] = UserID;
                return true;
            }
            
            return false;
        }

        [WebMethod(EnableSession = true)]
        public bool UserLogout()
        {
            Session["Username"] = null;
            Session["UserID"] = null;
            
            return true;
        }

        [WebMethod(EnableSession = true)]
        public bool IsUserLogin()
        {
            return Session["Username"] != null;
        }
    }
}
