using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TelegrammSpamer.Models;

namespace TelegrammSpamer
{
    public partial class TelegramSpamer : Form
    {
        IWebDriver? _webDriver;

        public TelegramSpamer()
        {
            InitializeComponent();
        }

        private void TelegramSpamer_Load(object sender, EventArgs e)
        {
            _webDriver = new ChromeDriver(); //open a browser

            _webDriver.Navigate().GoToUrl(@"https://web.telegram.org/z/"); //open telegram page
        }

        private void buttonSend_Click(object sender, EventArgs e) => SendMessage();

        private void SendMessage()
        {
            //if text boxes are empty exit from method
            if (IsTextBoxesEmpty())
            {
                MessageBox.Show("Text boxes must be filled", "Error", MessageBoxButtons.OK);
                return;
            }

            try
            {
                _webDriver.FindElement(Roots.GetRecepient(_webDriver, textBoxName.Text)).Click();

                for (int i = 0; i < int.Parse(textBoxCount.Text); i++)
                {
                    _webDriver.FindElement(Roots.SendField).SendKeys(textBoxMessage.Text);
                    _webDriver.FindElement(Roots.SendButton).Click();
                    Thread.Sleep(TimeSpan.FromSeconds(double.Parse(textBoxDelay.Text)));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private bool IsTextBoxesEmpty()
        {
            if (textBoxName.Text is null || textBoxMessage.Text is null || textBoxDelay.Text is null || textBoxCount.Text is null) return true;
            return false;    
        }
    }
}