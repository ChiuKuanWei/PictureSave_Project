using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureSave_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 初始化右鍵選單和選單項目
            InitializeContextMenu();
        }

        private void btn_LoadPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImage = openFileDialog.FileName;
                pictureBox1.Image = new System.Drawing.Bitmap(selectedImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void InitializeContextMenu()
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem saveImageMenuItem = new ToolStripMenuItem();
            saveImageMenuItem.Text = "另存圖檔";
            saveImageMenuItem.Click += SaveImageMenuItem_Click;

            contextMenuStrip.Items.Add(saveImageMenuItem);

            pictureBox1.ContextMenuStrip = contextMenuStrip;
        }

        private void SaveImageMenuItem_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image == null)
            {
                MessageBox.Show("請先載入圖片!!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
                saveFileDialog.Title = "另存圖檔";
                //saveFileDialog.FileName = "image";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Image image = pictureBox1.Image;
                    if (image != null)
                    {
                        try
                        {
                            image.Save(saveFileDialog.FileName);
                            MessageBox.Show("儲存成功!!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("儲存失敗:" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }
    }
}
