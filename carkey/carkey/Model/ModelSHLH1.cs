using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using carkey.Common;

namespace carkey.Model
{
    class ModelSHLH1
    {
        public byte[] pin = new byte[4];
        public string pin_str;

        public byte[] secretkey = new byte[8];
        public string secretkey_str;

        public byte[] manufacturer = new byte[2];
        public string manufacturer_str;

        public byte[] vin = new byte[17];
        public string vin_str;
        public string vin_ascii;

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

        public byte[] errcode = new byte[6];
        public string errcode_str;

        public byte[] pwderrcnt = new byte[5];
        public string pwderrcnt_str;

        //back up   
        public byte[] pin_bkp = new byte[4];
        public string pin_bkp_str;

        public byte[] secretkey_bkp = new byte[8];
        public string secretkey_bkp_str;

        public byte[] manufacturer_bkp = new byte[2];
        public string manufacturer_bkp_str;

        public byte[] vin_bkp = new byte[17];
        public string vin_bkp_str;
        public string vin_bkp_ascii;

        public byte[] keyidentification1_bkp = new byte[4];
        public byte[] keyidentification2_bkp = new byte[4];
        public byte[] keyidentification3_bkp = new byte[4];
        public byte[] keyidentification4_bkp = new byte[4];
        public byte[] keyidentification5_bkp = new byte[4];
        public byte[] keyidentification6_bkp = new byte[4];
        public byte[] keyidentification7_bkp = new byte[4];
        public byte[] keyidentification8_bkp = new byte[4];

        public string keyidentification1_bkp_str;
        public string keyidentification2_bkp_str;
        public string keyidentification3_bkp_str;
        public string keyidentification4_bkp_str;
        public string keyidentification5_bkp_str;
        public string keyidentification6_bkp_str;
        public string keyidentification7_bkp_str;
        public string keyidentification8_bkp_str;

        public byte[] errcode_bkp = new byte[6];
        public string errcode_bkp_str;

        public ModelSHLH1(byte[] bin)
        {
            int i = 0x11, j = 0;

            for (j = 0; j < 4; j++)
            {
                pin[j] = bin[i++];
            }

            for (j = 0; j < 8; j++)
            {
                secretkey[j] = bin[i++];
            }

            for (j = 0; j < 2; j++)
            {
                manufacturer[j] = bin[i++];
            }

            for (j = 0; j < 17; j++)
            {
                vin[j] = bin[i++];
            }
            this.vin_ascii = System.Text.Encoding.ASCII.GetString(this.vin);

            for (j = 0; j < 4; j++)
            {
                keyidentification1[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification2[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification3[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification4[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification5[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification6[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification7[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification8[j] = bin[i++];
            }

            i = 0x54;
            for (j = 0; j < 6; j++)
            {
                errcode[j] = bin[i++];
            }

            i = 0x81;
            for (j = 0; j < 5; j++)
            {
                pwderrcnt[j] = bin[i++];
            }

            //back up
            i = 0xa1;
            for (j = 0; j < 4; j++)
            {
                pin_bkp[j] = bin[i++];
            }

            for (j = 0; j < 8; j++)
            {
                secretkey_bkp[j] = bin[i++];
            }

            for (j = 0; j < 2; j++)
            {
                manufacturer_bkp[j] = bin[i++];
            }

            for (j = 0; j < 17; j++)
            {
                vin_bkp[j] = bin[i++];
            }
            this.vin_bkp_ascii = System.Text.Encoding.ASCII.GetString(this.vin_bkp);

            for (j = 0; j < 4; j++)
            {
                keyidentification1_bkp[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification2_bkp[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification3_bkp[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification4_bkp[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification5_bkp[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification6_bkp[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification7_bkp[j] = bin[i++];
            }

            for (j = 0; j < 4; j++)
            {
                keyidentification8_bkp[j] = bin[i++];
            }

            i = 0xe4;
            for (j = 0; j < 6; j++)
            {
                errcode_bkp[j] = bin[i++];
            }

            Misc.ConvertPrintHex(pin, 4, ref pin_str);
            Misc.ConvertPrintHex(secretkey, 8, ref secretkey_str);
            Misc.ConvertPrintHex(manufacturer, 2, ref manufacturer_str);
            Misc.ConvertPrintHex(vin, 17, ref vin_str);
            Misc.ConvertPrintHex(keyidentification1, 4, ref keyidentification1_str);
            Misc.ConvertPrintHex(keyidentification2, 4, ref keyidentification2_str);
            Misc.ConvertPrintHex(keyidentification3, 4, ref keyidentification3_str);
            Misc.ConvertPrintHex(keyidentification4, 4, ref keyidentification4_str);
            Misc.ConvertPrintHex(keyidentification5, 4, ref keyidentification5_str);
            Misc.ConvertPrintHex(keyidentification6, 4, ref keyidentification6_str);
            Misc.ConvertPrintHex(keyidentification7, 4, ref keyidentification7_str);
            Misc.ConvertPrintHex(keyidentification8, 4, ref keyidentification8_str);
            Misc.ConvertPrintHex(errcode, 6, ref errcode_str);
            Misc.ConvertPrintHex(pwderrcnt, 5, ref pwderrcnt_str);

            //back up
            Misc.ConvertPrintHex(pin_bkp, 4, ref pin_bkp_str);
            Misc.ConvertPrintHex(secretkey_bkp, 8, ref secretkey_bkp_str);
            Misc.ConvertPrintHex(manufacturer_bkp, 2, ref manufacturer_bkp_str); 
            Misc.ConvertPrintHex(vin_bkp, 17, ref vin_bkp_str);
            Misc.ConvertPrintHex(keyidentification1_bkp, 4, ref keyidentification1_bkp_str);
            Misc.ConvertPrintHex(keyidentification2_bkp, 4, ref keyidentification2_bkp_str);
            Misc.ConvertPrintHex(keyidentification3_bkp, 4, ref keyidentification3_bkp_str);
            Misc.ConvertPrintHex(keyidentification4_bkp, 4, ref keyidentification4_bkp_str);
            Misc.ConvertPrintHex(keyidentification5_bkp, 4, ref keyidentification5_bkp_str);
            Misc.ConvertPrintHex(keyidentification6_bkp, 4, ref keyidentification6_bkp_str);
            Misc.ConvertPrintHex(keyidentification7_bkp, 4, ref keyidentification7_bkp_str);
            Misc.ConvertPrintHex(keyidentification8_bkp, 4, ref keyidentification8_bkp_str);
            Misc.ConvertPrintHex(errcode_bkp, 6, ref errcode_bkp_str);
        }

    }
}
