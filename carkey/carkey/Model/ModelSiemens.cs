using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using carkey.Common;

namespace carkey.Model
{
    class ModelSiemens
    {
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

        public byte[] pin = new byte[2];
        public byte pin_ver;
        public string pin_str;

        public byte[] secretkey = new byte[16];
        public byte secretkey_ver;
        public string secretkey_str;

        public byte[] manufacturer = new byte[4];
        public byte manufacturer_ver;
        public string manufacturer_str;
        public string manufacturer_ascii;

        public byte errcode1 = 0;
        public byte errcode1_ver;
        public string errcode1_str;

        public byte errcode2 = 0;
        public byte errcode2_ver;
        public string errcode2_str;

        public byte errcode3 = 0;
        public byte errcode3_ver;
        public string errcode3_str;

        public byte errcode4 = 0;
        public byte errcode4_ver;
        public string errcode4_str;

        public byte[] vin = new byte[17];
        public byte vin_ver;
        public string vin_str;
        public string vin_ascii;

        public byte[] field2 = new byte[2];
        public byte field2_ver;
        public string field2_str;

        public byte[] immobilisercode1 = new byte[10];
        public byte immobilisercode1_ver;
        public string immobilisercode1_str;
        public string immobilisercode1_ascii;

        public byte[] immobilisercode2 = new byte[8];
        public byte immobilisercode2_ver;
        public string immobilisercode2_str;
        public string immobilisercode2_ascii;

        public byte[] field1 = new byte[6];
        public byte field1_ver;
        public string field1_str;

        public byte[] factorydate = new byte[6];
        public byte factorydate_ver;
        public string factorydate_str;
        public string factorydate_ascii;

        public byte[] softwareversion = new byte[10];
        public byte softwareversion_ver;
        public string softwareversion_str;

        //back up
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

        public byte[] pin_bkp = new byte[2];
        public byte pin_ver_bkp;
        public string pin_str_bkp;

        public byte[] secretkey_bkp = new byte[16];
        public byte secretkey_ver_bkp;
        public string secretkey_str_bkp;

        public byte[] manufacturer_bkp = new byte[4];
        public byte manufacturer_ver_bkp;
        public string manufacturer_str_bkp;
        public string manufacturer_ascii_bkp;


        public ModelSiemens(byte[] bin)
        {
            int i = 0x00, j = 0;

            i = 0x14;
            keyidentification1_ver = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification1[j] = bin[i++];
            }

            i = 0x19;
            keyidentification2_ver = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification2[j] = bin[i++];
            }
            
            i = 0x1e;
            keyidentification3_ver = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification3[j] = bin[i++];
            }
            
            i = 0x23;
            keyidentification4_ver = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification4[j] = bin[i++];
            }
            
            i = 0x28;
            keyidentification5_ver = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification5[j] = bin[i++];
            }

            i = 0x2d;
            pin_ver = bin[i++];
            for (j = 0; j < 2; j++)
            {
                pin[j] = bin[i++];
            }

            i = 0x30;
            secretkey_ver = bin[i++];
            for (j = 0; j < 16; j++)
            {
                secretkey[j] = bin[i++];
            }

            i = 0x41;
            manufacturer_ver = bin[i++];
            for (j = 0; j < 4; j++)             //len
            {
                manufacturer[j] = bin[i++];
            }
            this.manufacturer_ascii = System.Text.Encoding.ASCII.GetString(this.manufacturer);

            i = 0x46;
            errcode1_ver = bin[i++];
            errcode1 = bin[i];

            i = 0x48;
            errcode2_ver = bin[i++];
            errcode2 = bin[i];

            i = 0x4a;
            errcode3_ver = bin[i++];
            errcode3 = bin[i];

            i = 0x4c;
            errcode4_ver = bin[i++];
            errcode4 = bin[i];

            i = 0x4e;
            vin_ver = bin[i++];
            for (j = 0; j < 17; j++)             //len
            {
                vin[j] = bin[i++];
            }
            this.vin_ascii = System.Text.Encoding.ASCII.GetString(this.vin);

            i = 0x60;
            field2_ver = bin[i++];
            for (j = 0; j < 2; j++)             //len
            {
                field2[j] = bin[i++];
            }

            i = 0x63;
            immobilisercode1_ver = bin[i++];
            for (j = 0; j < 10; j++)             //len
            {
                immobilisercode1[j] = bin[i++];
            }
            this.immobilisercode1_ascii = System.Text.Encoding.ASCII.GetString(this.immobilisercode1);

            i = 0x6e;
            immobilisercode2_ver = bin[i++];
            for (j = 0; j < 8; j++)             //len
            {
                immobilisercode2[j] = bin[i++];
            }
            this.immobilisercode2_ascii = System.Text.Encoding.ASCII.GetString(this.immobilisercode2);

            i = 0x77;
            field1_ver = bin[i++];
            for (j = 0; j < 5; j++)             //len
            {
                field1[j] = bin[i++];
            }

            i = 0x7d;
            factorydate_ver = bin[i++];
            for (j = 0; j < 6; j++)             //len
            {
                factorydate[j] = bin[i++];
            }
            this.factorydate_ascii = System.Text.Encoding.ASCII.GetString(this.factorydate);

            i = 0x84;
            softwareversion_ver = bin[i++];
            for (j = 0; j < 10; j++)             //len
            {
                softwareversion[j] = bin[i++];
            }

            //back up
            i = 0xc2;
            keyidentification1_ver_bkp = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification1_bkp[j] = bin[i++];
            }

            i = 0xc7;
            keyidentification2_ver_bkp = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification2_bkp[j] = bin[i++];
            }

            i = 0xcc;
            keyidentification3_ver_bkp = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification3_bkp[j] = bin[i++];
            }

            i = 0xd1;
            keyidentification4_ver_bkp = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification4_bkp[j] = bin[i++];
            }

            i = 0xd6;
            keyidentification5_ver_bkp = bin[i++];
            for (j = 0; j < 4; j++)
            {
                keyidentification5_bkp[j] = bin[i++];
            }

            i = 0xdb;
            pin_ver_bkp = bin[i++];
            for (j = 0; j < 2; j++)
            {
                pin_bkp[j] = bin[i++];
            }

            i = 0xde;
            secretkey_ver_bkp = bin[i++];
            for (j = 0; j < 16; j++)
            {
                secretkey_bkp[j] = bin[i++];
            }

            i = 0xef;
            manufacturer_ver_bkp = bin[i++];
            for (j = 0; j < 4; j++)             //len
            {
                manufacturer_bkp[j] = bin[i++];
            }
            this.manufacturer_ascii_bkp = System.Text.Encoding.ASCII.GetString(this.manufacturer_bkp);
            
            Misc.ConvertPrintHex(keyidentification1, 4, ref keyidentification1_str);
            Misc.ConvertPrintHex(keyidentification2, 4, ref keyidentification2_str);
            Misc.ConvertPrintHex(keyidentification3, 4, ref keyidentification3_str);
            Misc.ConvertPrintHex(keyidentification4, 4, ref keyidentification4_str);
            Misc.ConvertPrintHex(keyidentification5, 4, ref keyidentification5_str);
            Misc.ConvertPrintHex(pin, 2, ref pin_str);
            Misc.ConvertPrintHex(secretkey, 16, ref secretkey_str);
            Misc.ConvertPrintHex(manufacturer, 4, ref manufacturer_str);
            errcode1_str = Convert.ToString(errcode1, 16);
            errcode2_str = Convert.ToString(errcode2, 16);
            errcode3_str = Convert.ToString(errcode3, 16);
            errcode4_str = Convert.ToString(errcode4, 16);
            Misc.ConvertPrintHex(vin, 17, ref vin_str);
            Misc.ConvertPrintHex(field2, 2, ref field2_str);
            Misc.ConvertPrintHex(immobilisercode1, 10, ref immobilisercode1_str);
            Misc.ConvertPrintHex(immobilisercode2, 8, ref immobilisercode2_str);
            Misc.ConvertPrintHex(field1, 5, ref field1_str);
            Misc.ConvertPrintHex(factorydate, 6, ref factorydate_str);
            Misc.ConvertPrintHex(softwareversion, 10, ref softwareversion_str);

            //back up
            Misc.ConvertPrintHex(keyidentification1_bkp, 4, ref keyidentification1_str_bkp);
            Misc.ConvertPrintHex(keyidentification2_bkp, 4, ref keyidentification2_str_bkp);
            Misc.ConvertPrintHex(keyidentification3_bkp, 4, ref keyidentification3_str_bkp);
            Misc.ConvertPrintHex(keyidentification4_bkp, 4, ref keyidentification4_str_bkp);
            Misc.ConvertPrintHex(keyidentification5_bkp, 4, ref keyidentification5_str_bkp);
            Misc.ConvertPrintHex(pin_bkp, 2, ref pin_str_bkp);
            Misc.ConvertPrintHex(secretkey_bkp, 16, ref secretkey_str_bkp);
            Misc.ConvertPrintHex(manufacturer_bkp, 4, ref manufacturer_str_bkp);
            
        }

        private byte CheckSumSiemens(byte data)
        {
            int i = 0;
            int num = data;
            byte check_sum = 0, hi = 0, low = 0;
            
            num = num & 0xffff;
            num = num ^ 0xffff;

            while (num > 0xff)
            {
                hi = (byte)((byte)(num & 0xff00) >> 8);
                low = (byte)(num & 0xff);
                num = (hi - 1 + low);
            }
            check_sum = (byte)(num & 0xff);
            return check_sum;
        }

        public byte CheckSumSiemens(byte[] data, int len)
        {
            int i = 0;
            int num = 0;
            byte check_sum = 0, hi = 0, low = 0;
            for (i = 0; i < len; i++)
            {
                num += data[i];
            }
            num = num & 0xffff;
            num = num ^ 0xffff;
            while (num > 0xff)
            {
                hi = (byte)((num & 0xff00) >> 8);
                low = (byte)(num & 0xff);
                num = (hi + low);
            }
            num--;
            check_sum = (byte)(num & 0xff);
            return check_sum;
        }

        public bool CheckKeyIdentification1()
        {
            return (CheckSumSiemens(this.keyidentification1, this.keyidentification1.Length) == this.keyidentification1_ver) ? true : false;
        }

        public bool CheckKeyIdentification2()
        {
            return (CheckSumSiemens(this.keyidentification2, this.keyidentification2.Length) == this.keyidentification2_ver) ? true : false;
        }

        public bool CheckKeyIdentification3()
        {
            return (CheckSumSiemens(this.keyidentification3, this.keyidentification3.Length) == this.keyidentification3_ver) ? true : false;
        }

        public bool CheckKeyIdentification4()
        {
            return (CheckSumSiemens(this.keyidentification4, this.keyidentification4.Length) == this.keyidentification4_ver) ? true : false;
        }

        public bool CheckKeyIdentification5()
        {
            return (CheckSumSiemens(this.keyidentification5, this.keyidentification5.Length) == this.keyidentification5_ver) ? true : false;
        }

        public bool CheckPin()
        {
            return (CheckSumSiemens(this.pin, this.pin.Length) == this.pin_ver) ? true : false;
        }

        public bool CheckSecretKey()
        {
            return (CheckSumSiemens(this.secretkey, this.secretkey.Length) == this.secretkey_ver) ? true : false;
        }

        public bool CheckManufacturer()
        {
            return (CheckSumSiemens(this.manufacturer, this.manufacturer.Length) == this.manufacturer_ver) ? true : false;
        }

        public bool CheckErrCode1()
        {
            return (CheckSumSiemens(this.errcode1) == this.errcode1_ver) ? true : false;
        }

        public bool CheckErrCode2()
        {
            return (CheckSumSiemens(this.errcode2) == this.errcode2_ver) ? true : false;
        }

        public bool CheckErrCode3()
        {
            return (CheckSumSiemens(this.errcode3) == this.errcode3_ver) ? true : false;
        }

        public bool CheckErrCode4()
        {
            return (CheckSumSiemens(this.errcode4) == this.errcode4_ver) ? true : false;
        }

        public bool CheckVIN()
        {
            return (CheckSumSiemens(this.vin, this.vin.Length) == this.vin_ver) ? true : false;
        }

        public bool CheckField2()
        {
            return (CheckSumSiemens(this.field2, this.field2.Length) == this.field2_ver) ? true : false;
        }

        public bool CheckImmobiliserCode1()
        {
            return (CheckSumSiemens(this.immobilisercode1, this.immobilisercode1.Length) == this.immobilisercode1_ver) ? true : false;
        }

        public bool CheckImmobiliserCode2()
        {
            return (CheckSumSiemens(this.immobilisercode2, this.immobilisercode2.Length) == this.immobilisercode2_ver) ? true : false;
        }

        public bool CheckField1()
        {
            return (CheckSumSiemens(this.field1, this.field1.Length) == this.field1_ver) ? true : false;
        }

        public bool CheckFactoryDate()
        {
            return (CheckSumSiemens(this.factorydate, this.factorydate.Length) == this.factorydate_ver) ? true : false;
        }

        public bool CheckSoftwareVersion()
        {
            return (CheckSumSiemens(this.softwareversion, this.softwareversion.Length-1) == this.softwareversion_ver) ? true : false;
        }

        //back up
        public bool CheckKeyIdentification1Bkp()
        {
            return (CheckSumSiemens(this.keyidentification1_bkp, this.keyidentification1_bkp.Length) == this.keyidentification1_ver_bkp) ? true : false;
        }

        public bool CheckKeyIdentification2Bkp()
        {
            return (CheckSumSiemens(this.keyidentification2_bkp, this.keyidentification2_bkp.Length) == this.keyidentification2_ver_bkp) ? true : false;
        }

        public bool CheckKeyIdentification3Bkp()
        {
            return (CheckSumSiemens(this.keyidentification3_bkp, this.keyidentification3_bkp.Length) == this.keyidentification3_ver_bkp) ? true : false;
        }

        public bool CheckKeyIdentification4Bkp()
        {
            return (CheckSumSiemens(this.keyidentification4_bkp, this.keyidentification4_bkp.Length) == this.keyidentification4_ver_bkp) ? true : false;
        }

        public bool CheckKeyIdentification5Bkp()
        {
            return (CheckSumSiemens(this.keyidentification5_bkp, this.keyidentification5_bkp.Length) == this.keyidentification5_ver_bkp) ? true : false;
        }

        public bool CheckPinBkp()
        {
            return (CheckSumSiemens(this.pin_bkp, this.pin_bkp.Length) == this.pin_ver_bkp) ? true : false;
        }

        public bool CheckSecretKeyBkp()
        {
            return (CheckSumSiemens(this.secretkey_bkp, this.secretkey_bkp.Length) == this.secretkey_ver_bkp) ? true : false;
        }

        public bool CheckManufacturerBkp()
        {
            return (CheckSumSiemens(this.manufacturer_bkp, this.manufacturer_bkp.Length) == this.manufacturer_ver_bkp) ? true : false;
        }

        //Check consistency
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

        public bool CheckConsistencyPin()
        {
            return Misc.IsArrayEqual(this.pin, this.pin_bkp);
        }

        public bool CheckConsistencySecretKey()
        {
            return Misc.IsArrayEqual(this.secretkey, this.secretkey_bkp);
        }

        public bool CheckConsistencyManufacturer()
        {
            return Misc.IsArrayEqual(this.manufacturer, this.manufacturer_bkp);
        }
    }

}
