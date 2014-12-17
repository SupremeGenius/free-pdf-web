/*********************************************
 # Author: ITEpxlore (Tran Phuc Tho) - MMS45 #
 # Email: itexplore09@yahoo.com.vn           #
 # Copyright © 2011 - 2012                   #
 *********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace DatabaseAccessLayer
{
    public class ConnectDevice
    {
        public static SqlConnection SQLConnect = CreateConnect();

        /// <summary>
        /// Create SQLConnection Object
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateConnect()
        {            
            return new SqlConnection(GetConnectionString("FreePDFConnectString"));
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
        public static String DirProject()
        {
            //string DirProject = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}