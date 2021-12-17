using BaseProject.PageElementLibrary;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.SeleniumHelpers
{
    public class CommonUtilityMethods
    {
        public static string GetCurrentTimestamp()
        {
            var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss").Replace(":", "-");
            return timeStamp;
        }
        public static string GetFolderPath(string folderName)
        {
            var projectPath = GetProjectPath();
            var path = Directory.GetDirectories(projectPath, "*", SearchOption.AllDirectories).
                                    FirstOrDefault(item => item.ToLower().Contains(folderName.ToLower()));

            return path;
        }

        public static string GetProjectPath()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var parentPath = Directory.GetParent(assemblyLocation).Parent.FullName;
            var projectPath = Path.GetFullPath(parentPath + @"..\..\");
            return projectPath;
        }

      

        internal static string GetCurrentDateTime()
        {
            var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss").Replace(":", "-");
            return timeStamp;
        }

        internal static void CreateDirectory(string pathWithFolder)
        {
            Directory.CreateDirectory(pathWithFolder);
        }

     
        public static IWebElement GetElementInDataTable(Element element, string valueToFind)
        {
            Synchronisation.WaitForPageToLoad();
            Synchronisation.WaitUntilObjectIsStale();
            DriverUtility.webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var tableContents = element.GetElement().FindElements(By.TagName("td"));
            DriverUtility.webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["Timeout"]));
            return tableContents.FirstOrDefault(item => item.Text.Contains(valueToFind));
        }

        //public static void RefreshPage()
        //{
        //    CommonMethods.RefreshPage();
        //}

        #region sandbox

        public static OpenQA.Selenium.IWebElement FindRowMatchingInputPatternInTable(string matchPattern, Element table)
        {
            Element rowObj = new Element("xpath", "//tr");
            var rows = rowObj.GetElementCollectionFromExistingObject(table.GetElement());
            var matchingRow = rows.FirstOrDefault(item =>
                                item.Text.ToLower().Contains(matchPattern.ToLower()));
            return matchingRow;
        }

        #endregion sandbox
    }

    [TestFixture]
    public class TestCommonMethodsUtility
    {
        [Test]
        public void TestGetFolderPath()
        {
            var folderName = CommonUtilityMethods.GetFolderPath("CodeUtilities");
        }
    }
}
