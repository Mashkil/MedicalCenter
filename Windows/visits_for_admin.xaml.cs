using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MedicalCenter.Windows
{

    public partial class visits_for_admin : Window
    {

        public visits_for_admin(int id_vis, string name_pat, string surname_pat, string patr_pat, string date_of_b_pat, string fio_doc, string serv_name, string date_of_rec, string time_of_rec)
        {

            try
            {
                InitializeComponent();
                name.Text = name_pat;
                surname.Text = surname_pat;
                patr.Text = patr_pat;
                date_of_birth.Text = date_of_b_pat;
                this.fio_doc.Text = fio_doc;
                this.service_name.Text = serv_name;
                this.date_of_rec.Text = date_of_rec;
                this.time_of_rec.Text = time_of_rec;

                using (medcentrDB db = new medcentrDB())
                {
                    var vis = db.Visits.FirstOrDefault(p => p.Id == id_vis);
                    this.complaints.Text = vis.Complaint;
                    this.appointment.Text = vis.Appointment;
                    this.therapy.Text = vis.Therapy;
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
                throw;
            }
            
        }

        private void save_Click(object sender, RoutedEventArgs e)  //сохранение в pdf
        {

            try
            {
                string file_path;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDFФайл (* .PDF) | * .PDF ";

                if (saveFileDialog.ShowDialog() == true)
                {
                    file_path = saveFileDialog.FileName;
                    Document document = new Document();
                    BaseFont baseFont = BaseFont.CreateFont("c:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(baseFont);
                    PdfPTable table = new PdfPTable(4);

                    table.AddCell(new Phrase("Имя   -  ", font));
                    table.AddCell(new Phrase(name.Text, font));
                    table.AddCell(new Phrase("ФИО Врача  -  ", font));
                    table.AddCell(new Phrase(fio_doc.Text, font));
                    table.AddCell(new Phrase("Фамилия   -  ", font));
                    table.AddCell(new Phrase(surname.Text, font));
                    table.AddCell(new Phrase("Специализация   -  ", font));
                    table.AddCell(new Phrase(service_name.Text, font));
                    table.AddCell(new Phrase("Отчество   -  ", font));
                    table.AddCell(new Phrase(patr.Text, font));
                    table.AddCell(new Phrase("Дата приема   -  ", font));
                    table.AddCell(new Phrase(date_of_rec.Text, font));
                    table.AddCell(new Phrase("Дата рождения   -  ", font));
                    table.AddCell(new Phrase(date_of_birth.Text, font));
                    table.AddCell(new Phrase("Время приема   -  ", font));
                    table.AddCell(new Phrase(time_of_rec.Text, font));

                    PdfPCell pdfPCell = new PdfPCell(new Phrase("\nЖалобы:", font));
                    pdfPCell.Colspan = 4;
                    pdfPCell.Border = 0;
                    table.AddCell(pdfPCell);

                    PdfPCell pdfPCell1 = new PdfPCell(new Phrase(complaints.Text, font));
                    pdfPCell1.Colspan = 4;
                    pdfPCell1.Border = 0;
                    table.AddCell(pdfPCell1);

                    PdfPCell pdfPCell2 = new PdfPCell(new Phrase("\nНазначения:", font));
                    pdfPCell2.Colspan = 4;
                    pdfPCell2.Border = 0;
                    table.AddCell(pdfPCell2);

                    PdfPCell pdfPCell3 = new PdfPCell(new Phrase(appointment.Text, font));
                    pdfPCell3.Colspan = 4;
                    pdfPCell3.Border = 0;
                    table.AddCell(pdfPCell3);

                    PdfPCell pdfPCell4 = new PdfPCell(new Phrase("\nЛечение:", font));
                    pdfPCell4.Colspan = 4;
                    pdfPCell4.Border = 0;
                    table.AddCell(pdfPCell4);

                    PdfPCell pdfPCell5 = new PdfPCell(new Phrase(therapy.Text, font));
                    pdfPCell5.Colspan = 4;
                    pdfPCell5.Border = 0;
                    table.AddCell(pdfPCell5);

                    PdfWriter.GetInstance(document, new FileStream(file_path, FileMode.Create));
                    document.Open();
                    document.Add(table);
                    document.Close();
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
                throw;
            }
            

        }
    }
}
