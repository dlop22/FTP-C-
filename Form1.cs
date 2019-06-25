using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace FTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(username, password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(filePath);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();

            MessageBox.Show("Uploaded Successfully");
        }

        private void btnupload_Click(object sender, EventArgs e)
        {
            btnupload.Enabled = false;
            Application.DoEvents();

            uploadFile(txtboxftp.Text, txtboxfile.Text, txtboxusername.Text, txtboxpassword.Text);
            btnupload.Enabled = true;
        }

        private void txtboxftp_TextChanged(object sender, EventArgs e)
        {
            if (!txtboxftp.Text.StartsWith("ftp://"))
                txtboxftp.Text = "ftp://" + txtboxftp.Text;
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            if (openFile1.ShowDialog() == DialogResult.OK)
                txtboxfile.Text = openFile1.FileName;
        }
    }
}
