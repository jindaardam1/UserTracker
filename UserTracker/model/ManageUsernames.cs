public static class UserManager
    {
        public static IEnumerable<string> CreateUserList(string users)
        {
            if (string.IsNullOrWhiteSpace(users))
            {
                yield break;
            }

            var usernames = users.Split(';').Select(username => GetFormattedUsername(username.Trim()));

            foreach (var username in usernames)
            {
                yield return username;
            }
        }

        private static string GetFormattedUsername(string username)
        {
            return username.Contains('@') ? username.Split('@').FirstOrDefault() ?? string.Empty : username;
        }
    }