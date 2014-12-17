using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace FreePDF
{
    public partial class document : System.Web.UI.Page
    {
        private DocumentModel docLogic;
        private CommentModel commentLogic;

        public String DocAlias
        {
            get { return RouteData.Values["DocAlias"].ToString(); }
        }

        public int DocID
        {
            get { return Convert.ToInt32(DocAlias.Split('-')[0]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDocumentInfo(DocID);
                LoadComment(DocID);
            }            
        }

        public void LoadDocumentInfo(int DocID)
        {
            docLogic = new DocumentModel();

            List<DocumentModel> documentDetails = new List<DocumentModel>();
            documentDetails.Add(docLogic.GetDocumentById(DocID));

            DocInfo.DataSource = documentDetails;
            DocInfo.DataBind();
        }

        public void LoadComment(int DocumentID)
        {
            commentLogic = new CommentModel();

            rpComment.DataSource = commentLogic.GetCommentByDocumentID(DocumentID);
            rpComment.DataBind();
        }

        public String GetThumbnailsPath()
        {
            return new PreferencesModel().GetPreferencesByName("ThumbnailPath").Value;
        }

        public String GetUsername(int UserID)
        {            
            return new UsersModel().GetUserById(UserID).Username;
        }

        public String GetCategoryName(int CategoryID)
        {
            return new CategoryModel().GetCategoryById(CategoryID).Name;
        }

        public String GetCollectionName(int CollectionID)
        {
            if (CollectionID == 0)
                return "N/A";

            return new CollectionModel().GetCollectionById(CollectionID).Name;
        }

        public int CommentCount()
        {
            return new CommentModel().GetCommentByDocumentID(DocID).Count();
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            if (txtCommentContent.Text.Length > 10)
            {
                commentLogic = new CommentModel();
                int AddedCommentID = commentLogic.AddComment(Convert.ToInt32(Session["UserID"]), DocID, 0, txtCommentContent.Text);

                if (AddedCommentID != 0)
                {
                    blInfo.Items.Add("Thêm bình luận thành công");
                    LoadComment(DocID);
                    txtCommentContent.Text = "";
                }
                else
                    blInfo.Items.Add("Có lỗi trong quá trình xử lí. Vui lòng thử lại sau");
            }
            else
            {
                blInfo.Items.Add("Bình luận tối thiểu 10 kí tự");
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }

        protected void btnReportError_Click(object sender, EventArgs e)
        {
            docLogic = new DocumentModel();
            docLogic.DocumentReportError(DocID);

            blMessage.Items.Add("Thao tác thành công. BQT sẽ khắc phục lỗi trong thời gian sớm nhất. Cảm ơn bạn đã quan tâm.");
            
        }
    }
}