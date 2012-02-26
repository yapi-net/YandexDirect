using System;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;

namespace Yandex.Direct.Serialization
{
    [JsonConverter(typeof(YesNoBooleanConverter))]
    public struct YesNo
    {
        public static YesNo Yes = new YesNo(true);
        public static YesNo No = new YesNo(false);

        private readonly bool _value;

        public YesNo(bool value)
        {
            _value = value;
        }

        public static bool TryParse(string s, out YesNo result)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(s), "s");

            switch (s.ToLowerInvariant())
            {
                case "yes":
                    result = Yes;
                    return true;

                case "no":
                    result = No;
                    return true;

                default:
                    result = No;
                    return false;
            }
        }

        public override string ToString()
        {
            return string.Format(_value ? "Yes" : "No");
        }

        public bool Equals(YesNo other)
        {
            return other._value.Equals(_value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (obj.GetType() != typeof(YesNo))
                return false;

            return Equals((YesNo)obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static explicit operator YesNo(bool value)
        {
            return new YesNo(value);
        }

        public static explicit operator bool(YesNo value)
        {
            return value._value;
        }

        public static bool operator ==(YesNo a, YesNo b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(YesNo a, YesNo b)
        {
            return !a.Equals(b);
        }

        public static bool operator ==(YesNo a, bool b)
        {
            return a.Equals(new YesNo(b));
        }

        public static bool operator !=(YesNo a, bool b)
        {
            return !a.Equals(new YesNo(b));
        }
    }
}
