using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BusinessLogicLayer;

namespace FreePDF.WebServices
{
    /// <summary>
    /// Summary description for RatingServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RatingServices : System.Web.Services.WebService
    {

        [WebMethod]
        public void SetRateValue(int DocumentID, int UserID, int RateValue)
        {
            DocumentRatingModel rating = new DocumentRatingModel();

            if (!rating.IsDocumentHasRating(DocumentID, UserID))
            {
                rating.AddRate(DocumentID, UserID, RateValue);
            }
            else
            {
                rating.UpdateRate(DocumentID, UserID, RateValue);
            }
        }
    }
}
