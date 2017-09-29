using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using carkey.Common;

namespace carkey.Model
{
    class ModelSH46
    {
        private const int SH46_BIN_LEN = 256;

        public byte mnufacturer_ver;
        public byte mnufacturer;
        public string mnufacturer_str;

        public byte activatekey_ver;
        public byte activatekey;
        public string activatekey_str;

        public byte keyidentification_ver;
        public byte[] keyidentification = new byte[20];
        public string keyidentification1_str;
        public string keyidentification2_str;
        public string keyidentification3_str;
        public string keyidentification4_str;
        public string keyidentification5_str;

        public byte pin_ver;
        public byte[] pin = new byte[2];
        public string pin_str;

        public byte field1_ver;
        public byte field1;
        public string field1_str;

        public byte field2_ver;
        public byte field2;
        public string field2_str;

        public byte field3_ver;
        public byte field3;
        public string field3_str;

        public byte keynum_ver;
        public byte keynum;
        public string keynum_str;

        public byte secretkey_ver;
        public byte[] secretkey = new byte[16];
        public string secretkey_str;

        public byte field4_ver;
        public byte[] field4 = new byte[2];
        public string field4_str;

        public byte immobilisercode_ver;
        public byte[] immobilisercode = new byte[10];
        public string immobilisercode_str;

        public byte vid_ver;
        public byte[] vid = new byte[17];
        public string vid_str;
        public string vid_ascii;

        public byte mnufacturer_ver_bkp;        //backup
        public byte mnufacturer_bkp;
        public string mnufacturer_str_bkp;

        public byte activatekey_ver_bkp;
        public byte activatekey_bkp;
        public string activatekey_str_bkp;

        public byte keyidentification_ver_bkp;
        public byte[] keyidentification_bkp = new byte[20];
        public string keyidentification1_str_bkp;
        public string keyidentification2_str_bkp;
        public string keyidentification3_str_bkp;
        public string keyidentification4_str_bkp;
        public string keyidentification5_str_bkp;

        public byte pin_ver_bkp;
        public byte[] pin_bkp = new byte[2];
        public string pin_str_bkp;

        public byte field1_ver_bkp;
        public byte field1_bkp;
        public string field1_str_bkp;

        public byte field2_ver_bkp;
        public byte field2_bkp;
        public string field2_str_bkp;

        public byte field3_ver_bkp;
        public byte field3_bkp;
        public string field3_str_bkp;

        public byte keynum_ver_bkp;
        public byte keynum_bkp;
        public string keynum_str_bkp;

        public byte secretkey_ver_bkp;
        public byte[] secretkey_bkp = new byte[16];
        public string secretkey_str_bkp;

        public ModelSH46(byte[] bin)
        {
            int i = 0, j = 0;
            int len = bin.Length;
            if (len > SH46_BIN_LEN)
                len = SH46_BIN_LEN;
            this.mnufacturer_ver = bin[i++];
            this.mnufacturer = bin[i++];
            this.activatekey_ver = bin[i++];
            this.activatekey = bin[i++];
            this.keyidentification_ver = bin[i++];
            for (j = 0; j < this.keyidentification.Length; j++)
            {
                this.keyidentification[j] = bin[i++];
            }
            this.pin_ver = bin[i++];
            for (j = 0; j < this.pin.Length; j++)
            {
                this.pin[j] = bin[i++];
            }
            this.field1_ver = bin[i++];
            this.field1 = bin[i++];
            this.field2_ver = bin[i++];
            this.field2 = bin[i++];
            this.field3_ver = bin[i++];
            this.field3 = bin[i++];
            this.keynum_ver = bin[i++];
            this.keynum = bin[i++];
            this.secretkey_ver = bin[i++];
            for (j = 0; j < this.secretkey.Length; j++)
            {
                this.secretkey[j] = bin[i++];
            }
            this.field4_ver = bin[i++];
            for (j = 0; j < this.field4.Length; j++)
            {
                this.field4[j] = bin[i++];
            }
            this.immobilisercode_ver = bin[i++];
            for (j = 0; j < this.immobilisercode.Length; j++)
            {
                this.immobilisercode[j] = bin[i++];
            }
            this.vid_ver = bin[i++];
            for (j = 0; j < this.vid.Length; j++)
            {
                this.vid[j] = bin[i++];
            }
            this.vid_ascii = System.Text.Encoding.ASCII.GetString(this.vid);

            i = 0x9a;

            this.mnufacturer_ver_bkp = bin[i++];
            this.mnufacturer_bkp = bin[i++];
            this.activatekey_ver_bkp = bin[i++];
            this.activatekey_bkp = bin[i++];
            this.keyidentification_ver_bkp = bin[i++];
            for (j = 0; j < this.keyidentification_bkp.Length; j++)
            {
                this.keyidentification_bkp[j] = bin[i++];
            }
            this.pin_ver_bkp = bin[i++];
            for (j = 0; j < this.pin_bkp.Length; j++)
            {
                this.pin_bkp[j] = bin[i++];
            }
            this.field1_ver_bkp = bin[i++];
            this.field1_bkp = bin[i++];
            this.field2_ver_bkp = bin[i++];
            this.field2_bkp = bin[i++];
            this.field3_ver_bkp = bin[i++];
            this.field3_bkp = bin[i++];
            this.keynum_ver_bkp = bin[i++];
            this.keynum_bkp = bin[i++];
            this.secretkey_ver_bkp = bin[i++];
            for (j = 0; j < this.secretkey_bkp.Length; j++)
            {
                this.secretkey_bkp[j] = bin[i++];
            }

            Misc.ConvertPrintHex(this.mnufacturer, ref this.mnufacturer_str);
            Misc.ConvertPrintHex(this.activatekey, ref this.activatekey_str);
            Misc.ConvertPrintHex(this.keyidentification, 4, ref this.keyidentification1_str);
            Misc.ConvertPrintHex(this.keyidentification.Skip(4).Take(4).ToArray(), 4, ref this.keyidentification2_str);
            Misc.ConvertPrintHex(this.keyidentification.Skip(8).Take(8).ToArray(), 4, ref this.keyidentification3_str);
            Misc.ConvertPrintHex(this.keyidentification.Skip(12).Take(12).ToArray(), 4, ref this.keyidentification4_str);
            Misc.ConvertPrintHex(this.keyidentification.Skip(16).Take(16).ToArray(), 4, ref this.keyidentification5_str);
            Misc.ConvertPrintHex(this.pin, this.pin.Length, ref this.pin_str);
            Misc.ConvertPrintHex(this.field1, ref this.field1_str);
            Misc.ConvertPrintHex(this.field2, ref this.field2_str);
            Misc.ConvertPrintHex(this.field3, ref this.field3_str);
            Misc.ConvertPrintHex(this.keynum, ref this.keynum_str);
            Misc.ConvertPrintHex(this.secretkey, this.secretkey.Length, ref this.secretkey_str);
            Misc.ConvertPrintHex(this.field4, this.field4.Length, ref this.field4_str);
            Misc.ConvertPrintHex(this.immobilisercode, this.immobilisercode.Length, ref this.immobilisercode_str);
            Misc.ConvertPrintHex(this.vid, this.vid.Length, ref this.vid_str);

            //backups
            Misc.ConvertPrintHex(this.mnufacturer_bkp, ref this.mnufacturer_str_bkp);
            Misc.ConvertPrintHex(this.activatekey_bkp, ref this.activatekey_str_bkp);
            Misc.ConvertPrintHex(this.keyidentification_bkp, 4, ref this.keyidentification1_str_bkp);
            Misc.ConvertPrintHex(this.keyidentification_bkp.Skip(4).Take(4).ToArray(), 4, ref this.keyidentification2_str_bkp);
            Misc.ConvertPrintHex(this.keyidentification_bkp.Skip(8).Take(8).ToArray(), 4, ref this.keyidentification3_str_bkp);
            Misc.ConvertPrintHex(this.keyidentification_bkp.Skip(12).Take(12).ToArray(), 4, ref this.keyidentification4_str_bkp);
            Misc.ConvertPrintHex(this.keyidentification_bkp.Skip(16).Take(16).ToArray(), 4, ref this.keyidentification5_str_bkp);
            Misc.ConvertPrintHex(this.pin_bkp, this.pin_bkp.Length, ref this.pin_str_bkp);
            Misc.ConvertPrintHex(this.field1_bkp, ref this.field1_str_bkp);
            Misc.ConvertPrintHex(this.field2_bkp, ref this.field2_str_bkp);
            Misc.ConvertPrintHex(this.field3_bkp, ref this.field3_str_bkp);
            Misc.ConvertPrintHex(this.keynum_bkp, ref this.keynum_str_bkp);
            Misc.ConvertPrintHex(this.secretkey_bkp, this.secretkey_bkp.Length, ref this.secretkey_str_bkp);
        }

        public bool CheckMnufacturer()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.mnufacturer) == mnufacturer_ver) ? true : false;
        }

        public bool CheckActivateKey()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.activatekey) == activatekey_ver) ? true : false;
        }

        public bool CheckKeyidentification()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.keyidentification, this.keyidentification.Length) == keyidentification_ver) ? true : false;
        }

        public bool CheckPin()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.pin, this.pin.Length) == this.pin_ver) ? true : false;
        }

        public bool CheckField1()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.field1) == this.field1_ver) ? true : false;
        }

        public bool CheckField2()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.field2) == this.field2_ver) ? true : false;
        }

        public bool CheckField3()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.field3) == this.field3_ver) ? true : false;
        }

        public bool CheckKeyNum()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.keynum) == this.keynum_ver) ? true : false;
        }

        public bool CheckSecretkey()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.secretkey, this.secretkey.Length) == this.secretkey_ver) ? true : false;
        }

        public bool CheckField4()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.field4, this.field4.Length) == this.field4_ver) ? true : false;
        }

        public bool CheckImmobilisercode()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.immobilisercode, this.immobilisercode.Length) == this.immobilisercode_ver) ? true : false;
        }

        public bool CheckVid()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.vid, this.vid.Length) == this.vid_ver) ? true : false;
        }

        //backup
        public bool CheckMnufacturerBkp()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.mnufacturer_bkp) == mnufacturer_ver_bkp) ? true : false;
        }

        public bool CheckActivateKeyBkp()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.activatekey_bkp) == activatekey_ver_bkp) ? true : false;
        }

        public bool CheckKeyidentificationBkp()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.keyidentification_bkp, this.keyidentification_bkp.Length) == keyidentification_ver_bkp) ? true : false;
        }

        public bool CheckPinBkp()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.pin_bkp, this.pin_bkp.Length) == this.pin_ver_bkp) ? true : false;
        }

        public bool CheckField1Bkp()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.field1_bkp) == this.field1_ver_bkp) ? true : false;
        }

        public bool CheckField2Bkp()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.field2_bkp) == this.field2_ver_bkp) ? true : false;
        }

        public bool CheckField3Bkp()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.field3) == this.field3_ver) ? true : false; 
        }

        public bool CheckKeyNumBkp()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.keynum_bkp) == this.keynum_ver_bkp) ? true : false;
        }

        public bool CheckSecretkeyBkp()
        {
            return (Misc.ChecksumAccumulatePlusOne(this.secretkey_bkp, this.secretkey_bkp.Length) == this.secretkey_ver_bkp) ? true : false;
        }

        //Check consistency
        public bool CheckConsistencyMnufacturer()
        {
            return (this.mnufacturer) == (this.mnufacturer_bkp) ? true : false;
        }

        public bool CheckConsistencyActivateKey()
        {
            return this.activatekey == this.activatekey_bkp;
        }

        public bool CheckConsistencyKeyidentification()
        {
            return Misc.IsArrayEqual(this.keyidentification, this.keyidentification_bkp);
        }

        public bool CheckConsistencyPin()
        {
            return Misc.IsArrayEqual(this.pin, this.pin_bkp);
        }

        public bool CheckConsistencyField1()
        {
            return this.field1 == this.field1_bkp;
        }

        public bool CheckConsistencyField2()
        {
            return this.field2 == this.field2_bkp;
        }

        public bool CheckConsistencyField3()
        {
            return this.field3 == this.field3_bkp;
        }

        public bool CheckConsistencyKeyNum()
        {
            return this.keynum == this.keynum_bkp;
        }

        public bool CheckConsistencySecretkey()
        {
            return Misc.IsArrayEqual(this.secretkey, this.secretkey_bkp);
        }
    }
}
