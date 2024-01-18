using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace User;

public class TwitterChecker
{
    public static bool CheckTwitterUsername(string username)
    {
        using (var driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl($"https://twitter.com/{username}");

            Thread.Sleep(2000);

            try
            {
                _ = driver.FindElement(By.CssSelector(".r-4qtqp9.r-yyyyoo.r-1xvli5t.r-dnmrzs.r-bnwqim.r-1plcrui.r-lrvibr.r-1bwzh9t.r-1d4mawv"));

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}