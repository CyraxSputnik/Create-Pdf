using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Globalization;

namespace CreatePdf
{
    internal class InvoiceToPdf
    {
        private readonly int smallFontSize = 6;
        private readonly int stdFontSize = 7;

        public void FillPdf(string filename)
        {
            using var pdfWriter = new PdfWriter(filename);
            using var pdfDoc = new PdfDocument(pdfWriter);
            using var doc = new Document(pdfDoc, PageSize.LETTER);

            doc.SetMargins(12, 12, 36, 12);

            AddData(doc);
            AddFooter(doc);

            doc.Close();
        }

        private void AddData(Document doc)
        {
            Table table = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            
            for (int i = 0; i < 250; i++)
            {
                var cell = new Cell(1, 5)
                    .Add(new Paragraph($"My Favorite animals are boars and hippos")
                    .SetFontSize(stdFontSize).SetFont(bold));
                
                cell.SetBorder(Border.NO_BORDER);
                cell.SetPadding(0);
                table.AddCell(cell);
            }

            doc.Add(table);
        }

        private void AddFooter(Document doc)
        {
            if (doc is null)
                return;

            Table table = new Table(UnitValue.CreatePercentArray(60)).UseAllAvailableWidth();

            int numberOfPages = doc.GetPdfDocument().GetNumberOfPages();
            for (int i = 1; i <= numberOfPages; i++)
            {
                PdfPage page = doc.GetPdfDocument().GetPage(i);
                PdfCanvas pdfCanvas = new PdfCanvas(page);
                Rectangle rectangle = new Rectangle(
                    0,
                    0,
                    page.GetPageSize().GetWidth(),
                    15);

                Canvas canvas = new Canvas(pdfCanvas, doc.GetPdfDocument(), rectangle);

                var cell = new Cell(1, 20).SetFontSize(smallFontSize);
                cell.SetBorder(Border.NO_BORDER);
                cell.SetPadding(0);                
                table.AddCell(cell);

                cell = new Cell(1, 20).Add(new Paragraph("This document is an invoice")
                    .SetTextAlignment(TextAlignment.CENTER)).SetFontSize(smallFontSize);
                cell.SetBorder(Border.NO_BORDER);
                cell.SetPadding(0);                
                table.AddCell(cell);

                cell = new Cell(1, 10).SetFontSize(smallFontSize);
                cell.SetBorder(Border.NO_BORDER);
                cell.SetPadding(0);                
                table.AddCell(cell);

                cell = new Cell(1, 7)
                    .Add(new Paragraph($"Page {string.Format(CultureInfo.InvariantCulture, "{0:#,0}", i)} of {string.Format(CultureInfo.InvariantCulture, "{0:#,0}", numberOfPages)}   ")
                    .SetTextAlignment(TextAlignment.RIGHT)).SetFontSize(smallFontSize);
                cell.SetBorder(Border.NO_BORDER);
                cell.SetPadding(0);                
                table.AddCell(cell);

                cell = new Cell(1, 3).SetFontSize(smallFontSize);
                cell.SetBorder(Border.NO_BORDER);
                cell.SetPadding(0);                
                table.AddCell(cell);

                canvas.Add(table).SetFontSize(smallFontSize);
                canvas.Close();
            }

        }
    }
}