using OnlineShopAPI.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;

namespace OnlineShopAPI.Utils
{
    public class Helper
    {
        public static string Hash(string input)
        {
            using (var hasher = SHA256.Create())
            {
                byte[] hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                string res = "";
                foreach (var kv in hash)
                {
                    res += kv.ToString("x2");
                }
                return res;
            }
        }
        public static string GenerateJWT(User user)
        {
            string username = user.Username;
            string role = user.Role;
            using (var hmac = new HMACSHA256())
            {
                hmac.Key = Encoding.UTF8.GetBytes(Vars.SECRET);
                JsonObject rawHeader = new JsonObject
                {
                    { "alg", "HS256" },
                    { "typ", "JWT" }
                };
                JsonObject rawPayload = new JsonObject
                {
                    { ClaimTypes.Name, username },
                    { ClaimTypes.Role, role },
                    { ClaimTypes.Expired, DateTime.Now.AddMinutes(20) },
                };
                string header = Convert.ToBase64String(Encoding.UTF8.GetBytes(rawHeader.ToString()));
                string payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(rawPayload.ToString()));
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(header + payload));
                string signature = Convert.ToHexString(hash);
                return $"{header}.{payload}.{signature}";
            }
        }
        public static string GetValueFromToken(string token, string key)
        {
            string payload = token.Split('.')[1];
            string s = "";
            try
            {
                string decodedPayload = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
                s = decodedPayload;
            } catch
            {
                return s;
            }

            string line;
            int seperatorLength = 4;
            using (StringReader reader = new StringReader(s))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(key))
                    {
                        return line.Substring(line.IndexOf(key) + key.Length + seperatorLength).Trim(',').Trim('\"');
                    }
                }
            }
            return "not found";
        }
    }
}
