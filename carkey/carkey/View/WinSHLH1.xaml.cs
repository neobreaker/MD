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

namespace carkey.View
{
    /// <summary>
    /// WinSHLH1.xaml 的交互逻辑
    /// </summary>
    public partial class WinSHLH1 : Window
    {
        private const int BYTE_BUFF_SIZE = 1024;

        private int m_bytes_len = 0;
        private byte[] m_encryptbytes = new byte[BYTE_BUFF_SIZE];
        private byte[] m_decryptbytes = new byte[BYTE_BUFF_SIZE];

        public WinSHLH1()
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

        private void CheckDataValid(Model.ModelSHLH1 shlh1)
        {
            if (shlh1.CheckConsistencyPin())
                this.tbPinBkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbPinBkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencySecretKey())
                this.tbSecretKeyBkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbSecretKeyBkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyManufacturer())
                this.tbManufacturerBkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbManufacturerBkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyVIN())
            {
                this.tbVINBkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                this.tbVINBkpASCII.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            }
            else
            {
                this.tbVINBkp.Background = new SolidColorBrush(Colors.Yellow);
                this.tbVINBkpASCII.Background = new SolidColorBrush(Colors.Yellow);
            }

            if (shlh1.CheckConsistencyKeyidentification1())
                this.tbKeyIdentification1Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbKeyIdentification1Bkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyKeyidentification2())
                this.tbKeyIdentification2Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbKeyIdentification2Bkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyKeyidentification3())
                this.tbKeyIdentification3Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbKeyIdentification3Bkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyKeyidentification4())
                this.tbKeyIdentification4Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbKeyIdentification4Bkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyKeyidentification5())
                this.tbKeyIdentification5Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbKeyIdentification5Bkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyKeyidentification6())
                this.tbKeyIdentification6Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbKeyIdentification6Bkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyKeyidentification7())
                this.tbKeyIdentification7Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbKeyIdentification7Bkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyKeyidentification8())
                this.tbKeyIdentification8Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbKeyIdentification8Bkp.Background = new SolidColorBrush(Colors.Yellow);

            if (shlh1.CheckConsistencyErrcode())
                this.tbErrCodeBkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbErrCodeBkp.Background = new SolidColorBrush(Colors.Yellow);
        }

        private void UpdateDisplay()
        {
            Model.ModelSHLH1 shlh1 = new Model.ModelSHLH1(m_decryptbytes);

            this.tbPin.Text = shlh1.pin_str;
            this.tbSecretKey.Text = shlh1.secretkey_str;
            this.tbManufacturer.Text = shlh1.manufacturer_str;
            this.tbVIN.Text = shlh1.vin_str;
            this.tbVINASCII.Text = shlh1.vin_ascii;
            this.tbKeyIdentification1.Text = shlh1.keyidentification1_str;
            this.tbKeyIdentification2.Text = shlh1.keyidentification2_str;
            this.tbKeyIdentification3.Text = shlh1.keyidentification3_str;
            this.tbKeyIdentification4.Text = shlh1.keyidentification4_str;
            this.tbKeyIdentification5.Text = shlh1.keyidentification5_str;
            this.tbKeyIdentification6.Text = shlh1.keyidentification6_str;
            this.tbKeyIdentification7.Text = shlh1.keyidentification7_str;
            this.tbKeyIdentification8.Text = shlh1.keyidentification8_str;
            this.tbErrCode.Text = shlh1.errcode_str;
            this.tbPwdErrCnt.Text = shlh1.pwderrcnt_str;

            //back up
            this.tbPinBkp.Text = shlh1.pin_bkp_str;
            this.tbSecretKeyBkp.Text = shlh1.secretkey_bkp_str;
            this.tbManufacturerBkp.Text = shlh1.manufacturer_bkp_str;
            this.tbVINBkp.Text = shlh1.vin_bkp_str;
            this.tbVINBkpASCII.Text = shlh1.vin_bkp_ascii;
            this.tbKeyIdentification1Bkp.Text = shlh1.keyidentification1_bkp_str;
            this.tbKeyIdentification2Bkp.Text = shlh1.keyidentification2_bkp_str;
            this.tbKeyIdentification3Bkp.Text = shlh1.keyidentification3_bkp_str;
            this.tbKeyIdentification4Bkp.Text = shlh1.keyidentification4_bkp_str;
            this.tbKeyIdentification5Bkp.Text = shlh1.keyidentification5_bkp_str;
            this.tbKeyIdentification6Bkp.Text = shlh1.keyidentification6_bkp_str;
            this.tbKeyIdentification7Bkp.Text = shlh1.keyidentification7_bkp_str;
            this.tbKeyIdentification8Bkp.Text = shlh1.keyidentification8_bkp_str;
            this.tbErrCodeBkp.Text = shlh1.errcode_bkp_str;

            this.uchbDecrypy.SetHexbox(m_decryptbytes);
            this.uchbEncrypy.SetHexbox(m_encryptbytes);

            CheckDataValid(shlh1);
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

        private void tbPin_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x11, 4);
        }

        private void tbPinBkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xa1, 4);
        }

        private void tbSecretKey_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x15, 8);
        }

        private void tbSecretKeyBkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xa5, 8);
        }

        private void tbManufacturer_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x1d, 2);
        }

        private void tbManufacturerBkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xad, 2);
        }

        private void tbVIN_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x1f, 17);
        }

        private void tbVINASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x1f, 17);
        }

        private void tbVINBkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xaf, 17);
        }

        private void tbVINBkpASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xaf, 17);
        }

        private void tbKeyIdentification1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x30, 4);
        }

        private void tbKeyIdentification1Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xc0, 4);
        }

        private void tbKeyIdentification2_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x34, 4);
        }

        private void tbKeyIdentification2Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xc4, 4);
        }

        private void tbKeyIdentification3_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x38, 4);
        }

        private void tbKeyIdentification3Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xc8, 4);
        }

        private void tbKeyIdentification4_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x3c, 4);
        }

        private void tbKeyIdentification4Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xcc, 4);
        }

        private void tbKeyIdentification5_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x40, 4);
        }

        private void tbKeyIdentification5Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xd0, 4);
        }

        private void tbKeyIdentification6_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x44, 4);
        }

        private void tbKeyIdentification6Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xd4, 4);
        }

        private void tbKeyIdentification7_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x48, 4);
        }

        private void tbKeyIdentification7Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xd8, 4);
        }

        private void tbKeyIdentification8_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x4c, 4);
        }

        private void tbKeyIdentification8Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xdc, 4);
        }

        private void tbErrCode_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x54, 6);
        }

        private void tbErrCodeBkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xe4, 4);
        }

        private void tbPwdErrCnt_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x81, 5);
        }

        private void UpdatePin()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x11, 4, this.tbPin.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xa1, 4, this.tbPin.Text);
        }

        private void UpdateSecretKey()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x15, 8, this.tbSecretKey.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xa5, 8, this.tbSecretKey.Text);
        }

        private void UpdateManufacturer()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x1d, 2, this.tbManufacturer.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xad, 2, this.tbManufacturer.Text);
        }

        private void UpdatevinVIN()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x1f, 17, this.tbVIN.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xaf, 17, this.tbVIN.Text);
        }

        private void UpdatevinKeyIdentification1()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x30, 4, this.tbKeyIdentification1.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xc0, 4, this.tbKeyIdentification1.Text);
        }

        private void UpdatevinKeyIdentification2()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x34, 4, this.tbKeyIdentification2.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xc4, 4, this.tbKeyIdentification2.Text);
        }

        private void UpdatevinKeyIdentification3()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x38, 4, this.tbKeyIdentification3.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xc8, 4, this.tbKeyIdentification3.Text);
        }

        private void UpdatevinKeyIdentification4()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x3c, 4, this.tbKeyIdentification4.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xcc, 4, this.tbKeyIdentification4.Text);
        }

        private void UpdatevinKeyIdentification5()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x40, 4, this.tbKeyIdentification5.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xd0, 4, this.tbKeyIdentification5.Text);
        }

        private void UpdatevinKeyIdentification6()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x44, 4, this.tbKeyIdentification6.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xd4, 4, this.tbKeyIdentification6.Text);
        }

        private void UpdatevinKeyIdentification7()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x48, 4, this.tbKeyIdentification7.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xd8, 4, this.tbKeyIdentification7.Text);
        }

        private void UpdatevinKeyIdentification8()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x4c, 4, this.tbKeyIdentification8.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xdc, 4, this.tbKeyIdentification8.Text);
        }

        private void UpdatevinErrCode()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x54, 6, this.tbErrCode.Text);
            Misc.DoUpdateArray(m_decryptbytes, 0xe4, 6, this.tbErrCode.Text);
        }

        private void UpdatevinPwdErrCnt()
        {
            Misc.DoUpdateArray(m_decryptbytes, 0x81, 5, this.tbPwdErrCnt.Text);
        }

        private void UpdateDecrtpt()
        {
            UpdatePin();
            UpdateSecretKey();
            UpdateManufacturer();
            UpdatevinVIN();
            UpdatevinKeyIdentification1();
            UpdatevinKeyIdentification2();
            UpdatevinKeyIdentification3();
            UpdatevinKeyIdentification4();
            UpdatevinKeyIdentification5();
            UpdatevinKeyIdentification6();
            UpdatevinKeyIdentification7();
            UpdatevinKeyIdentification8();
            UpdatevinErrCode();
            UpdatevinPwdErrCnt();
        }

        private void UpdateEncrtpt()
        {
            EnOrDecryptBytes(m_decryptbytes, m_bytes_len, ref m_encryptbytes);
        }

        private void btnAlter_Click(object sender, RoutedEventArgs e)
        {
            UpdateDecrtpt();
            UpdateEncrtpt();
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
