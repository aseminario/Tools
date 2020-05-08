using System;
using System.Collections.Generic;

namespace Tools
{

    public static class Extensions
    {
        public static CaseInsensitiveString ToCaseInsensitiveString(this string anyString)
        {
            return new CaseInsensitiveString(anyString);
        }
    }

    public class CaseInsensitiveString
        : IEqualityComparer<CaseInsensitiveString>,
        IEquatable<CaseInsensitiveString>,
        IComparable<CaseInsensitiveString>,
        IComparer<CaseInsensitiveString>
    {

        #region Constructor
        public CaseInsensitiveString(string value)
        {
            this.OriginalValue = value;
        }
        #endregion

        #region Properties

        public string OriginalValue { get; }
        #endregion


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var cis = obj as CaseInsensitiveString;
            if (cis == null) return false;
            return EqualsInternal(this, cis);
        }
        public override int GetHashCode()
        {
            return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.OriginalValue);
        }

        public string ToUpper()
        {
            return this.OriginalValue.ToUpper();
        }

        public bool Contains(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return false;
            return this.OriginalValue.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant());
        }

        public bool Contains(CaseInsensitiveString searchTerm)
        {
            if (searchTerm == null) return false;
            return this.Contains(searchTerm.OriginalValue);
        }

        public override string ToString()
        {
            return this.OriginalValue;
        }


        #region Interface IComparer<CaseInsensitiveString>
        public int Compare(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            return CompareInternal(x, y);
        }

        #endregion

        #region Interface IEqualityComparer<CaseInsensitiveString>

        public bool Equals(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            return EqualsInternal(x, y);
        }

        public int GetHashCode(CaseInsensitiveString obj)
        {
            return obj.GetHashCode();
        }


        #endregion

        #region Interface IComparable<CaseInsensitiveString>
        public int CompareTo(CaseInsensitiveString other)
        {
            return CompareInternal(this, other);
        }

        #endregion

        #region Interface IEquatable<CaseInsensitiveString>

        public bool Equals(CaseInsensitiveString other)
        {
            return EqualsInternal(this, other);
        }

        #endregion

        #region Operator overloading
        public static bool operator ==(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            return EqualsInternal(x, y);
        }
        public static bool operator !=(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            return !EqualsInternal(x, y);
        }
        public static bool operator >(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            return CompareInternal(x, y) > 0;
        }
        public static bool operator <(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            return CompareInternal(x, y) < 0;
        }
        public static bool operator >=(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            return (CompareInternal(x, y) >= 0);
        }
        public static bool operator <=(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            return (CompareInternal(x, y) <= 0);
        }
        #endregion

        public static implicit operator string(CaseInsensitiveString d)
        {
            return d.ToString();
        }

        private static bool EqualsInternal(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            if (x is null && y is null) return true;
            if (x is null || y is null) return false;
            return (StringComparer.InvariantCultureIgnoreCase.Compare(x.OriginalValue, y.OriginalValue) == 0);
        }
        public static int CompareInternal(CaseInsensitiveString x, CaseInsensitiveString y)
        {
            if (x is null && y is null) return 0;
            if (x is null) return -1; // x>y because y cannot be null
            if (y is null) return 1; // x<y because x cannot be null
            return StringComparer.InvariantCultureIgnoreCase.Compare(x.OriginalValue, y.OriginalValue);
        }



    }
}