using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using carkey.View;

namespace carkey
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        

        private string[] m_chip;
        public MainWindow()
        {
            InitializeComponent();

            m_chip = ConfigurationManager.AppSettings.AllKeys;
            cbChipSelect.ItemsSource = m_chip;

            cbChipSelect.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cbChipSelect.SelectedItem.ToString())
                {
                    case "上海交通实业46芯片":
                        WinSH46 winsh46 = new WinSH46();
                        winsh46.Show();
                        this.Close();
                        break;
                    case "上海联创":
                        WinSHLC winshlc = new WinSHLC();
                        winshlc.Show();
                        this.Close();
                        break;
                    case "上海联合":
                        WinSHLH1 winshlh1 = new WinSHLH1();
                        winshlh1.Show();
                        this.Close();
                        break;
                    case "上海联合2代":
                        WinSHLH2 winshlh2 = new WinSHLH2();
                        winshlh2.Show();
                        this.Close();
                        break;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
