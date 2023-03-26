using System.Security.Cryptography;
using System.Text;
using SHA3.Net;

namespace Game
{
    public class HMAC
    {
        private string _key = "";

        public void GenerateKey()
        {
            var salt = new byte[32];
            RandomNumberGenerator.Create().GetBytes(salt);

            _key = ToHex(salt);
        }

        public string GenerateHash(string str)
        {
            var hash = Sha3.Sha3256().ComputeHash(Encoding.Default.GetBytes(str + _key));

            return ToHex(hash);
        }

        public void DisplaySaltAfterRound()
        {
            Console.WriteLine(Dialogs.Crypt.HMACKey + _key);
        }

        private static string ToHex(byte[] bytes)
        {
            var result = new StringBuilder(bytes.Length * 2);

            foreach (var currentByte in bytes)
            {
                result.Append(currentByte.ToString("x2"));
            }

            return result.ToString();
        }
    }
}
