using BaseProject.SeleniumHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using TechTalk.SpecFlow;


namespace BaseProject.Selenium_Helpers
{
    public class Reporting
    {
        private static Status logLevel = Status.Info;

        // During test case development, it might be valuable to log to the console when the various hooks are getting called.
        // I can't think of any good reason to log the hooks to the Extent Report.
        // NOTE: You will also need to set the 'LogLevel' in the app.config to 'Debug' for the hooks to be logged.
        private static bool logHooks = false;

        // Logging at the 'Feature' level adds a 'test case' to the Extent Report skewing the results. 
        // Only set this to 'true' if you want to include the feature level logging in the Extent Report for some reason.
        private static bool logFeatureLevel = false;

        private static ExtentReports extentReport;
        public static ExtentTest extentTest;
        private static string reportName;

        private static object syncLock = new object();

        private static Dictionary<string, string> addedSystemInfo = new Dictionary<string, string>();


        static Reporting()
        {
            CommonUtilityMethods.CreateDirectory(CommonUtilityMethods.GetProjectPath() + "ExtentReporting\\Images\\");
        }

        public static void AddSystemInfo(string name, string value)
        {
            if (!addedSystemInfo.ContainsKey(name))
            {
                extentReport.AddSystemInfo(name, value);
                addedSystemInfo.Add(name, value);
            }
        }

        public static void CreateFeature()
        {
            InitializeReport();

            if (logFeatureLevel)
                extentTest = extentReport.CreateTest(string.Format("<p style='background-color:#46BFBD'><i>FEATURE: {0}</i></p>", FeatureContext.Current.FeatureInfo.Title));
        }

        public static void CreateScenario()
        {
            var description = TestContext.CurrentContext.Test.Properties.Get("Description");

            if (description != null)
            {
                CreateScenario(description.ToString());
            }
            else
            {
                CreateScenario(TestContext.CurrentContext.Test.Name);
            }
        }

        public static void CreateScenario(string name)
        {
            extentTest = extentReport.CreateTest(string.Format("SCENARIO: {0}", name));
        }

        public static void Error(string errorMessage)
        {
            Error(errorMessage, "");
        }

        public static void Error(string errorMessage, Exception e)
        {
            Error(errorMessage, e.StackTrace.ToString());
        }

        public static void Error(string errorMessage, string stacktrace)
        {
            if (extentTest != null)
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("<p style='background-color:#FF6347'>");
                msg.Append("<b>").Append(errorMessage).Append("</b>");

                if (stacktrace != null && stacktrace != "")
                    msg.Append("<br><br>").Append(stacktrace);

                msg.Append("</p>");

                string screenshotPath = GetScreenshotPath();

                if (screenshotPath != null)
                {
                    extentTest.Error(msg.ToString().Replace("\n", "<br>"), MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                else
                {
                    extentTest.Error(msg.ToString().Replace("\n", "<br>"));
                }
            }
        }

        public static void Fail(string failureMessage)
        {
            Fail(failureMessage, null);
        }

        public static void Fail(string failureMessage, AssertionException e)
        {
            if (extentTest != null)
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("<p style='background-color:#F7464A'>");
                // Ensure that any '<' or '>' characters in the failure message, eg. <null>, are NOT considered HTML elements.
                msg.Append("<b>").Append(failureMessage.Replace("<", "&lt;").Replace(">", "&gt;")).Append("</b>");

                if (e != null)
                    msg.Append("<br><br>").Append(e.ToString().Replace("<", "&lt;").Replace(">", "&gt;"));

                msg.Append("</p>");

                string screenshotPath = GetScreenshotPath();

                if (screenshotPath != null)
                {
                    extentTest.Fail(msg.ToString().Replace("\n", "<br>"), MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                else
                {
                    extentTest.Fail(msg.ToString().Replace("\n", "<br>"));
                }
            }
        }

        public static void Flush()
        {
            extentReport.Flush();
        }

        private static string GetScreenshotPath()
        {
            return DriverUtility.CaptureScreenWithExtent(CommonUtilityMethods.GetFolderPath(@"ExtentReporting\Images") + @"\");
        }

        public static void InitializeReport()
        {
            if (extentReport == null)
            {
                string reportPath = CommonUtilityMethods.GetFolderPath("ExtentReporting");

                if (reportName == null)
                {
                    reportName = string.Format("{0}_{1}_{2}.html", FeatureContext.Current.FeatureInfo.Title.Replace(" > ", "_").Replace(" ", "_"), ConfigurationManager.AppSettings["Language"], CommonUtilityMethods.GetCurrentDateTime());
                }
                else
                {
                    if (reportName.Contains("[Language]"))
                    {
                        reportName = reportName.Replace("[Language]", ConfigurationManager.AppSettings["Language"]);
                    }

                    if (reportName.Contains("[DateTime]"))
                    {
                        reportName = reportName.Replace("[DateTime]", CommonUtilityMethods.GetCurrentDateTime());
                    }
                }

                lock (syncLock)
                {
                    ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath + "\\" + reportName);
                    htmlReporter.Configuration().DocumentTitle = TestContext.CurrentContext.Test.Name;
                    htmlReporter.Configuration().ReportName = TestContext.CurrentContext.Test.Name + " Report Name";
                    htmlReporter.AppendExisting = true;

                    extentReport = new ExtentReports();
                    extentReport.AttachReporter(htmlReporter);

                    extentReport.AddSystemInfo("Browser", ConfigurationManager.AppSettings["Browser"]);
                    extentReport.AddSystemInfo("Environment", ConfigurationManager.AppSettings["Environment"]);
                    extentReport.AddSystemInfo("Environment URL", ConfigurationManager.AppSettings[string.Format("GoIBiBo_{0}", ConfigurationManager.AppSettings["Environment"])]);
                }
            }
        }

        public static void Log(Status status, string message)
        {
            if (logLevel.CompareTo(status) > -1)
            {
                System.Console.WriteLine(status + ": " + message);

                if (extentTest != null)
                    extentTest.Log(status, message.Replace("\n", "<br>"));
            }
        }

        public static void LogHooks(string declaringTypeName, string methodName)
        {
            if (logHooks)
            {
                //System.Console.WriteLine(String.Format("{0}: {1}.{2}(): Executing...\nNUnit TestName: {3}, Result: {4}.",
                //    Status.Debug, declaringTypeName, methodName, TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Result.Outcome));

                string msg = String.Format("{0}.{1}(): Executing...\nNUnit TestName: {2}, Result: {3}.",
                        declaringTypeName, methodName, TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Result.Outcome);

                Log(Status.Debug, msg);
            }
        }

        public static void Pass(string passMessage)
        {
            if (extentTest != null)
            {
                // NOTE: Don't log to steps and results to the console as they are logged by the SpecFlow infrastructure.
                extentTest.Pass(String.Format("<p style='background-color:#00AF00'>{0}</p> ", passMessage.Replace("\n", "<br>")));
            }
        }

        public static void TestFail(string failureMessage, Exception e)
        {
            string msg = failureMessage;

            if (e != null)
                msg = failureMessage + "\n\n" + e.ToString();

            Fail(msg);
        }

        public static void TestPass(string passMessage)
        {
            Pass(passMessage);
        }

    }
}
