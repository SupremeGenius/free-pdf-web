using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DatabaseAccessLayer;

namespace BusinessLogicLayer
{
    public class RequestBLL
    {
        public int RequestID { get; set; }
	    public int UserID { get; set; }
	    public String RequestContent { get; set; }
	    public DateTime RequestDate { get; set; }	

	    public RequestBLL() { }

        public RequestBLL(int RequestID, int UserID, String RequestContent, DateTime RequestDate)
	    {
		    this.RequestID = RequestID;
		    this.UserID = UserID;
		    this.RequestContent = RequestContent;
		    this.RequestDate = RequestDate;
	    }

        public List<RequestBLL> GetRequestList()
	    {
            List<RequestBLL> requestLogic = new List<RequestBLL>();	

		    DataSet requestDS = DAL.CallProcedureReturnDataset("GetRequestList");
		    DataTable requestTable = requestDS.Tables[0];
		    DataRowCollection requestRow = requestTable.Rows;		

		    foreach(DataRow row in requestRow)
		    {
                requestLogic.Add(new RequestBLL(
				    Int32.Parse(row["RequestID"].ToString()),
				    Int32.Parse(row["UserID"].ToString()),
				    row["RequestContent"].ToString(),
				    DateTime.Parse(row["RequestDate"].ToString())
			    ));
		    }		

		    return requestLogic;
	    }

        public RequestBLL GetRequestById(int RequestID)
	    {
            RequestBLL requestLogic;

		    DataSet requestDS = DAL.CallProcedureReturnDataset("GetRequestById", "@requestid=" + RequestID);
		    DataTable requestTable = requestDS.Tables[0];
		    DataRow requestRow = requestTable.Rows[0];

            requestLogic = new RequestBLL(Int32.Parse(requestRow["RequestID"].ToString()),
            Int32.Parse(requestRow["UserID"].ToString()),
            requestRow["RequestContent"].ToString(),
            DateTime.Parse(requestRow["RequestDate"].ToString()));

		    return requestLogic;
	    }	

	    public bool AddRequest(String Content, int UserID)
	    {
		    int rowAffected = DAL.CallUpdateProcedure("AddRequest", "@userid=" + UserID, "@content=" + Content);

		    return rowAffected == 1;
	    }	

	    public bool RemoveRequest(int RequestID)
	    {
		    int rowAffected = DAL.CallUpdateProcedure("RemoveRequest", "@requestid=" + RequestID);

		    return rowAffected == 1;
	    }
    }
}
