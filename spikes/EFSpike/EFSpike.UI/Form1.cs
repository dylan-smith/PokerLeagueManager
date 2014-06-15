using System;
using System.Linq;
using System.Windows.Forms;
using EFSpike.Domain;

namespace EFSpike.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var repo = new Repository();

            var data = repo.GetResults();

            MessageBox.Show(data.Count().ToString());
        }
    }
}
