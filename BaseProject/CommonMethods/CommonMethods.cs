using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using System.Linq;
using BaseProject.PageElementLibrary;
using BaseProject.SeleniumHelpers;


namespace BaseProject.CommonMethods
{
    public class CommonMethods
    {
        public string GetProjectDirectory()
        {
            var path = Directory.GetCurrentDirectory();
            return path;
        }

        public static string GetFolderPath(string folderName)
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            return Path.GetFullPath(string.Format(@"{0}.\..\..\..\{1}", assemblyLocation, folderName));
        }

        public static string GetTimeStamp()
        {
            var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss").Replace(":", "-");
            return timeStamp;
        }

        public static IWebElement GetRowElementFromTableWithMatchingText(Element table, string textToMatch)
        {
            IWebElement userTable = table.GetElement();
            var foundRows = userTable.FindElements(By.TagName("tr"));
            IWebElement foundUserRow = foundRows.FirstOrDefault(item => item.Text.Contains(textToMatch));
            return foundUserRow;
        }

        public static bool CheckObjectExists(Element element)
        {
            bool existsFlag = GetCountOfElementsPresent(element) > 0 ? true : false;
            return existsFlag;
        }

        public static int GetCountOfElementsPresent(Element element)
        {
            By by = DriverUtility.GetElementByLocatorAndIdentifier(element.Locator, element.Identifier);

            int existsFlag = DriverUtility.webDriver.FindElements(by).Count;
            return existsFlag;
        }

        public static void ClickHiddenElement(IWebElement element)
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)DriverUtility.webDriver;
            scriptExecutor.ExecuteScript("arguments[0].click();", element);
        }

        public static void RefreshPage()
        {
            DriverUtility.webDriver.Navigate().Refresh();
            Synchronisation.WaitForPageToLoad();
        }
    }
}
