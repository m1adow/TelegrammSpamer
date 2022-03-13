using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TelegrammSpamer
{
    public partial class TelegramSpamer : Form
    {
        WebDriver? _webDriver;

        public TelegramSpamer()
        {
            InitializeComponent();
        }

        private void TelegramSpamer_Load(object sender, EventArgs e)
        {
            _webDriver = new ChromeDriver(); //open a browser

            _webDriver.Navigate().GoToUrl(@"https://web.telegram.org/"); //open telegram page
        }

        private void buttonSend_Click(object sender, EventArgs e) => SendMessage();

        private void SendMessage()
        {
            //if text boxes are empty exit from method
            /*if (IsTextBoxesEmpty())
            {
                MessageBox.Show("Text boxes must be filled", "Error", MessageBoxButtons.OK);
                return;
            }*/

            try
            {
                _webDriver.FindElement(By.XPath($@"//span[contains(text(), '{textBoxName.Text}')]")).Click();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private bool IsTextBoxesEmpty()
        {
            if (textBoxName.Text is null || textBoxMessage.Text is null || textBoxDelay.Text is null || textBoxCount.Text is null) return false;
            return true;    
        }
    }
}