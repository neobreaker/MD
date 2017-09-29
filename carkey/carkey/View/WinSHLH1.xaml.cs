using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
