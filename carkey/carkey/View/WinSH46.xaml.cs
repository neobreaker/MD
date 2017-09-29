using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using carkey.Common;
using carkey.Model;
using Be.Windows.Forms;

namespace carkey.View
{
    /// <summary>
    /// WinSH46.xaml 的交互逻辑
    /// </summary>
    public partial class WinSH46 : Window
    {

        private static byte[] s_passwd_tbl = new byte[]
        {
            0x2c, 0x7f, 0xd8, 0xbd, 0xab, 0xfa, 0xd5, 0x5e, 0xc5, 0xb0, 0x99, 0xa7, 0x74, 0xf1, 0x69, 0x51,
            0x76, 0x45, 0xbc, 0x3f, 0x26, 0x62, 0xb2, 0x8c, 0xe4, 0xae, 0x79, 0xd4, 0xda, 0xe0, 0x96, 0xeb,     //check ok 2017/09/26
            0x66, 0xc0, 0x1e, 0x13, 0x46, 0x3b, 0xce, 0xff, 0x7d, 0x3e, 0x7a, 0x12, 0xef, 0x47, 0x8d, 0xdf,
            0x21, 0x4c, 0xd1, 0xa8, 0xb9, 0x32, 0xcb, 0x95, 0xf6, 0xc7, 0xa5, 0xcc, 0xc3, 0x9d, 0x5b, 0xf8,
            0x72, 0x1a, 0x8b, 0xea, 0xfb, 0xb6, 0x9a, 0x09, 0x86, 0xfc, 0xc6, 0xf7, 0x2a, 0xa1, 0x30, 0x3d,
            0x28, 0x25, 0xf0, 0x53, 0x78, 0x00, 0xfe, 0xcf, 0xb3, 0xe5, 0x1c, 0x87, 0x89, 0xb7, 0xd9, 0xa0,
            0x04, 0x9f, 0x7b, 0x48, 0x1d, 0x57, 0x91, 0xaf, 0x18, 0x67, 0x24, 0x77, 0xa4, 0x27, 0xde, 0x90,
            0x41, 0x17, 0x9e, 0xe3, 0xee, 0x6B, 0x88, 0xca, 0xba, 0x84, 0xf5, 0x93, 0x80, 0xc2, 0x37, 0xb4,
            0xe9, 0xd3, 0x6f, 0x10, 0x35, 0x38, 0x4f, 0xe7, 0x5f, 0x15, 0x58, 0x39, 0xc8, 0x6e, 0xbe, 0xf3,
            0xa6, 0xec, 0x19, 0x8a, 0x9b, 0xfd, 0x65, 0x0a, 0x4d, 0x7e, 0xe6, 0x63, 0x1f, 0x49, 0x23, 0x52,
            0xf9, 0x7c, 0x98, 0xd6, 0xf4, 0x8e, 0x07, 0x5d, 0xe2, 0x83, 0xaa, 0x94, 0x56, 0xdd, 0x40, 0x73,
            0xbf, 0x81, 0x4b, 0x0d, 0x14, 0x8f, 0x6c, 0x54, 0x34, 0x60, 0x08, 0x70, 0x64, 0x5c, 0xc1, 0x4a,
            0xa2, 0x9c, 0x36, 0x75, 0x59, 0x61, 0x2f, 0xac, 0x33, 0x4e, 0x0f, 0x55, 0x97, 0x0c, 0xf2, 0xa3,
            0xed, 0xbb, 0x42, 0xc9, 0xc4, 0xad, 0x3c, 0x68, 0x2d, 0x20, 0xb1, 0x3a, 0x44, 0x29, 0x43, 0x05,
            0xa9, 0x22, 0xd7, 0x85, 0xb8, 0xcd, 0x50, 0x31, 0xb5, 0xd0, 0xe1, 0xdb, 0x01, 0x92, 0x1b, 0x16,
            0xe8, 0xd2, 0x2b, 0x5a, 0x71, 0xdc, 0x0e, 0x03, 0x6d, 0x02, 0x6a, 0x2e, 0x06, 0x0b, 0x82, 0x11,
        };

        private static Dictionary<byte, byte> s_passwd_dic = new Dictionary<byte, byte>();

        private const int BYTE_BUFF_SIZE = 1024;

        private string m_encryptstr = "";
        private string m_decryptstr = "";

        private int m_bytes_len = 0;
        private byte[] m_encryptbytes = new byte[BYTE_BUFF_SIZE];
        private byte[] m_decryptbytes = new byte[BYTE_BUFF_SIZE];

        private ModelSH46 m_modelsh46;

        private string[] m_manufacturer_tbl = {"海马专用芯片", "东风帅客专用芯片", "奇瑞专用芯片（瑞虎 优雅）", 
                                               "吉利专用芯片（远景 英伦 华普）", "众泰专用芯片", "开瑞专用芯片",
                                               "长城专用芯片", "其它车型专用芯片"};
        private string[] m_activatekey_tbl = { "0", "1", "2", "3", "4", "5" , "数据错误"};

        void DecryptHexBoxInit()
        {
            this.hbDecrypt.BackColor = System.Drawing.Color.Green;
            this.hbDecrypt.CopyHex();
            this.hbDecrypt.BytesPerLine = 0x10;
            this.hbDecrypt.InsertActive = true;
            this.hbDecrypt.ResetText();
            this.hbDecrypt.ReadOnly = false;

            bool cancpoy = this.hbDecrypt.CanCopy();
            bool canpaste = this.hbDecrypt.CanPaste();
            bool cancut = this.hbDecrypt.CanCut();
            this.hbDecrypt.Enabled = true;

            byte[] hex = new byte[256];
            DynamicByteProvider dbp = new DynamicByteProvider(hex);
            dbp.WriteByte(0, 0x5a);
            dbp.WriteByte(1, 0xa5);

            this.hbDecrypt.ByteProvider = dbp;
        }

        public WinSH46()
        {
            InitializeComponent();
            CheckPasswd();
        }

        private bool CheckPasswd()
        {

            for (int i = 0; i < 256; i++)
            {
                if (s_passwd_dic.ContainsKey(s_passwd_tbl[i]))
                {
                    return false;
                }
                else
                {
                    s_passwd_dic.Add(s_passwd_tbl[i], (byte)i);
                }
            }
            return true;
        }

        private void CheckDataValid()
        {
            if (m_modelsh46.CheckMnufacturer())
                //this.tbManufacturer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                this.tbManufacturer.Background = new SolidColorBrush(Colors.White);
            else
                this.tbManufacturer.Background = new SolidColorBrush(Colors.Red);

            if(m_modelsh46.CheckActivateKey())
                this.tbActivateKey.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            else
                this.tbActivateKey.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckKeyidentification())
            {
                this.tbKeyIdentification1.Background = new SolidColorBrush(Colors.White);
                this.tbKeyIdentification2.Background = new SolidColorBrush(Colors.White);
                this.tbKeyIdentification3.Background = new SolidColorBrush(Colors.White);
                this.tbKeyIdentification4.Background = new SolidColorBrush(Colors.White);
                this.tbKeyIdentification5.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                this.tbKeyIdentification1.Background = new SolidColorBrush(Colors.Red);
                this.tbKeyIdentification2.Background = new SolidColorBrush(Colors.Red);
                this.tbKeyIdentification3.Background = new SolidColorBrush(Colors.Red);
                this.tbKeyIdentification4.Background = new SolidColorBrush(Colors.Red);
                this.tbKeyIdentification5.Background = new SolidColorBrush(Colors.Red);
            }

            if (m_modelsh46.CheckPin())
                this.tbPin.Background = new SolidColorBrush(Colors.White);
            else
                this.tbPin.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckField1())
                this.tbfield1.Background = new SolidColorBrush(Colors.White);
            else
                this.tbfield1.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckField2())
                this.tbfield2.Background = new SolidColorBrush(Colors.White);
            else
                this.tbfield2.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckField3())
                this.tbfield3.Background = new SolidColorBrush(Colors.White);
            else
                this.tbfield3.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckKeyNum())
                this.tbKeyNum.Background = new SolidColorBrush(Colors.White);
            else
                this.tbKeyNum.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckSecretkey())
                this.tbSecretkey.Background = new SolidColorBrush(Colors.White);
            else
                this.tbSecretkey.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckField4())
                this.tbfield4.Background = new SolidColorBrush(Colors.White);
            else
                this.tbfield4.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckImmobilisercode())
                this.tbImmobiliserCode.Background = new SolidColorBrush(Colors.White);
            else
                this.tbImmobiliserCode.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckVid())
            {
                this.tbVid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                this.tbVidASCII.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                this.tbVid.Background = new SolidColorBrush(Colors.Red);
                this.tbVidASCII.Background = new SolidColorBrush(Colors.Red);
            }

            //backup
            if (m_modelsh46.CheckMnufacturerBkp())
            {
                if (m_modelsh46.CheckConsistencyMnufacturer())
                    this.tbManufacturerbkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                else
                    this.tbManufacturerbkp.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
                this.tbManufacturerbkp.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckActivateKeyBkp())
            {
                if (m_modelsh46.CheckConsistencyActivateKey())
                    this.tbActivateKeybkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                else
                    this.tbActivateKeybkp.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
                this.tbActivateKeybkp.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckKeyidentificationBkp())
            {
                if (m_modelsh46.CheckConsistencyKeyidentification())
                {
                    this.tbKeyIdentification1bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                    this.tbKeyIdentification2bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                    this.tbKeyIdentification3bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                    this.tbKeyIdentification4bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                    this.tbKeyIdentification5bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                }
                else
                {
                    this.tbKeyIdentification1bkp.Background = new SolidColorBrush(Colors.Yellow);
                    this.tbKeyIdentification2bkp.Background = new SolidColorBrush(Colors.Yellow);
                    this.tbKeyIdentification3bkp.Background = new SolidColorBrush(Colors.Yellow);
                    this.tbKeyIdentification4bkp.Background = new SolidColorBrush(Colors.Yellow);
                    this.tbKeyIdentification5bkp.Background = new SolidColorBrush(Colors.Yellow);
                }
                
            }
            else
            {
                this.tbKeyIdentification1bkp.Background = new SolidColorBrush(Colors.Red);
                this.tbKeyIdentification2bkp.Background = new SolidColorBrush(Colors.Red);
                this.tbKeyIdentification3bkp.Background = new SolidColorBrush(Colors.Red);
                this.tbKeyIdentification4bkp.Background = new SolidColorBrush(Colors.Red);
                this.tbKeyIdentification5bkp.Background = new SolidColorBrush(Colors.Red);
            }

            if (m_modelsh46.CheckPinBkp())
            {
                if(m_modelsh46.CheckConsistencyPin())
                {
                    this.tbPinbkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                } 
                else
                    this.tbPinbkp.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
                this.tbPinbkp.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckField1Bkp())
            {
                if (m_modelsh46.CheckConsistencyField1())
                    this.tbfield1bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                else
                    this.tbfield1bkp.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
                this.tbfield1bkp.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckField2Bkp())
            {
                if (m_modelsh46.CheckConsistencyField2())
                    this.tbfield2bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                else
                    this.tbfield2bkp.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
                this.tbfield2bkp.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckField3Bkp())
            {
                if (m_modelsh46.CheckConsistencyField3())
                    this.tbfield3bkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                else
                    this.tbfield3bkp.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
                this.tbfield3bkp.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckKeyNumBkp())
            {
                if (m_modelsh46.CheckConsistencyKeyNum())
                    this.tbKeyNumbkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                else
                    this.tbKeyNumbkp.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
                this.tbKeyNumbkp.Background = new SolidColorBrush(Colors.Red);

            if (m_modelsh46.CheckSecretkeyBkp())
            {
                if (m_modelsh46.CheckConsistencySecretkey())
                    this.tbSecretkeybkp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
                else
                    this.tbSecretkeybkp.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
                this.tbSecretkeybkp.Background = new SolidColorBrush(Colors.Red);
        }

        private void SetcbManufacturerSelect()
        {

            switch (m_decryptbytes[1])
            {
                case 0xa3:
                    this.cbManufacturerSelect.SelectedIndex = 0;
                    break;
                case 0x3b:
                    this.cbManufacturerSelect.SelectedIndex = 1;
                    break;
                case 0x1f:
                    this.cbManufacturerSelect.SelectedIndex = 2;
                    break;
                case 0xc2:
                    this.cbManufacturerSelect.SelectedIndex = 3;
                    break;
                case 0xd4:
                    this.cbManufacturerSelect.SelectedIndex = 4;
                    break;
                case 0x0f:
                    this.cbManufacturerSelect.SelectedIndex = 5;
                    break;
                case 0xb9:
                    this.cbManufacturerSelect.SelectedIndex = 6;
                    break;
                default:
                    this.cbManufacturerSelect.SelectedIndex = 7;
                    break;
            }
            
        }

        private void SetcbActivateKeySelect()
        {
            

            switch (m_decryptbytes[3])
            {
                case 0x00:
                    this.cbActivateKeySelect.SelectedIndex = 0;
                    break;
                case 0x03:
                    this.cbActivateKeySelect.SelectedIndex = 1;
                    break;
                case 0x07:
                    this.cbActivateKeySelect.SelectedIndex = 2;
                    break;
                case 0x0f:
                    this.cbActivateKeySelect.SelectedIndex = 3;
                    break;
                case 0x11:
                    this.cbActivateKeySelect.SelectedIndex = 4;
                    break;
                case 0x31:
                    this.cbActivateKeySelect.SelectedIndex = 5;
                    break;
                default:
                    this.cbActivateKeySelect.SelectedIndex = 6;
                    break;
            }
        }

        private void UpdateDisplay()
        {
            

            this.m_modelsh46 = new ModelSH46(m_decryptbytes);

            DynamicByteProvider dbp_decrypt = new DynamicByteProvider(m_decryptbytes);
            this.hbDecrypt.ByteProvider = dbp_decrypt;
            DynamicByteProvider dbp_encrypt = new DynamicByteProvider(m_encryptbytes);
            this.hbEncrypt.ByteProvider = dbp_encrypt;

            this.tbManufacturer.Text = this.m_modelsh46.mnufacturer_str;
            
            SetcbManufacturerSelect();

            this.tbActivateKey.Text = this.m_modelsh46.activatekey_str;
            

            this.tbKeyIdentification1.Text = this.m_modelsh46.keyidentification1_str;
            this.tbKeyIdentification2.Text = this.m_modelsh46.keyidentification2_str;
            this.tbKeyIdentification3.Text = this.m_modelsh46.keyidentification3_str;
            this.tbKeyIdentification4.Text = this.m_modelsh46.keyidentification4_str;
            this.tbKeyIdentification5.Text = this.m_modelsh46.keyidentification5_str;
            this.tbPin.Text = this.m_modelsh46.pin_str;
            this.tbfield1.Text = this.m_modelsh46.field1_str;
            this.tbfield2.Text = this.m_modelsh46.field2_str;
            this.tbfield3.Text = this.m_modelsh46.field3_str;
            this.tbKeyNum.Text = this.m_modelsh46.keynum_str;
            this.tbSecretkey.Text = this.m_modelsh46.secretkey_str;
            this.tbfield4.Text = this.m_modelsh46.field4_str;
            this.tbImmobiliserCode.Text = this.m_modelsh46.immobilisercode_str;
            this.tbVid.Text = this.m_modelsh46.vid_str;
            this.tbVidASCII.Text = this.m_modelsh46.vid_ascii;

            //backup
            this.tbManufacturerbkp.Text = this.m_modelsh46.mnufacturer_str_bkp;
            this.tbActivateKeybkp.Text = this.m_modelsh46.activatekey_str_bkp;
            this.tbKeyIdentification1bkp.Text = this.m_modelsh46.keyidentification1_str_bkp;
            this.tbKeyIdentification2bkp.Text = this.m_modelsh46.keyidentification2_str_bkp;
            this.tbKeyIdentification3bkp.Text = this.m_modelsh46.keyidentification3_str_bkp;
            this.tbKeyIdentification4bkp.Text = this.m_modelsh46.keyidentification4_str_bkp;
            this.tbKeyIdentification5bkp.Text = this.m_modelsh46.keyidentification5_str_bkp;
            this.tbPinbkp.Text = this.m_modelsh46.pin_str_bkp;
            this.tbfield1bkp.Text = this.m_modelsh46.field1_str_bkp;
            this.tbfield2bkp.Text = this.m_modelsh46.field2_str_bkp;
            this.tbfield3bkp.Text = this.m_modelsh46.field3_str_bkp;
            this.tbKeyNumbkp.Text = this.m_modelsh46.keynum_str_bkp;
            this.tbSecretkeybkp.Text = this.m_modelsh46.secretkey_str_bkp;

            CheckDataValid();
        }

        private void ComboBoxInit()
        {
            this.cbManufacturerSelect.ItemsSource = m_manufacturer_tbl;
            this.cbActivateKeySelect.ItemsSource = m_activatekey_tbl;
            SetcbActivateKeySelect();
        }

        private void LoadData()
        {
            ComboBoxInit();
            UpdateDisplay();

            this.cbManufacturerSelect.SelectionChanged += cbManufacturerSelect_SelectionChanged;
            
        }

        private void DecryptBytes(byte[] encryptbytes, int len, ref byte[] decryptbytes)
        {
            for (int i = 0; i < len; i++)
            {
                decryptbytes[i] = s_passwd_tbl[encryptbytes[i]];
            }
        }

        private bool EncryptBytes(byte[] decryptbytes, int len, ref byte[] encryptbytes)
        {
            for (int i = 0; i < len; i++)
            {
                if (s_passwd_dic.ContainsKey(decryptbytes[i]))
                    encryptbytes[i] = s_passwd_dic[decryptbytes[i]];
                else
                    return false;
            }
            return true;
        }

        private void menuOpenEncpyptFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "bin|*.BIN" + "|AllFiles|*.*";
                var result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    m_bytes_len = Misc.ReadHex(openFileDialog.FileName, ref m_encryptbytes, BYTE_BUFF_SIZE);
                    DecryptBytes(m_encryptbytes, m_bytes_len, ref m_decryptbytes);
                    LoadData();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void menuOpenDecryptFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "bin|*.BIN" + "|AllFiles|*.*";
                var result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    m_bytes_len = Misc.ReadHex(openFileDialog.FileName, ref m_decryptbytes, BYTE_BUFF_SIZE);
                    EncryptBytes(m_decryptbytes, m_bytes_len, ref m_encryptbytes);
                    LoadData();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string FixMnufacturer()
        {
            string fix = "厂商型号";
            string fix_log = "检查"+fix+"数据...";
            
            if (!m_modelsh46.CheckMnufacturer())     //数据错误
            {
                fix_log += "错误\n尝试使用备份数据修复...";
                if (m_modelsh46.CheckMnufacturerBkp())  //尝试用备份数据修复
                {
                    fix_log += "成功\n";
                    m_modelsh46.mnufacturer = m_modelsh46.mnufacturer_bkp;
                    m_modelsh46.mnufacturer_ver = m_modelsh46.mnufacturer_ver_bkp;
                }
                else
                {
                    fix_log += "失败\n";
                }
            }
            else     //数据正确
	        {
                fix_log += "正确\n检查备份"+fix+"数据...";
                if (!m_modelsh46.CheckMnufacturerBkp() || !m_modelsh46.CheckConsistencyMnufacturer())  //检查备份数据 备份数据校验错误或者与原数据不一致
                {
                    fix_log += "错误\n修复备份数据...成功\n";
                    m_modelsh46.mnufacturer_bkp = m_modelsh46.mnufacturer;
                    m_modelsh46.mnufacturer_ver_bkp = m_modelsh46.mnufacturer_ver;
                }
                else 
                {
                    fix_log += "正确\n";
                }
            }
            return fix_log;
        }

        private string FixActivateKey()
        {
            string fix = "激活钥匙数量";

            string fix_log = "检查"+fix+"数据...";

            if (!m_modelsh46.CheckActivateKey())     //数据错误
            {
                fix_log += "错误\n尝试使用备份数据修复...";
                if (m_modelsh46.CheckActivateKeyBkp())  //尝试用备份数据修复
                {
                    fix_log += "成功\n";
                    m_modelsh46.activatekey = m_modelsh46.activatekey_bkp;
                    m_modelsh46.activatekey_ver = m_modelsh46.activatekey_ver_bkp;
                }
                else
                {
                    fix_log += "失败\n";
                }
            }
            else     //数据正确
            {
                fix_log += "正确\n检查备份"+fix+"数据...";
                if (!m_modelsh46.CheckActivateKeyBkp() || !m_modelsh46.CheckConsistencyActivateKey())  //检查备份数据 备份数据校验错误或者与原数据不一致
                {
                    fix_log += "错误\n修复备份"+fix+"数据...成功\n";
                    m_modelsh46.activatekey_bkp = m_modelsh46.activatekey;
                    m_modelsh46.activatekey_ver_bkp = m_modelsh46.activatekey_ver;
                }
                else
                {
                    fix_log += "正确\n";
                }
            }
            return fix_log;
        }

        private string FixKeyidentification()
        {
            string fix = "钥匙标识码";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckKeyidentification())     //数据错误
            {
                fix_log += "错误\n尝试使用备份数据修复...";
                if (m_modelsh46.CheckKeyidentificationBkp())  //尝试用备份数据修复
                {
                    fix_log += "成功\n";
                    Array.Copy(m_modelsh46.keyidentification_bkp, m_modelsh46.keyidentification, m_modelsh46.keyidentification_bkp.Length);
                    m_modelsh46.keyidentification_ver = m_modelsh46.keyidentification_ver_bkp;
                }
                else
                {
                    fix_log += "失败\n";
                }
            }
            else     //数据正确
            {
                fix_log += "正确\n检查备份" + fix + "数据...";
                if (!m_modelsh46.CheckKeyidentificationBkp() || !m_modelsh46.CheckConsistencyKeyidentification())  //检查备份数据 备份数据校验错误或者与原数据不一致
                {
                    fix_log += "错误\n修复备份" + fix + "数据...成功\n";
                    Array.Copy(m_modelsh46.keyidentification, m_modelsh46.keyidentification_bkp, m_modelsh46.keyidentification.Length);
                    m_modelsh46.keyidentification_ver_bkp = m_modelsh46.keyidentification_ver;
                }
                else
                {
                    fix_log += "正确\n";
                }
            }
            return fix_log;
        }

        private string FixPIN()
        {
            string fix = "PIN";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckPin())     //数据错误
            {
                fix_log += "错误\n尝试使用备份数据修复...";
                if (m_modelsh46.CheckPinBkp())  //尝试用备份数据修复
                {
                    fix_log += "成功\n";
                    Array.Copy(m_modelsh46.pin_bkp, m_modelsh46.pin, m_modelsh46.pin_bkp.Length);
                    m_modelsh46.pin_ver = m_modelsh46.pin_ver_bkp;
                }
                else
                {
                    fix_log += "失败\n";
                }
            }
            else     //数据正确
            {
                fix_log += "正确\n检查备份" + fix + "数据...";
                if (!m_modelsh46.CheckPinBkp() || !m_modelsh46.CheckConsistencyPin())  //检查备份数据 备份数据校验错误或者与原数据不一致
                {
                    fix_log += "错误\n修复备份" + fix + "数据...成功\n";
                    Array.Copy(m_modelsh46.pin, m_modelsh46.pin_bkp, m_modelsh46.pin.Length);
                    m_modelsh46.pin_ver_bkp = m_modelsh46.pin_ver;
                }
                else
                {
                    fix_log += "正确\n";
                }
            }
            return fix_log;
        }

        private string FixField1()
        {
            string fix = "域1";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckField1())     //数据错误
            {
                fix_log += "错误\n尝试使用备份数据修复...";
                if (m_modelsh46.CheckField1Bkp())  //尝试用备份数据修复
                {
                    fix_log += "成功\n";
                    m_modelsh46.field1 = m_modelsh46.field1_bkp;
                    m_modelsh46.field1_ver = m_modelsh46.field1_ver_bkp;
                }
                else
                {
                    fix_log += "失败\n";
                }
            }
            else     //数据正确
            {
                fix_log += "正确\n检查备份" + fix + "数据...";
                if (!m_modelsh46.CheckField1Bkp() || !m_modelsh46.CheckConsistencyField1())  //检查备份数据 备份数据校验错误或者与原数据不一致
                {
                    fix_log += "错误\n修复备份" + fix + "数据...成功\n";
                    m_modelsh46.field1_bkp = m_modelsh46.field1;
                    m_modelsh46.field1_ver_bkp = m_modelsh46.field1_ver;
                }
                else
                {
                    fix_log += "正确\n";
                }
            }
            return fix_log;
        }

        private string FixField2()
        {
            string fix = "域2";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckField2())     //数据错误
            {
                fix_log += "错误\n尝试使用备份数据修复...";
                if (m_modelsh46.CheckField2Bkp())  //尝试用备份数据修复
                {
                    fix_log += "成功\n";
                    m_modelsh46.field2 = m_modelsh46.field2_bkp;
                    m_modelsh46.field2_ver = m_modelsh46.field2_ver_bkp;
                }
                else
                {
                    fix_log += "失败\n";
                }
            }
            else     //数据正确
            {
                fix_log += "正确\n检查备份" + fix + "数据...";
                if (!m_modelsh46.CheckField2Bkp() || !m_modelsh46.CheckConsistencyField2())  //检查备份数据 备份数据校验错误或者与原数据不一致
                {
                    fix_log += "错误\n修复备份" + fix + "数据...成功\n";
                    m_modelsh46.field2_bkp = m_modelsh46.field2;
                    m_modelsh46.field2_ver_bkp = m_modelsh46.field2_ver;
                }
                else
                {
                    fix_log += "正确\n";
                }
            }
            return fix_log;
        }

        private string FixField3()
        {
            string fix = "域3";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckField3())     //数据错误
            {
                fix_log += "错误\n尝试使用备份数据修复...";
                if (m_modelsh46.CheckField3Bkp())  //尝试用备份数据修复
                {
                    fix_log += "成功\n";
                    m_modelsh46.field3 = m_modelsh46.field3_bkp;
                    m_modelsh46.field3_ver = m_modelsh46.field3_ver_bkp;
                }
                else
                {
                    fix_log += "失败\n";
                }
            }
            else     //数据正确
            {
                fix_log += "正确\n检查备份" + fix + "数据...";
                if (!m_modelsh46.CheckField3Bkp() || !m_modelsh46.CheckConsistencyField3())  //检查备份数据 备份数据校验错误或者与原数据不一致
                {
                    fix_log += "错误\n修复备份" + fix + "数据...成功\n";
                    m_modelsh46.field3_bkp = m_modelsh46.field3;
                    m_modelsh46.field3_ver_bkp = m_modelsh46.field3_ver;
                }
                else
                {
                    fix_log += "正确\n";
                }
            }
            return fix_log;
        }

        private string FixKeyNum()
        {
            string fix = "钥匙数量";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckKeyNum())     //数据错误
            {
                fix_log += "错误\n尝试使用备份数据修复...";
                if (m_modelsh46.CheckKeyNumBkp())  //尝试用备份数据修复
                {
                    fix_log += "成功\n";
                    m_modelsh46.keynum = m_modelsh46.keynum_bkp;
                    m_modelsh46.keynum_ver = m_modelsh46.keynum_ver_bkp;
                }
                else
                {
                    fix_log += "失败\n";
                }
            }
            else     //数据正确
            {
                fix_log += "正确\n检查备份" + fix + "数据...";
                if (!m_modelsh46.CheckKeyNumBkp() || !m_modelsh46.CheckConsistencyKeyNum())  //检查备份数据 备份数据校验错误或者与原数据不一致
                {
                    fix_log += "错误\n修复备份" + fix + "数据...成功\n";
                    m_modelsh46.keynum_bkp = m_modelsh46.keynum;
                    m_modelsh46.keynum_ver_bkp = m_modelsh46.keynum_ver;
                }
                else
                {
                    fix_log += "正确\n";
                }
            }
            return fix_log;
        }

        private string FixSecretkey()
        {
            string fix = "秘钥码";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckSecretkey())     //数据错误
            {
                fix_log += "错误\n尝试使用备份数据修复...";
                if (m_modelsh46.CheckSecretkeyBkp())  //尝试用备份数据修复
                {
                    fix_log += "成功\n";
                    Array.Copy(m_modelsh46.secretkey_bkp, m_modelsh46.secretkey, m_modelsh46.secretkey_bkp.Length);
                    m_modelsh46.secretkey_ver = m_modelsh46.secretkey_ver_bkp;
                }
                else
                {
                    fix_log += "失败\n";
                }
            }
            else     //数据正确
            {
                fix_log += "正确\n检查备份" + fix + "数据...";
                if (!m_modelsh46.CheckSecretkeyBkp() || !m_modelsh46.CheckConsistencySecretkey())  //检查备份数据 备份数据校验错误或者与原数据不一致
                {
                    fix_log += "错误\n修复备份" + fix + "数据...成功\n";
                    Array.Copy(m_modelsh46.secretkey, m_modelsh46.secretkey_bkp, m_modelsh46.secretkey.Length);
                    m_modelsh46.secretkey_ver_bkp = m_modelsh46.secretkey_ver;
                }
                else
                {
                    fix_log += "正确\n";
                }
            }
            return fix_log;
        }

        private string FixField4()
        {
            string fix = "域4";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckField4())     //数据错误
            {
                fix_log += "错误\n";
               
            }
            else     //数据正确
            {
                fix_log += "正确\n";
                
            }
            return fix_log;
        }

        private string FixImmobilisercode()
        {
            string fix = "防盗盒码";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckImmobilisercode())     //数据错误
            {
                fix_log += "错误\n";

            }
            else     //数据正确
            {
                fix_log += "正确\n";

            }
            return fix_log;
        }

        private string FixVID()
        {
            string fix = "VID";

            string fix_log = "检查" + fix + "数据...";

            if (!m_modelsh46.CheckVid())     //数据错误
            {
                fix_log += "错误\n";

            }
            else     //数据正确
            {
                fix_log += "正确\n";

            }
            return fix_log;
        }

        private string FixData()
        {
            string fix_log = "";
            fix_log += FixMnufacturer();
            fix_log += FixActivateKey();
            fix_log += FixKeyidentification();
            fix_log += FixPIN();
            fix_log += FixField1();
            fix_log += FixField2();
            fix_log += FixField3();
            fix_log += FixKeyNum();
            fix_log += FixSecretkey();
            fix_log += FixField4();
            fix_log += FixImmobilisercode();
            fix_log += FixVID();
            return fix_log;
        }

        private void btnFix_Click(object sender, RoutedEventArgs e)
        {
            string fix_log = "";
            fix_log = FixData();
            this.tbInfo.Text = fix_log;
        }

        private void UpdateManufacturer()
        {
            if (this.tbManufacturer.Text == "")
                return;
            this.m_decryptbytes[1] =  Convert.ToByte(this.tbManufacturer.Text, 16);
            this.m_decryptbytes[0] = Misc.ChecksumAccumulatePlusOne(this.m_decryptbytes[1]);

            this.m_decryptbytes[0x9a + 0] = this.m_decryptbytes[0];
            this.m_decryptbytes[0x9a + 1] = this.m_decryptbytes[1];
        }

        private void UpdateActiveKey(byte value)
        {

            this.m_decryptbytes[3] = value;
            this.m_decryptbytes[2] = Misc.ChecksumAccumulatePlusOne(this.m_decryptbytes[3]);

            this.m_decryptbytes[0x9c + 0] = this.m_decryptbytes[2];
            this.m_decryptbytes[0x9c + 1] = this.m_decryptbytes[3];
        }

        private void UpdateKeyidentification(int num)
        {
            int i = 0;
            string[] str = new string[4];
            byte[] key_id = new byte[20];

            int kid_idx = 0, kid_bkp_idx = 0;
            string input_str = "";

            switch (num)
            {
                case 0:
                    kid_idx = 0x5;
                    kid_bkp_idx = 0x9f;
                    input_str = this.tbKeyIdentification1.Text;
                    break;
                case 1:
                    kid_idx = 0x9;
                    kid_bkp_idx = 0xa3;
                    input_str = this.tbKeyIdentification2.Text;
                    break;
                case 2:
                    kid_idx = 0xd;
                    kid_bkp_idx = 0xa7;
                    input_str = this.tbKeyIdentification3.Text;
                    break;
                case 3:
                    kid_idx = 0x11;
                    kid_bkp_idx = 0xab;
                    input_str = this.tbKeyIdentification4.Text;
                    break;
                case 4:
                    kid_idx = 0x15;
                    kid_bkp_idx = 0xaf;
                    input_str = this.tbKeyIdentification5.Text;
                    break;
                default:
                    return;
            }

            Misc.ParseTextInput(input_str, ref str);

            for (i = 0; i < 4; i++)
            {
                if (str[i] == "")
                    return;
            }
            
            for (i = 0; i < 4; i++)
            {
                this.m_decryptbytes[kid_idx+i] = Convert.ToByte(str[i], 16);
                this.m_decryptbytes[kid_bkp_idx + i] = this.m_decryptbytes[kid_idx + i];
            }

            for (i = 0; i < 20; i++)
            {
                key_id[i] = this.m_decryptbytes[5 + i];
            }

            this.m_decryptbytes[4] = Misc.ChecksumAccumulatePlusOne(key_id, 20);
            this.m_decryptbytes[0x9e] = this.m_decryptbytes[4];
        }

        private void UpdatePIN()
        {
            byte[] pin = new byte[2];
            string[] str = new string[2];

            Misc.ParseTextInput(this.tbPin.Text, ref str);

            for(int i = 0; i < 2; i++)
            {
                this.m_decryptbytes[0x1a+i] = Convert.ToByte(str[i], 16);
                this.m_decryptbytes[0xb4 + i] = this.m_decryptbytes[0x1a + i];
                pin[i] = this.m_decryptbytes[0x1a + i];
            }

            this.m_decryptbytes[0x19] = Misc.ChecksumAccumulatePlusOne(pin, 2);
            this.m_decryptbytes[0xb3] = this.m_decryptbytes[0x19];

        }

        void UpdateField(int idx)
        {
            int field_idx = 0, field_bkp_idx = 0, field_ver_idx = 0, field_ver_bkp_idx = 0;
            string input_str = "";
            switch(idx)
            {
                case 1:
                    field_idx = 0x1d;
                    field_ver_idx = 0x1c;
                    field_bkp_idx = 0xb7;
                    field_ver_bkp_idx = 0xb6;
                    input_str = this.tbfield1.Text;
                    break;
                case 2:
                    field_idx = 0x1f;
                    field_ver_idx = 0x1e;
                    field_bkp_idx = 0xb9;
                    field_ver_bkp_idx = 0xb8;
                    input_str = this.tbfield2.Text;
                    break;
                case 3:
                    field_idx = 0x21;
                    field_ver_idx = 0x20;
                    field_bkp_idx = 0xbb;
                    field_ver_bkp_idx = 0xba;
                    input_str = this.tbfield3.Text;
                    break;
            }

            this.m_decryptbytes[field_idx] = Convert.ToByte(input_str, 16);
            this.m_decryptbytes[field_ver_idx] = Misc.ChecksumAccumulatePlusOne(this.m_decryptbytes[field_idx]);

            this.m_decryptbytes[field_bkp_idx] = this.m_decryptbytes[field_idx];
            this.m_decryptbytes[field_ver_bkp_idx] = this.m_decryptbytes[field_ver_idx];

        }

        private void UpdateKeyNum()
        {
            int field_idx = 0x23, field_bkp_idx = 0xbd, field_ver_idx = 0x22, field_ver_bkp_idx = 0xbc;
            this.m_decryptbytes[field_idx] = Convert.ToByte(this.tbKeyNum.Text, 16);
            this.m_decryptbytes[field_ver_idx] = Misc.ChecksumAccumulatePlusOne(this.m_decryptbytes[field_idx]);

            this.m_decryptbytes[field_bkp_idx] = this.m_decryptbytes[field_idx];
            this.m_decryptbytes[field_ver_bkp_idx] = this.m_decryptbytes[field_ver_idx];

        }

        private void DoUpdateArray(int field_idx, int field_ver_idx, string input_str, int num)
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

            for (i = 0; i < num; i++)
            {
                tmp_byte[i] = this.m_decryptbytes[field_idx + i];
            }

            this.m_decryptbytes[field_ver_idx] = Misc.ChecksumAccumulatePlusOne(tmp_byte, num);

        }

        private void DoUpdateArray(int field_idx, int field_bkp_idx, int field_ver_idx, int field_ver_bkp_idx, string input_str, int num)
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
            }

            for (i = 0; i < num; i++)
            {
                tmp_byte[i] = this.m_decryptbytes[field_idx + i];
            }

            this.m_decryptbytes[field_ver_idx] = Misc.ChecksumAccumulatePlusOne(tmp_byte, num);
            this.m_decryptbytes[field_ver_bkp_idx] = this.m_decryptbytes[field_ver_idx];

        }

        private void UpdateSecretKey()
        {
            this.DoUpdateArray(0x25, 0xbf, 0x24, 0xbe, this.tbSecretkey.Text, 16);
        }

        private void UpdateFielf4()
        {
            this.DoUpdateArray(0x36, 0x35, this.tbfield4.Text, 2);
        }

        private void UPdateImmobilisercode()
        {
            this.DoUpdateArray(0x39, 0x38, this.tbImmobiliserCode.Text, 10);
        }

        private void UpdateVID()
        {
            int i = 0, vid_len = 17;
            int field_idx = 0x44, field_ver_idx = 0x43;
            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] vid = ASCII.GetBytes(this.tbVidASCII.Text);

            for (i = 0; i < vid_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = vid[i];
            }

            this.m_decryptbytes[field_ver_idx] = Misc.ChecksumAccumulatePlusOne(vid, vid_len);
        }

        private void UpdateDecrypt()
        {
            UpdateManufacturer();
            UpdateKeyidentification(0);
            UpdateKeyidentification(1);
            UpdateKeyidentification(2);
            UpdateKeyidentification(3);
            UpdateKeyidentification(4);
            UpdatePIN();
            UpdateField(1);
            UpdateField(2);
            UpdateField(3);
            UpdateKeyNum();
            UpdateSecretKey();
            UpdateFielf4();
            UPdateImmobilisercode();
            UpdateVID();
        }

        private void UpdateEncrypt()
        {
            EncryptBytes(m_decryptbytes, m_bytes_len, ref m_encryptbytes);
        }

        private void cbManufacturerSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            switch (this.cbManufacturerSelect.SelectedIndex)
            {
                    
                case 0:
                    this.tbManufacturer.Text = "0xa3";
                    break;
                case 1:
                    this.tbManufacturer.Text = "0x3b";
                    break;
                case 2:
                    this.tbManufacturer.Text = "0x1f";
                    break;
                case 3:
                    this.tbManufacturer.Text = "0xc2";
                    break;
                case 4:
                    this.tbManufacturer.Text = "0xd4";
                    break;
                case 5:
                    this.tbManufacturer.Text = "0x0f";
                    break;
                case 6:
                    this.tbManufacturer.Text = "0xb9";
                    break;
                case 7:
                    return;
                default:
                    break;
                    
            }
            
            UpdateManufacturer();

            UpdateDisplay();
            
        }

        private void cbActivateKeySelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            byte key_active = 0;
            switch (this.cbActivateKeySelect.SelectedIndex)
            {
                case 0:
                    key_active = 0x00;
                    break;
                case 1:
                    key_active = 0x03;
                    break;
                case 2:
                    key_active = 0x07;
                    break;
                case 3:
                    key_active = 0x0f;
                    break;
                case 4:
                    key_active = 0x11;
                    break;
                case 5:
                    key_active = 0x31;
                    break;
                default:
                    key_active = 0x00;
                    break;
            }
            UpdateActiveKey(key_active);
            UpdateDisplay();
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

        private void tbManufacturer_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(1, 1);
        }

        private void tbManufacturerbkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x9b, 1);
        }

        private void tbActivateKey_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(3, 1);
        }

        private void tbActivateKeybkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x9d, 1);
        }

        private void tbKeyIdentification1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(5, 4);
        }

        private void tbKeyIdentification1bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x9f, 4);
        }

        private void tbKeyIdentification2_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(9, 4);
        }

        private void tbKeyIdentification2bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xa3, 4);
        }

        private void tbKeyIdentification3_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xd, 4);
        }

        private void tbKeyIdentification3bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xa7, 4);
        }

        private void tbKeyIdentification4_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x11, 4);
        }

        private void tbKeyIdentification4bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xab, 4);
        }

        private void tbKeyIdentification5_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x15, 4);
        }

        private void tbKeyIdentification5bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xaf, 4);
        }

        private void tbPin_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x1a, 2);
        }

        private void tbPinbkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xb4, 2);
        }

        private void tbfield1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x1d, 1);
        }

        private void tbfield1bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xb7, 1);
        }

        private void tbfield2_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x1f, 1);
        }

        private void tbfield2bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xb9, 1);
        }

        private void tbfield3_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x21, 1);
        }

        private void tbfield3bkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xbb, 1);
        }

        private void tbKeyNum_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x23, 1);
        }

        private void tbKeyNumbkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xbd, 1);
        }

        private void tbSecretkey_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x25, 16);
        }

        private void tbSecretkeybkp_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0xbf, 16);
        }

        private void tbfield4_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x36, 2);
        }

        private void tbImmobiliserCode_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x39, 11);
        }

        private void tbVid_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x54, 17);
        }

        private void tbVidASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hbDecrypt.Select(0x54, 17);
        }

    }
}
