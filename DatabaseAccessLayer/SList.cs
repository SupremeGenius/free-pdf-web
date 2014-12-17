/*********************************************
 # Author: ITEpxlore (Tran Phuc Tho) - MMS45 #
 # Email: itexplore09@yahoo.com.vn           #
 # Copyright © 2011 - 2012                   #
 *********************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccessLayer
{
    class Data
    {
        public String Key { get; set; }
        public String Value { get; set; }

        public Data(String Key, String Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }

    public class SList
    {
        private List<Data> arrList;
        private int count;

        public SList()
        {
            arrList = new List<Data>();
            count = 0;
        }

        /// <summary>
        /// Get total element
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Add new element with key, value
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void Add(String Key, String Value)
        {
            arrList.Add(new Data(Key, Value));
            count++;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            arrList.Clear();
            count = 0;
        }

        /// <summary>
        /// Get value by index
        /// </summary>
        /// <param name="index">Index to get value</param>
        /// <returns></returns>
        public String this[int index]
        {
            get { return arrList[index].Value; }
        }

        /// <summary>
        /// Get value by key
        /// </summary>
        /// <param name="key">Key to get value</param>
        /// <returns></returns>
        public String this[string key]
        {
            get
            {
                int idx = 0;
                for (int i = 0; i < arrList.Count; i++)
                {
                    if (arrList[i].Key.Equals(key))
                    {
                        idx = i;
                        break;
                    }
                }
                return arrList.Count == 0 ? null : arrList[idx].Value;
            }
        }
    }
}