using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegionAndLanguage {


    public partial class FormLogOff : Form {
        [DllImport("user32.dll")]
        public static extern int ExitWindowsEx(int uFlags, int dwReason);
        public FormLogOff() {
            InitializeComponent();
        }

        private void buttonLogoff_Click(object sender, EventArgs e) {
            ExitWindowsEx(4, 0);
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
