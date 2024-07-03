using System;
namespace Library.Shared.Validations
{
	public class ISBNValidator
	{
        public static bool IsValidISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                return false;
            }

            // Remove any hyphens or spaces
            isbn = isbn.Replace("-", "").Replace(" ", "");

            if (isbn.Length == 10)
            {
                return IsValidISBN10(isbn);
            }
            else if (isbn.Length == 13)
            {
                return IsValidISBN13(isbn);
            }

            return false;
        }

        private static bool IsValidISBN10(string isbn)
        {
            if (isbn.Length != 10)
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                if (!char.IsDigit(isbn[i]))
                {
                    return false;
                }

                sum += (i + 1) * (isbn[i] - '0');
            }

            char lastChar = isbn[9];
            if (lastChar != 'X' && !char.IsDigit(lastChar))
            {
                return false;
            }

            sum += lastChar == 'X' ? 10 : (lastChar - '0');

            return sum % 11 == 0;
        }

        private static bool IsValidISBN13(string isbn)
        {
            if (isbn.Length != 13 || !isbn.All(char.IsDigit))
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = isbn[i] - '0';
                sum += (i % 2 == 0) ? digit : 3 * digit;
            }

            int checksum = 10 - (sum % 10);
            if (checksum == 10)
            {
                checksum = 0;
            }

            return checksum == (isbn[12] - '0');
        }
    }
}