using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using BusinessLogicLayer;

namespace FreePDF
{
    public class CacheProcess
    {
        Cache cacheAdapter;

        public CacheProcess()
        {
            cacheAdapter = System.Web.HttpContext.Current.Cache;
        }

        /// <summary>
        /// Cache Data Use Method: Part By Part
        /// </summary>
        /// <param name="CacheObjectType">Cache Object Type's Name</param>
        /// <param name="CacheName">Name Of Cache Data Will Be Storage In Cache</param>
        /// <param name="CacheDuration">Cache Duration (in Minutes)</param>
        /// <param name="CachePositionIndex">Position Of A Part Will Be Cache</param>
        /// <param name="GetFromIndex">Get Data From Database From Index</param>
        /// <param name="GetAmount">Amount Of Record Be Got From Database</param>
        /// <param name="TotalPart">Total Part Will Be Cache</param>
        /// <param name="MethodToInvoke">Get Data From Database By Method</param>
        /// <param name="MethodParameter">Parameter List For MethodToInvoke (Can Be Null)</param>
        /// <returns></returns>
        public dynamic SuperCacheData(String CacheObjectType, String CacheName, int CacheDuration, int CachePositionIndex, int GetFromIndex, int GetAmount, int TotalPart, String MethodToInvoke, Object[] MethodParameter = null, bool CheckingExpression = true)
        {
            dynamic cacheObject = null;
            int TotalCached = 0;
            bool IsCacheComplete = false;
            bool IsCurrentPageCached = false;
            bool IsCachedListEmpty = false;

            cacheObject = Utilities.CreateInstanceByText(CacheObjectType);

            //Get Cache
            IEnumerable<dynamic>[] lstCachedData = (IEnumerable<dynamic>[])cacheAdapter[CacheName];

            //Conditions
            IsCacheComplete = TotalCached == TotalPart;
            IsCurrentPageCached = lstCachedData == null ? false : lstCachedData[CachePositionIndex - 1] != null;
            IsCachedListEmpty = lstCachedData == null;

            //Count Total Cached Part
            int i = 0;
            while (!IsCachedListEmpty && i < lstCachedData.Length && lstCachedData[i] != null)
            {
                TotalCached++;
                i++;
            }

            if ((IsCachedListEmpty || ((!IsCurrentPageCached || !CheckingExpression) && !IsCacheComplete)))
            {
                //Create List For Storage Of Data
                if (IsCachedListEmpty)
                    lstCachedData = new IEnumerable<dynamic>[TotalPart];

                //Save A Part Of Data To Cached List
                IEnumerable<dynamic> tmpData = ((IEnumerable<dynamic>)Utilities.CallMethodByText(cacheObject, MethodToInvoke, MethodParameter)).Skip(GetFromIndex).Take(GetAmount).ToList();
                lstCachedData[CachePositionIndex - 1] = tmpData; //cacheObject.GetDocumentList(GetFromIndex, GetAmount);

                //Add Data List To Cache
                cacheAdapter.Add(CacheName, lstCachedData, null, DateTime.Now.AddMinutes(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
            }

            return lstCachedData;
        }

        /// <summary>
        /// Cache Data Use Method: Part By Part
        /// </summary>
        /// <param name="CacheObjectType">Cache Object Type's Name</param>
        /// <param name="CacheName">Name Of Cache Data Will Be Storage In Cache</param>
        /// <param name="CacheMinutes">Cache Duration (in Minutes)</param>
        /// <param name="PartNumber">Part Number Will Be Cache</param>
        /// <param name="GetFromIndex">Get Data From Database From Index</param>
        /// <param name="GetAmount">Amount Of Record Be Got From Database</param>
        /// <param name="TotalPart">Total Part Will Be Cache</param>
        /// <param name="MethodToInvoke">Get Data From Database By Method</param>
        /// <param name="MethodParameter">Parameter List For MethodToInvoke (Can Be Null)</param>
        /// <param name="AnotherConditional">Another Conditional</param>
        /// <returns></returns>
        public dynamic FragmentDataCache(String CacheObjectType, String CacheName, int CacheMinutes, int PartNumber, int GetFromIndex, int GetAmount, int TotalPart, String MethodToInvoke, Object[] MethodParameter = null, bool AnotherConditional = true)
        {
            dynamic cacheObject = null;
            int TotalCached = 0;
            bool IsCacheComplete = false;
            bool IsCurrentPartCached = false;
            bool IsCachedListEmpty = false;
            String PartName = String.Format("{0}_Part{1}", CacheName, PartNumber);

            cacheObject = Utilities.CreateInstanceByText(CacheObjectType);

            //Get Cache
            Hashtable lstCachedData = (Hashtable)cacheAdapter[CacheName];

            //Count Total Cache Part
            TotalCached = lstCachedData == null ? 0 : lstCachedData.Count;

            //Conditions
            IsCacheComplete = TotalCached == TotalPart;
            IsCurrentPartCached = lstCachedData == null ? false : lstCachedData[PartName] != null;
            IsCachedListEmpty = lstCachedData == null;

            if ((IsCachedListEmpty || ((!IsCurrentPartCached || !AnotherConditional) && !IsCacheComplete)))
            {
                //Create List For Storage Of Data
                if (IsCachedListEmpty)
                    lstCachedData = new Hashtable();

                //Save A Part Of Data To Cached List
                IEnumerable<dynamic> tmpData = ((IEnumerable<dynamic>)Utilities.CallMethodByText(cacheObject, MethodToInvoke, MethodParameter)).Skip(GetFromIndex).Take(GetAmount).ToList();
                lstCachedData.Add(PartName, tmpData); //cacheObject.GetDocumentList(GetFromIndex, GetAmount);

                //Add Data List To Cache
                cacheAdapter.Add(CacheName, lstCachedData, null, DateTime.Now.AddMinutes(CacheMinutes), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
            }

            return lstCachedData;
        }
    }
}