using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient;
using ConvertLetterAccent;

namespace BusinessLogicLayer
{
    [Table(Name="Tags")]
    public class TagsModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int TagID { get; set; }

        [Column(DbType="INT", CanBeNull=true)]
        public Nullable<int> DocumentID { get; set; }

        [Column(DbType = "INT", CanBeNull = true)]
        public Nullable<int> CollectionID { get; set; }

        [Column(DbType = "NVARCHAR(200)", CanBeNull = false)]
        public String TagName { get; set; }

        [Column(DbType = "VARCHAR(200)", CanBeNull = false)]
        public String Alias { get; set; }

        public IEnumerable<TagsModel> GetTagsList()
        {
            return LinqAdapter.GetTable<TagsModel>().ToList();
        }

        public IEnumerable<TagsModel> GetTagsList(int startRowIndex, int maximumRows)
        {
            return LinqAdapter.GetTable<TagsModel>().Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public TagsModel GetTagById(int TagID)
        {
            return LinqAdapter.GetTable<TagsModel>().Single(t => t.TagID == TagID);
        }

        public TagsModel GetTagByTagName(String TagName)
        {
            return LinqAdapter.GetTable<TagsModel>().Single(t => t.TagName == TagName);
        }

        public IEnumerable<TagsModel> SearchByTagName(String TagName)
        {
            return LinqAdapter.GetTable<TagsModel>().Where(t => SqlMethods.Like(t.TagName, "%" + TagName + "%")).ToList();
        }

        public TagsModel GetDocumentTags(int DocumentID)
        {
            return LinqAdapter.GetTable<TagsModel>().Single(t => t.DocumentID == DocumentID);
        }

        public void AddTag(String TagName, Nullable<int> DocumentID, Nullable<int> CollectionID)
        {
            ConvertLetter cv = new ConvertLetter();
            String Alias = cv.ClearAccent(TagName);

            TagsModel tagToInsert = new TagsModel();
            if (DocumentID == 0)
                tagToInsert.DocumentID = null;
            else
                tagToInsert.DocumentID = DocumentID;
            if (CollectionID == 0)
                tagToInsert.CollectionID = null;
            else
                tagToInsert.CollectionID = CollectionID;
            tagToInsert.TagName = TagName;
            tagToInsert.Alias = Alias;

            LinqAdapter.GetTable<TagsModel>().InsertOnSubmit(tagToInsert);
            LinqAdapter.SubmitChanges();
        }

        public void RemoveTag(int TagID)
        {
            TagsModel tagToRemove = GetTagById(TagID);

            LinqAdapter.GetTable<TagsModel>().DeleteOnSubmit(tagToRemove);
            LinqAdapter.SubmitChanges();
        }
    }
}
