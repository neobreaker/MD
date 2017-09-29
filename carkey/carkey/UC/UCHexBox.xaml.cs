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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Be.Windows.Forms;

namespace carkey.UC
{
    /// <summary>
    /// UCHexBox.xaml 的交互逻辑
    /// </summary>
    public partial class UCHexBox : UserControl
    {
        private DynamicByteProvider dbp;

        public UCHexBox()
        {
            InitializeComponent();
        }

        public HexBox GetHexbox()
        {
            return this.hb;
        }

        public void SetHexbox(byte[] data)
        {
            dbp = dbp = new DynamicByteProvider(data);
            this.hb.ByteProvider = dbp;
        }

        public void Select(long start, long length)
        {
            this.hb.Select(start, length);
        }
    }
}
