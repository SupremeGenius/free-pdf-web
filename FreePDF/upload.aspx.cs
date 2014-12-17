using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConvertLetterAccent;
using BusinessLogicLayer;

namespace FreePDF
{
    public partial class upload : System.Web.UI.Page
    {
        private CategoryModel categoryLogic;
        private CollectionModel collectionLogic;
        private DocumentModel docLogic;
        private TagsModel tagsLogic;
        private PreferencesModel prefencesLogic;
        private ConvertLetter cvLetter;
        private String FilePath, FileName, ThumbnailPath, ThumbnailFileName;
        private int FileSize = 0, UserID, MaxFileSize;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
                Response.Redirect("default.aspx");

            UserID = Convert.ToInt32(Session["UserID"]);

            if (!Page.IsPostBack)
            {
                LoadCategory();
                LoadCollection();
            }
        }

        private void LoadCategory()
        {
            categoryLogic = new CategoryModel();
            ddlCategory.DataSource = categoryLogic.GetCategoryList();
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
        }

        private void LoadCollection()
        {
            collectionLogic = new CollectionModel();
            ddlCollection.DataSource = collectionLogic.GetCollectionByUserId(UserID);
            ddlCollection.DataTextField = "Name";
            ddlCollection.DataValueField = "CollectionID";
            ddlCollection.DataBind();

            if (ddlCollection.Items.Count == 0)
                ddlCollection.Enabled = false;
        }  

        private bool IsPDFFile(String fileName)
        {
            return System.IO.Path.GetExtension(fileName).ToLower() == ".pdf";
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (fileUpload.HasFile && IsPDFFile(fileUpload.FileName))
                    {
                        prefencesLogic = new PreferencesModel();
                        cvLetter = new ConvertLetter();
                        FileName = cvLetter.ClearAccent(fileUpload.FileName).ToLower().Replace(' ', '_');
                        FilePath = prefencesLogic.GetPreferencesByName("FileServer").Value;
                        FileSize = fileUpload.PostedFile.ContentLength;
                        MaxFileSize = 50 * 1024 * 1024;

                        //Upload Thumbnails
                        if (thumbUpload.HasFile)
                        {
                            String FileExtension = System.IO.Path.GetExtension(thumbUpload.FileName).ToLower();
                            ThumbnailFileName = System.IO.Path.GetRandomFileName();
                            ThumbnailPath = prefencesLogic.GetPreferencesByName("ThumbnailPath").Value;

                            thumbUpload.SaveAs(MapPath(new Uri(ThumbnailPath).AbsolutePath + "/" + ThumbnailFileName + FileExtension));
                        }

                        //Upload Document
                        if (FileSize < MaxFileSize)
                        {
                            fileUpload.SaveAs(MapPath(new Uri(FilePath + @"/" + FileName).AbsolutePath));

                            docLogic = new DocumentModel();
                            collectionLogic = new CollectionModel();
                            tagsLogic = new TagsModel();                            

                            int addedCollectionID = 0;
                            int addedDocumentID = 0;

                            //Add Document
                            if (String.IsNullOrEmpty(txtCollectionName.Text))
                            {
                                addedDocumentID = docLogic.AddDocument(
                                    txtDocumentName.Text, txtDescription.Text, ThumbnailFileName, FileName, FileSize,
                                    UserID, Int32.Parse(ddlCategory.SelectedValue),
                                    ddlCollection.Items.Count != 0 ? Int32.Parse(ddlCollection.SelectedValue) : 0
                                );
                            }
                            else
                            {
                                addedCollectionID = collectionLogic.AddCollection(txtCollectionName.Text, String.Empty, UserID);

                                addedDocumentID = docLogic.AddDocument(txtDocumentName.Text, txtDescription.Text, ThumbnailFileName, FileName, FileSize, UserID, Int32.Parse(ddlCategory.SelectedValue), addedCollectionID);
                            }

                            //Add Tags
                            if (!String.IsNullOrEmpty(txtTags.Text))
                            {
                                tagsLogic.AddTag(txtTags.Text, addedDocumentID, null);
                            }
                            else
                            {
                                String tmpTags = txtDocumentName.Text.Replace(" ", ",");
                                tagsLogic.AddTag(tmpTags, addedDocumentID, null);
                            }
                               
                            blInfo.Items.Add("Thêm tài liệu thành công");
                        }
                        else
                        {                            
                            blInfo.Items.Add("Dung lượng file cho phép không quá 50MB");
                        }
                    }
                    else
                    {                        
                        blInfo.Items.Add("Sai định dạng file. Chỉ cho phép định dạng PDF");
                    }
                }
            }
            catch (ApplicationException ex)
            {
                blInfo.Items.Clear();
                blInfo.Items.Add(ex.Message);
            }
        }
    }
}