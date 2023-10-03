using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Andrew
{
    public partial class Form1 : Form
    {
        private string inputFilePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void SelectPathButton_Click(object sender, EventArgs e)
        {
            inputFilePath = SelectConvertPath();

            if (string.IsNullOrEmpty(inputFilePath))
            {
                MessageBox.Show("You didn't select the file!");
            }
        }

        //selecting where to save a converted file
        private string SelectSavePath()
        {
            string resultPath = string.Empty;

            // Create an instance of the SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Set the file filter and default extension (optional)
            saveFileDialog.Filter = "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = "jpg";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                resultPath = saveFileDialog.FileName;
            }

            return resultPath;
        }

        //selecting file you want to convert
        private string SelectConvertPath()
        {
            string resultPath = string.Empty;

            // Create an instance of the OpenFileDialog
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Select a title for a dialog window
            openFileDialog1.Title = "Select File";

            //Select a filter for formats of files
            openFileDialog1.Filter = "DNG and CR2 Files | *.dng; *.cr2 | All Files | *.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();

            if (!string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                resultPath = openFileDialog1.FileName;
            }

            return resultPath;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string saveFilePath = SelectSavePath();

            // Show the dialog to let the user select the save location and file name
            if (string.IsNullOrEmpty(saveFilePath))
            {
                MessageBox.Show("Input file path is empty or null.");
                return;
            }

            using (MagickImage image = new MagickImage(inputFilePath))
            {
                // Set the output format to JPEG and specify the compression quality (0-100)
                image.Format = MagickFormat.Jpeg;
                image.Quality = 10; // Adjust the quality as needed

                // Save the converted image to the selected file path
                image.Write(saveFilePath);

                MessageBox.Show("Image saved successfully.");
            }
        }
    }
}
