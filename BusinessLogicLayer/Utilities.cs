using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace BusinessLogicLayer
{
    public class Utilities
    {
        /// <summary>
        /// Call A Method By Text and Return Dynamic Type
        /// </summary>
        /// <param name="TypeObject">Type Object (An Instance Of Object/Class)</param>
        /// <param name="MethodName">Method Name To Call</param>
        /// <param name="ParameterList">Parameter List For Called Method</param>
        /// <returns></returns>
        public static dynamic CallMethodByText(dynamic TypeObject, String MethodName, Object[] ParameterList)
        {      
            Type type = TypeObject.GetType();
            return type.GetMethod(MethodName).Invoke(TypeObject, ParameterList);
        }

        /// <summary>
        /// Call A Method By Text And Return Dynamic Type
        /// </summary>
        /// <param name="TypeName">Type Name (Include Namespace)</param>
        /// <param name="MethodName">Method Name To Call</param>
        /// <param name="ParameterList">Parameter List For Called Method</param>
        /// <returns></returns>
        public static dynamic ComplexCallMethodByText(String TypeName, String MethodName, Object[] ParameterList)
        {
            dynamic typeObject = CreateInstanceByText(TypeName);
            Type type = typeObject.GetType();

            return type.GetMethod(MethodName).Invoke(typeObject, ParameterList);
        }

        /// <summary>
        /// Create An Object's Instance By Text
        /// </summary>
        /// <param name="TypeName">Type Name (Include Namespace)</param>
        /// <returns></returns>
        public static dynamic CreateInstanceByText(String TypeName)
        {
            //String AssemblyName = TypeName.Split('.')[0];
            //System.Reflection.Assembly[] arrAssembly = AppDomain.CurrentDomain.GetAssemblies();
            //String AssemblyInfo = String.Empty;

            //foreach (System.Reflection.Assembly item in arrAssembly)
            //{
            //    if (item.FullName.Contains(AssemblyName))
            //    {
            //        AssemblyInfo = item.FullName;
            //        break;
            //    }
            //}

            //AssemblyInfo = AssemblyInfo.Insert(0, TypeName + ", ");

            //Type type = Type.GetType(AssemblyInfo, true);
            //Object typeObj = Activator.CreateInstance(type);

            //return typeObj;

            String Namespace = TypeName.Split('.')[0];
            String FullTypeName = TypeName + ", " + Namespace;

            Type type = Type.GetType(FullTypeName, true);
            dynamic typeObject = Activator.CreateInstance(type);

            return typeObject;
        }        
    }
}
