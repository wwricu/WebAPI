using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Service
{
    public class SecurityService
    {
        public static string GetMD5Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            MD5 MD5Hash = MD5.Create();
            byte[] data = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new();

            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
