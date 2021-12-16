using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using BaseProject.SeleniumHelpers;

namespace BaseProject.PageElementLibrary
{
    public class ReactDropDown : Element
    {
        private readonly Element dropDownMenu = new Element("CssSelector", "div[data-testid='select-menu'],div[data-testid='menu'][role='menu']");
        private readonly Element dropDownMenuOptions = new Element("CssSelector", "div[role='menuitem']");

        public ReactDropDown(string locator, string identifier) : base(locator, identifier)
        {
        }

        public string GetSelectedValue()
        {
            return GetElement().GetAttribute("value");
        }

        public string GetSelectedValue(IWebElement ancestor)
        {
            return GetElement(ancestor).GetAttribute("value");
        }

        public List<string> GetAllValues()
        {
            return GetAllValues(null);
        }

        public List<string> GetAllValues(IWebElement ancestor)
        {
            IWebElement dropDownWE;

            if (ancestor == null)
            {
                dropDownWE = GetElement();
            }
            else
            {
                dropDownWE = GetElement(ancestor);
            }

            dropDownWE.Click();
            // Give those following along at home time to see the dropdown is displayed.
            Thread.Sleep(1000);

            // This assumes that there will only be one menu displayed in the browser at once. If if doesn't work, we need to tie it to the dropdown so how.
            ReadOnlyCollection<IWebElement> dropDownMenuOptionWEs = dropDownMenu.GetElement().FindElements(dropDownMenuOptions.GetElementByLocator());

            List<string> menuOptions = new List<string>();

            foreach (IWebElement dropDownMenuOptionWE in dropDownMenuOptionWEs)
            {
                //menuOptions.Add(dropDownMenuOptionWE.GetAttribute("innerText"));
                menuOptions.Add(dropDownMenuOptionWE.Text);
            }

            // Since we are only getting the dropdown values, close the dropdown.
            // DOES NOT WORK: Simply clicking the dropdown again, eg. 'Add Admin' > 'Details' > 'Time Zone'. 
            // dropDownWE.Click();
            // WORKS: Click to the left of the element to close the dropdown as the menu can be displayed above or below the dropdown.
            Actions action = new Actions(DriverUtility.webDriver);
            action.MoveToElement(dropDownWE, -1, 10);
            action.Click().Build().Perform();

            return menuOptions;
        }

        public void SelectValueInDropdown(string value)
        {
            SelectValueInDropdown(null, value);
        }

        public void SelectValueInDropdown(IWebElement ancestor, string value)
        {
            IWebElement dropDownWE;

            if (ancestor == null)
            {
                dropDownWE = GetElement();
            }
            else
            {
                dropDownWE = GetElement(ancestor);
            }

            dropDownWE.Click();

            // This assumes that there will only be one menu displayed in the browser at once. If if doesn't work, we need to tie it to the current dropdown some how.
            ReadOnlyCollection<IWebElement> dropDownMenuOptionWEs = dropDownMenu.GetElement().FindElements(dropDownMenuOptions.GetElementByLocator());
            IWebElement dropDownMenuOptionWE = dropDownMenuOptionWEs.FirstOrDefault(optionWE => optionWE.Text.Equals(value));

            if (dropDownMenuOptionWE != null)
            {
                dropDownMenuOptionWE.Click();
            }
            else
            {
                Console.WriteLine(string.Format("Error : Cannot select value ({0}) in dropdown (Locator: {1}, Identifer: {2})", value, Locator, Identifier));
                throw new KeyNotFoundException(string.Format("Error : Cannot select value ({0}) in dropdown (Locator: {1}, Identifer: {2})", value, Locator, Identifier));
            }
        }

        public void SelectValueInDropdownUsingIndex(int index)
        {
            SelectValueInDropdownUsingIndex(null, index);
        }

        public void SelectValueInDropdownUsingIndex(IWebElement ancestor, int index)
        {
            try
            {
                IWebElement dropDownWE;

                if (ancestor == null)
                {
                    dropDownWE = GetElement();
                }
                else
                {
                    dropDownWE = GetElement(ancestor);
                }

                dropDownWE.Click();

                ReadOnlyCollection<IWebElement> dropDownMenuOptionWEs = dropDownMenu.GetElement().FindElements(dropDownMenuOptions.GetElementByLocator());
                dropDownMenuOptionWEs[index].Click();
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Error : Cannot select value with index ({0}) in dropdown (Locator: {1}, Identifer: {2})", index, Locator, Identifier));
                throw new KeyNotFoundException(String.Format("Error : Cannot select index ({0}) in dropdown (Locator: {1}, Identifer: {2})", index, Locator, Identifier));
            }
        }
    }
}
