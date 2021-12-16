using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;


namespace BaseProject.PageElementLibrary
{
    public class DropDown : Element
    {
        public DropDown(string locator, string identifier) : base(locator, identifier)
        {
        }

        public void SelectValueInDropdown(string value)
        {
            try
            {
                var dropdownSelect = GetElement();

                SelectElement dropDownElement = new SelectElement(dropdownSelect);
                dropDownElement.SelectByText(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Error : Cannot select value ({0}) in dropdown (Locator: {1}, Identifer: {2}) ({3})", value, Locator, Identifier, e));
                throw new KeyNotFoundException(String.Format("Error : Cannot select value ({0}) in dropdown (Locator: {1}, Identifer: {2}) ({3})", value, Locator, Identifier, e));
            }
        }

        public string GetSelectedValue()
        {
            var selectElement = GetElement();
            SelectElement selectedValue = new SelectElement(selectElement);
            var text = selectedValue.SelectedOption.Text;
            return text;
        }

        public List<string> GetAllValues()
        {
            List<string> itemsInDropDown = new List<string>();

            var selectElement = GetElement();
            SelectElement selectedValue = new SelectElement(selectElement);
            var elements = selectedValue.Options;

            foreach (var element in elements)
            {
                itemsInDropDown.Add(element.Text);
            }

            return itemsInDropDown;
        }

        public void SelectValueInDropdownUsingIndex(int index)
        {
            try
            {
                var dropdownSelect = GetElement();

                SelectElement dropDownElement = new SelectElement(dropdownSelect);
                dropDownElement.SelectByIndex(index);
            }
            catch(Exception e)
            {
                Console.WriteLine(String.Format("Error : Cannot select value with index ({0}) in dropdown (Locator: {1}, Identifer: {2}) ({3})", index, Locator, Identifier, e));
                throw new KeyNotFoundException(String.Format("Error : Cannot select index ({0}) in dropdown (Locator: {1}, Identifer: {2}) ({3})", index, Locator, Identifier, e));

            }
        }
    }
}
