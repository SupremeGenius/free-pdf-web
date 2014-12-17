using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient;

namespace BusinessLogicLayer
{
    [Table(Name="Comment")]
    public class CommentModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int CommentID { get; set; }

        [Column(DbType="INT", CanBeNull=false)]
        public int UserID { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public Nullable<int> DocumentID { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public Nullable<int> CollectionID { get; set; }

        [Column(DbType = "NVARCHAR(800)", CanBeNull = false)]
        public String CommentContent { get; set; }

        public static int TotalRecord
        {
            get { return LinqAdapter.GetTable<CommentModel>().Count(); }
        }

        public IEnumerable<CommentModel> GetCommentList()
        {
            return LinqAdapter.GetTable<CommentModel>().ToList();
        }

        public IEnumerable<CommentModel> GetCommentList(int startRowIndex, int maximumRows)
        {
            return LinqAdapter.GetTable<CommentModel>().Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public CommentModel GetCommentById(int CommentID)
        {
            return LinqAdapter.GetTable<CommentModel>().SingleOrDefault(cm => cm.CommentID == CommentID);
        }

        public IEnumerable<CommentModel> GetCommentByDocumentID(int DocumentID)
        {
            return LinqAdapter.GetTable<CommentModel>().Where(cm => cm.DocumentID == DocumentID).ToList();
        }

        public int AddComment(int UserID, Nullable<int> DocumentID, Nullable<int> CollectionID, String CommentContent)
        {
            CommentModel commentToInsert = new CommentModel();
            commentToInsert.UserID = UserID;
            if (DocumentID == 0)
                commentToInsert.DocumentID = null;
            else
                commentToInsert.DocumentID = DocumentID;
            if (CollectionID == 0)
                commentToInsert.CollectionID = null;
            else
                commentToInsert.CollectionID = CollectionID;
            commentToInsert.CommentContent = CommentContent;

            LinqAdapter.GetTable<CommentModel>().InsertOnSubmit(commentToInsert);
            LinqAdapter.SubmitChanges();

            return commentToInsert.CommentID;
        }
    }
}
