using OpenQA.Selenium;


namespace BaseProject.PageElementLibrary
{
    public class CheckBox : Element
    {
        public CheckBox(string locator, string identifier) : base(locator, identifier)
        {
        }

        public bool Checked
        {
            get => IsCheckBoxChecked();
            set => CheckBoxAction(value);
        }

        public void CheckBoxAction(bool checkTheCheckBox)
        {
            bool checkBoxChecked = IsCheckBoxChecked();
            IWebElement checkBoxElement = GetElement();

            if (checkTheCheckBox)
            {
                if (!checkBoxChecked)
                    checkBoxElement.Click();
            }
            else
            {
                if (checkBoxChecked)
                    checkBoxElement.Click();
            }
        }

        public void CheckBoxAction(string checkTheCheckBox)
        {
            bool checkBoxChecked = IsCheckBoxChecked();
            IWebElement checkBoxElement = GetElement();

            if (checkTheCheckBox.ToLower() == "check")
            {
                if (!checkBoxChecked)
                    checkBoxElement.Click();
            }
            else
            {
                if (checkBoxChecked)
                    checkBoxElement.Click();
            }
        }


        private bool IsCheckBoxChecked()
        {
            IWebElement checkBoxElement = GetElement();
            return checkBoxElement.Selected;
        }
    }
}

