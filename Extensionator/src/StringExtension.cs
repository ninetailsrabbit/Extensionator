﻿using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Extensionator {
    public static partial class StringExtension {
        private static readonly Random _rng = new(DateTime.Now.Millisecond);

        public static readonly string HexCharacters = "0123456789ABCDEF";
        public static readonly string AsciiAphanumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static readonly string AsciiLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly string AsciiLowercase = "abcdefghijklmnopqrstuvwxyz";
        public static readonly string AsciiUppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly string AsciiDigits = "0123456789";
        public static readonly string AsciiPunctuation = "!\"#$%&'()*+, -./:;<=>?@[\\]^_`{|}~";
        public static readonly string[] EscapeCharacters = ["\r", "\n", "\t", "\v", @"\c", @"\e", "\f", "\a", "\b", "\\", @"\NNN", @"\xHH"];

        /// <summary>
        /// Checks if a string represents a valid absolute URL (HTTP or HTTPS).
        /// </summary>
        /// <param name="url">The string to validate as a URL.</param>
        /// <returns>True if the string is a valid absolute URL with scheme http or https, False otherwise.</returns>
        public static bool IsValidUrl(this string url)
            => Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);


        /// <summary>
        /// Removes BBCode tags from a string using a pre-compiled regular expression.
        /// </summary>
        /// <param name="bbcode">The string containing BBCode tags.</param>
        /// <returns>A new string with the BBCode tags stripped.</returns>
        public static string StripBBcode(this string bbcode) => StripBBCodeRegex().Replace(bbcode, "");

        /// <summary>
        /// Removes HTML tags from a string while preserving whitespace characters.
        /// </summary>
        /// <param name="html">The string potentially containing HTML tags to be stripped. (Can be null)</param>
        /// <returns>A new string with HTML tags removed, preserving whitespace.</returns>
        public static string StripHTML(this string? html) {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            html = StripHtmlRegex().Replace(html, string.Empty);

            return html.Replace("&nbsp;", " ", StringComparison.OrdinalIgnoreCase)
                       .Replace("&#160;", string.Empty, StringComparison.Ordinal);
        }

        public static bool IsNullOrEmpty(this string text) => string.IsNullOrEmpty(text);
        public static bool IsNullOrWhitespace(this string text) => string.IsNullOrWhiteSpace(text);

        /// <summary>
        /// Converts the specified string to title case using the specified culture (default is "en-US").
        /// </summary>
        /// <param name="text">The string to convert.</param>
        /// <param name="iso">The culture code (ISO format) used for title casing rules. Defaults to "en-US" (English - United States).</param>
        /// <returns>The converted string in title case.</returns>
        public static string ToTitleCase(this string text, string iso = "en-US") => new CultureInfo(iso).TextInfo.ToTitleCase(text);

        /// <summary>
        /// Converts the specified string to title case using the specified culture information.
        /// </summary>
        /// <param name="text">The string to convert.</param>
        /// <param name="cultureInfo">The culture information object that defines the title casing rules.</param>
        /// <returns>The converted string in title case.</returns>
        public static string ToTitleCase(this string text, CultureInfo cultureInfo) => cultureInfo.TextInfo.ToTitleCase(text);

        /// <summary>
        /// Converts a string to a slug (URL-friendly format) This is a title with spaces -> this-is-a-title-with-spaces.
        /// </summary>
        /// <param name="value">The string to be converted to a slug (can be null or empty).</param>
        /// <returns>A lowercase slug representing the provided string, or an empty string if the input is null or empty.</returns>
        public static string ToSlug(this string? value) {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var firstReplace = Regex.Replace(Encoding.ASCII.GetString(value.ToBytes()), @"\s{2,}|[^\w]", " ",
                RegexOptions.ECMAScript, TimeSpan.FromMilliseconds(100)).Trim();

            return Regex.Replace(firstReplace, @"\s+", "_", RegexOptions.None, TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
        }

        /// <summary>
        /// Truncates the specified string to the given maximum length and adds a suffix if necessary.
        /// </summary>
        /// <param name="text">The string to truncate.</param>
        /// <param name="maxLength">The maximum length of the resulting string (excluding the suffix).</param>
        /// <param name="suffix">The suffix to add to the truncated string (default is "...").</param>
        /// <returns>The truncated string with the suffix if necessary, otherwise the original string.</returns>
        public static string Truncate(this string text, int maxLength, string suffix = "...") {
            if (text.IsNullOrEmpty() || text.Length <= maxLength)
                return text;

            return string.Concat(text.AsSpan(0, maxLength - suffix.Length), suffix);
        }

        /// <summary>
        /// Compares two strings for equality, ignoring case differences.
        /// </summary>
        /// <param name="source">The first string to compare (extended on).</param>
        /// <param name="other">The second string to compare.</param>
        /// <returns>True if the strings are equal ignoring case, false otherwise.</returns>
        /// <remarks>
        /// This extension method provides a convenient way to compare strings without regard to case sensitivity. It leverages the `Equals` method with the `StringComparison.OrdinalIgnoreCase` flag. This comparison mode performs a case-insensitive ordinal (character-by-character) comparison.
        /// </remarks>
        public static bool EqualsIgnoreCase(this string source, string other)
            => source.Equals(other, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Compares two strings for equality, ignoring case differences.
        /// </summary>
        /// <param name="source">The first string to compare (extended on).</param>
        /// <param name="other">The second string to compare.</param>
        /// <returns>True if the strings are equal ignoring case, false otherwise.</returns>
        /// <remarks>
        /// This extension method offers a case-insensitive string comparison that considers the casing rules of the current culture. It achieves this by using the `Equals` method with the `StringComparison.CurrentCultureIgnoreCase` flag. This comparison mode takes into account the culture-specific casing rules, which might differ from `StringComparison.OrdinalIgnoreCase`.
        /// </remarks>
        public static bool EqualsCultureIgnoreCase(this string source, string other)
           => source.Equals(other, StringComparison.CurrentCultureIgnoreCase);

        /// <summary>
        /// Repeats a given string a specified number of times.
        /// </summary>
        /// <param name="value">The string to repeat.</param>
        /// <param name="count">The number of times to repeat the string.</param>
        /// <returns>A new string containing the repeated value.</returns>
        /// <remarks>
        /// This method uses a `StringBuilder` for efficient string concatenation when repeating a string multiple times.
        /// It avoids creating temporary strings in each iteration, improving performance.
        /// </remarks>
        public static string Repeat(this string value, int count)
            => new StringBuilder(value.Length * count).Insert(0, value, count).ToString();

        /// <summary>
        /// Repeats a given char a specified number of times.
        /// </summary>
        /// <param name="value">The char to repeat.</param>
        /// <param name="count">The number of times to repeat the char.</param>
        /// <returns>A new string containing the repeated value.</returns>
        /// <remarks>
        /// This method uses a `StringBuilder` for efficient string concatenation when repeating a string multiple times.
        /// It avoids creating temporary strings in each iteration, improving performance.
        /// </remarks>
        public static string Repeat(this char value, int count) => value.ToString().Repeat(count);


        /// <summary>
        /// Checks if all characters in a string are uppercase letters.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>True if all characters in the string are uppercase, false otherwise.</returns>
        /// <example>
        /// This example shows how to use the IsUpper method:
        /// <code>
        /// string text = "HELLO WORLD";
        /// bool isUpper = text.IsUpper();  // isUpper will be false
        /// 
        /// text = "ABCDEFG";
        /// isUpper = text.IsUpper();  // isUpper will be true
        /// </code>
        /// </example>

        public static bool IsUpper(this string value) => value.All(char.IsUpper);
        /// <summary>
        /// Checks if all characters in a string are lowercase letters.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>True if all characters in the string are lowercase, false otherwise.</returns>
        /// <example>
        /// This example shows how to use the IsLower method:
        /// <code>
        /// string text = "hello world";
        /// bool isLower = text.IsLower();  // isLower will be true
        /// 
        /// text = "This Has Mixed Case";
        /// isLower = text.IsLower();  // isLower will be false
        /// </code>
        /// </example>
        public static bool IsLower(this string value) => value.All(char.IsLower);

        /// <summary>
        /// Checks if a string contains at least one special character.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>True if the string contains at least one special character, false otherwise.</returns>
        /// <remarks>
        /// This method uses the `IsSpecialCharacter` method to check each character in the string.
        /// A special character is defined as any character included in the ASCII_PUNCTUATION set.
        /// </remarks>
        public static bool HasSpecialCharacter(this string value) => value.Trim().Any(IsSpecialCharacter);

        /// <summary>
        /// Checks if a character is considered a special character.
        /// </summary>
        /// <param name="value">The character to check.</param>
        /// <returns>True if the character is a special character, false otherwise.</returns>
        /// <remarks>
        /// This method checks if the character is included in the built-in `ASCII_PUNCTUATION` set.
        /// The `ASCII_PUNCTUATION` set contains various punctuation marks and symbols.
        /// </remarks>
        public static bool IsSpecialCharacter(this char value) => value.In(AsciiPunctuation);

        /// <summary>
        /// Checks if a string can be parsed into a valid integer.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>True if the string can be parsed as an integer, false otherwise.</returns>
        /// <remarks>
        /// This method uses `int.TryParse` to attempt to convert the string to an integer.
        /// It discards the output integer value using the discard underscore (`_`).
        /// If the parsing is successful (returns true), the string is considered a valid number.
        /// </remarks>
        public static bool IsNumber(this string value) => int.TryParse(value, out _);

        /// <summary>
        /// Checks if a string contains any escape characters from a predefined set.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>True if the string contains at least one escape character, false otherwise.</returns>
        /// <remarks>
        /// This method uses `ESCAPE_CHARACTERS` (assumed to be a static array of escape character strings)
        /// to check if any of the escape characters exist within the provided string.
        /// It utilizes the `Any` method from LINQ to perform the efficient check.
        /// </remarks>
        public static bool ContainsEscapeCharacters(this string value)
            => EscapeCharacters.Any(value.Contains);

        /// <summary>
        /// Removes all escape characters from a string based on a predefined set.
        /// </summary>
        /// <param name="value">The string from which to remove escape characters.</param>
        /// <returns>A new string with all escape characters removed, or the original string if none were found.</returns>
        /// <remarks>
        /// This method first checks if the string contains any escape characters using the `ContainsEscapeCharacters` method.
        /// If escape characters are present, it splits the string using the `ESCAPE_CHARACTERS` array and `StringSplitOptions.RemoveEmptyEntries`.
        /// Finally, it joins the split parts into a new string without the escape characters.
        /// If no escape characters are found, the original string is returned.
        /// </remarks>
        public static string RemoveEscapeCharacters(this string value) {
            if (value.ContainsEscapeCharacters())
                return string.Join("", value.Split(EscapeCharacters, StringSplitOptions.RemoveEmptyEntries));

            return value;
        }

        /// <summary>
        /// Shuffles the characters in a string using a thread-safe random number generator.
        /// </summary>
        /// <param name="str">The string to be shuffled.</param>
        /// <returns>A new string with the characters in a random order.</returns>
        /// <remarks>
        /// This extension method shuffles the characters in a string by iterating through each position.
        /// For each character, it uses a thread-safe random number generator (`_rng`) to select another position within the string.
        /// The characters at these two positions are then swapped using tuple deconstruction and assignment.
        /// Finally, a new string is created from the shuffled character array.
        /// </remarks>
        public static string Shuffle(this string str) {
            char[] charArray = str.ToCharArray();

            for (int i = 0; i < charArray.Length; i++) {
                int randomIndex = _rng.Next(i, charArray.Length);
                (charArray[randomIndex], charArray[i]) = (charArray[i], charArray[randomIndex]);
            }

            return new string(charArray);
        }


        /// <summary>
        /// Removes characters from a filename that are considered invalid by the operating system.
        /// </summary>
        /// <param name="filename">The filename from which to remove invalid characters.</param>
        /// <returns>A new string with invalid filename characters removed.</returns>
        /// <remarks>
        /// This extension method checks if the provided filename is null or empty. If so, it returns the original filename.
        /// Otherwise, it utilizes the `Path.GetInvalidFileNameChars` method to obtain a list of invalid characters for filenames.
        /// It then employs the `Aggregate` method with a lambda expression to iterate through each invalid character.
        /// Within the lambda, the `Replace` method is used to replace each invalid character with an empty string.
        /// The result is a new string with all invalid filename characters removed.
        /// </remarks>
        public static string RemoveInvalidFileNameCharacters(this string filename) {
            if (filename.IsNullOrEmpty())
                return filename;

            return Path.GetInvalidFileNameChars().Except(['*']).Aggregate(filename, (current, character) => current.Replace(character.ToString(), string.Empty));
        }

        /// <summary>
        /// Removes characters from a path that are considered invalid by the operating system.
        /// </summary>
        /// <param name="path">The path from which to remove invalid characters.</param>
        /// <returns>A new string with invalid path characters removed.</returns>
        /// <remarks>
        /// This extension method behaves similarly to `RemoveInvalidFileNameCharacters`.
        /// It checks for null or empty input and returns the original path if so.
        /// Otherwise, it retrieves a list of invalid path characters using `Path.GetInvalidPathChars`.
        /// The `Aggregate` method with a lambda expression is used to iterate and remove these characters using `Replace`.
        /// The final result is a new string with all invalid path characters removed.
        /// </remarks>
        public static string RemoveInvalidPathCharacters(this string path) {
            if (path.IsNullOrEmpty())
                return path;

            return Path.GetInvalidPathChars().Aggregate(path, (current, character) => current.Replace(character.ToString(), string.Empty));
        }


        /// <summary>
        /// Converts a string to a byte array representation using UTF-8 encoding.
        /// </summary>
        /// <param name="value">The string to be converted.</param>
        /// <returns>A byte array containing the UTF-8 encoded representation of the string.</returns>
        /// <remarks>
        /// This extension method assumes UTF-8 encoding, a common character encoding for text data.
        /// It utilizes the `Encoding.UTF8.GetBytes` method to convert the string into a byte array.
        /// UTF-8 is a widely used and compatible encoding that can represent a vast range of characters.
        /// However, if you require a different encoding for specific use cases, consider using the overload that allows specifying the encoding.
        /// </remarks>
        public static byte[] ToBytes(this string value) => Encoding.UTF8.GetBytes(value);

        /// <summary>
        /// Converts a string to a byte array representation using the specified encoding.
        /// </summary>
        /// <param name="value">The string to be converted.</param>
        /// <param name="encoding">The desired character encoding to use for the conversion.</param>
        /// <returns>A byte array containing the encoded representation of the string based on the provided encoding.</returns>
        /// <remarks>
        /// This extension method offers more flexibility by accepting an `Encoding` object as a parameter.
        /// This allows you to specify the character encoding that best suits your needs.
        /// It then employs the `encoding.GetBytes` method to perform the conversion based on the chosen encoding.
        /// Selecting the appropriate encoding is essential to ensure accurate representation and avoid data corruption when converting the byte array back to a string.
        /// </remarks>
        public static byte[] ToBytes(this string value, Encoding encoding) => encoding.GetBytes(value);



        #region RegularExpressions

        [GeneratedRegex(@"\[.+?\]")]
        private static partial Regex StripBBCodeRegex();

        [GeneratedRegex(@"[^\u0000-\u007F]", RegexOptions.Compiled)]
        private static partial Regex UnicodeRegex();

        [GeneratedRegex("<[^>]*>", RegexOptions.Compiled)]
        private static partial Regex StripHtmlRegex();
        #endregion
    }
}
