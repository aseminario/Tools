using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using Xunit;

namespace ProntoAyuda.Tools.Tests
{
    public class CaseInsensitiveStringShould
    {

        #region OriginalValue
        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.OriginalValue))]
        public void KeepOriginalValueUnchanged()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            Assert.Equal(value, sut.OriginalValue);

        }
        #endregion

        #region Equals
        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Equals))]
        public void EqualsShouldReturnFalseWhenComparedToNull()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            var result = sut.Equals(null);

            Assert.False(result);

        }

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Equals))]
        public void EqualsShouldReturnFalseWhenComparedToNonCaseSenstiveObject()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            var result = sut.Equals(value);
            
            Assert.False(result);

        }

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Equals))]
        public void EqualsShouldReturnFalseWhenComparedToNullCaseInsensitiveString()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);
            CaseInsensitiveString comparer = null;

            var result = sut.Equals(comparer);

            Assert.False(result);

        }

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Equals))]
        public void EqualsShouldReturnFalseWhenComparedToDifferentValue()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            var comparer = new CaseInsensitiveString("OtherValue");

            var result = sut.Equals(comparer);

            Assert.False(result);
        }

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Equals))]
        public void EqualsShouldReturnTrueWhenComparedToSameValueWithSameCase()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            var comparer = new CaseInsensitiveString(value);

            var result = sut.Equals(comparer);

            Assert.True(result);
        }

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Equals))]
        public void EqualsShouldReturnTrueWhenComparedToSameValueWithDifferentCase()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            // ReSharper disable once StringLiteralTypo
            var comparer = new CaseInsensitiveString("somevalue");

            var result = sut.Equals(comparer);

            Assert.True(result);
        }

        #endregion

        #region "ToUpper"

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.ToUpper))]
        public void ToUpperReturnValueInAllCaps()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            Assert.Equal(value.ToUpper(),sut.ToUpper());
        }
        #endregion

        #region "Contains"

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Contains))]
        public void ContainReturnTrueWhenFindsPartOfStringInDifferentCase()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            var result = sut.Contains("somev");

            Assert.True(result);
        }

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Contains))]
        public void ContainsReturnFalseWhenSearchingNull()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            var result = sut.Contains(null);

            Assert.False(result);
        }

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Contains))]
        public void ContainsReturnFalseWhenSearchingEmpty()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            var result = sut.Contains(string.Empty);

            Assert.False(result);
        }

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Contains))]
        public void ContainsReturnFalseWhenSearchingForNonExistingPart()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            var result = sut.Contains("other");

            Assert.False(result);
        }

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.Contains))]
        public void ContainsReturnFalseWhenSearchingNullCaseInsensitiveObject()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            CaseInsensitiveString partToSearch = null;

            var result = sut.Contains(partToSearch);

            Assert.False(result);
        }


        #endregion

        #region "ToString"

        [Fact]
        [Trait("Method", nameof(CaseInsensitiveString.ToUpper))]
        public void ToStringReturnOriginalValue()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            Assert.Equal(value, sut.ToString());
        }
        #endregion

        #region "IComparer<CaseInsensitiveString>"

        [Fact(Skip="No clear how it works")]
        [Trait("Method", nameof(CaseInsensitiveString.ToUpper))]
        public void ComparerReturn0()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            const string comparerValue = "soMevAlue";
            var comparer = new CaseInsensitiveString(comparerValue);

            var resultNormal = string.Compare(value, comparerValue);
            var result = sut.Compare(sut, comparer);

           // normal comparison should be 1 because capital S is after small s.
            Assert.Equal(1, resultNormal);

            // case insensitive comparison should be 0 because small s and capital S are the same.
            Assert.Equal(0, result);
        }
        #endregion

        #region "IComparer<CaseInsensitiveString>"

        [Fact(Skip = "No clear how it works")]
        [Trait("Method", nameof(CaseInsensitiveString.ToUpper))]
        public void ComparerReturnNegative1()
        {
            const string value = "SomeValue";
            var sut = new CaseInsensitiveString(value);

            const string comparerValue = "spmeValue";
            var comparer = new CaseInsensitiveString(comparerValue);

            var resultNormal = string.Compare(value, comparerValue);
            var result = sut.Compare(sut, comparer);

            // normal comparison should be 1 because capital S is after small s.
            Assert.Equal(1, resultNormal);

            // case insensitive comparison should be -1 because small s and capital S are the same
            // but second letter o is before second letter p
            Assert.Equal(-1, result);
        }
        #endregion

    }
}
