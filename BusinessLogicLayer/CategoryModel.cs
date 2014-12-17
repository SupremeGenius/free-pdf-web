using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient;
using ConvertLetterAccent;

namespace BusinessLogicLayer
{
    [Table(Name="DocumentCategory")]
    public class CategoryModel : ConnectDevice
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int CategoryID { get; set; }

        [Column(DbType = "NVARCHAR(45)", CanBeNull = false)]
        public String Name { get; set; }

        [Column(DbType = "VARCHAR(45)", CanBeNull = false)]
        public String Alias { get; set; }

        public static int TotalRecord
        {
            get { return LinqAdapter.GetTable<CategoryModel>().Count(); }
        }

        public IEnumerable<CategoryModel> GetCategoryList()
        {
            return LinqAdapter.GetTable<CategoryModel>().ToList();
        }

        public IEnumerable<CategoryModel> GetCategoryList(int startRowIndex, int maximumRows)
        {
            return LinqAdapter.GetTable<CategoryModel>().Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public CategoryModel GetCategoryById(int CategoryID)
        {
            return LinqAdapter.GetTable<CategoryModel>().SingleOrDefault(c => c.CategoryID == CategoryID);
        }

        public int CountDocumentInCategory(int CategoryID)
        {
            return new DocumentModel().GetDocumentList().Where(d => d.CategoryID == CategoryID).Count();
        }

        public CategoryModel GetCategoryNameByAlias(String Alias)
        {
            return LinqAdapter.GetTable<CategoryModel>().SingleOrDefault(c => c.Alias == Alias);
        }

        public void AddCategory(String Name)
        {
            ConvertLetter cvLetter = new ConvertLetter();
            String Alias = cvLetter.ClearAccent(Name).ToTitleCase().Replace(" ", "");

            CategoryModel categoryToInsert = new CategoryModel();
            categoryToInsert.Name = Name;
            categoryToInsert.Alias = Alias;

            LinqAdapter.GetTable<CategoryModel>().InsertOnSubmit(categoryToInsert);
            LinqAdapter.SubmitChanges();
        }

        public void UpdateCategory(String Name, int CategoryID)
        {
            ConvertLetter cvLetter = new ConvertLetter();
            String Alias = cvLetter.ClearAccent(Name).ToTitleCase().Replace(" ", "");

            CategoryModel categoryToUpdate = GetCategoryById(CategoryID);
            categoryToUpdate.Name = Name;
            categoryToUpdate.Alias = Alias;

            LinqAdapter.SubmitChanges();
        }

        public void RemoveCategory(int CategoryID)
        {
            CategoryModel categoryToRemove = GetCategoryById(CategoryID);
            LinqAdapter.GetTable<CategoryModel>().DeleteOnSubmit(categoryToRemove);
            LinqAdapter.SubmitChanges();
        }
    }
}
