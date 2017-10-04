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
using carkey.Model;
using carkey.Common;

namespace carkey.View
{
    /// <summary>
    /// WinCQJC1.xaml 的交互逻辑
    /// </summary>
    public partial class WinCQJC1 : Window
    {
        private const int BYTE_BUFF_SIZE = 1024;

        private int m_bytes_len = 0;
        private byte[] m_encryptbytes = new byte[BYTE_BUFF_SIZE];
        private byte[] m_decryptbytes = new byte[BYTE_BUFF_SIZE];

        private ModelCQJC1 m_cqjc1 = null;

        private string[] m_activatekey_tbl = { "0", "1", "2", "3", "4", "5", "数据错误" };

        public WinCQJC1()
        {
            InitializeComponent();
        }

        private void CheckDataValid()
        {
            this.tbvin.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            this.tbField1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            this.tbImmobiliserCode.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            this.tbActivateKey.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
            this.tbField2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2D2D2"));
        }

        private void UpdateDisplay()
        {
            m_cqjc1 = new ModelCQJC1(m_decryptbytes);

            this.tbvin.Text = m_cqjc1.vin_str;
            this.tbvinASCII.Text = m_cqjc1.vin_ascii;
            this.tbField1.Text = m_cqjc1.field1_str;
            this.tbField1ASCII.Text = m_cqjc1.field1_ascii;
            this.tbImmobiliserCode.Text = m_cqjc1.immobilisercode_str;
            this.tbImmobiliserCodeASCII.Text = m_cqjc1.immobilisercode_ascii;
            this.tbActivateKey.Text = m_cqjc1.activatekey_str;
            SetcbActivateKeySelect(m_cqjc1.activatekey);
            this.tbErrCode.Text = m_cqjc1.errcode_str;
            this.tbKeyInfo.Text = m_cqjc1.keyinfo_str;
            this.tbPIN.Text = m_cqjc1.pin_str;
            this.tbKeyIdentification1.Text = m_cqjc1.keyidentification1_str;
            this.tbKeyIdentification2.Text = m_cqjc1.keyidentification2_str;
            this.tbKeyIdentification3.Text = m_cqjc1.keyidentification3_str;
            this.tbKeyIdentification4.Text = m_cqjc1.keyidentification4_str;
            this.tbKeyIdentification5.Text = m_cqjc1.keyidentification5_str;
            this.tbKeyNum.Text = m_cqjc1.keynum_str;
            this.tbField2.Text = m_cqjc1.field2_str;
            this.tbField2ASCII.Text = m_cqjc1.field2_ascii;

            this.uchbDecrypy.SetHexbox(m_decryptbytes);

            CheckDataValid();
        }

        private void SetcbActivateKeySelect(byte keyactive)
        {
            switch (keyactive)
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

        private void ComboBoxInit()
        {
            this.cbActivateKeySelect.ItemsSource = m_activatekey_tbl;
        }

        private void LoadData()
        {
            ComboBoxInit();
            UpdateDisplay();
            this.cbActivateKeySelect.SelectionChanged += cbActivateKeySelect_SelectionChanged;
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
                    LoadData();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void UpdateFiled(int field_idx, string input_str, int num)
        {
            int i = 0;
            string[] str = new string[32];

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

        private void UpdateVIN()
        {
            int field_idx = 0x28, field_len = 17;

            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] data = ASCII.GetBytes(this.tbvinASCII.Text);

            for (int i = 0; i < field_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = data[i];
            }

        }

        private void UpdateField1()
        {
            int field_idx = 0x39, field_len = 10;

            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] data = ASCII.GetBytes(this.tbField1ASCII.Text);

            for (int i = 0; i < field_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = data[i];
            }
        }

        private void UpdateImmobiliserCode()
        {
            int field_idx = 0x43, field_len = 12;

            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] data = ASCII.GetBytes(this.tbImmobiliserCodeASCII.Text);

            for (int i = 0; i < field_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = data[i];
            }
        }

        private void UpdateActiveKey(byte keyactive)
        {
            this.m_decryptbytes[0x56] = keyactive;
        }

        private void UpdateErrCode()
        {
            int field_idx = 0x57, field_len = 2;
            UpdateFiled(field_idx, tbErrCode.Text, field_len);
        }

        private void UpdateKeyInfo()
        {
            int field_idx = 0x5c, field_len = 5;
            UpdateFiled(field_idx, this.tbKeyInfo.Text, field_len);
        }

        private void UpdatePIN()
        {
            int field_idx = 0x63, field_len = 2;
            UpdateFiled(field_idx, this.tbPIN.Text, field_len);
        }

        private void UpdateKeyIdentification1()
        {
            int field_idx = 0x66, field_len = 3;
            UpdateFiled(field_idx, this.tbKeyIdentification1.Text, field_len);
        }

        private void UpdateKeyIdentification2()
        {
            int field_idx = 0x69, field_len = 3;
            UpdateFiled(field_idx, this.tbKeyIdentification2.Text, field_len);
        }

        private void UpdateKeyIdentification3()
        {
            int field_idx = 0x6c, field_len = 3;
            UpdateFiled(field_idx, this.tbKeyIdentification3.Text, field_len);
        }

        private void UpdateKeyIdentification4()
        {
            int field_idx = 0x6f, field_len = 3;
            UpdateFiled(field_idx, this.tbKeyIdentification4.Text, field_len);
        }

        private void UpdateKeyIdentification5()
        {
            int field_idx = 0x72, field_len = 3;
            UpdateFiled(field_idx, this.tbKeyIdentification5.Text, field_len);
        }

        private void UpdateKeynNum()
        {
            int field_idx = 0x75, field_len = 1;
            UpdateFiled(field_idx, this.tbKeyNum.Text, field_len);
        }

        private void UpdateField2()
        {
            int field_idx = 0x89, field_len = 18;

            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
            Byte[] data = ASCII.GetBytes(this.tbField2ASCII.Text);

            for (int i = 0; i < field_len; i++)
            {
                this.m_decryptbytes[field_idx + i] = data[i];
            }
        }

        private void UpdateDecrypt()
        {
            UpdateVIN();
            UpdateField1();
            UpdateImmobiliserCode();
            UpdateErrCode();
            UpdateKeyInfo();
            UpdatePIN();
            UpdateKeyIdentification1();
            UpdateKeyIdentification2();
            UpdateKeyIdentification3();
            UpdateKeyIdentification4();
            UpdateKeyIdentification5();
            UpdateKeynNum();
            UpdateField2();
        }

        private void btnAlter_Click(object sender, RoutedEventArgs e)
        {
            UpdateDecrypt();
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

        private void tbvin_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x28, 17);
        }

        private void tbvinASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x28, 17);
        }

        private void tbField1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x39, 10);
        }

        private void tbField1ASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x39, 10);
        }

        private void tbImmobiliserCode_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x43, 12);
        }

        private void tbImmobiliserCodeASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x43, 12);
        }

        private void tbActivateKey_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x56, 1);
        }

        private void tbErrCode_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x57, 2);
        }

        private void tbKeyInfo_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x5c, 5);
        }

        private void tbPIN_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x63, 2);
        }

        private void tbKeyIdentification1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x66, 3);
        }

        private void tbKeyIdentification2_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x69, 3);
        }

        private void tbKeyIdentification3_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x6c, 3);
        }

        private void tbKeyIdentification4_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x6f, 3);
        }

        private void tbKeyIdentification5_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x72, 3);
        }

        private void tbKeyNum_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x75, 1);
        }

        private void tbField2_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x8a, 18);
        }

        private void tbField2ASCII_GotFocus(object sender, RoutedEventArgs e)
        {
            this.uchbDecrypy.Select(0x8a, 18);
        }

    }
}
