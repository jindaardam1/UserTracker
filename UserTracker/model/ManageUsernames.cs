public static class UserManager
    {
    public static List<string> CreateUserList(string users)
    {
        List<string> userList = new List<string>();

        if (string.IsNullOrWhiteSpace(users))
        {
            return userList;
        }

        var usernames = users.Split(';').Select(username => GetFormattedUsername(username.Trim()));

        userList.AddRange(usernames);

        return userList;
    }


    private static string GetFormattedUsername(string username)
        {
            return username.Contains('@') ? username.Split('@').FirstOrDefault() ?? string.Empty : username;
        }
    }