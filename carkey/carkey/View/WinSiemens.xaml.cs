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
    /// WinSiemens.xaml 的交互逻辑
    /// </summary>
    public partial class WinSiemens : Window
    {
        private const int BYTE_BUFF_SIZE = 1024;

        private int m_bytes_len = 0;
        private byte[] m_encryptbytes = new byte[BYTE_BUFF_SIZE];
        private byte[] m_decryptbytes = new byte[BYTE_BUFF_SIZE];

        private ModelSiemens m_siemens = null;

        public WinSiemens()
        {
            InitializeComponent();
        }

        private void CheckDataValid()
        {
            if (m_siemens.CheckKeyIdentification1())
                this.tbKeyIdentification1.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification1.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckKeyIdentification2())
                this.tbKeyIdentification2.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification2.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckKeyIdentification3())
                this.tbKeyIdentification3.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification3.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckKeyIdentification4())
                this.tbKeyIdentification4.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification4.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckKeyIdentification5())
                this.tbKeyIdentification5.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification5.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckPin())
                this.tbPin.Background = new SolidColorBrush(Colors.White);
            else
                this.tbPin.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckSecretKey())
                this.tbSecretKey.Background = new SolidColorBrush(Colors.White);
            else
                this.tbSecretKey.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckManufacturer())
            {
                this.tbManufacturer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                this.tbManufacturerASCII.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                this.tbManufacturer.Background = new SolidColorBrush(Colors.Red);
                this.tbManufacturerASCII.Background = new SolidColorBrush(Colors.Red);
            }

            if (m_siemens.CheckErrCode1())
                this.tbErrCode1.Background = new SolidColorBrush(Colors.White);
            else
                this.tbErrCode1.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckErrCode2())
                this.tbErrCode2.Background = new SolidColorBrush(Colors.White);
            else
                this.tbErrCode2.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckErrCode3())
                this.tbErrCode3.Background = new SolidColorBrush(Colors.White);
            else
                this.tbErrCode3.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckErrCode4())
                this.tbErrCode4.Background = new SolidColorBrush(Colors.White);
            else
                this.tbErrCode4.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckVIN())
            {
                this.tbvin.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                this.tbvinASCII.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                this.tbvin.Background = new SolidColorBrush(Colors.Red);
                this.tbvinASCII.Background = new SolidColorBrush(Colors.Red);
            }

            if (m_siemens.CheckImmobiliserCode1())
                this.tbImmobiliserCode1.Background = new SolidColorBrush(Colors.White);
            else
                this.tbImmobiliserCode1.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckImmobiliserCode2())
                this.tbImmobiliserCode2.Background = new SolidColorBrush(Colors.White);
            else
                this.tbImmobiliserCode2.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckField1())
                this.tbField1.Background = new SolidColorBrush(Colors.White);
            else
                this.tbField1.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckFactoryDate())
                this.tbFactoryDate.Background = new SolidColorBrush(Colors.White);
            else
                this.tbFactoryDate.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckSoftwareVersion())
                this.tbSoftwareVersion.Background = new SolidColorBrush(Colors.White);
            else
                this.tbSoftwareVersion.Background = new SolidColorBrush(Colors.Red);

            //back up
            if (m_siemens.CheckKeyIdentification1Bkp())
            {
                if (m_siemens.CheckConsistencyKeyId1())
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

            if (m_siemens.CheckKeyIdentification2Bkp())
            {
                if (m_siemens.CheckConsistencyKeyId2())
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

            if (m_siemens.CheckKeyIdentification3Bkp())
            {
                if (m_siemens.CheckConsistencyKeyId3())
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

            if (m_siemens.CheckKeyIdentification4Bkp())
            {
                if (m_siemens.CheckConsistencyKeyId4())
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

            if (m_siemens.CheckKeyIdentification5Bkp())
            {
                if (m_siemens.CheckConsistencyKeyId5())
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

            if (m_siemens.CheckPinBkp())
            {
                if (m_siemens.CheckConsistencyPin())
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

            if (m_siemens.CheckSecretKeyBkp())
            {
                if (m_siemens.CheckConsistencySecretKey())
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

            if (m_siemens.CheckManufacturerBkp())
            {
                if (m_siemens.CheckConsistencyManufacturer())
                {
                    this.tbManufacturerBkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                    this.tbManufacturerASCIIBkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                }
                else
                {
                    this.tbManufacturerBkp.Background = new SolidColorBrush(Colors.Yellow);
                    this.tbManufacturerASCIIBkp.Background = new SolidColorBrush(Colors.Yellow);
                }
            }
            else
            {
                this.tbManufacturerBkp.Background = new SolidColorBrush(Colors.Red);
                this.tbManufacturerASCIIBkp.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void UpdateDisplay()
        {
            m_siemens = new ModelSiemens(m_decryptbytes);

            this.tbKeyIdentification1.Text = m_siemens.keyidentification1_str;
            this.tbKeyIdentification2.Text = m_siemens.keyidentification2_str;
            this.tbKeyIdentification3.Text = m_siemens.keyidentification3_str;
            this.tbKeyIdentification4.Text = m_siemens.keyidentification4_str;
            this.tbKeyIdentification5.Text = m_siemens.keyidentification5_str;
            this.tbPin.Text = m_siemens.pin_str;
            this.tbSecretKey.Text = m_siemens.secretkey_str;
            this.tbManufacturer.Text = m_siemens.manufacturer_str;
            this.tbManufacturerASCII.Text = m_siemens.manufacturer_ascii;
            this.tbManufacturerBkp.Text = m_siemens.manufacturer_str_bkp;
            this.tbManufacturerASCIIBkp.Text = m_siemens.manufacturer_ascii_bkp;
            this.tbErrCode1.Text = m_siemens.errcode1_str;
            this.tbErrCode2.Text = m_siemens.errcode2_str;
            this.tbErrCode3.Text = m_siemens.errcode3_str;
            this.tbErrCode4.Text = m_siemens.errcode4_str;
            this.tbvin.Text = m_siemens.vin_str;
            this.tbvinASCII.Text = m_siemens.vin_ascii;
            this.tbImmobiliserCode1.Text = m_siemens.immobilisercode1_str;
            this.tbImmobiliserCode2.Text = m_siemens.immobilisercode2_str;
            this.tbField1.Text = m_siemens.field1_str;
            this.tbFactoryDate.Text = m_siemens.factorydate_str;
            this.tbSoftwareVersion.Text = m_siemens.softwareversion_str;

            //back up
            this.tbKeyIdentification1Bkp.Text = m_siemens.keyidentification1_str_bkp;
            this.tbKeyIdentification2Bkp.Text = m_siemens.keyidentification2_str_bkp;
            this.tbKeyIdentification3Bkp.Text = m_siemens.keyidentification3_str_bkp;
            this.tbKeyIdentification4Bkp.Text = m_siemens.keyidentification4_str_bkp;
            this.tbKeyIdentification5Bkp.Text = m_siemens.keyidentification5_str_bkp;
            this.tbPinBkp.Text = m_siemens.pin_str_bkp;
            this.tbSecretKeyBkp.Text = m_siemens.secretkey_str_bkp;

            this.uchbDecrypy.SetHexbox(m_decryptbytes);
            this.uchbEncrypy.SetHexbox(m_encryptbytes);

            CheckDataValid();
        }

        private void LoadData()
        {
            UpdateDisplay();
        }

        private void EnOrDecryptBytes(byte[] src, int len, ref byte[] dst)
        {
            int i = 0;

            for (i = 0; i < len; i+=2)
            {
                dst[i] = src[i + 1];
                dst[i + 1] = src[i];
            }
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
