using System;
using System.IO;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.Helpers;

namespace PizzaMasterEmporium.Framework.Tests.Helpers
{
    [TestFixture]
    public class CSVHelperTests
    {
        #region Tests

        #region Import

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "File parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void ImportNullFileParameter()
        {
            CSVHelper.Import(null, false, new Char());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "File parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void ImportEmptyFileParameter()
        {
            CSVHelper.Import(String.Empty, false, new Char());
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException), ExpectedMessage = "Cannot find the file specified", MatchType = MessageMatch.Exact)]
        public void ImportInvalidFileParameter()
        {
            CSVHelper.Import("Unknown File", false, new Char());
        }

        #endregion

        #region Create

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Path parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void CreateNullPathParameter()
        {
            CSVHelper.Create(null, null, null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Path parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void CreateBlankPathParameter()
        {
            CSVHelper.Create(String.Empty, null, null, null);
        }

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException), ExpectedMessage = "Unable to find directory for path supplied", MatchType = MessageMatch.Exact)]
        public void CreateInvalidPathParameter()
        {
            CSVHelper.Create("D:\\Work\\", null, null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "File parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void CreateNullFileParameter()
        {
            CSVHelper.Create("C:\\", null, null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "File parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void CreateBlankFileParameter()
        {
            CSVHelper.Create("C:\\", String.Empty, null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Data parameter is null", MatchType = MessageMatch.Exact)]
        public void CreateNullDataParameter()
        {
            CSVHelper.Create("C:\\", "test.csv", null, null);
        }

        #endregion

        #region Append

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Path parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void AppendNullPathParameter()
        {
            CSVHelper.Append(null, null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Path parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void AppendBlankPathParameter()
        {
            CSVHelper.Append(String.Empty, null, null);
        }

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException), ExpectedMessage = "Unable to find directory for path supplied", MatchType = MessageMatch.Exact)]
        public void AppendInvalidPathParameter()
        {
            CSVHelper.Append("D:\\Work\\", null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "File parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void AppendNullFileParameter()
        {
            CSVHelper.Append("C:\\", null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "File parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void AppendBlankFileParameter()
        {
            CSVHelper.Append("C:\\", String.Empty, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Data parameter is null", MatchType = MessageMatch.Exact)]
        public void AppendNullDataParameter()
        {
            CSVHelper.Append("C:\\", "test.csv", null);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException), ExpectedMessage = "File Not Found", MatchType = MessageMatch.Exact)]
        public void AppendInvalidFile()
        {
            CSVHelper.Append("C:\\", "UnknownFile.csv", new String[0]);
        }

        #endregion

        #endregion
    }
}
