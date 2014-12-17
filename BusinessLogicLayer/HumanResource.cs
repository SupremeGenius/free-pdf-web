using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

namespace BusinessLogicLayer
{
    [Table(Name = "Sales.SalesOrderDetail")]
    public class HumanResource
    {
        [Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public int SalesOrderID { get; set; }
        [Column]
        public int ProductID { get; set; }
        [Column]
        public decimal UnitPrice { get; set; }

        public static int TotalRecord
        {
            get { return LinqAdapter.GetTable<HumanResource>().Count(); }
        }

        private static DataContext _LinqAdapter;

        public static DataContext LinqAdapter
        {
            get
            {
                if (_LinqAdapter == null)
                    _LinqAdapter = new DataContext(@"Data Source=ITESERVER;Initial Catalog=AdventureWorks;Integrated Security=True");
                return _LinqAdapter;
            }
        }

        public IEnumerable<HumanResource> GetEmployeeList()
        {
            return LinqAdapter.GetTable<HumanResource>().ToList();
        }

        //public IEnumerable<HumanResource> GetEmployeeList(int startRowIndex, int maximumRows)
        //{
        //    return GetEmployeeList().Skip(startRowIndex).Take(maximumRows).ToList();
        //}

        public IEnumerable<HumanResource> GetEmployeeById(int ID)
        {
            return GetEmployeeList().Where(e => e.UnitPrice > 1000).ToList();
        }
    }
}
