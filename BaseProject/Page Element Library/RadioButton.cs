using OpenQA.Selenium;

namespace BaseProject.PageElementLibrary
{
    public class RadioButton : Element
    {
        public RadioButton(string locator, string identifier) : base(locator, identifier)
        {
        }

        public bool Checked
        {
            get => IsRadioButtonChecked();
            set => RadioButtonAction(value);
        }

        private void RadioButtonAction(bool checkTheRadioButton)
        {
            bool radioButtonChecked = IsRadioButtonChecked();
            IWebElement radioButtonElement = GetElement();

            if (checkTheRadioButton)
            {
                if (!radioButtonChecked)
                    radioButtonElement.Click();
            }
            else
            {
                if (radioButtonChecked)
                    radioButtonElement.Click();
            }
        }

        private bool IsRadioButtonChecked()
        {
            IWebElement radioButtonElement = GetElement();
            return radioButtonElement.Selected;
        }
    }
}
