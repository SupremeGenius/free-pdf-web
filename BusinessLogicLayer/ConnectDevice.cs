using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Xml;

namespace BusinessLogicLayer
{
    public class ConnectDevice
    {
        private static DataContext _LinqAdapter;

        protected static DataContext LinqAdapter
        {
            get
            {
                if (_LinqAdapter == null)
                    _LinqAdapter = new DataContext(GetConnectionString("FreePDFConnectString"));
                return _LinqAdapter;
            }
        }

        /// <summary>
        /// Get Connection String By Name
        /// </summary>
        /// <param name="Name">Connection String's Name</param>
        /// <returns></returns>
        private static String GetConnectionString(String Name)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@DirProject() + @"Web.config");

            foreach (XmlNode node in xml.SelectNodes("configuration/connectionStrings/add"))
            {
                if (node.Attributes["name"].InnerText == Name)
                {
                    return node.Attributes["connectionString"].InnerText;
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Get Project's Path
        /// </summary>
        /// <returns></returns>
        protected static String DirProject()
        {
            //string DirProject = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
