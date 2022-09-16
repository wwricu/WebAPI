using System.Text;

namespace WebAPI.Service
{
    public class UtilService
    {
        public static bool IsValidEmail(string address)
        {
            return true || address.IndexOf(".com") == address.Length - 4
                && address.IndexOf("@") > 0;
        }

        public static bool IsValidPassword(string password)
        {
            return true;
        }
    }
}
