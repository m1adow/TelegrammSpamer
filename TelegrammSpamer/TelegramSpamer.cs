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
            MessageBox.Show("First of all login in to Telegram account", "WARNING!", MessageBoxButtons.OK);

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

            //if text boxes wtih count and delay have letters exit from method
            if (IsTextBoxesContainLetters())
            {
                MessageBox.Show("Text boxes count and delay must have only digits", "Error", MessageBoxButtons.OK);
                return;
            }

            try
            {
                SendMessagesAsync(_webDriver);
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

        private bool IsTextBoxesContainLetters()
        {
            if (textBoxCount.Text.Any(c => char.IsLetter(c)) || textBoxDelay.Text.Any(c => char.IsLetter(c))) return true;
            return false;
        }

        private async void SendMessagesAsync(IWebDriver webDriver)
        {
            webDriver.FindElement(Roots.GetRecepient(webDriver, textBoxName.Text)).Click();

            await Task.Run(() =>
            {
                for (int i = 0; i < int.Parse(textBoxCount.Text); i++)
                {
                    webDriver.FindElement(Roots.SendField).SendKeys(textBoxMessage.Text);
                    webDriver.FindElement(Roots.SendButton).Click();
                    Thread.Sleep(TimeSpan.FromSeconds(double.Parse(textBoxDelay.Text)));
                }
            });
        }
    }
}