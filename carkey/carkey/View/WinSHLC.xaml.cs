using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using carkey.Common;
using carkey.Model;
using carkey.UC;

namespace carkey.View
{
    /// <summary>
    /// WinSHLC.xaml 的交互逻辑
    /// </summary>
    public partial class WinSHLC : Window
    {
        private const int BYTE_BUFF_SIZE = 1024;

        private int m_bytes_len = 0;
        private byte[] m_encryptbytes = new byte[BYTE_BUFF_SIZE];
        private byte[] m_decryptbytes = new byte[BYTE_BUFF_SIZE];

        public WinSHLC()
        {
            InitializeComponent();
        }

        private void EnOrDecryptBytes(byte[] src, int len, ref byte[] dst)
        {
            int i = 0;
            byte hi = 0, low = 0;

            for (i = 0; i < len; i++)
            {
                hi = (byte)(src[i] >> 4);
                low = (byte)(src[i] & 0xf);
                dst[i] = (byte)((low << 4) | hi);
                dst[i] = (byte)(dst[i] ^ 0xbb);
            }
        }

        private void UpdateDisplay()
        {
            Model.ModelSHLC shlc = new ModelSHLC(m_decryptbytes);

            this.tbManufacturer.Text = shlc.mnufacturer_str;
            this.tbSecretkey.Text = shlc.secretkey_str;
            this.tbPin.Text = shlc.pin_str;
            this.tbKeyIdentification1.Text = shlc.keyidentification1_str;
            this.tbKeyIdentification2.Text = shlc.keyidentification2_str;
            this.tbKeyIdentification3.Text = shlc.keyidentification3_str;
            this.tbKeyIdentification4.Text = shlc.keyidentification4_str;
            this.tbKeyIdentification5.Text = shlc.keyidentification5_str;
            this.tbKeyIdentification6.Text = shlc.keyidentification6_str;
            this.tbKeyIdentification7.Text = shlc.keyidentification7_str;
            this.tbKeyIdentification8.Text = shlc.keyidentification8_str;
            this.tbField1.Text = shlc.field1_str;
            this.tbErrorCode.Text = shlc.errcode_str;
            this.tbVIN.Text = shlc.vin_str;
            this.tbVINASCII.Text = shlc.vin_ascii;

            this.uchbDecrypy.SetHexbox(m_decryptbytes);
            this.uchbEncrypy.SetHexbox(m_encryptbytes);
        }

        private void LoadData()
        {
            UpdateDisplay();
        }

        private void menuOpenEncrypyFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "bin|*.BIN" + "|AllFiles|*.*";
                var result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    m_bytes_len = Misc.ReadHex(openFileDialog.FileName, ref m_encryptbytes, BYTE_BUFF_SIZE);
                    EnOrDecryptBytes(m_encryptbytes, m_bytes_len, ref m_decryptbytes);
                    LoadData();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void menuOpenDecrypyFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "bin|*.BIN" + "|AllFiles|*.*";
                var result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    m_bytes_len = Misc.ReadHex(openFileDialog.FileName, ref m_decryptbytes, BYTE_BUFF_SIZE);
                    EnOrDecryptBytes(m_decryptbytes, m_bytes_len, ref m_encryptbytes);
                    LoadData();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void tbManufacturer_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x1c, 2);
        }

        private void tbSecretkey_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x20, 8);
        }

        private void tbPin_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x30, 4);
        }

        private void tbKeyIdentification1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x40, 4);
        }

        private void tbKeyIdentification2_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x44, 4);
        }

        private void tbKeyIdentification3_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x48, 4);
        }

        private void tbKeyIdentification4_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x4c, 4);
        }

        private void tbKeyIdentification5_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x50, 4);
        }

        private void tbKeyIdentification6_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x54, 4);
        }

        private void tbKeyIdentification7_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x58, 4);
        }

        private void tbKeyIdentification8_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x5c, 4);
        }

        private void tbField1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xa0, 17);
        }

        private void tbErrorCode_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xb1, 5);
        }

        private void tbVIN_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xc0, 17);
        }

        private void tbVINASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xc0, 17);
        }

        private void DoUpdateArray(int field_idx, string input_str, int num)
        {
            int i = 0;
            string[] str = new string[32];
            byte[] tmp_byte = new byte[32];

            Misc.ParseTextInput(input_str, ref str);

            for (i = 0; i < num; i++)
            {
                if (str[i] == "")
                    return;
            }

            for (i = 0; i < num; i++)
            {
                this.m_decryptbytes[field_idx + i] = Convert.ToByte(str[i], 16);
            }

        }

        private void UpdateManufacturer()
        {
            DoUpdateArray(0x1c, this.tbManufacturer.Text, 2);
        }

        private void UpdateSecretKey()
        {
            DoUpdateArray(0x20, this.tbSecretkey.Text, 8);
        }

        private void UpdatePin()
        {
            DoUpdateArray(0x30, this.tbPin.Text, 4);
        }

        private void UpdateKeyIdentification1()
        {
            DoUpdateArray(0x40, this.tbKeyIdentification1.Text, 4);
        }

        private void UpdateKeyIdentification2()
        {
            DoUpdateArray(0x44, this.tbKeyIdentification2.Text, 4);
        }

        private void UpdateKeyIdentification3()
        {
            DoUpdateArray(0x48, this.tbKeyIdentification3.Text, 4);
        }

        private void UpdateKeyIdentification4()
        {
            DoUpdateArray(0x4c, this.tbKeyIdentification4.Text, 4);
        }

        private void UpdateKeyIdentification5()
        {
            DoUpdateArray(0x50, this.tbKeyIdentification5.Text, 4);
        }

        private void UpdateKeyIdentification6()
        {
            DoUpdateArray(0x54, this.tbKeyIdentification6.Text, 4);
        }

        private void UpdateKeyIdentification7()
        {
            DoUpdateArray(0x58, this.tbKeyIdentification7.Text, 4);
        }

        private void UpdateKeyIdentification8()
        {
            DoUpdateArray(0x5c, this.tbKeyIdentification8.Text, 4);
        }

        private void UpdateField1()
        {
            DoUpdateArray(0xa0, this.tbField1.Text, 17);
        }

        private void UpdateErrCode()
        {
            DoUpdateArray(0xb1, this.tbErrorCode.Text, 5);
        }

        private void UpdateVIN()
        {
            int i = 0, vid_len = 17;
            int field_idx = 0xc0;
            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] vid = ASCII.GetBytes(this.tbVINASCII.Text);

            for (i = 0; i < vid_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = vid[i];
            }
        }

        private void UpdateDecrtpt()
        {
            UpdateManufacturer();
            UpdateSecretKey();
            UpdatePin();
            UpdateKeyIdentification1();
            UpdateKeyIdentification2();
            UpdateKeyIdentification3();
            UpdateKeyIdentification4();
            UpdateKeyIdentification5();
            UpdateKeyIdentification6();
            UpdateKeyIdentification7();
            UpdateKeyIdentification8();
            UpdateField1();
            UpdateErrCode();
            UpdateVIN();
        }

        private void UpdateEncrypt()
        {
            EnOrDecryptBytes(m_decryptbytes, m_bytes_len, ref m_encryptbytes);
        }

        private void btnAlter_Click(object sender, RoutedEventArgs e)
        {
            UpdateDecrtpt();
            UpdateEncrypt();
            UpdateDisplay();
        }

        private void btnExportDecrypt_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDecryptDialog = new SaveFileDialog();
            saveDecryptDialog.Filter = "bin|*.BIN" + "|AllFiles|*.*";

            var result = saveDecryptDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string file_path = saveDecryptDialog.FileName;
                FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate);
                fs.Write(m_decryptbytes, 0, m_bytes_len);
                fs.Flush();
                fs.Close();
            }
        }

        private void btnExportEncrypt_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDecryptDialog = new SaveFileDialog();
            saveDecryptDialog.Filter = "bin|*.BIN" + "|AllFiles|*.*";

            var result = saveDecryptDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string file_path = saveDecryptDialog.FileName;
                FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate);
                fs.Write(m_encryptbytes, 0, m_bytes_len);
                fs.Flush();
                fs.Close();
            }
        }
    }
}
