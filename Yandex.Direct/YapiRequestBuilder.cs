using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Yandex.Direct
{
    internal class YapiRequestBuilder
    {
        readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        public void Add(string key, object value, bool dontEscapeArray = false)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(key));
            if (value == null)
                return;

            var strValue = value.ToString();
            if (string.IsNullOrWhiteSpace(strValue))
                return;

            var escape = dontEscapeArray && (strValue.StartsWith("[") || strValue.StartsWith("{")) || strValue.StartsWith("\"");
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
                .Where(x => x.Value != null)
                .Select(x => string.Format("\"{0}\": {1}", x.Key, x.Value))
                .Merge(", ");

            return string.Format("{{{0}}}", merged);
        }
    }
}