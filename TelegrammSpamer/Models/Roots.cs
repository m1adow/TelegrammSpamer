using OpenQA.Selenium;

namespace TelegrammSpamer.Models
{
    internal class Roots
    {
        public static By SendButton = By.XPath("//*[@id=\"MiddleColumn\"]/div[3]/div[2]/div/div[2]/div/button");
        public static By SendField = By.XPath("//*[@id=\"editable-message-text\"]");

        public static By? GetRecepient(IWebDriver webDriver, string name)
        {
            for (int i = 1; i < 50; i++)
            {
                By recepient = By.XPath($"//*[@id=\"LeftColumn-main\"]/div[2]/div/div/div/div/div/div/div[{i}]/div/div[2]/div[1]/h3");

                if (webDriver.FindElement(recepient).Text.Contains(name)) return By.XPath($"//*[@id=\"LeftColumn-main\"]/div[2]/div/div/div/div/div/div/div[{i}]/div");
            }
            return null;
        }
    }
}
