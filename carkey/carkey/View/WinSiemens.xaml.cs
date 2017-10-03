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
            {
                this.tbImmobiliserCode1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                this.tbImmobiliserCode1ASCII.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                this.tbImmobiliserCode1.Background = new SolidColorBrush(Colors.Red);
                this.tbImmobiliserCode1ASCII.Background = new SolidColorBrush(Colors.Red);
            }

            if (m_siemens.CheckImmobiliserCode2())
            {
                this.tbImmobiliserCode2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                this.tbImmobiliserCode2ASCII.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                this.tbImmobiliserCode2.Background = new SolidColorBrush(Colors.Red);
                this.tbImmobiliserCode2ASCII.Background = new SolidColorBrush(Colors.Red);
            }

            if (m_siemens.CheckField1())
                this.tbField1.Background = new SolidColorBrush(Colors.White);
            else
                this.tbField1.Background = new SolidColorBrush(Colors.Red);

            if (m_siemens.CheckFactoryDate())
            {
                this.tbFactoryDate.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                this.tbFactoryDateASCII.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                this.tbFactoryDate.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                this.tbFactoryDateASCII.Background = new SolidColorBrush(Colors.Red);
            }

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
            this.tbImmobiliserCode1ASCII.Text = m_siemens.immobilisercode1_ascii;
            this.tbImmobiliserCode2.Text = m_siemens.immobilisercode2_str;
            this.tbImmobiliserCode2ASCII.Text = m_siemens.immobilisercode2_ascii;
            this.tbField1.Text = m_siemens.field1_str;
            this.tbFactoryDate.Text = m_siemens.factorydate_str;
            this.tbFactoryDateASCII.Text = m_siemens.factorydate_ascii;
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

        private void Fixkeyidentification1(StringBuilder log)
        {
            string fix = "钥匙标识码1";

            log.Append("检查" + fix + "数据...");
            if (m_siemens.CheckKeyIdentification1())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_siemens.CheckKeyIdentification1Bkp() && m_siemens.CheckConsistencyKeyId1())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_siemens.keyidentification1, m_siemens.keyidentification1_bkp, m_siemens.keyidentification1.Length);
                    m_siemens.keyidentification1_ver_bkp = m_siemens.keyidentification1_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_siemens.CheckKeyIdentification1Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_siemens.keyidentification1_bkp, m_siemens.keyidentification1, m_siemens.keyidentification1_bkp.Length);
                    m_siemens.keyidentification1_ver = m_siemens.keyidentification1_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void Fixkeyidentification2(StringBuilder log)
        {
            string fix = "钥匙标识码2";

            log.Append("检查" + fix + "数据...");
            if (m_siemens.CheckKeyIdentification2())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_siemens.CheckKeyIdentification2Bkp() && m_siemens.CheckConsistencyKeyId2())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_siemens.keyidentification2, m_siemens.keyidentification2_bkp, m_siemens.keyidentification2.Length);
                    m_siemens.keyidentification2_ver_bkp = m_siemens.keyidentification2_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_siemens.CheckKeyIdentification2Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_siemens.keyidentification2_bkp, m_siemens.keyidentification2, m_siemens.keyidentification2_bkp.Length);
                    m_siemens.keyidentification2_ver = m_siemens.keyidentification2_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void Fixkeyidentification3(StringBuilder log)
        {
            string fix = "钥匙标识码3";

            log.Append("检查" + fix + "数据...");
            if (m_siemens.CheckKeyIdentification3())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_siemens.CheckKeyIdentification3Bkp() && m_siemens.CheckConsistencyKeyId3())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_siemens.keyidentification3, m_siemens.keyidentification3_bkp, m_siemens.keyidentification3.Length);
                    m_siemens.keyidentification3_ver_bkp = m_siemens.keyidentification3_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_siemens.CheckKeyIdentification3Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_siemens.keyidentification3_bkp, m_siemens.keyidentification3, m_siemens.keyidentification3_bkp.Length);
                    m_siemens.keyidentification3_ver = m_siemens.keyidentification3_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void Fixkeyidentification4(StringBuilder log)
        {
            string fix = "钥匙标识码4";

            log.Append("检查" + fix + "数据...");
            if (m_siemens.CheckKeyIdentification4())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_siemens.CheckKeyIdentification4Bkp() && m_siemens.CheckConsistencyKeyId4())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_siemens.keyidentification4, m_siemens.keyidentification4_bkp, m_siemens.keyidentification4.Length);
                    m_siemens.keyidentification4_ver_bkp = m_siemens.keyidentification4_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_siemens.CheckKeyIdentification4Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_siemens.keyidentification4_bkp, m_siemens.keyidentification4, m_siemens.keyidentification4_bkp.Length);
                    m_siemens.keyidentification4_ver = m_siemens.keyidentification4_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void Fixkeyidentification5(StringBuilder log)
        {
            string fix = "钥匙标识码5";

            log.Append("检查" + fix + "数据...");
            if (m_siemens.CheckKeyIdentification5())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_siemens.CheckKeyIdentification5Bkp() && m_siemens.CheckConsistencyKeyId5())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_siemens.keyidentification5, m_siemens.keyidentification5_bkp, m_siemens.keyidentification5.Length);
                    m_siemens.keyidentification5_ver_bkp = m_siemens.keyidentification5_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_siemens.CheckKeyIdentification5Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_siemens.keyidentification5_bkp, m_siemens.keyidentification5, m_siemens.keyidentification5_bkp.Length);
                    m_siemens.keyidentification5_ver = m_siemens.keyidentification5_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void FixPin(StringBuilder log)
        {
            string fix = "PIN";

            log.Append("检查" + fix + "数据...");
            if (m_siemens.CheckPin())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_siemens.CheckPinBkp() && m_siemens.CheckConsistencyPin())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_siemens.pin, m_siemens.pin_bkp, m_siemens.pin.Length);
                    m_siemens.pin_ver_bkp = m_siemens.pin_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_siemens.CheckPinBkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_siemens.pin_bkp, m_siemens.pin, m_siemens.pin_bkp.Length);
                    m_siemens.pin_ver = m_siemens.pin_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void FixSecretKey(StringBuilder log)
        {
            string fix = "秘钥码";

            log.Append("检查" + fix + "数据...");
            if (m_siemens.CheckSecretKey())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_siemens.CheckSecretKeyBkp() && m_siemens.CheckConsistencySecretKey())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_siemens.secretkey, m_siemens.secretkey_bkp, m_siemens.secretkey.Length);
                    m_siemens.secretkey_ver_bkp = m_siemens.secretkey_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_siemens.CheckSecretKeyBkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_siemens.secretkey_bkp, m_siemens.secretkey, m_siemens.secretkey_bkp.Length);
                    m_siemens.secretkey_ver = m_siemens.secretkey_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void FixManufacturer(StringBuilder log)
        {
            string fix = "厂商";

            log.Append("检查" + fix + "数据...");
            if (m_siemens.CheckManufacturer())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_siemens.CheckManufacturerBkp() && m_siemens.CheckConsistencyManufacturer())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_siemens.manufacturer, m_siemens.manufacturer_bkp, m_siemens.manufacturer.Length);
                    m_siemens.manufacturer_ver_bkp = m_siemens.manufacturer_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_siemens.CheckManufacturerBkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_siemens.manufacturer_bkp, m_siemens.manufacturer, m_siemens.manufacturer_bkp.Length);
                    m_siemens.manufacturer_ver = m_siemens.manufacturer_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void FixData(StringBuilder sblog)
        {
            Fixkeyidentification1(sblog);
            Fixkeyidentification2(sblog);
            Fixkeyidentification3(sblog);
            Fixkeyidentification4(sblog);
            Fixkeyidentification5(sblog);
            FixPin(sblog);
            FixSecretKey(sblog);
            FixManufacturer(sblog);
        }

        private void btnFix_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sblog = new StringBuilder(512);
            FixData(sblog);
            this.tbInfo.Text = sblog.ToString();
            UpdateDisplay();
        }

        private void UpdateField(int field_idx, int field_ver_idx, string input_str, int num)
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
                tmp_byte[i] = this.m_decryptbytes[field_idx + i];
            }

            this.m_decryptbytes[field_ver_idx] = m_siemens.CheckSumSiemens(tmp_byte, num);
        }

        private void UpdateField(int field_idx, int field_bkp_idx, int field_ver_idx, int field_ver_idx_bkp, string input_str, int num)
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
                this.m_decryptbytes[field_bkp_idx + i] = this.m_decryptbytes[field_idx + i];
                tmp_byte[i] = this.m_decryptbytes[field_idx + i];
            }

            this.m_decryptbytes[field_ver_idx] = m_siemens.CheckSumSiemens(tmp_byte, num);
            this.m_decryptbytes[field_ver_idx_bkp] = this.m_decryptbytes[field_ver_idx];
        }

        private void Updatekeyidentification1()
        {
            int field_idx = 0x15, field_bkp_idx = 0xc3, field_ver_idx = 0x14, field_ver_idx_bkp = 0xc2, field_len = 4;

            UpdateField(field_idx, field_bkp_idx, field_ver_idx, field_ver_idx_bkp, this.tbKeyIdentification1.Text, field_len);
        }

        private void Updatekeyidentification2()
        {
            int field_idx = 0x1a, field_bkp_idx = 0xc8, field_ver_idx = 0x19, field_ver_idx_bkp = 0xc7, field_len = 4;

            UpdateField(field_idx, field_bkp_idx, field_ver_idx, field_ver_idx_bkp, this.tbKeyIdentification2.Text, field_len);
        }

        private void Updatekeyidentification3()
        {
            int field_idx = 0x1f, field_bkp_idx = 0xcd, field_ver_idx = 0x1e, field_ver_idx_bkp = 0xcc, field_len = 4;

            UpdateField(field_idx, field_bkp_idx, field_ver_idx, field_ver_idx_bkp, this.tbKeyIdentification3.Text, field_len);
        }

        private void Updatekeyidentification4()
        {
            int field_idx = 0x24, field_bkp_idx = 0xd2, field_ver_idx = 0x23, field_ver_idx_bkp = 0xd1, field_len = 4;

            UpdateField(field_idx, field_bkp_idx, field_ver_idx, field_ver_idx_bkp, this.tbKeyIdentification4.Text, field_len);
        }

        private void Updatekeyidentification5()
        {
            int field_idx = 0x29, field_bkp_idx = 0xd7, field_ver_idx = 0x28, field_ver_idx_bkp = 0xd6, field_len = 4;

            UpdateField(field_idx, field_bkp_idx, field_ver_idx, field_ver_idx_bkp, this.tbKeyIdentification5.Text, field_len);
        }

        private void UpdatePin()
        {
            int field_idx = 0x2e, field_bkp_idx = 0xdc, field_ver_idx = 0x2d, field_ver_idx_bkp = 0xdb, field_len = 2;

            UpdateField(field_idx, field_bkp_idx, field_ver_idx, field_ver_idx_bkp, this.tbPin.Text, field_len);
        }

        private void UpdateSecretKey()
        {
            int field_idx = 0x31, field_bkp_idx = 0xdf, field_ver_idx = 0x30, field_ver_idx_bkp = 0xde, field_len = 16;

            UpdateField(field_idx, field_bkp_idx, field_ver_idx, field_ver_idx_bkp, this.tbSecretKey.Text, field_len);
        }

        private void UpdateManufacturer()
        {
            int field_idx = 0x42, field_idx_bkp = 0xf0, field_ver_idx = 0x41, field_ver_idx_bkp = 0xef, field_len = 4;

            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] manufacturer = ASCII.GetBytes(this.tbManufacturerASCII.Text);

            for (int i = 0; i < field_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = manufacturer[i];
                this.m_decryptbytes[field_idx_bkp + i] = manufacturer[i];
            }

            this.m_decryptbytes[field_ver_idx] = m_siemens.CheckSumSiemens(manufacturer, field_len);
            this.m_decryptbytes[field_ver_idx_bkp] = this.m_decryptbytes[field_ver_idx];
        }

        private void UpdateErrCode1()
        {
            int field_idx = 0x47, field_ver_idx = 0x46, field_len = 1;

            UpdateField(field_idx, field_ver_idx, this.tbErrCode1.Text, field_len);
        }

        private void UpdateErrCode2()
        {
            int field_idx = 0x49, field_ver_idx = 0x48, field_len = 1;

            UpdateField(field_idx, field_ver_idx, this.tbErrCode2.Text, field_len);
        }

        private void UpdateErrCode3()
        {
            int field_idx = 0x4b, field_ver_idx = 0x4a, field_len = 1;

            UpdateField(field_idx, field_ver_idx, this.tbErrCode3.Text, field_len);
        }

        private void UpdateErrCode4()
        {
            int field_idx = 0x4d, field_ver_idx = 0x4c, field_len = 1;

            UpdateField(field_idx, field_ver_idx, this.tbErrCode4.Text, field_len);
        }

        private void UpdateVIN()
        {
            int field_idx = 0x4f, field_ver_idx = 0x4e, field_len = 17;

            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] manufacturer = ASCII.GetBytes(this.tbvinASCII.Text);

            for (int i = 0; i < field_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = manufacturer[i];
            }

            this.m_decryptbytes[field_ver_idx] = m_siemens.CheckSumSiemens(manufacturer, field_len);
        }

        private void UpdateImmobiliserCode1()
        {
            int field_idx = 0x61, field_ver_idx = 0x60, field_len = 13;

            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] data = ASCII.GetBytes(this.tbImmobiliserCode1ASCII.Text);

            for (int i = 0; i < field_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = data[i];
            }

            this.m_decryptbytes[field_ver_idx] = m_siemens.CheckSumSiemens(data, field_len);
        }

        private void UpdateImmobiliserCode2()
        {
            int field_idx = 0x6f, field_ver_idx = 0x6e, field_len = 8;

            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] data = ASCII.GetBytes(this.tbImmobiliserCode2ASCII.Text);

            for (int i = 0; i < field_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = data[i];
            }

            this.m_decryptbytes[field_ver_idx] = m_siemens.CheckSumSiemens(data, field_len);
        }

        private void UpdateField1()
        {
            int field_idx = 0x78, field_ver_idx = 0x77, field_len = 5;

            UpdateField(field_idx, field_ver_idx, this.tbField1.Text, field_len);
        }

        private void UpdateFactoryDate()
        {
            int field_idx = 0x7e, field_ver_idx = 0x7d, field_len = 6;
            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] data = ASCII.GetBytes(this.tbFactoryDateASCII.Text);

            for (int i = 0; i < field_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = data[i];
            }

            this.m_decryptbytes[field_ver_idx] = m_siemens.CheckSumSiemens(data, field_len);
        }

        private void UpdateSoftwareVersion()
        {
            int field_idx = 0x85, field_ver_idx = 0x84, field_len = 11;

            UpdateField(field_idx, field_ver_idx, this.tbSoftwareVersion.Text, field_len);
        }

        private void UpdateDecrypt()
        {
            Updatekeyidentification1();
            Updatekeyidentification2();
            Updatekeyidentification3();
            Updatekeyidentification4();
            Updatekeyidentification5();
            UpdatePin();
            UpdateSecretKey();
            UpdateManufacturer();
            UpdateErrCode1();
            UpdateErrCode2();
            UpdateErrCode3();
            UpdateErrCode4();
            UpdateVIN();
            UpdateImmobiliserCode1();
            UpdateImmobiliserCode2();
            UpdateField1();
            UpdateFactoryDate();
            UpdateSoftwareVersion();
        }

        private void UpdateEncrypt()
        {
            EnOrDecryptBytes(m_decryptbytes, m_bytes_len, ref m_encryptbytes);
        }

        private void btnAlter_Click(object sender, RoutedEventArgs e)
        {
            UpdateDecrypt();
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

        private void tbKeyIdentification1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x15, 4);
        }

        private void tbKeyIdentification1Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xc3, 4);
        }

        private void tbKeyIdentification2_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x1a, 4);
        }

        private void tbKeyIdentification2Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xc8, 4);
        }

        private void tbKeyIdentification3_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x1f, 4);
        }

        private void tbKeyIdentification3Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xcd, 4);
        }

        private void tbKeyIdentification4_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x24, 4);
        }

        private void tbKeyIdentification4Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xd2, 4);
        }

        private void tbKeyIdentification5_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x29, 4);
        }

        private void tbKeyIdentification5Bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xd7, 4);
        }

        private void tbPin_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x2e, 2);
        }

        private void tbPinBkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xdc, 2);
        }

        private void tbSecretKey_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x31, 16);
        }

        private void tbSecretKeyBkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xdf, 16);
        }

        private void tbManufacturer_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x42, 4);
        }

        private void tbManufacturerASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x42, 4);
        }

        private void tbManufacturerBkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xf0, 4);
        }

        private void tbManufacturerASCIIBkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0xf0, 4);
        }

        private void tbErrCode1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x47, 1);
        }

        private void tbErrCode2_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x49, 1);
        }

        private void tbErrCode3_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x4b, 1);
        }


        private void tbErrCode4_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x4d, 1);
        }

        private void tbvin_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x4f, 17);
        }

        private void tbvinASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x4f, 17);
        }

        private void tbImmobiliserCode1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x61, 13);
        }

        private void tbImmobiliserCode1ASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x61, 13);
        }

        private void tbImmobiliserCode2_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x6f, 8);
        }

        private void tbImmobiliserCode2ASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x6f, 8);
        }

        private void tbField1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x78, 5);
        }


        private void tbFactoryDate_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x7e, 6);
        }

        private void tbFactoryDateASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x7e, 6);
        }

        private void tbSoftwareVersion_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x85, 11);
        }

    }
}
