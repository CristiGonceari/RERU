using System;
using System.Collections.Generic;
using System.Linq;

namespace CODWER.RERU.Core.Application.Common.Services.PasswordGenerator
{
    public class PasswordGenerator : IPasswordGenerator
    {
        private int _minimumLengthPassword;
        private int _maximumLengthPassword;
        private int _minimumLowerCaseChars;
        private int _minimumUpperCaseChars;
        private int _minimumNumericChars;
        private int _minimumSpecialChars;

        private readonly string _allLowerCaseChars;
        private readonly string _allUpperCaseChars;
        private readonly string _allNumericChars;
        private readonly string _allSpecialChars;
        private string _allAvailableChars;

        private readonly RandomSecureVersion _randomSecure = new RandomSecureVersion();
        private int _minimumNumberOfChars;

        public PasswordGenerator()
        {
            // Ranges not using confusing characters
            _allLowerCaseChars = GetCharRange('a', 'z', exclusiveChars: "ilo");
            _allUpperCaseChars = GetCharRange('A', 'Z', exclusiveChars: "IO");
            _allNumericChars = GetCharRange('2', '9');
            _allSpecialChars = "!@#%*()$?+-=";
            InitPasswordGenerator(8, 15, 1, 1, 1, 1);

        }

        public void InitPasswordGenerator(int minimumLengthPassword = 8, int maximumLengthPassword = 15, int minimumLowerCaseChars = 1,
                                  int minimumUpperCaseChars = 1, int minimumNumericChars = 1, int minimumSpecialChars = 1)
        {
            if (minimumLengthPassword < 1)
            {
                throw new ArgumentException("The minimumlength is smaller than 1.", "minimumLengthPassword");
            }

            if (minimumLengthPassword > maximumLengthPassword)
            {
                throw new ArgumentException("The minimumLength is bigger than the maximum length.", "minimumLengthPassword");
            }

            if (minimumLowerCaseChars < 0)
            {
                throw new ArgumentException("The minimumLowerCase is smaller than 0.", "minimumLowerCaseChars");
            }

            if (minimumUpperCaseChars < 0)
            {
                throw new ArgumentException("The minimumUpperCase is smaller than 0.", "minimumUpperCaseChars");
            }

            if (minimumNumericChars < 0)
            {
                throw new ArgumentException("The minimumNumeric is smaller than 0.", "minimumNumericChars");
            }

            if (minimumSpecialChars < 0)
            {
                throw new ArgumentException("The minimumSpecial is smaller than 0.", "minimumSpecialChars");
            }

            _minimumNumberOfChars = minimumLowerCaseChars + minimumUpperCaseChars + minimumNumericChars + minimumSpecialChars;

            if (minimumLengthPassword < _minimumNumberOfChars)
            {
                throw new ArgumentException(
                        "The minimum length ot the password is smaller than the sum " +
                        "of the minimum characters of all catagories.",
                        "maximumLengthPassword");
            }

            _minimumLengthPassword = minimumLengthPassword;
            _maximumLengthPassword = maximumLengthPassword;

            _minimumLowerCaseChars = minimumLowerCaseChars;
            _minimumUpperCaseChars = minimumUpperCaseChars;
            _minimumNumericChars = minimumNumericChars;
            _minimumSpecialChars = minimumSpecialChars;

            _allAvailableChars =
            OnlyIfOneCharIsRequired(minimumLowerCaseChars, _allLowerCaseChars) +
            OnlyIfOneCharIsRequired(minimumUpperCaseChars, _allUpperCaseChars) +
            OnlyIfOneCharIsRequired(minimumNumericChars, _allNumericChars) +
            OnlyIfOneCharIsRequired(minimumSpecialChars, _allSpecialChars);
        }

        private string OnlyIfOneCharIsRequired(int minimum, string allChars)
        {
            return minimum > 0 || _minimumNumberOfChars == 0 ? allChars : string.Empty;
        }

        public string Generate()
        {
            return $"{RandomUppercase(1)}{RandomLowercase(5)}{RandomNumber(1)}{RandomSpecialChar(1)}";
        }

        public string RandomUppercase(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public string RandomLowercase(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public string RandomSpecialChar(int length)
        {
            const string chars = "!@#$%^&*()";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        //public string Generate()
        //{
        //    var lengthOfPassword = _randomSecure.Next(_minimumLengthPassword, _maximumLengthPassword);

        //    // Get the required number of characters of each catagory and 
        //    // add random charactes of all catagories
        //    var minimumChars = GetRandomString(_allLowerCaseChars, _minimumLowerCaseChars) +
        //                GetRandomString(_allUpperCaseChars, _minimumUpperCaseChars) +
        //                GetRandomString(_allNumericChars, _minimumNumericChars) +
        //                GetRandomString(_allSpecialChars, _minimumSpecialChars);
        //    var rest = GetRandomString(_allAvailableChars, lengthOfPassword - minimumChars.Length);
        //    var unshuffeledResult = minimumChars + rest;

        //    // Shuffle the result so the order of the characters are unpredictable
        //    var result = unshuffeledResult.ShuffleTextSecure();
        //    return result;
        //}

        private string GetRandomString(string possibleChars, int lenght)
        {
            var result = string.Empty;
            for (var position = 0; position < lenght; position++)
            {
                var index = _randomSecure.Next(possibleChars.Length);
                result += possibleChars[index];
            }
            return result;
        }

        private static string GetCharRange(char minimum, char maximum, string exclusiveChars = "")
        {
            var result = string.Empty;
            for (char value = minimum; value <= maximum; value++)
            {
                result += value;
            }
            if (!string.IsNullOrEmpty(exclusiveChars))
            {
                var inclusiveChars = result.Except(exclusiveChars).ToArray();
                result = new string(inclusiveChars);
            }
            return result;
        }

        public string RandomEmailCode()
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 4).Select(s => s[_randomSecure.Next(s.Length)]).ToArray());
        }
    }

    internal static class Extensions
    {
        private static readonly Lazy<RandomSecureVersion> RandomSecure = new Lazy<RandomSecureVersion>(() => new RandomSecureVersion());
        public static IEnumerable<T> ShuffleSecure<T>(this IEnumerable<T> source)
        {
            var sourceArray = source.ToArray();
            for (int counter = 0; counter < sourceArray.Length; counter++)
            {
                int randomIndex = RandomSecure.Value.Next(counter, sourceArray.Length);
                yield return sourceArray[randomIndex];

                sourceArray[randomIndex] = sourceArray[counter];
            }
        }

        public static string ShuffleTextSecure(this string source)
        {
            var shuffeldChars = source.ShuffleSecure().ToArray();
            return new string(shuffeldChars);
        }
    }
}