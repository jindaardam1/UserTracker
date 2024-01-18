using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace User;

public class InstagramChecker
{
    public static bool CheckInstagramUsername(string username)
    {
        using (var driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl($"https://instagram.com/{username}");

            Thread.Sleep(2000);

            try
            {
                _ = driver.FindElement(By.CssSelector(".x9f619.xjbqb8w.x168nmei.x13lgxp2.x5pf9jr.xo71vjh.x1uhb9sk.x1plvlek.xryxfnj.x1c4vz4f.x2lah0s.x1q0g3np.xqjyukv.x1oa3qoh.x6s0dn4.x78zum5.x1b8z93w.x1amjocr.xl56j7k"));

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}