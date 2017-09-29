using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using carkey.Common;

namespace carkey.Model
{
    class ModelSHLC
    {
        public byte[] mnufacturer = new byte[2];
        public string mnufacturer_str;

        public byte[] secretkey = new byte[8];
        public string secretkey_str;

        public byte[] pin = new byte[4];
        public string pin_str;

        public byte[] keyidentification1 = new byte[4];
        public byte[] keyidentification2 = new byte[4];
        public byte[] keyidentification3 = new byte[4];
        public byte[] keyidentification4 = new byte[4];
        public byte[] keyidentification5 = new byte[4];
        public byte[] keyidentification6 = new byte[4];
        public byte[] keyidentification7 = new byte[4];
        public byte[] keyidentification8 = new byte[4];

        public string keyidentification1_str;
        public string keyidentification2_str;
        public string keyidentification3_str;
        public string keyidentification4_str;
        public string keyidentification5_str;
        public string keyidentification6_str;
        public string keyidentification7_str;
        public string keyidentification8_str;

        public byte[] field1 = new byte[17];
        public string field1_str;

        public byte[] errcode = new byte[5];
        public string errcode_str;

        public byte[] vin = new byte[17];
        public string vin_str;
        public string vin_ascii;

        public ModelSHLC(byte[] bin)
        {
            int i = 0x1c, j = 0;

            for(j = 0; j < 2; j++)
            {
                mnufacturer[j] = bin[i+j];
            }

            i = 0x20;
            for (j = 0; j < 8; j++)
            {
                secretkey[j] = bin[j + i];
            }

            i = 0x30;
            for (j = 0; j < 4; j++)
            {
                pin[j] = bin[j + i];
            }

            i = 0x40;
            for (j = 0; j < 4; j++)
            {
                keyidentification1[j] = bin[j + i];
            }

            i = 0x44;
            for (j = 0; j < 4; j++)
            {
                keyidentification2[j] = bin[j + i];
            }

            i = 0x48;
            for (j = 0; j < 4; j++)
            {
                keyidentification3[j] = bin[j + i];
            }

            i = 0x4c;
            for (j = 0; j < 4; j++)
            {
                keyidentification4[j] = bin[j + i];
            }

            i = 0x50;
            for (j = 0; j < 4; j++)
            {
                keyidentification5[j] = bin[j + i];
            }

            i = 0x54;
            for (j = 0; j < 4; j++)
            {
                keyidentification6[j] = bin[j + i];
            }

            i = 0x58;
            for (j = 0; j < 4; j++)
            {
                keyidentification7[j] = bin[j + i];
            }

            i = 0x5c;
            for (j = 0; j < 4; j++)
            {
                keyidentification8[j] = bin[j + i];
            }

            i = 0xa0;
            for (j = 0; j < 17; j++)
            {
                field1[j] = bin[j + i];
            }

            i = 0xb1;
            for (j = 0; j < 5; j++)
            {
                errcode[j] = bin[i + j];
            }

            i = 0xc0;
            for (j = 0; j < 17; j++)
            {
                vin[j] = bin[i + j];
            }
            this.vin_ascii = System.Text.Encoding.ASCII.GetString(this.vin);

            Misc.ConvertPrintHex(mnufacturer, 2, ref mnufacturer_str);
            Misc.ConvertPrintHex(secretkey, 8, ref secretkey_str);
            Misc.ConvertPrintHex(pin, 4, ref pin_str);
            Misc.ConvertPrintHex(keyidentification1, 4, ref keyidentification1_str);
            Misc.ConvertPrintHex(keyidentification2, 4, ref keyidentification2_str);
            Misc.ConvertPrintHex(keyidentification3, 4, ref keyidentification3_str);
            Misc.ConvertPrintHex(keyidentification4, 4, ref keyidentification4_str);
            Misc.ConvertPrintHex(keyidentification5, 4, ref keyidentification5_str);
            Misc.ConvertPrintHex(keyidentification6, 4, ref keyidentification6_str);
            Misc.ConvertPrintHex(keyidentification7, 4, ref keyidentification7_str);
            Misc.ConvertPrintHex(keyidentification8, 4, ref keyidentification8_str);
            Misc.ConvertPrintHex(field1, 17, ref field1_str);
            Misc.ConvertPrintHex(errcode, 5, ref errcode_str);
            Misc.ConvertPrintHex(vin, 17, ref vin_str);
            
        }

    }
}
