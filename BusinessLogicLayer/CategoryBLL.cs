using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DatabaseAccessLayer;
using ConvertLetterAccent;

namespace BusinessLogicLayer
{
    public class CategoryBLL
    {
        public int CategoryID { get; set; }
        public String Name { get; set; }
        public String Alias { get; set; }

        public CategoryBLL() { }

        public CategoryBLL(int CategoryID, String Name, String Alias)
        {
            this.CategoryID = CategoryID;
            this.Name = Name;
            this.Alias = Alias;
        }

        public List<CategoryBLL> GetCategoryList()
        {
            List<CategoryBLL> categoryLogic = new List<CategoryBLL>();
            DataSet categoryDS = DAL.CallProcedureReturnDataset("GetCategoryList");
            DataTable categoryTable = categoryDS.Tables[0];
            DataRowCollection categoryRow = categoryTable.Rows;

            foreach (DataRow row in categoryRow)
            {
                categoryLogic.Add(new CategoryBLL(
                    Int32.Parse(row["CategoryID"].ToString()),
                    row["Name"].ToString(),
                    row["Alias"].ToString()
                    ));
            }

            return categoryLogic;
        }

        public CategoryBLL GetCategoryById(int CategoryID)
        {

            CategoryBLL categoryLogic;
            DataSet categoryDS = DAL.CallProcedureReturnDataset("GetCategoryById", "@categoryid=" + CategoryID);
            DataTable categoryTable = categoryDS.Tables[0];
            DataRow categoryRow = categoryTable.Rows[0];

            categoryLogic = new CategoryBLL(
                Int32.Parse(categoryRow["CategoryID"].ToString()),
                categoryRow["Name"].ToString(),
                categoryRow["Alias"].ToString()
                );

            return categoryLogic;
        }

        public int GetTotalDocumentInCategoryById(int CategoryID)
        {
            return Int32.Parse(DAL.CallProcedure("GetTotalDocumentInCategoryById", "@categoryid=" + CategoryID)[0]["Total"].ToString());
        }

        public String GetCategoryNameByAlias(String Alias)
        {
            return DAL.CallProcedure("GetCategoryNameByAlias", "@alias=" + Alias)[0]["Name"];
        }

        public bool AddCategory(String Name)
        {
            ConvertLetter cvLetter = new ConvertLetter();
            String alias = cvLetter.ClearAccent(Name).ToTitleCase().Replace(" ", "");

            int rowAffected = DAL.CallUpdateProcedure("AddCategory", "@name=" + Name, "@alias=" + alias);

            return rowAffected == 1;
        }

        public bool UpdateCategory(String Name, int CategoryID)
        {
            ConvertLetter cvLetter = new ConvertLetter();
            String alias = cvLetter.ClearAccent(Name).ToTitleCase().Replace(" ", "");

            int rowAffected = DAL.CallUpdateProcedure("UpdateCategory", "@name=" + Name, "@alias=" + alias, "@categoryid=" + CategoryID);

            return rowAffected == 1;
        }

        public bool RemoveCategory(int CategoryID)
        {
            int rowAffected = DAL.CallUpdateProcedure("RemoveCategory", "@categoryid=" + CategoryID);

            return rowAffected == 1;
        }
    }
}
