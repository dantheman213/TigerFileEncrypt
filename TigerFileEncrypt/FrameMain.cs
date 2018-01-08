using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TigerFileEncrypt
{
    public partial class FrameMain : Form
    {
        public FrameMain()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtFile.Text))
            {

                return;
            }

            if(String.IsNullOrEmpty(txtPassword.Text))
            {

                return;
            }

            if (tabControl.SelectedTab.Text == "Decrypt")
            {
                decryptFile();
            }
            else
            {
                encryptFile();              
            }
        }

        private void btnFileBrowse_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();

            if (tabControl.SelectedTab.Text == "Decrypt")
            {
                dlg.Filter = "Encrypted File (*.enc)|*.enc|All Files (*.*)|*.*";
            }
            else
            {
                dlg.Filter = "All Files (*.*)|*.*";
            }

            dlg.Multiselect = false;

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            txtFile.Text = dlg.FileName;
        }

        private void encryptFile()
        {
            runCommand(AppDomain.CurrentDomain.BaseDirectory + "openssl\\bin\\openssl.exe aes-256-cbc -salt -a -e -in " + txtFile.Text + " -out " + txtFile.Text + ".enc -k " + txtPassword.Text);
            MessageBox.Show("Successful! A *.enc file was placed in the same folder as the source file.");
        }

        private void decryptFile()
        {
            runCommand(AppDomain.CurrentDomain.BaseDirectory + "openssl\\bin\\openssl.exe aes-256-cbc -salt -a -d -in " + txtFile.Text + " -out " + txtFile.Text.Substring(0, txtFile.Text.Length - 4) + " -k " + txtPassword.Text);
            MessageBox.Show("Successful! The file has been decrypted and placed in the same folder as the source file.");
        }

        private void runCommand(String command)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFile.Text = "";

            if(tabControl.SelectedTab.Text == "Decrypt")
            {
                btnGenerate.Hide();
                btnExecute.Text = "Decrypt";
            }
            else
            {
                btnGenerate.Show();
                btnExecute.Text = "Encrypt";

            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TBD");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
