using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using carkey.Common;

namespace carkey.Model
{
    class ModelSHLH2
    {
        public byte[] pin = new byte[4];
        public byte pin_ver;
        public string pin_str;

        public byte[] secretkey = new byte[8];
        public byte secretkey_ver;
        public string secretkey_str;

        public byte[] keyidentification1 = new byte[4];
        public byte keyidentification1_ver;
        public string keyidentification1_str;

        public byte[] keyidentification2 = new byte[4];
        public byte keyidentification2_ver;
        public string keyidentification2_str;

        public byte[] keyidentification3 = new byte[4];
        public byte keyidentification3_ver;
        public string keyidentification3_str;

        public byte[] keyidentification4 = new byte[4];
        public byte keyidentification4_ver;
        public string keyidentification4_str;

        public byte[] keyidentification5 = new byte[4];
        public byte keyidentification5_ver;
        public string keyidentification5_str;

        public byte[] vin = new byte[17];
        public string vin_str;
        public string vin_ascii;

        public byte[] pwderrcnt = new byte[4];
        public string pwderrcnt_str;

        //back up
        public byte[] pin_bkp = new byte[4];
        public byte pin_ver_bkp;
        public string pin_str_bkp;

        public byte[] secretkey_bkp = new byte[8];
        public byte secretkey_ver_bkp;
        public string secretkey_str_bkp;

        public byte[] keyidentification1_bkp = new byte[4];
        public byte keyidentification1_ver_bkp;
        public string keyidentification1_str_bkp;

        public byte[] keyidentification2_bkp = new byte[4];
        public byte keyidentification2_ver_bkp;
        public string keyidentification2_str_bkp;

        public byte[] keyidentification3_bkp = new byte[4];
        public byte keyidentification3_ver_bkp;
        public string keyidentification3_str_bkp;

        public byte[] keyidentification4_bkp = new byte[4];
        public byte keyidentification4_ver_bkp;
        public string keyidentification4_str_bkp;

        public byte[] keyidentification5_bkp = new byte[4];
        public byte keyidentification5_ver_bkp;
        public string keyidentification5_str_bkp;

        public byte[] vin_bkp = new byte[17];
        public string vin_str_bkp;
        public string vin_ascii_bkp;

        public ModelSHLH2(byte[] bin)
        {
            int i = 0x00, j = 0;

            for(j = 0 ; j < 4; j++)
            {
                pin[j] = bin[i++];
            }
            pin_ver = bin[i++];

            i = 0x06;
            for (j = 0; j < 8; j++)
            {
                secretkey[j] = bin[i++];
            }
            secretkey_ver = bin[i++];

            i = 0x10;
            for (j = 0; j < 4; j++)
            {
                keyidentification1[j] = bin[i++];
            }
            keyidentification1_ver = bin[i++];

            i = 0x15;
            for (j = 0; j < 4; j++)
            {
                keyidentification2[j] = bin[i++];
            }
            keyidentification2_ver = bin[i++];

            i = 0x1a;
            for (j = 0; j < 4; j++)
            {
                keyidentification3[j] = bin[i++];
            }
            keyidentification3_ver = bin[i++];

            i = 0x20;
            for (j = 0; j < 4; j++)
            {
                keyidentification4[j] = bin[i++];
            }
            keyidentification4_ver = bin[i++];

            i = 0x25;
            for (j = 0; j < 4; j++)
            {
                keyidentification5[j] = bin[i++];
            }
            keyidentification5_ver = bin[i++];

            i = 0x30;
            for (j = 0; j < 17; j++)
            {
                vin[j] = bin[i++];
            }
            this.vin_ascii = System.Text.Encoding.ASCII.GetString(this.vin);

            //back up
            i = 0x80;
            for (j = 0; j < 4; j++)
            {
                pin_bkp[j] = bin[i++];
            }
            pin_ver_bkp = bin[i++];

            i = 0x86;
            for (j = 0; j < 8; j++)
            {
                secretkey_bkp[j] = bin[i++];
            }
            secretkey_ver_bkp = bin[i++];

            i = 0x90;
            for (j = 0; j < 4; j++)
            {
                keyidentification1_bkp[j] = bin[i++];
            }
            keyidentification1_ver_bkp = bin[i++];

            i = 0x95;
            for (j = 0; j < 4; j++)
            {
                keyidentification2_bkp[j] = bin[i++];
            }
            keyidentification2_ver_bkp = bin[i++];

            i = 0x9a;
            for (j = 0; j < 4; j++)
            {
                keyidentification3_bkp[j] = bin[i++];
            }
            keyidentification3_ver_bkp = bin[i++];

            i = 0xa0;
            for (j = 0; j < 4; j++)
            {
                keyidentification4_bkp[j] = bin[i++];
            }
            keyidentification4_ver_bkp = bin[i++];

            i = 0xa5;
            for (j = 0; j < 4; j++)
            {
                keyidentification5_bkp[j] = bin[i++];
            }
            keyidentification5_ver_bkp = bin[i++];

            i = 0xb0;
            for (j = 0; j < 17; j++)
            {
                vin_bkp[j] = bin[i++];
            }
            this.vin_ascii_bkp = System.Text.Encoding.ASCII.GetString(this.vin);

            i = 0xf5;
            for (j = 0; j < 4; j++)
            {
                pwderrcnt[j] = bin[i++];
            }

            Misc.ConvertPrintHex(pin, 4, ref pin_str);
            Misc.ConvertPrintHex(secretkey, 8, ref secretkey_str);
            Misc.ConvertPrintHex(vin, 17, ref vin_str);
            Misc.ConvertPrintHex(keyidentification1, 4, ref keyidentification1_str);
            Misc.ConvertPrintHex(keyidentification2, 4, ref keyidentification2_str);
            Misc.ConvertPrintHex(keyidentification3, 4, ref keyidentification3_str);
            Misc.ConvertPrintHex(keyidentification4, 4, ref keyidentification4_str);
            Misc.ConvertPrintHex(keyidentification5, 4, ref keyidentification5_str);
            Misc.ConvertPrintHex(pwderrcnt, 4, ref pwderrcnt_str);

            //back up
            Misc.ConvertPrintHex(pin_bkp, 4, ref pin_str_bkp);
            Misc.ConvertPrintHex(secretkey_bkp, 8, ref secretkey_str_bkp);
            Misc.ConvertPrintHex(vin_bkp, 17, ref vin_str_bkp);
            Misc.ConvertPrintHex(keyidentification1_bkp, 4, ref keyidentification1_str_bkp);
            Misc.ConvertPrintHex(keyidentification2_bkp, 4, ref keyidentification2_str_bkp);
            Misc.ConvertPrintHex(keyidentification3_bkp, 4, ref keyidentification3_str_bkp);
            Misc.ConvertPrintHex(keyidentification4_bkp, 4, ref keyidentification4_str_bkp);
            Misc.ConvertPrintHex(keyidentification5_bkp, 4, ref keyidentification5_str_bkp);
        }

        public bool CheckPin()
        {
            return (Misc.CheckSumAccumulate(this.pin, this.pin.Length) == this.pin_ver) ? true : false;
        }

        public bool CheckSecretKey()
        {
            return (Misc.CheckSumAccumulate(this.secretkey, this.secretkey.Length) == this.secretkey_ver) ? true : false;
        }

        public bool CheckKeyIdentification1()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification1, this.keyidentification1.Length) == this.keyidentification1_ver) ? true : false;
        }

        public bool CheckKeyIdentification2()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification2, this.keyidentification2.Length) == this.keyidentification2_ver) ? true : false;
        }

        public bool CheckKeyIdentification3()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification3, this.keyidentification3.Length) == this.keyidentification3_ver) ? true : false;
        }

        public bool CheckKeyIdentification4()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification4, this.keyidentification4.Length) == this.keyidentification4_ver) ? true : false;
        }

        public bool CheckKeyIdentification5()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification5, this.keyidentification5.Length) == this.keyidentification5_ver) ? true : false;
        }


        //back up
        public bool CheckPinBkp()
        {
            return (Misc.CheckSumAccumulate(this.pin_bkp, this.pin_bkp.Length) == this.pin_ver_bkp) ? true : false;
        }

        public bool CheckSecretKeyBkp()
        {
            return (Misc.CheckSumAccumulate(this.secretkey_bkp, this.secretkey_bkp.Length) == this.secretkey_ver_bkp) ? true : false;
        }

        public bool CheckKeyIdentification1Bkp()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification1_bkp, this.keyidentification1_bkp.Length) == this.keyidentification1_ver_bkp) ? true : false;
        }

        public bool CheckKeyIdentification2Bkp()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification2_bkp, this.keyidentification2_bkp.Length) == this.keyidentification2_ver_bkp) ? true : false;
        }

        public bool CheckKeyIdentification3Bkp()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification3_bkp, this.keyidentification3_bkp.Length) == this.keyidentification3_ver_bkp) ? true : false;
        }

        public bool CheckKeyIdentification4Bkp()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification4_bkp, this.keyidentification4_bkp.Length) == this.keyidentification4_ver_bkp) ? true : false;
        }

        public bool CheckKeyIdentification5Bkp()
        {
            return (Misc.CheckSumAccumulate(this.keyidentification5_bkp, this.keyidentification5_bkp.Length) == this.keyidentification5_ver_bkp) ? true : false;
        }

        //Check consistency
        public bool CheckConsistencyPin()
        {
            return Misc.IsArrayEqual(this.pin, this.pin_bkp);
        }

        public bool CheckConsistencySecretKey()
        {
            return Misc.IsArrayEqual(this.secretkey, this.secretkey_bkp);
        }

        public bool CheckConsistencyKeyId1()
        {
            return Misc.IsArrayEqual(this.keyidentification1, this.keyidentification1_bkp);
        }

        public bool CheckConsistencyKeyId2()
        {
            return Misc.IsArrayEqual(this.keyidentification2, this.keyidentification2_bkp);
        }

        public bool CheckConsistencyKeyId3()
        {
            return Misc.IsArrayEqual(this.keyidentification3, this.keyidentification3_bkp);
        }

        public bool CheckConsistencyKeyId4()
        {
            return Misc.IsArrayEqual(this.keyidentification4, this.keyidentification4_bkp);
        }

        public bool CheckConsistencyKeyId5()
        {
            return Misc.IsArrayEqual(this.keyidentification5, this.keyidentification5_bkp);
        }

        public bool CheckConsistencyVin()
        {
            return Misc.IsArrayEqual(this.vin, this.vin_bkp);
        }
    }
}
