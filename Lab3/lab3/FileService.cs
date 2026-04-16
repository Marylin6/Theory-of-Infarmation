using System.IO;
using System.Numerics;


namespace lab3
{
    internal class FileService
    {
        public static byte[] ReadBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
        public static void WriteEncrypted(string path, List<BigInteger> data)
        {
            using var writer = new BinaryWriter(File.Open(path, FileMode.Create));

            foreach (var c in data)
                writer.Write((uint)c);
        }
        public static List<BigInteger> ReadEncrypted(string path)
        {
            List<BigInteger> result = new List<BigInteger>();

            using var reader = new BinaryReader(File.Open(path, FileMode.Open));

            while (reader.BaseStream.Position < reader.BaseStream.Length)
                result.Add(reader.ReadUInt32());

            return result;
        }

        public static void WriteDecrypted(string path, List<byte> data)
        {
            File.WriteAllBytes(path, data.ToArray());
        }
    }
}
