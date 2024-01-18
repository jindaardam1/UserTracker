namespace User;

public static class DictionaryCreator
{ // TODO: Optimise the dictionary generation (someday)
    public static IEnumerable<string> CreateDictionary(bool useDots, bool useLowBars, string username, int[] interruptionIndex)
    {
        var dictionary = new List<string>();

        if (useDots)
        {
            AddDots(dictionary, username);
        }

        if (useLowBars)
        {
            AddLowBars(dictionary, username);
        }

        if (useDots && useLowBars)
        {
            AddDotsAndLowBars(dictionary, username);
        }

        if (interruptionIndex.Length > 0)
        {
            dictionary.Clear();
            AddToInterruptionIndex(dictionary, username, interruptionIndex);
        }

        return dictionary;
    }

    private static void AddDots(ICollection<string> dictionary, string username)
    {
        const int maxDots = 2;

        for (var i = 1; i <= maxDots; i++)
        {
            var usernameWithPrefixDots = new string('.', i) + username;

            for (var j = 1; j <= maxDots; j++)
            {
                var usernameWithDots = usernameWithPrefixDots + new string('.', j);

                dictionary.Add(usernameWithDots);
            }
        }
    }

    private static void AddLowBars(ICollection<string> dictionary, string username)
    {
        const int maxLowBars = 2;

        for (var i = 1; i <= maxLowBars; i++)
        {
            var usernameWithPrefixLowBars = new string('_', i) + username;

            for (var j = 1; j <= maxLowBars; j++)
            {
                var usernameWithLowBars = usernameWithPrefixLowBars + new string('_', j);

                dictionary.Add(usernameWithLowBars);
            }
        }
    }
    
    private static void AddDotsAndLowBars(ICollection<string> dictionary, string username)
    {
        const int maxSymbols = 3;

        for (var i = 1; i <= maxSymbols; i++)
        {
            var prefixDots = new string('.', i);

            for (var j = 1; j <= maxSymbols; j++)
            {
                var suffixDots = new string('.', j);

                dictionary.Add(prefixDots + username + suffixDots);

                dictionary.Add((prefixDots + username + suffixDots).Replace('.', '_'));
            }

            for (var k = 1; k <= maxSymbols; k++)
            {
                var suffixLowBars = new string('_', k);

                dictionary.Add(prefixDots + username + suffixLowBars);

                dictionary.Add((prefixDots + username + suffixLowBars).Replace('_', '.'));
            }
        }
    }

    private static void AddToInterruptionIndex(ICollection<string> dictionary, string username, int[] interruptionIndex)
    {
        const int maxCharacters = 2;

        var usernames = new List<string>();

        foreach (var index in interruptionIndex)
        {
            if (index >= 0 && index < username.Length)
            {
                for (var i = 1; i <= maxCharacters; i++)
                {
                    var prefix = username[..index];
                    var suffix = username[index..];

                    var newUsername = prefix + new string('_', i) + suffix;

                    dictionary.Add(newUsername);
                    usernames.Add(newUsername);

                    newUsername = prefix + new string('.', i) + suffix;

                    dictionary.Add(newUsername);
                    usernames.Add(newUsername);

                    newUsername = prefix + new string('_', i) + new string('.', i) + suffix;

                    dictionary.Add(newUsername);
                    usernames.Add(newUsername);
                }
            }
        }

        foreach (var un in usernames)
        {
            AddDotsAndLowBars(dictionary, un);
        }
    }
}