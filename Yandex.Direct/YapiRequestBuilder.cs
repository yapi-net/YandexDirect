using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Yandex.Direct
{
    internal class YapiRequestBuilder
    {
        readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        /// <summary>
        /// Add key-value pair to parameter collection. 
        /// Parameters are automatically escaped with "" if needed. 
        /// Null and empty values are ignored.
        /// </summary>
        /// <param name="key">Key to add</param>
        /// <param name="value">Value to add</param>
        /// <param name="dontEscapeArray">Specify true, if value can be already jsone-ed array and no escaping is needed</param>
        public void AddParameter(string key, object value, bool dontEscapeArray = false)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(key));
            if (value == null)
                return;

            var strValue = value.ToString();
            if (string.IsNullOrWhiteSpace(strValue))
                return;

            var escape = dontEscapeArray
                             ? (strValue.StartsWith("[") || strValue.StartsWith("{"))
                             : strValue.StartsWith("\"");
            if (escape)
                _dictionary[key] = strValue;
            else
                _dictionary[key] = string.Format("\"{0}\"", strValue);
        }

        /// <summary>
        /// Creates json-formatted request body
        /// </summary>
        public string BuildRequestBody()
        {
            var merged = _dictionary
                .Select(x => string.Format("\"{0}\": {1}", x.Key, x.Value))
                .Merge(", ");

            return string.Format("{{{0}}}", merged);
        }
    }
}