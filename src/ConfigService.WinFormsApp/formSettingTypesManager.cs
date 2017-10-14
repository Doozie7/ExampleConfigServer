using System;
using System.Windows.Forms;

namespace ConfigService.WinFormsApp
{
    public partial class FormSettingTypesManager : Form
    {
        public FormSettingTypesManager()
        {
            InitializeComponent();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options = new FormOptions();
            options.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new FormAboutBox();
            about.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
