using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    internal static class Extensions
    {
        public static string Merge(this IEnumerable< string> strings, string separator = null)
        {
            Contract.Requires(strings!= null);
            if (separator == null)
                return strings.Aggregate(new StringBuilder(), (x, y) => x.Append(y)).ToString();
            var stringBuilder = strings.Aggregate(new StringBuilder(), (x, y) => x.Append(y).Append(separator));
            return (stringBuilder.Length >= separator.Length)
                       ? stringBuilder.ToString(0, stringBuilder.Length - separator.Length)
                       : stringBuilder.ToString();

        }
    }
}