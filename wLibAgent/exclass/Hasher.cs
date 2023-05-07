using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace wLibAgent
{
    public enum HashType { MD5, SHA1, SHA256, SHA384, SHA512 }

    internal class Hasher
    {
        internal static string HashFile(string filePath, HashType type)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                switch (type)
                {
                    case HashType.MD5:
                        return MakeHashString(MD5.Create().ComputeHash(fs));
                    case HashType.SHA1:
                        return MakeHashString(SHA1.Create().ComputeHash(fs));
                    case HashType.SHA256:
                        return MakeHashString(SHA256.Create().ComputeHash(fs));
                    case HashType.SHA384:
                        return MakeHashString(SHA384.Create().ComputeHash(fs));
                    case HashType.SHA512:
                        return MakeHashString(SHA512.Create().ComputeHash(fs));
                    default:
                        return "";
                }
            }
        }

        internal static string HashString(string text, HashType type)
        {
            using (MemoryStream fs = new MemoryStream(Encoding.Default.GetBytes(text)))
            {
                switch (type)
                {
                    case HashType.MD5:
                        return MakeHashString(MD5.Create().ComputeHash(fs));
                    case HashType.SHA1:
                        return MakeHashString(SHA1.Create().ComputeHash(fs));
                    case HashType.SHA256:
                        return MakeHashString(SHA256.Create().ComputeHash(fs));
                    case HashType.SHA384:
                        return MakeHashString(SHA384.Create().ComputeHash(fs));
                    case HashType.SHA512:
                        return MakeHashString(SHA512.Create().ComputeHash(fs));
                    default:
                        return "";
                }
            }
        }

        private static string MakeHashString(byte[] hash)
        {
            StringBuilder s = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                s.Append(b.ToString("x2"));
            }

            return s.ToString();
        }
    }
}
