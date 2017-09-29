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
using carkey.Model;

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
                dst[i] = (byte)((hi << 4) | low);
                dst[i] = (byte)(dst[i] ^ 0xbb);
            }
        }

        void LoadData()
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
