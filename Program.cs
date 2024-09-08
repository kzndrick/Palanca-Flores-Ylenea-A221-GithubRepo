using System;

class Tokenizer
{
    // Define delimiters as a char array
    private static char[] delimiters = { ' ', ',', '.', '!', '?', ';', '/', '-', '\n' };

    // Phase 1 Tokenize and classify each token
    public static void Tokenize(string input)
    {
        Console.WriteLine("------------- Phase 1: --------------");
        Console.WriteLine();

        string token = string.Empty;

        foreach (char ch in input)
        {
            if (IsDelimiter(ch))
            {
                // If a token is accumulated, classify and print it
                if (!string.IsNullOrWhiteSpace(token))
                {
                    string tokenType = ClassifyToken(token);
                    if (!string.IsNullOrEmpty(tokenType))
                    {
                        Console.WriteLine($"Token: \"{token}\" - Type: {tokenType}");
                    }
                    token = string.Empty; // Reset token after processing
                }

                // classify and print the delimiter
                if (ch == '\n')
                {
                    Console.WriteLine("Token: \"\\n\" - Type: End of Line");
                }
                else
                {
                    string delimiterTokenType = ClassifyToken(ch.ToString());
                    if (!string.IsNullOrEmpty(delimiterTokenType))
                    {
                        Console.WriteLine($"Token: \"{ch}\" - Type: {delimiterTokenType}");
                    }
                }
            }
            else
            {
                // Accumulate characters to form a token
                token += ch;
            }
        }

        // Process the last token if any remains
        if (!string.IsNullOrWhiteSpace(token))
        {
            string tokenType = ClassifyToken(token);
            if (!string.IsNullOrEmpty(tokenType))
            {
                Console.WriteLine($"Token: \"{token}\" - Type: {tokenType}");
            }
        }

        // Call Phase 2 for granular breakdown
        Console.WriteLine("\n------------- Phase 2: --------------");
        Phase2Breakdown(input);
    }

    // Check if the character is a delimiter
    public static bool IsDelimiter(char ch)
    {
        foreach (char delimiter in delimiters)
        {
            if (ch == delimiter)
            {
                return true;
            }
        }
        return false;
    }

    // Classifiers
    public static string ClassifyToken(string token)
    {
        if (double.TryParse(token, out _))
        {
            return "Number";
        }
        else if (IsWord(token))
        {
            return "Word";
        }
        else if (IsAlphanumeric(token))
        {
            return "Alphanumeric";
        }
        else if (IsPunctuation(token))
        {
            return "Punctuation";
        }
        return string.Empty;  // Ignore unclassified tokens
    }

    // Check if a token is a word (letters only)
    public static bool IsWord(string token)
    {
        foreach (char c in token)
        {
            if (!char.IsLetter(c))
            {
                return false;
            }
        }
        return true;
    }

    // Check if a token is alphanumeric (letters and digits)
    public static bool IsAlphanumeric(string token)
    {
        foreach (char c in token)
        {
            if (!char.IsLetterOrDigit(c))
            {
                return false;
            }
        }
        return true;
    }

    // Check if a token is punctuation (non-alphanumeric, like .,!?;-)
    public static bool IsPunctuation(string token)
    {
        if (token.Length == 1)
        {
            char c = token[0];
            return c == '.' || c == ',' || c == '!' || c == '?' || c == ';' || c == '/' || c == '-';
        }
        return false;
    }

    // Phase 2: Break down tokens into granular components (characters)
    public static void Phase2Breakdown(string input)
    {
        string token = string.Empty;

        foreach (char ch in input)
        {
            if (IsDelimiter(ch))
            {
                if (!string.IsNullOrWhiteSpace(token))
                {
                    Console.Write($"Token: \"{token}\" -> ");
                    foreach (char c in token)
                    {
                        Console.Write($"'{c}', ");
                    }
                    Console.WriteLine();
                    token = string.Empty;
                }

                if (ch == '\n')
                {
                    Console.WriteLine("Token: \"\\n\" -> '\\n', ");
                }
                else
                {
                    Console.Write($"Token: \"{ch}\" -> '{ch}', ");
                    Console.WriteLine();
                }
            }
            else
            {
                token += ch;
            }
        }

        // Process the last token if any remains
        if (!string.IsNullOrWhiteSpace(token))
        {
            Console.Write($"Token: \"{token}\" -> ");
            foreach (char c in token)
            {
                Console.Write($"'{c}', ");
            }
            Console.WriteLine();
        }
    }

    static void Main(string[] args)
    {
        // Test input string with alphanumeric and newline characters
        string inputText = "";

        // Start tokenizing
        Tokenize(inputText);
    }
}
