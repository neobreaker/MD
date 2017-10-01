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

        private Model.ModelSHLH2 m_shlh2 = null
;

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

        private void CheckDataValid(ModelSHLH2 m_shlh2)
        {
            if (m_shlh2.CheckPin())
                this.tbPin.Background = new SolidColorBrush(Colors.White);
            else
                this.tbPin.Background = new SolidColorBrush(Colors.Red);

            if (m_shlh2.CheckSecretKey())
                this.tbSecretKey.Background = new SolidColorBrush(Colors.White);
            else
                this.tbSecretKey.Background = new SolidColorBrush(Colors.Red);

            if (m_shlh2.CheckKeyIdentification1())
                this.tbKeyIdentification1.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification1.Background = new SolidColorBrush(Colors.Red);

            if (m_shlh2.CheckKeyIdentification2())
                this.tbKeyIdentification2.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification2.Background = new SolidColorBrush(Colors.Red);

            if (m_shlh2.CheckKeyIdentification3())
                this.tbKeyIdentification3.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification3.Background = new SolidColorBrush(Colors.Red);

            if (m_shlh2.CheckKeyIdentification4())
                this.tbKeyIdentification4.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification4.Background = new SolidColorBrush(Colors.Red);

            if (m_shlh2.CheckKeyIdentification5())
                this.tbKeyIdentification5.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyIdentification5.Background = new SolidColorBrush(Colors.Red);


            if (m_shlh2.CheckPinBkp())
            {
                if (m_shlh2.CheckConsistencyPin())
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

            if (m_shlh2.CheckSecretKeyBkp())
            {
                if (m_shlh2.CheckConsistencySecretKey())
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

            if (m_shlh2.CheckKeyIdentification1Bkp())
            {
                if (m_shlh2.CheckConsistencyKeyId1())
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

            if (m_shlh2.CheckKeyIdentification2Bkp())
            {
                if (m_shlh2.CheckConsistencyKeyId2())
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

            if (m_shlh2.CheckKeyIdentification3Bkp())
            {
                if (m_shlh2.CheckConsistencyKeyId3())
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

            if (m_shlh2.CheckKeyIdentification4Bkp())
            {
                if (m_shlh2.CheckConsistencyKeyId4())
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

            if (m_shlh2.CheckKeyIdentification5Bkp())
            {
                if (m_shlh2.CheckConsistencyKeyId5())
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

            m_shlh2 = new ModelSHLH2(m_decryptbytes);

            this.tbPin.Text = m_shlh2.pin_str;
            this.tbSecretKey.Text = m_shlh2.secretkey_str;
            this.tbKeyIdentification1.Text = m_shlh2.keyidentification1_str;
            this.tbKeyIdentification2.Text = m_shlh2.keyidentification2_str;
            this.tbKeyIdentification3.Text = m_shlh2.keyidentification3_str;
            this.tbKeyIdentification4.Text = m_shlh2.keyidentification3_str;
            this.tbKeyIdentification5.Text = m_shlh2.keyidentification3_str;
            this.tbVIN.Text = m_shlh2.vin_str;
            this.tbVINASCII.Text = m_shlh2.vin_ascii;
            this.tbPwdErrCnt.Text = m_shlh2.pwderrcnt_str;

            //back up
            this.tbPinBkp.Text = m_shlh2.pin_str_bkp;
            this.tbSecretKeyBkp.Text = m_shlh2.secretkey_str_bkp;
            this.tbKeyIdentification1Bkp.Text = m_shlh2.keyidentification1_str_bkp;
            this.tbKeyIdentification2Bkp.Text = m_shlh2.keyidentification2_str_bkp;
            this.tbKeyIdentification3Bkp.Text = m_shlh2.keyidentification3_str_bkp;
            this.tbKeyIdentification4Bkp.Text = m_shlh2.keyidentification3_str_bkp;
            this.tbKeyIdentification5Bkp.Text = m_shlh2.keyidentification3_str_bkp;
            this.tbVINBkp.Text = m_shlh2.vin_str_bkp;
            this.tbVINBkpASCII.Text = m_shlh2.vin_ascii_bkp;

            this.uchbDecrypy.SetHexbox(m_decryptbytes);
            this.uchbEncrypy.SetHexbox(m_encryptbytes);

            CheckDataValid(m_shlh2);
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

        private void FixPin(StringBuilder log)
        {
            string fix = "PIN";

            log.Append("检查" + fix + "数据...");
            if (m_shlh2.CheckPin())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_shlh2.CheckPinBkp() && m_shlh2.CheckConsistencyPin())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_shlh2.pin, m_shlh2.pin_bkp, m_shlh2.pin.Length);
                    m_shlh2.pin_ver_bkp = m_shlh2.pin_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if( m_shlh2.CheckPinBkp() )
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_shlh2.pin_bkp, m_shlh2.pin, m_shlh2.pin_bkp.Length);
                    m_shlh2.pin_ver = m_shlh2.pin_ver_bkp;
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
            if (m_shlh2.CheckSecretKey())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_shlh2.CheckSecretKeyBkp() && m_shlh2.CheckConsistencySecretKey())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_shlh2.secretkey, m_shlh2.secretkey_bkp, m_shlh2.secretkey.Length);
                    m_shlh2.secretkey_ver_bkp = m_shlh2.secretkey_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_shlh2.CheckSecretKeyBkp() )
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_shlh2.secretkey_bkp, m_shlh2.secretkey, m_shlh2.secretkey_bkp.Length);
                    m_shlh2.secretkey_ver = m_shlh2.secretkey_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void Fixkeyidentification1(StringBuilder log)
        {
            string fix = "钥匙标识码1";

            log.Append("检查" + fix + "数据...");
            if (m_shlh2.CheckKeyIdentification1())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_shlh2.CheckKeyIdentification1Bkp() && m_shlh2.CheckConsistencyKeyId1())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_shlh2.keyidentification1, m_shlh2.keyidentification1_bkp, m_shlh2.keyidentification1.Length);
                    m_shlh2.keyidentification1_ver_bkp = m_shlh2.keyidentification1_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_shlh2.CheckKeyIdentification1Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_shlh2.keyidentification1_bkp, m_shlh2.keyidentification1, m_shlh2.keyidentification1_bkp.Length);
                    m_shlh2.keyidentification1_ver = m_shlh2.keyidentification1_ver_bkp;
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
            if (m_shlh2.CheckKeyIdentification2())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_shlh2.CheckKeyIdentification2Bkp() && m_shlh2.CheckConsistencyKeyId2())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_shlh2.keyidentification2, m_shlh2.keyidentification2_bkp, m_shlh2.keyidentification2.Length);
                    m_shlh2.keyidentification2_ver_bkp = m_shlh2.keyidentification2_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_shlh2.CheckKeyIdentification2Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_shlh2.keyidentification2_bkp, m_shlh2.keyidentification2, m_shlh2.keyidentification2_bkp.Length);
                    m_shlh2.keyidentification2_ver = m_shlh2.keyidentification2_ver_bkp;
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
            if (m_shlh2.CheckKeyIdentification3())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_shlh2.CheckKeyIdentification3Bkp() && m_shlh2.CheckConsistencyKeyId3())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_shlh2.keyidentification3, m_shlh2.keyidentification3_bkp, m_shlh2.keyidentification3.Length);
                    m_shlh2.keyidentification3_ver_bkp = m_shlh2.keyidentification3_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_shlh2.CheckKeyIdentification3Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_shlh2.keyidentification3_bkp, m_shlh2.keyidentification3, m_shlh2.keyidentification3_bkp.Length);
                    m_shlh2.keyidentification3_ver = m_shlh2.keyidentification3_ver_bkp;
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
            if (m_shlh2.CheckKeyIdentification4())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_shlh2.CheckKeyIdentification4Bkp() && m_shlh2.CheckConsistencyKeyId4())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_shlh2.keyidentification4, m_shlh2.keyidentification4_bkp, m_shlh2.keyidentification4.Length);
                    m_shlh2.keyidentification4_ver_bkp = m_shlh2.keyidentification4_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_shlh2.CheckKeyIdentification4Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_shlh2.keyidentification4_bkp, m_shlh2.keyidentification4, m_shlh2.keyidentification4_bkp.Length);
                    m_shlh2.keyidentification4_ver = m_shlh2.keyidentification4_ver_bkp;
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
            if (m_shlh2.CheckKeyIdentification5())
            {
                log.Append("正确\n检查备份" + fix + "数据...");
                if (m_shlh2.CheckKeyIdentification5Bkp() && m_shlh2.CheckConsistencyKeyId5())
                {
                    //all ok
                    log.Append("正确\n");
                }
                else
                {
                    //back up error
                    log.Append("错误\n修复备份" + fix + "数据...成功\n");
                    Array.Copy(m_shlh2.keyidentification5, m_shlh2.keyidentification5_bkp, m_shlh2.keyidentification5.Length);
                    m_shlh2.keyidentification5_ver_bkp = m_shlh2.keyidentification5_ver;
                }
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...");
                if (m_shlh2.CheckKeyIdentification5Bkp())
                {
                    //back up ok
                    log.Append("成功\n");
                    Array.Copy(m_shlh2.keyidentification5_bkp, m_shlh2.keyidentification5, m_shlh2.keyidentification5_bkp.Length);
                    m_shlh2.keyidentification5_ver = m_shlh2.keyidentification5_ver_bkp;
                }
                else
                {
                    //all error
                    log.Append("失败\n");
                }
            }
        }

        private void FixVin(StringBuilder log)
        {
            string fix = "车架号";

            log.Append("检查" + fix + "数据...");
            if (m_shlh2.CheckConsistencyVin())
            {
                log.Append("正确\n");
            }
            else
            {
                log.Append("错误\n尝试使用备份数据修复...成功");
                Array.Copy(m_shlh2.vin_bkp, m_shlh2.vin, m_shlh2.vin_bkp.Length);
            }
        }

        private int FixData(StringBuilder sblog)
        {
            FixPin(sblog);
            FixSecretKey(sblog);
            Fixkeyidentification1(sblog);
            Fixkeyidentification2(sblog);
            Fixkeyidentification3(sblog);
            Fixkeyidentification4(sblog);
            Fixkeyidentification5(sblog);
            FixVin(sblog);
            return 0;
        }

        private void btnFix_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sblog = new StringBuilder(512);
            FixData(sblog);
            this.tbInfo.Text = sblog.ToString();
            UpdateDisplay();
        }

        private void btnPwdErrCnt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAlter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExportDecrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExportEncrypt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
