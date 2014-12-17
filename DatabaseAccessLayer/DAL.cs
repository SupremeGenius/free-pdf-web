/*********************************************
 # Author: ITEpxlore (Tran Phuc Tho) - MMS45 #
 # Email: itexplore09@yahoo.com.vn           #
 # Copyright © 2011 - 2012                   #
 *********************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseAccessLayer
{
    public class DAL
    {
        private static SqlConnection SQLConnect = ConnectDevice.SQLConnect;
        public delegate void Functions();
        public static Functions fnc;

        /// <summary>
        /// Simple query (Update data)
        /// </summary>
        /// <param name="Query">Query String</param>
        /// <param name="ParameterList">Input Parameter</param>
        /// <returns></returns>
        public static int UpdateQuery(String Query, params Object[] ParameterList)
        {            
            int rowAffected = 0;
            SQLCompactFunctions(delegate()
            {
                SqlCommand queryCommand = new SqlCommand(Query, SQLConnect);
                int countParam = 0, startPos = 0, endPos = 0, idx = 0;
                String[] arrParamName;

                //Count total Param
                while((startPos = Query.IndexOf("@", startPos + 1)) != -1)
                    countParam++;

                startPos = 0;
                arrParamName = new String[countParam];

                //Get Param Name
                while ((startPos = Query.IndexOf("@", startPos + 1)) != -1)
                {
                    for (int i = startPos; i < Query.Length && (Query[i] != ' ' && Query[i] != ',' && Query[i] != ')'); i++)
                        endPos = i;

                    arrParamName[idx] = Query.Substring(startPos, (endPos - startPos) + 1);
                    idx++;
                }

                //Add Param
                for (int i = 0; i < countParam; i++)
                    queryCommand.Parameters.AddWithValue(arrParamName[i], ParameterList[i]);                    

                rowAffected = queryCommand.ExecuteNonQuery();
            });
            return rowAffected;
        }
       
        /// <summary>
        /// Simple Query (Get data)
        /// </summary>
        /// <param name="Query">Query String</param>
        /// <param name="ParameterList">Input Parameter</param>
        /// <returns></returns>
        public static List<SList> SelectQuery(String Query, params Object[] ParameterList)
        {
            List<SList> lstResult = new List<SList>();
            SQLCompactFunctions(delegate()
            {                
                SqlDataReader readerDevice;
                SqlCommand queryCommand = new SqlCommand(Query, SQLConnect);
                int countParam = 0;
                List<String> arrParamName;

                //Count Param
                countParam = ParamCount(Query);

                //Get Param Name
                arrParamName = GetParamList(Query);

                //Add Param
                for (int i = 0; i < countParam; i++)
                    queryCommand.Parameters.AddWithValue(arrParamName[i], ParameterList[i]);

                readerDevice = queryCommand.ExecuteReader();

                //Kiem tra thu cho nay
                if (readerDevice.HasRows)
                {
                    while (readerDevice.Read())
                    {
                        SList resultItem = new SList();
                        for (int i = 0; i < readerDevice.FieldCount; i++)
                            resultItem.Add(readerDevice.GetName(i), readerDevice[i].ToString());
                        lstResult.Add(resultItem);                        
                    }
                }
            });
            return lstResult;
        }
        
        /// <summary>
        /// Simple select query (return to Dataset)
        /// </summary>
        /// <param name="Query">Query string</param>
        /// <param name="ParameterList">Input parameter</param>
        /// <returns></returns>
        public static DataSet SelectQueryReturnDataset(String Query, params Object[] ParameterList)
        {
            DataSet dataDevice = new DataSet();
            SqlDataAdapter adapterDevice;
            SQLCompactFunctions(delegate()
            {
                SqlCommand queryCommand = new SqlCommand(Query, SQLConnect);
                int countParam = 0;
                List<String> arrParamName;

                //Count Param
                countParam = ParamCount(Query);

                //Get Param Name
                arrParamName = GetParamList(Query);

                //Add Param
                for (int i = 0; i < countParam; i++)
                    queryCommand.Parameters.AddWithValue(arrParamName[i], ParameterList[i]);

                adapterDevice = new SqlDataAdapter(queryCommand);
                adapterDevice.Fill(dataDevice);
            });
            return dataDevice;
        }

        /// <summary>
        /// Call a SQL's Procedure to get data
        /// </summary>
        /// <param name="ProcudureName">Procedure Name</param>
        /// <param name="ParameterList">Input Parameter [@Param=value]</param>
        /// <returns></returns>
        public static List<SList> CallProcedure(String ProcudureName, params Object[] ParameterList)
        {
            List<SList> lstResult = new List<SList>();
            SqlDataReader readerDevice;
            SQLCompactFunctions(delegate()
            {
                String[] arrParam = new String[ParameterList.Length];

                SqlCommand queryCommand = new SqlCommand(ProcudureName, SQLConnect);
                queryCommand.CommandType = CommandType.StoredProcedure;                

                for (int i = 0; i < ParameterList.Length; i++)
                {
                    arrParam = ParameterList[i].ToString().Split('=');
                    queryCommand.Parameters.AddWithValue(arrParam[0], arrParam[1]);
                }

                readerDevice = queryCommand.ExecuteReader();

                if (readerDevice.HasRows)
                {
                    while (readerDevice.Read())
                    {
                        SList resultItem = new SList();
                        for (int i = 0; i < readerDevice.FieldCount; i++)                         
                            resultItem.Add(readerDevice.GetName(i), readerDevice[i].ToString());
                        lstResult.Add(resultItem);
                    }                                       
                }

            });
            return lstResult;
        }

        /// <summary>
        /// Call a SQL's Procedure to get data (Return result sets to Dataset object)
        /// </summary>
        /// <param name="ProcudureName">Procedure Name</param>
        /// <param name="ParameterList">Input Parameter [@Param=value]</param>
        /// <returns></returns>
        public static DataSet CallProcedureReturnDataset(String ProcudureName, params Object[] ParameterList)
        {
            DataSet dataDevice = new DataSet();
            SqlDataAdapter adapterDevice;
            SQLCompactFunctions(delegate()
            {
                String[] arrParam = new String[ParameterList.Length];

                SqlCommand sqlcmd = new SqlCommand(ProcudureName, SQLConnect);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < ParameterList.Length; i++)
                {
                    arrParam = ParameterList[i].ToString().Split('=');
                    sqlcmd.Parameters.AddWithValue(arrParam[0], arrParam[1]);
                }

                adapterDevice = new SqlDataAdapter(sqlcmd);
                adapterDevice.Fill(dataDevice);
                adapterDevice.TableMappings.Add("Table", "TempTable");
            });
            return dataDevice;
        }

        /// <summary>
        /// Call a SQL's Procedure to update data
        /// </summary>
        /// <param name="ProcudureName">Procedure Name</param>
        /// <param name="ParameterList">Input Parameter [@Param=value]</param>
        /// <returns></returns>
        public static int CallUpdateProcedure(String ProcudureName, params Object[] ParameterList)
        {
            int rowAffected = 0;
            SQLCompactFunctions(delegate()
            {
                String[] arrParam = new String[ParameterList.Length];

                SqlCommand queryCommand = new SqlCommand(ProcudureName, SQLConnect);
                queryCommand.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < ParameterList.Length; i++)
                {
                    arrParam = ParameterList[i].ToString().Split('=');
                    queryCommand.Parameters.AddWithValue(arrParam[0], arrParam[1]);
                }

                rowAffected = queryCommand.ExecuteNonQuery();

            });
            return rowAffected;
        }

        private static void SQLCompactFunctions(Functions f)
        {
            if (fnc != null)
                fnc = null;

            fnc += f;
            try
            {
                if (SQLConnect.State == System.Data.ConnectionState.Closed)
                    SQLConnect.Open();
                fnc();
            }
            catch (SqlException ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script type='text/javascript'>alert('" + ex.StackTrace + "')</script>");
                //MessageBox.Show(ex.Message, "Data Exception");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script type='text/javascript'>alert('" + ex.StackTrace + "')</script>");
                //MessageBox.Show(ex.Message);
            }
            finally
            {                
                SQLConnect.Close();
            }
        }

        private static int ParamCount(String Query)
        {
            int countParam = 0, startPos = 0;

            //Count total Param
            while ((startPos = Query.IndexOf("@", startPos + 1)) != -1)
                countParam++;

            return countParam;
        }

        private static List<String> GetParamList(String Query)
        {
            int startPos = 0;
            int endPos = 0;
            List<String> arrParamName = new List<String>();

            //Param List
            while ((startPos = Query.IndexOf("@", startPos + 1)) != -1)
            {
                for (int i = startPos; i < Query.Length && (Query[i] != ' ' && Query[i] != ',' && Query[i] != ')'); i++)
                    endPos = i;

                String paramName = Query.Substring(startPos, (endPos - startPos) + 1);

                arrParamName.Add(paramName);
            }

            return arrParamName;
        }
    }
}