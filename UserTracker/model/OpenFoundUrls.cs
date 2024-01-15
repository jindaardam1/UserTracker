using System.Diagnostics;
using System.IO;

public static class OpenFoundUrls
{
    public static void OpenUrls(string username)
    {
        var urls = new[] { GetTwitterUrl(username), GetInstagramUrl(username) };

        OpenUrlsInChrome(urls);
    }
    
    private static void OpenUrlsInChrome(string[] urls)
    {
        try
        {
            Process.Start(FindChromePath(), $"--new-window {string.Join(" ", urls)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening URLs in Google Chrome: {ex.Message}");
        }
    }
    
    private static string FindChromePath()
    {
        string[] commonPaths = {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google", "Chrome", "Application", "chrome.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Google", "Chrome", "Application", "chrome.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google", "Chrome", "Application", "chrome.exe")
        };

        return commonPaths.FirstOrDefault(File.Exists) ?? "";
    }

    private static string GetTwitterUrl(string username)
    {
        return $"https://twitter.com/{username}";
    }
    
    private static string GetInstagramUrl(string username)
    {
        return $"https://instagram.com/{username}";
    }
}