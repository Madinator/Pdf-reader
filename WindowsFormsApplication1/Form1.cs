using System;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            size_of_screen();
        }
     
        private void cur_page()
        {
            VScrollBar vsb = new VScrollBar();
            vsb.Scroll += new System.Windows.Forms.ScrollEventHandler(vScroller_Scroll);
            
        }

        private void vScroller_Scroll(object sender, ScrollEventArgs e)
        {
            MessageBox.Show("123");
        }

        private void size_of_screen()
        {
            int x = SystemInformation.VirtualScreen.Width;
            int y = SystemInformation.VirtualScreen.Height - axAcroPDF1.Location.Y - 50;
            
            axAcroPDF1.Size = new System.Drawing.Size(x, y);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog odf = new OpenFileDialog();
            odf.Filter = "Pdf files (*.pdf)|*.pdf";

            /*if(odf.FileName != "")
            {
                axAcroPDF1.LoadFile(odf.FileName);
            }*/

            
            if (odf.ShowDialog() == DialogResult.OK)
            {

                axAcroPDF1.src = odf.FileName;
                viewToolStripMenuItem.Enabled = true;
                               
            }
            
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void convertTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog odf = new OpenFileDialog();
            odf.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            string path_to_desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            odf.InitialDirectory = path_to_desktop;
            if (odf.ShowDialog() == DialogResult.OK)
            {
                string path = odf.FileName.ToString();


            }
            try
            {
                List<string> data = File.ReadAllLines(odf.FileName.ToString()).ToList();
                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(path_to_desktop + "\\result_txt.pdf", FileMode.Create, FileAccess.Write));
                doc.Open();
                foreach (string s in data)
                {
                    doc.Add(new Paragraph(s));
                }
                doc.Close();
            }
            catch (ArgumentException)
            {
                
            }
            
        }

        private void convertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document document = new Document();
            OpenFileDialog odf = new OpenFileDialog();
            string path_to_desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            PdfWriter.GetInstance(document, new FileStream(path_to_desktop + "\\result_jpg.pdf",FileMode.Create));
            odf.Filter = "Jpg files (*.jpg)|*.jpg";
            odf.InitialDirectory = path_to_desktop;
            document.Open();

            if (odf.ShowDialog() == DialogResult.OK)
            {
                string path = odf.FileName.ToString();
                System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                iTextSharp.text.Image itext= iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                document.Add(itext);
                document.Close();
            }

            /*using (var stream = new FileStream("test.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();
                using (var imageStream = new FileStream("test.jpg", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var image = Image.GetInstance(imageStream);
                    document.Add(image);
                }
                document.Close();
            }*/
        }

        private void turnToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SendKeys.Send("+^{ADD}");
            
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("+^{SUBTRACT}");
        }



        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axAcroPDF1.gotoFirstPage();
        }

        private void lastPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axAcroPDF1.gotoLastPage();
        }

        private void nextPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axAcroPDF1.gotoNextPage();
        }

        private void previousPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axAcroPDF1.gotoPreviousPage();
        }

        
    }

}

