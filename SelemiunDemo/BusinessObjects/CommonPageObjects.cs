using BaseProject.PageElementLibrary;
using BaseProject.SeleniumHelpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProject.BusinessObjects
{
    public class CommonPageObjects
    {
        private BrowserUtility browser = new BrowserUtility();

        private Element rowSelector = new Element("cssSelector", "[data-testid='paging-select-page-size']");
        private Element optionSelector = new Element("cssSelector", "[data-popper-placement='top-start']");
        private Element loggedInUser = new Element("cssSelector", "[data-testid='user']");

        public void VerifyFieldsAndUIControlsOnTab(string tabName, string pageName)
        {
            RunUIAndFieldVerificationMethodForInputTabOnPage(pageName, tabName, "PageObjects");
        }

        public void VerifyPageIsDisplayed(string pageName, string parentPage = "")
        {
            RunPageVerificationMethodForInputPage(pageName, "PageObjects", "PageObjectVerification", parentPage);
        }

        public void VerifyFieldsAndUIControlsOnDialog(string dialogName, string pageName)
        {
            RunUIAndFieldVerificationMethodForInputDialogOnPage(pageName, dialogName, "PageObjects");
        }

        public void VerifyFieldsAndUIControlsOnPage(string pageName)
        {
            RunPageVerificationMethodForInputPage(pageName, "PageObjects", "VerifyFieldsAndUIControlsOnPage");
        }

        public void RunPageVerificationMethodForInputPage(string pageName, string namespaceStr, string methodType, string parentPage = "")
        {
            pageName = pageName.Replace(" ", "");
            parentPage = parentPage.Replace(" ", "");
            string className = parentPage == "" ? pageName : parentPage;
            Type pageObjectClass = GetPageObjectClassForPageOrTabNameInput(className, namespaceStr);
            object objPageObjectClass = Activator.CreateInstance(pageObjectClass);

            string classNameForMethod = parentPage == "" ? "" : pageName;
            MethodInfo validationMethod = GetMethodFromClass(pageObjectClass, methodType, classNameForMethod);
            validationMethod.Invoke(objPageObjectClass, null);
        }

        public void RunUIAndFieldVerificationMethodForInputTabOnPage(string pageName, string tabName, string namespaceStr)
        {
            pageName = pageName.Replace(" ", "");
            Type pageObjectClass = GetPageObjectClassForPageOrTabNameInput(pageName, namespaceStr);
            object objPageObjectClass = Activator.CreateInstance(pageObjectClass);
            tabName = tabName.Replace(" ", "") + "TabUIVerification";
            MethodInfo validationMethod = GetMethodFromClass(pageObjectClass, tabName, "");
            validationMethod.Invoke(objPageObjectClass, null);
        }

        public void RunUIAndFieldVerificationMethodForInputDialogOnPage(string pageName, string dialogName, string namespaceStr)
        {
            pageName = pageName.Replace(" ", "");
            Type pageObjectClass = GetPageObjectClassForPageOrTabNameInput(pageName, namespaceStr);
            object objPageObjectClass = Activator.CreateInstance(pageObjectClass);
            dialogName = dialogName.Replace(" ", "") + "DialogVerification";
            MethodInfo validationMethod = GetMethodFromClass(pageObjectClass, dialogName, "");
            validationMethod.Invoke(objPageObjectClass, null);
        }

        internal void RefreshBrowser()
        {
            DriverUtility.RefreshBrowser();
            Synchronisation.WaitForPageToLoad();
            Synchronisation.WaitUntilObjectIsStale();
        }

        public void GetTabForNavigation(string tabName, string pageName)
        {
            Synchronisation.WaitForPageToLoad();
            Synchronisation.WaitUntilObjectIsStale();

            var pageObjectClass = GetPageObjectClassForPageOrTabNameInput(pageName, "PageObjects");
            object instanceObj = Activator.CreateInstance(pageObjectClass);

            tabName = tabName.Replace(" ", "") + "Tab";
            FieldInfo member = GetFieldMemberFromClass(pageObjectClass, tabName);
            dynamic field = member.GetValue(instanceObj);
            field.ClickElement();
            Synchronisation.WaitForPageToLoad();
        }

        internal void VerifyLandingPageIsDisplayed(string landingPage)
        {
            RunPageVerificationMethodForInputPage(landingPage, "PageObjects", "LandingPageVerification");
        }

        internal Type GetPageObjectClassForPageOrTabNameInput(string pageName, string namespaceString)
        {
            pageName = pageName.Replace(" ", "");
            Type pageObjectClass = Assembly.GetExecutingAssembly().GetTypes()
                                .FirstOrDefault(item => item.Namespace.Contains(namespaceString) &&
                                                item.IsClass &&
                                                item.FullName.ToLower().Contains(pageName.ToLower())
                                              );
            pageObjectClass.Should().NotBeNull();
            return pageObjectClass;
        }

        internal FieldInfo GetFieldMemberFromClass(Type pageObjectClass, string fieldName)
        {
            FieldInfo member = pageObjectClass.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                                    .FirstOrDefault(item =>
                                    item.Name.ToLower().Contains(fieldName.ToLower()));
            member.Should().NotBeNull();
            return member;
        }

        internal MethodInfo GetMethodFromClass(Type pageObjectClass, string methodName, string className = "")
        {
            MethodInfo validationMethod = pageObjectClass.GetMethods()
                                              .FirstOrDefault(item =>
                                              item.Name.ToLower().Contains(methodName.ToLower()) &&
                                              item.Name.ToLower().Contains(className.ToLower()));
            validationMethod.Should().NotBeNull();
            return validationMethod;
        }

        internal void GetTableForSelectionOfRow(string tableName, string pageName, string rowNumber, string rowItemToSelect)
        {
            pageName = pageName.Replace(" ", "");
            Type pageObjectClass = GetPageObjectClassForPageOrTabNameInput(pageName, "PageObject");
            object objPageObjectClass = Activator.CreateInstance(pageObjectClass);
            var methodName = "SelectEntryOn" + tableName.Replace(" ", "") + "Table";
            MethodInfo validationMethod = GetMethodFromClass(pageObjectClass, methodName, "");
            validationMethod.Invoke(objPageObjectClass, new object[] { rowNumber, rowItemToSelect });
        }

     
        public string GetLoggedInUser()
        {
            var userName = loggedInUser.GetElementTextValue();
            return userName;
        }

        public void WaitForTime(int time)
        {
            time = time * 1000;
            Thread.Sleep(time);
        }
    }
}
