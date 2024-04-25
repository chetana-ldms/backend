using LDP.Common.Helpers.Interfaces;
using System.Text;

namespace LDP.Common.Helpers.Security
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string GeneratePassword(int numberofCharactors)
        {
            if (numberofCharactors == 0) numberofCharactors = 10;

            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+{}|[]:;<>,.?/~";

            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < numberofCharactors; i++)
            {
                int randomIndex = random.Next(0, validChars.Length);
                sb.Append(validChars[randomIndex]);
            }

            return sb.ToString();

        }
    }
}
