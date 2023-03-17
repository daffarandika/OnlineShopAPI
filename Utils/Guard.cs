using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShopAPI.Utils
{
    public class Guard
    {
        public static bool FailsAgainstInvalidToken(string token)
        {

            if (token.Substring(0, 7).ToLower() != "bearer ") return true;

            token = token.Substring(7);
            string[] arr = token.Split('.');

            if (arr.Length != 3) return true;

            string header = arr[0];
            string payload = arr[1];
            string signature = arr[2];
            string decodedPayload = Encoding.UTF8.GetString(Convert.FromBase64String(payload));

            if (!decodedPayload.Contains(ClaimTypes.Expired)) return true;

            using (var hmac = new HMACSHA256())
            {
                hmac.Key = Encoding.UTF8.GetBytes(Vars.SECRET);
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(header + payload));
                string newSignature = Convert.ToHexString(hash);
                return signature != newSignature;
            }
        }
        public static bool FailsAgainstExpiredToken(string token)
        {
            string[] arr = token.Split('.');

            if (arr.Length != 3) return true;

            string payload = arr[1];
            string decodedPayload = Encoding.UTF8.GetString(Convert.FromBase64String(payload));

            if (!(decodedPayload.Contains(ClaimTypes.Expired))) return true;

            DateTime exp = DateTime.Parse(Helper.GetValueFromToken(token, ClaimTypes.Expired));

            if (exp < DateTime.Now) return true;

            return false;
        }
    }
}
