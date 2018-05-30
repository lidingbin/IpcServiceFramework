﻿using Newtonsoft.Json.Linq;
using System;

namespace JKang.IpcServiceFramework.Services
{
    public class DefaultValueConverter : IValueConverter
    {
        public bool TryConvert(object origValue, Type destType, out object destValue)
        {
            if (destType.IsAssignableFrom(origValue.GetType()))
            {
                // copy value directly if it can be assigned to destType
                destValue = origValue;
                return true;
            }

            if (origValue is JObject jObj)
            {
                // rely on JSON.Net to convert complexe type
                destValue = jObj.ToObject(destType);
                // TODO: handle error
                return true;
            }

            if (origValue is JArray jArray)
            {
                destValue = jArray.ToObject(destType);
                return true;
            }

            try
            {
                destValue = Convert.ChangeType(origValue, destType);
                return true;
            }
            catch (Exception)
            {
                destValue = null;
                return false;
            }
        }
    }
}