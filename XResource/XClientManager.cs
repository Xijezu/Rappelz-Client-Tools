using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace System
{
    public class XClientManager
    {
        public string[] tempDataName = new string[9];

        #region XOR
        private readonly byte[] xor1 = {  0x77, 0xE8, 0x5E, 0xEC, 0xB7, 0x4E, 0xC1, 0x87, 0x4F, 0xE6, 0xF5, 0x3C, 0x1F, 0xB3, 0x15, 0x43,
                                        0x6A, 0x49, 0x30, 0xA6, 0xBF, 0x53, 0xA8, 0x35, 0x5B, 0xE5, 0x9E, 0x0E, 0x41, 0xEC, 0x22, 0xB8,
                                        0xD4, 0x80, 0xA4, 0x8C, 0xCE, 0x65, 0x13, 0x1D, 0x4B, 0x08, 0x5A, 0x6A, 0xBB, 0x6F, 0xAD, 0x25,
                                        0xB8, 0xDD, 0xCC, 0x77, 0x30, 0x74, 0xAC, 0x8C, 0x5A, 0x4A, 0x9A, 0x9B, 0x36, 0xBC, 0x53, 0x0A,
                                        0x3C, 0xF8, 0x96, 0x0B, 0x5D, 0xAA, 0x28, 0xA9, 0xB2, 0x82, 0x13, 0x6E, 0xF1, 0xC1, 0x93, 0xA9,
                                        0x9E, 0x5F, 0x20, 0xCF, 0xD4, 0xCC, 0x5B, 0x2E, 0x16, 0xF5, 0xC9, 0x4C, 0xB2, 0x1C, 0x57, 0xEE,
                                        0x14, 0xED, 0xF9, 0x72, 0x97, 0x22, 0x1B, 0x4A, 0xA4, 0x2E, 0xB8, 0x96, 0xEF, 0x4B, 0x3F, 0x8E,
                                        0xAB, 0x60, 0x5D, 0x7F, 0x2C, 0xB8, 0xAD, 0x43, 0xAD, 0x76, 0x8F, 0x5F, 0x92, 0xE6, 0x4E, 0xA7,
                                        0xD4, 0x47, 0x19, 0x6B, 0x69, 0x34, 0xB5, 0x0E, 0x62, 0x6D, 0xA4, 0x52, 0xB9, 0xE3, 0xE0, 0x64,
                                        0x43, 0x3D, 0xE3, 0x70, 0xF5, 0x90, 0xB3, 0xA2, 0x06, 0x42, 0x02, 0x98, 0x29, 0x50, 0x3F, 0xFD,
                                        0x97, 0x58, 0x68, 0x01, 0x8C, 0x1E, 0x0F, 0xEF, 0x8B, 0xB3, 0x41, 0x44, 0x96, 0x21, 0xA8, 0xDA,
                                        0x5E, 0x8B, 0x4A, 0x53, 0x1B, 0xFD, 0xF5, 0x21, 0x3F, 0xF7, 0xBA, 0x68, 0x47, 0xF9, 0x65, 0xDF,
                                        0x52, 0xCE, 0xE0, 0xDE, 0xEC, 0xEF, 0xCD, 0x77, 0xA2, 0x0E, 0xBC, 0x38, 0x2F, 0x64, 0x12, 0x8D,
                                        0xF0, 0x5C, 0xE0, 0x0B, 0x59, 0xD6, 0x2D, 0x99, 0xCD, 0xE7, 0x01, 0x15, 0xE0, 0x67, 0xF4, 0x32,
                                        0x35, 0xD4, 0x11, 0x21, 0xC3, 0xDE, 0x98, 0x65, 0xED, 0x54, 0x9D, 0x1C, 0xB9, 0xB0, 0xAA, 0xA9,
                                        0x0C, 0x8A, 0xB4, 0x66, 0x60, 0xE1, 0xFF, 0x2E, 0xC8, 0x00, 0x43, 0xA9, 0x67, 0x37, 0xDB, 0x9C };

        public byte[] xor2 = new byte[128]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 103,
        (byte) 32,
        (byte) 0,
        (byte) 38,
        (byte) 119,
        (byte) 44,
        (byte) 108,
        (byte) 78,
        (byte) 88,
        (byte) 79,
        (byte) 0,
        (byte) 55,
        (byte) 46,
        (byte) 37,
        (byte) 101,
        (byte) 0,
        (byte) 56,
        (byte) 95,
        (byte) 93,
        (byte) 35,
        (byte) 80,
        (byte) 49,
        (byte) 45,
        (byte) 36,
        (byte) 86,
        (byte) 91,
        (byte) 0,
        (byte) 89,
        (byte) 0,
        (byte) 94,
        (byte) 0,
        (byte) 0,
        (byte) 75,
        (byte) 125,
        (byte) 106,
        (byte) 48,
        (byte) 64,
        (byte) 71,
        (byte) 83,
        (byte) 41,
        (byte) 65,
        (byte) 120,
        (byte) 121,
        (byte) 54,
        (byte) 57,
        (byte) 69,
        (byte) 70,
        (byte) 123,
        (byte) 87,
        (byte) 98,
        (byte) 61,
        (byte) 82,
        (byte) 118,
        (byte) 116,
        (byte) 104,
        (byte) 50,
        (byte) 52,
        (byte) 77,
        (byte) 40,
        (byte) 107,
        (byte) 0,
        (byte) 109,
        (byte) 97,
        (byte) 43,
        (byte) 126,
        (byte) 68,
        (byte) 39,
        (byte) 67,
        (byte) 33,
        (byte) 74,
        (byte) 73,
        (byte) 100,
        (byte) 66,
        (byte) 85,
        (byte) 96,
        (byte) 113,
        (byte) 102,
        (byte) 112,
        (byte) 72,
        (byte) 81,
        (byte) 51,
        (byte) 76,
        (byte) 110,
        (byte) 111,
        (byte) 90,
        (byte) 105,
        (byte) 114,
        (byte) 115,
        (byte) 117,
        (byte) 59,
        (byte) 122,
        (byte) 99,
        (byte) 0,
        (byte) 84,
        (byte) 53,
        (byte) 0
      };
        public byte[] xor3 = new byte[85]
        {
        (byte) 94,
        (byte) 38,
        (byte) 84,
        (byte) 95,
        (byte) 78,
        (byte) 115,
        (byte) 100,
        (byte) 123,
        (byte) 120,
        (byte) 111,
        (byte) 53,
        (byte) 118,
        (byte) 96,
        (byte) 114,
        (byte) 79,
        (byte) 89,
        (byte) 86,
        (byte) 43,
        (byte) 44,
        (byte) 105,
        (byte) 73,
        (byte) 85,
        (byte) 35,
        (byte) 107,
        (byte) 67,
        (byte) 74,
        (byte) 113,
        (byte) 56,
        (byte) 36,
        (byte) 39,
        (byte) 126,
        (byte) 76,
        (byte) 48,
        (byte) 80,
        (byte) 93,
        (byte) 70,
        (byte) 101,
        (byte) 66,
        (byte) 110,
        (byte) 45,
        (byte) 65,
        (byte) 117,
        (byte) 40,
        (byte) 112,
        (byte) 88,
        (byte) 72,
        (byte) 90,
        (byte) 104,
        (byte) 119,
        (byte) 68,
        (byte) 121,
        (byte) 50,
        (byte) 125,
        (byte) 97,
        (byte) 103,
        (byte) 87,
        (byte) 71,
        (byte) 55,
        (byte) 75,
        (byte) 61,
        (byte) 98,
        (byte) 81,
        (byte) 59,
        (byte) 83,
        (byte) 82,
        (byte) 116,
        (byte) 41,
        (byte) 52,
        (byte) 54,
        (byte) 108,
        (byte) 64,
        (byte) 106,
        (byte) 69,
        (byte) 37,
        (byte) 57,
        (byte) 33,
        (byte) 99,
        (byte) 49,
        (byte) 91,
        (byte) 51,
        (byte) 102,
        (byte) 109,
        (byte) 77,
        (byte) 122,
        (byte) 0
        };
        #endregion

        public List<CSDATA_INDEX> m_lClient = new List<CSDATA_INDEX>();
        public List<CSDATA_INDEX> m_lClientNew = new List<CSDATA_INDEX>();

        public void Cipher(ref byte[] buffer, ref byte index)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                byte[] expr_0C_cp_0 = buffer;
                int expr_0C_cp_1 = i;
                expr_0C_cp_0[expr_0C_cp_1] ^= xor1[(int)index];
                ++index;
            }
        }

        public void Cipher(ref byte buffer, ref byte index)
        {
            buffer ^= xor1[(int)index];
            ++index;
        }

        public bool Encrypted(string ext)
        {
            if (!ext.Contains("dds")
                && !ext.Contains("cob")
                && !ext.Contains("naf")
                && !ext.Contains("nx3")
                && !ext.Contains("nfm")
                && !ext.Contains("tga"))
                return true;
            return false;
        }

        public string Encode(string name)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(name.ToLower());
            uint num1 = 0;
            int length = bytes.Length;
            for (int index = 0; index < length; ++index)
            {
                uint num2 = (uint)bytes[index] << 4;
                num1 += (uint)((int)num2 + (int)bytes[index] + 1);
            }
            uint num3 = num1 + (uint)length & 2147483679U;
            if ((int)num3 == 0)
                num3 = 32U;
            uint num4 = 0;
            int num5 = (int)num3;
            do
            {
                byte num2 = bytes[num4];
                byte num6 = num2;
                if (num5 == 0)
                    num5 = 32;
                if (num5 > 0)
                {
                    int num7 = num5;
                    do
                    {
                        --num7;
                        num6 = this.xor2[(int)num6];
                    }
                    while (num7 != 0);
                }
                bytes[num4] = num6;
                num5 = ((int)num2 + num5 + 16 * (int)num2 + 1) % 32;
                ++num4;
            }
            while ((long)num4 < (long)length);
            byte addChar = this.get_add_char(bytes);
            this.swap2chars(ref bytes);
            byte[] b1 = new byte[5];
            b1[0] = addChar;
            byte[] b2 = new byte[5];
            b2[0] = this.xor3[num3];
            return Encoding.ASCII.GetString(b1).Substring(0, 1) + Encoding.ASCII.GetString(bytes) + Encoding.ASCII.GetString(b2).Substring(0, 1);
        }

        private void swap2chars(ref byte[] pHash)
        {
            double num1 = (double)pHash.Length;
            byte num2 = (byte)(num1 * 0.660000026226044);
            byte num3 = pHash[(int)num2];
            pHash[(int)num2] = pHash[0];
            pHash[0] = num3;
            byte num4 = (byte)(num1 * 0.330000013113022);
            byte num5 = pHash[(int)num4];
            pHash[(int)num4] = pHash[1];
            pHash[1] = num5;
        }

        private byte get_add_char(byte[] pHash)
        {
            uint num1 = 0;
            uint num2 = 0;
            byte num3 = Convert.ToByte(pHash.Length);
            if ((int)num3 != 0)
            {
                do
                {
                    num1 += (uint)pHash[num2];
                    ++num2;
                }
                while (num2 < (uint)num3);
            }
            return this.xor3[(int)Convert.ToByte(num1 % 84U)];
        }

        public string Decode(string hash)
        {
            if (hash.Length > 0)
            {
                char[] array = hash.Substring(1, hash.Length - 2).ToCharArray();
                if (array.Length > 4)
                {
                    int num = (int)Math.Floor(0.33000001311302191 * (double)array.Length);
                    int num2 = (int)Math.Floor(0.6600000262260437 * (double)array.Length);
                    char c = array[num2];
                    char c2 = array[num];
                    array[num2] = array[0];
                    array[num] = array[1];
                    array[0] = c;
                    array[1] = c2;
                }

                int num3 = Array.IndexOf<byte>(xor3, (byte)hash[hash.Length - 1], 0, xor3.Length);
                int num4 = num3;
                for (int i = 0; i < array.Length; i++)
                {
                    num3 = (int)array[i];
                    for (int j = 0; j < num4; j++)
                    {
                        int num5 = Array.IndexOf<byte>(xor2, (byte)num3, 0, xor2.Length);
                        if (num5 < xor2.Length)
                        {
                            num3 = num5;
                        }
                        else
                        {
                            num3 = 255;
                        }
                    }
                    array[i] = (char)num3;
                    num4 = (1 + num4 + 17 * num3 & 31);
                    if (num4 == 0)
                    {
                        num4 = 32;
                    }
                }
                return new string(array);
            }
            return string.Empty;
        }

        public int GetFileID(string hash)
        {
            byte[] bytes = Encoding.Default.GetBytes(hash.ToLower());
            int num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                num = (num << 5) - num + (int)bytes[i];
            }
            if (num < 0)
            {
                num *= -1;
            }
            return num % 8 + 1;
        }

        public HashSet<string> hash = new HashSet<string>();

        public void Open(string szFile)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(szFile), Encoding.Default))
            {
                byte nCipherIndex = 0;
                while (br.PeekChar() != -1)
                {
                    CSDATA_INDEX index = new CSDATA_INDEX();

                    byte nStrLen = br.ReadByte();
                    Cipher(ref nStrLen, ref nCipherIndex);

                    byte[] szHash = br.ReadBytes(nStrLen);
                    Cipher(ref szHash, ref nCipherIndex);

                    byte[] nVal = br.ReadBytes(8);
                    Cipher(ref nVal, ref nCipherIndex);

                    index.szHash = Encoding.Default.GetString(szHash);
                    index.nOffset = BitConverter.ToUInt32(nVal, 0);
                    index.nSize = BitConverter.ToUInt32(nVal, 4);
                    index.nFile = GetFileID(index.szHash);
                    index.szName = Decode(index.szHash);
                    hash.Add(index.szName);
                    m_lClient.Add(index);
                }
            }
        }

        public void Save(string szFile)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(szFile)))
            {
                byte nCipherIndex = 0;

                foreach (CSDATA_INDEX index in m_lClient)
                {
                    byte nStrLen = Convert.ToByte(index.szHash.Length);
                    Cipher(ref nStrLen, ref nCipherIndex);
                    bw.Write(nStrLen);

                    byte[] pBuffer = Encoding.Default.GetBytes(index.szHash);
                    Cipher(ref pBuffer, ref nCipherIndex);
                    bw.Write(pBuffer);

                    byte[] nVal = new byte[8];
                    Buffer.BlockCopy(BitConverter.GetBytes(index.nOffset), 0, nVal, 0, 4);
                    Buffer.BlockCopy(BitConverter.GetBytes(index.nSize), 0, nVal, 4, 4);
                    Cipher(ref nVal, ref nCipherIndex);

                    bw.Write(nVal);
                }
            }
        }

        public void AddFile(string szFile, string szPath)
        {
            FileInfo pInfo = new FileInfo(szFile);
            string szHash = Encode(Path.GetFileName(szFile));

            var index = m_lClient.Find(r => r.szHash == szHash);

            bool bIsNew = false;
            if (index == null)
            {
                bIsNew = true;
                index = new CSDATA_INDEX();
                index.szHash = szHash;
                index.nFile = GetFileID(index.szHash);
            }
            else
            {
                m_lClient.Remove(index);
            }

            string szDataFile = Path.Combine(szPath, "data.00" + index.nFile);

            FileStream tFileStream = (!bIsNew && pInfo.Length <= index.nSize)
                                        ? new FileStream(szDataFile, FileMode.Open)
                                        : new FileStream(szDataFile, FileMode.Append);

            using (BinaryWriter bWriter = new BinaryWriter(tFileStream))
            {
                if (!bIsNew && pInfo.Length <= index.nSize)
                {
                    // Replace
                    bWriter.BaseStream.Position = index.nOffset;
                    index.nSize = (uint)pInfo.Length;
                }
                else
                {
                    // Append
                    bWriter.BaseStream.Position = new FileInfo(szDataFile).Length;
                    index.nSize = (uint)pInfo.Length;
                    index.nOffset = (uint)bWriter.BaseStream.Position;
                }

                using (BinaryReader bReader = new BinaryReader(File.OpenRead(szFile)))
                {
                    byte[] pBuffer = bReader.ReadBytes((int)bReader.BaseStream.Length);
                    if (Encrypted(pInfo.Extension))
                    {
                        byte nCipherIndex = 0;
                        Cipher(ref pBuffer, ref nCipherIndex);
                    }
                    bWriter.Write(pBuffer);
                }
                m_lClient.Add(index);
            }
        }



        public int Patch(string szPath, CSDATA_INDEX patchIndex)
        {
            FileInfo fiCheck = new FileInfo(patchIndex.szName);
            string szValue = string.Format("{0}(ascii){1}", Path.GetFileNameWithoutExtension(fiCheck.Name), fiCheck.Extension).ToLower();

             if (hash.Contains(szValue) && fiCheck.Extension.Contains("rdb"))
                 return -1;

            string szDataFile = Path.Combine(szPath, "data.00" + patchIndex.nFile);
            string szDataFileNew = tempDataName[patchIndex.nFile] != null ? tempDataName[patchIndex.nFile] : (tempDataName[patchIndex.nFile] = Path.GetTempFileName()); //Path.Combine(string.Format("{0}\\new\\", szPath), "data.00" + patchIndex.nFile);

            if (!File.Exists(szDataFileNew))
                File.Create(szDataFileNew).Close();

            FileStream tFileStream = new FileStream(szDataFileNew, FileMode.Append);
            using (BinaryWriter bWriter = new BinaryWriter(tFileStream))
            {
                var oldOffset = patchIndex.nOffset;
                // Append
                bWriter.BaseStream.Position = new FileInfo(szDataFileNew).Length;
                patchIndex.nOffset = (uint)bWriter.BaseStream.Position;

                using (BinaryReader bReader = new BinaryReader(File.OpenRead(szDataFile)))
                {
                    bReader.BaseStream.Position = oldOffset;
                    bWriter.Write(bReader.ReadBytes((int)patchIndex.nSize));
                }
            }
            m_lClientNew.Add(patchIndex);
            return 0;
        }

    } // END CLASS

    public class CSDATA_INDEX
    {
        public string szHash { get; set; }
        public string szName { get; set; }
        public uint nOffset { get; set; }
        public uint nSize { get; set; }
        public int nFile { get; set; }
    }

    public class CompareCustomDataType : IComparer<CSDATA_INDEX>
    {

        public int Compare(CSDATA_INDEX x, CSDATA_INDEX y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.szName.ToLower().Equals(y.szName.ToLower()) ? 1 : 0;
        }
    }
}