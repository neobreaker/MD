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

namespace carkey.View
{
    /// <summary>
    /// WinSHLH2.xaml 的交互逻辑
    /// </summary>
    public partial class WinSHLH2 : Window
    {
        private const int BYTE_BUFF_SIZE = 1024;

        private int m_bytes_len = 0;
        private byte[] m_encryptbytes = new byte[BYTE_BUFF_SIZE];
        private byte[] m_decryptbytes = new byte[BYTE_BUFF_SIZE];

        public WinSHLH2()
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

        private void CheckDataValid(ModelSHLH2 shlh2)
        {
            if (shlh2.CheckPin())
                this.tbPin.Background = new SolidColorBrush(Colors.White);
            else
                this.tbPin.Background = new SolidColorBrush(Colors.Red);

            if (shlh2.CheckSecretKey())
                this.tbSecretKey.Background = new SolidColorBrush(Colors.White);
            else
                this.tbSecretKey.Background = new SolidColorBrush(Colors.Red);

            if (shlh2.CheckKeyIdentification1())
                this.tbKeyIdentification1.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification1.Background = new SolidColorBrush(Colors.Red);

            if (shlh2.CheckKeyIdentification2())
                this.tbKeyIdentification2.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification2.Background = new SolidColorBrush(Colors.Red);

            if (shlh2.CheckKeyIdentification3())
                this.tbKeyIdentification3.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification3.Background = new SolidColorBrush(Colors.Red);

            if (shlh2.CheckKeyIdentification4())
                this.tbKeyIdentification4.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification4.Background = new SolidColorBrush(Colors.Red);

            if (shlh2.CheckKeyIdentification5())
                this.tbKeyIdentification5.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification5.Background = new SolidColorBrush(Colors.Red);


            if (shlh2.CheckPinBkp())
            {
                if (shlh2.CheckConsistencyPin())
                {
                    this.tbPinBkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                }
                else
                {
                    this.tbPinBkp.Background = new SolidColorBrush(Colors.Yellow);
                }
            }
            else
            {
                this.tbPinBkp.Background = new SolidColorBrush(Colors.Red);
            }

            if (shlh2.CheckSecretKeyBkp())
            {
                if (shlh2.CheckConsistencySecretKey())
                {
                    this.tbSecretKeyBkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                }
                else
                {
                    this.tbSecretKeyBkp.Background = new SolidColorBrush(Colors.Yellow);
                }
            }
            else
            {
                this.tbSecretKeyBkp.Background = new SolidColorBrush(Colors.Red);
            }

            if (shlh2.CheckKeyIdentification1Bkp())
            {
                if (shlh2.CheckConsistencyKeyId1())
                {
                    this.tbKeyIdentification1Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                }
                else
                {
                    this.tbKeyIdentification1Bkp.Background = new SolidColorBrush(Colors.Yellow);
                }
            }
            else
            {
                this.tbKeyIdentification1Bkp.Background = new SolidColorBrush(Colors.Red);
            }

            if (shlh2.CheckKeyIdentification2Bkp())
            {
                if (shlh2.CheckConsistencyKeyId2())
                {
                    this.tbKeyIdentification2Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                }
                else
                {
                    this.tbKeyIdentification2Bkp.Background = new SolidColorBrush(Colors.Yellow);
                }
            }
            else
            {
                this.tbKeyIdentification2Bkp.Background = new SolidColorBrush(Colors.Red);
            }

            if (shlh2.CheckKeyIdentification3Bkp())
            {
                if (shlh2.CheckConsistencyKeyId3())
                {
                    this.tbKeyIdentification3Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                }
                else
                {
                    this.tbKeyIdentification3Bkp.Background = new SolidColorBrush(Colors.Yellow);
                }
            }
            else
            {
                this.tbKeyIdentification3Bkp.Background = new SolidColorBrush(Colors.Red);
            }

            if (shlh2.CheckKeyIdentification4Bkp())
            {
                if (shlh2.CheckConsistencyKeyId4())
                {
                    this.tbKeyIdentification4Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                }
                else
                {
                    this.tbKeyIdentification4Bkp.Background = new SolidColorBrush(Colors.Yellow);
                }
            }
            else
            {
                this.tbKeyIdentification4Bkp.Background = new SolidColorBrush(Colors.Red);
            }

            if (shlh2.CheckKeyIdentification5Bkp())
            {
                if (shlh2.CheckConsistencyKeyId5())
                {
                    this.tbKeyIdentification5Bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                }
                else
                {
                    this.tbKeyIdentification5Bkp.Background = new SolidColorBrush(Colors.Yellow);
                }
            }
            else
            {
                this.tbKeyIdentification5Bkp.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void UpdateDisplay()
        {
            Model.ModelSHLH2 shlh2 = new ModelSHLH2(m_decryptbytes);

            this.tbPin.Text = shlh2.pin_str;
            this.tbSecretKey.Text = shlh2.secretkey_str;
            this.tbKeyIdentification1.Text = shlh2.keyidentification1_str;
            this.tbKeyIdentification2.Text = shlh2.keyidentification2_str;
            this.tbKeyIdentification3.Text = shlh2.keyidentification3_str;
            this.tbKeyIdentification4.Text = shlh2.keyidentification3_str;
            this.tbKeyIdentification5.Text = shlh2.keyidentification3_str;
            this.tbVIN.Text = shlh2.vin_str;
            this.tbVINASCII.Text = shlh2.vin_ascii;
            this.tbPwdErrCnt.Text = shlh2.pwderrcnt_str;

            //back up
            this.tbPinBkp.Text = shlh2.pin_str_bkp;
            this.tbSecretKeyBkp.Text = shlh2.secretkey_str_bkp;
            this.tbKeyIdentification1Bkp.Text = shlh2.keyidentification1_str_bkp;
            this.tbKeyIdentification2Bkp.Text = shlh2.keyidentification2_str_bkp;
            this.tbKeyIdentification3Bkp.Text = shlh2.keyidentification3_str_bkp;
            this.tbKeyIdentification4Bkp.Text = shlh2.keyidentification3_str_bkp;
            this.tbKeyIdentification5Bkp.Text = shlh2.keyidentification3_str_bkp;
            this.tbVINBkp.Text = shlh2.vin_str_bkp;
            this.tbVINBkpASCII.Text = shlh2.vin_ascii_bkp;

            this.uchbDecrypy.SetHexbox(m_decryptbytes);
            this.uchbEncrypy.SetHexbox(m_encryptbytes);

            CheckDataValid(shlh2);
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
    }
}
