using Application.Common.Interfaces.Authentication;
using System.Security.Cryptography;

namespace Infrastructure.services.Authentication
{
    public class PasswordGeneratorService : IPasswordGenerator
    {

        private const string Upper =
           "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private const string Lower =
            "abcdefghijklmnopqrstuvwxyz";

        private const string Numbers =
            "0123456789";

        private const string Special =
            "!@#$%^&*()_-+=<>?";

        private static readonly string All =
            Upper + Lower + Numbers + Special;
        public string Generate()
        {
            const int length = 12;

            if (length < 8)
                throw new ArgumentException("Password length must be at least 8.");

            var password = new char[length];

            password[0] = Upper[RandomNumberGenerator.GetInt32(Upper.Length)];
            password[1] = Lower[RandomNumberGenerator.GetInt32(Lower.Length)];
            password[2] = Numbers[RandomNumberGenerator.GetInt32(Numbers.Length)];
            password[3] = Special[RandomNumberGenerator.GetInt32(Special.Length)];

            for (int i = 4; i < length; i++)
            {
                password[i] = All[
                    RandomNumberGenerator.GetInt32(All.Length)];
            }

            Shuffle(password);

            return new string(password);
        }
        private static void Shuffle(char[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i + 1);

                (array[i], array[j]) =
                    (array[j], array[i]);
            }
        }

    }
}
