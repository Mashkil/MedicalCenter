using System;
using System.IO;
using System.Linq;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;

namespace MedicalCenter.Windows
{
    public partial class create_report : Window
    {
        public create_report()
        {
            InitializeComponent();
            docs_and_pos();
        }

        public void docs_and_pos() //добавление специальностей и врачей
        {
            try
            {
                using (medcentrDB db = new medcentrDB())
                {
                    var docs = from doc in db.Doctors
                               select new
                               {
                                   Surname = doc.Lastname,
                                   Name = doc.Firstname,
                                   Patr = doc.Patronymic,
                                   doc.Id
                               };
                    var pos = from doc in db.Doctors
                              select new
                              {
                                  Position = doc.Position
                              };

                    foreach (var p in pos.Distinct())  //заполнение специальностей
                    {
                        pos_box.Items.Add(p.Position);
                    }

                    foreach (var doc in docs) //заполение списка докторов
                    {
                        string fio;
                        fio = $"{doc.Surname} {doc.Name.Substring(0, 1)}.{doc.Patr.Substring(0, 1)}. {doc.Id}";
                        doc_box.Items.Add(fio);
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private void create_report1_Click(object sender, RoutedEventArgs e) // формирование отчета
        {
            DateTime star_date = new DateTime(2022, 01, 01);
            if (start_date.Text != "")
            {
                star_date = DateTime.Parse(start_date.Text);
            }
            else
                start_date.Text = star_date.ToShortDateString();

            if (end_date.Text == "")
                end_date.Text = DateTime.Now.ToShortDateString();

            DateTime end = DateTime.Parse(end_date.Text);

            using (medcentrDB db = new medcentrDB())
            {
                if (doc_box.Text == "" && pos_box.Text == "")
                {
                    int? cost_of_serv = 0;
                    var res = from time in db.Time
                              join dat in db.Date on time.DateId equals dat.Id
                              where dat.Date1 >= star_date && dat.Date1 <= end
                              join vis in db.Visits on time.VisitId equals vis.Id
                              where vis.Appointment != null || vis.Complaint != null || vis.Therapy != null
                              join serv in db.Services on vis.Id_service equals serv.Id
                              where serv.Id == vis.Id_service
                              select new { vis.Id, serv.Cost };
                    
                    foreach (var item in res)
                    {
                        cost_of_serv += item.Cost;
                    }

                    create_pdf(res, cost_of_serv);
                }
                else if (pos_box.Text != "" && doc_box.Text == "")
                {
                    int? cost_of_serv = 0;
                    var res = from time in db.Time
                              join dat in db.Date on time.DateId equals dat.Id
                              where dat.Date1 >= star_date && dat.Date1 <= end
                              join vis in db.Visits on time.VisitId equals vis.Id
                              where vis.Appointment != null || vis.Complaint != null || vis.Therapy != null
                              join doc in db.Doctors on vis.DoctorId equals doc.Id
                              where doc.Position == pos_box.Text
                              join serv in db.Services on vis.Id_service equals serv.Id
                              where serv.Id == vis.Id_service
                              select new { vis.Id, serv.Cost };
                    
                    foreach (var item in res)
                    {
                        cost_of_serv += item.Cost;
                    }

                    create_pdf(res, cost_of_serv);
                }
                else if (doc_box.Text != "" && pos_box.Text == "")
                {
                    int? cost_of_serv=0;
                    int id_doc = Convert.ToInt32(doc_box.Text.Split(' ')[2]);
                    var res = from time in db.Time
                              join dat in db.Date on time.DateId equals dat.Id
                              where dat.Date1 >= star_date && dat.Date1 <= end
                              where time.DateId == dat.Id
                              join vis in db.Visits on time.VisitId equals vis.Id
                              where vis.Appointment != null || vis.Complaint != null || vis.Therapy != null
                              join doc in db.Doctors on vis.DoctorId equals doc.Id
                              where doc.Id == id_doc
                              join serv in db.Services on vis.Id_service equals serv.Id
                              where serv.Id == vis.Id_service
                              select new { vis.Id, serv.Cost };
                    
                    foreach (var item in res)
                    {
                        cost_of_serv +=item.Cost;
                    }

                    create_pdf(res,cost_of_serv);
                }
            }
        }

        private void create_pdf<T>(IQueryable<T> ts, int? cost) //создание pdf файла
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

                PdfPTable table = new PdfPTable(1);

                PdfPCell pdfPCell = new PdfPCell(new Phrase($"Всего пациентов за период с {start_date.Text} до {end_date.Text}\n\n", font));
                pdfPCell.Border = 0;
                table.AddCell(pdfPCell);

                if (doc_box.Text != "")
                {
                    PdfPCell pdfPCell1 = new PdfPCell(new Phrase($"У врача {doc_box.Text.Split(' ')[0]} {doc_box.Text.Split(' ')[1]} за этот период было {ts.Count()} пациентов", font));
                    pdfPCell1.Border = 0;
                    table.AddCell(pdfPCell1);
                    
                    PdfPCell pdfPCell4 = new PdfPCell(new Phrase($"Выручка от услуг врача за этот период составила {cost} руб.", font));
                    pdfPCell4.Border = 0;
                    table.AddCell(pdfPCell4);
                }
                else if (pos_box.Text != "")
                {
                    PdfPCell pdfPCell2 = new PdfPCell(new Phrase($"По направлению {pos_box.Text} за этот период было {ts.Count()} пациентов", font));
                    pdfPCell2.Border = 0;
                    table.AddCell(pdfPCell2);
                    
                    PdfPCell pdfPCell4 = new PdfPCell(new Phrase($"Выручка от услуг врачей по направлению {pos_box.Text} за этот период составила {cost} руб.", font));
                    pdfPCell4.Border = 0;
                    table.AddCell(pdfPCell4);
                }
                else
                {
                    PdfPCell pdfPCell3 = new PdfPCell(new Phrase($"По всем направлениям за этот период было {ts.Count()} пациентов", font));
                    pdfPCell3.Border = 0;
                    table.AddCell(pdfPCell3);
                    
                    PdfPCell pdfPCell4 = new PdfPCell(new Phrase($"По всем направлениям выручка за этот период составила {cost} руб.", font));
                    pdfPCell4.Border = 0;
                    table.AddCell(pdfPCell4);
                }

                PdfWriter.GetInstance(document, new FileStream(file_path, FileMode.Create));
                document.Open();
                document.Add(table);
                document.Close();

                if (MessageBox.Show("Отчет успешно создан, закрыть окно?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    this.Close();
            }
        }

    }
}
