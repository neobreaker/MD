using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace carkey.Common
{
    class Misc
    {
        public static void ConvertPrintHex(List<byte> data, int len, ref string hexstr)
        {
            for (int i = 0; i < len; i++)
            {
                string tmpstr = Convert.ToString(data[i], 16);
                if (tmpstr.Length == 1)
                    tmpstr = "0" + tmpstr;
                hexstr = hexstr + tmpstr + " ";
            }
        }

        public static void ConvertPrintHex(byte[] data, int len, ref string hexstr)
        {
            hexstr = "";
            for (int i = 0; i < len; i++)
            {
                string tmpstr = Convert.ToString(data[i], 16);
                if (tmpstr.Length == 1)
                    tmpstr = "0" + tmpstr;
                hexstr = hexstr + tmpstr + " ";
            }
        }

        public static void ConvertPrintHex(byte data, ref string hexstr)
        {
            hexstr = Convert.ToString(data, 16);
            if (hexstr.Length == 1)
                hexstr = "0" + hexstr;
            
        }

        public static int ReadHex(string filename,ref byte[] buff, int buff_len)
        {
            int read_num = 0;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            for (int i = 0; i < (int)fs.Length && i < buff_len; i++)
            {
                byte tmpbyte = br.ReadByte();
                buff[i] = tmpbyte;
                read_num++;
            }
            br.Close();
            fs.Close();
            return  read_num;
        }

        public static byte ChecksumAccumulatePlusOne(byte data)
        {
            int check_sum = data;

            check_sum = (check_sum++ > 0xff) ? 0x00 : check_sum;

            return (byte)check_sum;
        }

        public static byte ChecksumAccumulatePlusOne(byte[] data, int len)
        {
            int check_sum = 0;
            byte[] data_tmp = new byte[4];

            for (int i = 0; i < data.Length; i++)
            {
                check_sum += data[i];
            }

            while (check_sum > 0xff)
            {
                data_tmp[3] = (byte)((check_sum & 0xff000000) >> 24);
                data_tmp[2] = (byte)((check_sum & 0xff0000) >> 16);
                data_tmp[1] = (byte)((check_sum & 0xff00) >> 8);
                data_tmp[0] = (byte)(check_sum & 0xff);

                check_sum = 0;
                for (int i = 0; i < 4; i++)
                {
                    check_sum += data_tmp[i];
                }
            }

            check_sum = (check_sum++ > 0xff) ?  0x00 : check_sum;

            return (byte)check_sum; 
        }

        public static byte CheckSumAccumulate(byte[] data, int len)
        {
            int check_sum = 0;
            byte[] data_tmp = new byte[4];

            for (int i = 0; i < data.Length; i++)
            {
                check_sum += data[i];
            }

            check_sum = (byte)(check_sum & 0xff);

            return (byte)check_sum;
        }

        public static bool IsArrayEqual(byte[] lhs, byte[] rhs)
        {
            if (lhs.Length != rhs.Length)
                return false;
            for (int i = 0; i < lhs.Length; i++)
            {
                if (lhs[i] != rhs[i])
                    return false;
            }

            return true;
        }

        public static void ParseTextInput(string input_str, ref string[] out_str)
        {
            out_str = input_str.Split(' ');
        }

        public static void DoUpdateArray(byte[] array, int start, int length, string input_str)
        {
            int i = 0;
            string[] str = new string[32];
            byte[] tmp_byte = new byte[32];

            Misc.ParseTextInput(input_str, ref str);

            for (i = 0; i < length; i++)
            {
                if (str[i] == "")
                    return;
            }

            for (i = 0; i < length; i++)
            {
                array[start + i] = Convert.ToByte(str[i], 16);
            }
        }
    }
}
