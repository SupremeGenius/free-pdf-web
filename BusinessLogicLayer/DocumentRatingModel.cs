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
    [Table(Name="DocumentRating")]
    public class DocumentRatingModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int RateID { get; set; }

        [Column(DbType="INT", CanBeNull=false)]
        public int DocumentID { get; set; }

        [Column(DbType = "INT", CanBeNull = false)]
        public int UserID { get; set; }

        [Column(DbType = "INT", CanBeNull = false)]
        public int Rate { get; set; }

        public int DocumentTotalRate(int DocumentID)
        {
            return LinqAdapter.GetTable<DocumentRatingModel>().Where(r => r.DocumentID == DocumentID).Count();
        }

        public double DocumentAverageRate(int DocumentID)
        {
            IEnumerable<DocumentRatingModel> documentRate = LinqAdapter.GetTable<DocumentRatingModel>().Where(r => r.DocumentID == DocumentID).ToList();
            dynamic documentVotes = documentRate.Select(r => new { r.Rate, Votes = 1 }).GroupBy(r => r.Rate).Count();

            return 0.0;
        }

        public bool IsDocumentHasRating(int DocumentID, int UserID)
        {
            return LinqAdapter.GetTable<DocumentRatingModel>().Where(r => r.UserID == UserID && r.DocumentID == DocumentID).Count() != 0;
        }

        public int AddRate(int DocumentID, int UserID, int Rate)
        {
            DocumentRatingModel documentRatingToInsert = new DocumentRatingModel();
            documentRatingToInsert.DocumentID = DocumentID;
            documentRatingToInsert.UserID = UserID;
            documentRatingToInsert.Rate = Rate;

            LinqAdapter.GetTable<DocumentRatingModel>().InsertOnSubmit(documentRatingToInsert);
            LinqAdapter.SubmitChanges();

            return documentRatingToInsert.RateID;
        }

        public void UpdateRate(int DocumentID, int UserID, int Rate)
        {
            DocumentRatingModel documentRatingToUpdate = LinqAdapter.GetTable<DocumentRatingModel>().Single(r => r.DocumentID == DocumentID && r.UserID == UserID);
            documentRatingToUpdate.Rate = Rate;

            LinqAdapter.SubmitChanges();
        }
    }
}
