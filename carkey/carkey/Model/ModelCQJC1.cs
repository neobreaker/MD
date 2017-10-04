using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using carkey.Common;

namespace carkey.Model
{
    class ModelCQJC1
    {
        public byte[] vin = new byte[17];
        public string vin_str;
        public string vin_ascii;

        public byte[] field1 = new byte[10];
        public string field1_str;
        public string field1_ascii;

        public byte[] immobilisercode = new byte[12];
        public string immobilisercode_str;
        public string immobilisercode_ascii;

        public byte activatekey;
        public string activatekey_str;

        public byte[] errcode = new byte[2];
        public string errcode_str;
        
        public byte[] keyinfo = new byte[5];
        public string keyinfo_str;

        public byte[] pin = new byte[2];
        public string pin_str;

        public byte[] keyidentification1 = new byte[4];
        public string keyidentification1_str;

        public byte[] keyidentification2 = new byte[4];
        public string keyidentification2_str;

        public byte[] keyidentification3 = new byte[4];
        public string keyidentification3_str;

        public byte[] keyidentification4 = new byte[4];
        public string keyidentification4_str;

        public byte[] keyidentification5 = new byte[4];
        public string keyidentification5_str;

        public byte keynum;
        public string keynum_str;

        public byte[] field2 = new byte[18];
        public string field2_str;
        public string field2_ascii;

        public ModelCQJC1(byte[] bin)
        {
            int i = 0, j = 0;

            i = 0x28;
            for (j = 0; j < 17; j++)
            {
                vin[j] = bin[i++];
            }
            this.vin_ascii = System.Text.Encoding.ASCII.GetString(this.vin);

            for (j = 0; j < 10; j++)
            {
                field1[j] = bin[i++];
            }
            this.field1_ascii = System.Text.Encoding.ASCII.GetString(this.field1);

            for (j = 0; j < 12; j++)
            {
                immobilisercode[j] = bin[i++];
            }
            this.immobilisercode_ascii = System.Text.Encoding.ASCII.GetString(this.immobilisercode);

            i = 0x56;
            activatekey = bin[i++];

            i = 0x57;
            for (j = 0; j < 2; j++)
            {
                errcode[j] = bin[i++];
            }

            i = 0x5c;
            for (j = 0; j < 5; j++)
            {
                keyinfo[j] = bin[i++];
            }

            i = 0x63;
            for (j = 0; j < 2; j++)
            {
                pin[j] = bin[i++];
            }

            i = 0x66;
            for (j = 0; j < 3; j++)
            {
                keyidentification1[j] = bin[i++];
            }

            for (j = 0; j < 3; j++)
            {
                keyidentification2[j] = bin[i++];
            }

            for (j = 0; j < 3; j++)
            {
                keyidentification3[j] = bin[i++];
            }

            for (j = 0; j < 3; j++)
            {
                keyidentification4[j] = bin[i++];
            }

            for (j = 0; j < 3; j++)
            {
                keyidentification5[j] = bin[i++];
            }


            i = 0x75;
            keynum = bin[i++];

            i = 0x89;
            for (j = 0; j < 18; j++)
            {
                field2[j] = bin[i++];
            }
            this.field2_ascii = System.Text.Encoding.ASCII.GetString(this.field2);

            Misc.ConvertPrintHex(vin, 17, ref vin_str);
            Misc.ConvertPrintHex(field1, 10, ref field1_str);
            Misc.ConvertPrintHex(immobilisercode, 12, ref immobilisercode_str);
            Misc.ConvertPrintHex(activatekey, ref activatekey_str);
            Misc.ConvertPrintHex(errcode, 2, ref errcode_str);
            Misc.ConvertPrintHex(keyinfo, 5, ref keyinfo_str);
            Misc.ConvertPrintHex(pin, 2, ref pin_str);
            Misc.ConvertPrintHex(keyidentification1, 3, ref keyidentification1_str);
            Misc.ConvertPrintHex(keyidentification2, 3, ref keyidentification2_str);
            Misc.ConvertPrintHex(keyidentification3, 3, ref keyidentification3_str);
            Misc.ConvertPrintHex(keyidentification4, 3, ref keyidentification4_str);
            Misc.ConvertPrintHex(keyidentification5, 3, ref keyidentification5_str);
            Misc.ConvertPrintHex(keynum, ref keynum_str);
            Misc.ConvertPrintHex(field2, 18, ref field2_str);
        }
    }
}
