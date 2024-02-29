using System.Security.Cryptography;
using System.Text;

namespace Bono.Employees.Infrastructure.Utils
{
    public class Security
    {
        public string EncryptPassword(string password)
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();

            byte[] encryptedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                stringBuilder.Append(caracter.ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
