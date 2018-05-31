using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class RegestrationHelper : HelperBase
    {
        public RegestrationHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            //открываем главную страницу
            OpenMainPage();
            //ооткрываем форму регистрации
            OpenRegestrationForm();
            //заполняем форму
            FillRegesctrationForm(account);
            //подтверждаем
            SubmitRegestration();


        }

        private void OpenRegestrationForm()
        {
            driver.FindElements(By.CssSelector("span.braked-link"))[0].Click();
        }

        private void SubmitRegestration()
        {
            driver.FindElement(By.CssSelector("input.button")).Click();
        }

        private void FillRegesctrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        private void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-1.2.17/login_page.php";
        }
    }
}
