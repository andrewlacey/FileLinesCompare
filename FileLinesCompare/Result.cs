using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileLinesCompare
{
    public partial class Result : Form
    {
        public Result()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void PopulateResults(string results)
        {
            this.txtResult.Text = results;
            this.Show();
        }
    }
}
