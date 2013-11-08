using System;
using NUnit.Framework;


namespace StringCalculator.Test
{
    [TestFixture]
    public class Test
    {
        private Application.StringCalculator _stringCalculator;
        [SetUp]
        public void Initialize()
        {
            _stringCalculator = new Application.StringCalculator();
        }
       [Test]
        public void AnEmptyStringReturnsZero()
        {
            Assert.That(_stringCalculator.Add(""),Is.EqualTo(0));
        }

        [TestCase("1",1)]
        [TestCase("10",10)]
        [TestCase("100",100)]
        public void OneNumberIsReturnedByHimself(string input,int expected)
        {
            Assert.That(_stringCalculator.Add("1"),Is.EqualTo(1));
        }

        [TestCase("1,2", 3)]
        [TestCase("4,5", 9)]
        [TestCase("10,11", 21)]
        public void TwoNumbersSeparatedByCommaReturnsTheSum(string input, int expected)
        {
            Assert.That(_stringCalculator.Add(input),Is.EqualTo(expected));
        }

        [TestCase("1\n2", 3)]
        [TestCase("4\n5", 9)]
        [TestCase("10\n11", 21)]
        public void TwoNumbersSeparatedByNewLineReturnsTheSum(string input, int expected)
        {
            Assert.That(_stringCalculator.Add(input),Is.EqualTo(expected));
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("//;\n5;6", 11)]
        [TestCase("//;\n7;5;2", 14)]
        [TestCase("//;\n11;5;1;6", 23)]
        public void TwoOrMoreNumbersSeparatedBySingleCustomDelimiterReturnsTheSum(string input, int expected)
        {
            Assert.That(_stringCalculator.Add(input),Is.EqualTo(expected));
        }

        [TestCase("//[***]\n1***2", 3)]
        [TestCase("//[***]\n1***20\n5", 26)]
        [TestCase("//[***]\n11***12\n12,10", 45)]
        public void NumbersSeparatedByLongLengthDelimiter(string input, int expected)
        {
            Assert.That(_stringCalculator.Add(input),Is.EqualTo(expected));
        }

        [TestCase("//[@][#][%][^]\n1@2^3%4", 10)]
        public void NumbersSepratedByVariousDelimitersReturnsTheSum(string input, int expected)
        {
            Assert.That(_stringCalculator.Add(input),Is.EqualTo(expected));
        }

        [TestCase("1001,2", 2)]
        [TestCase("//;\n1001;2;3", 5)]
        [TestCase("//[***]\n11***2***1001", 13)]
        public void NumbersGreaterThan_aThousandIsIgnored(string input, int expected)
        {
            Assert.That(_stringCalculator.Add(input),Is.EqualTo(expected));
        }

        [TestCase("-1,2", "Negatives not Allowed!")]
        [TestCase("1,2,-5", "Negatives not Allowed!")]
        [TestCase("-10,-200", "Negatives not Allowed!")]
        public void ExceptionMessageReturnedIfAnyNegativeNumbersAreFound(string input, string expected)
        {
            var exception = Assert.Throws<Exception>(() => _stringCalculator.Add(input));
            Assert.That(exception,Has.Message.Contains(expected));
        }
    }
}