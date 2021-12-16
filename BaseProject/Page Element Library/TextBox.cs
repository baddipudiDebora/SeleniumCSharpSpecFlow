using OpenQA.Selenium;

namespace BaseProject.PageElementLibrary
{
    public class TextBox : Element
    {
        public TextBox(string locator, string identifier) : base(locator, identifier)
        {
        }

        public void SetText(string input, bool clearTextBox = true)
        {
            if (clearTextBox)
                GetElement().Clear();

            GetElement().SendKeys(input);
        }

        public string GetText()
        {
            string result = string.Empty;

            result = GetElement().GetAttribute("value");

            return result;
        }

        public void ClearText()
        {
            GetElement().Clear();
        }


        /// <summary>
        /// Clears the textbox by sending backspaces to the element as <i>ClearText()</i> does not to always function as expected.
        /// </summary>
        public void ClearTextUsingBackspaces()
        {
            IWebElement textboxWE = this.GetElement();
            string currentText = textboxWE.GetAttribute("value");

            if (currentText.Length > 0)
            {
                textboxWE.SendKeys(Keys.Control + Keys.End);

                for (int i = 0; i < currentText.Length; i++)
                {
                    textboxWE.SendKeys(Keys.Backspace);
                }
            }
        }
    }
}
