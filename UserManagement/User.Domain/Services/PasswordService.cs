using System.Security.Cryptography;
using System.Text;
using User.Domain.Services.Interfaces;

namespace User.Domain.Services
{
    public class PasswordService : IPasswordService
    {
        private const string _saltString = "cmaqdcuzyidmmlfobtwduukqexizuflvwvzgwhdozhsuuadovmkgtogqjphrcbrwvkdhucpokstwgoff";
        private readonly byte[] _salt;

        public PasswordService()
        {
            _salt = Encoding.UTF8.GetBytes(_saltString);
        }

        /// <summary>
        /// Method to create password hash, allowing password encryptions
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Password in hash code</returns>
        public byte[] CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512(_salt))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Method to ensure that the password provided matches the password stored in the DB
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns>True if password stored matches password provided</returns>
        public bool VerifyPasswordHash(string password, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(_salt))
            {
                byte[] computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computerHash.Length; i++)
                {
                    if (computerHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }            
        }
    }
}
