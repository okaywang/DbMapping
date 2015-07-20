using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMapping
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void 新增策略ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmMapping().ShowDialog();
        }
    }
}
