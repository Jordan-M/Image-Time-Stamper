using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;

namespace ImageTimeStamp
{
    class PDFManager
    {
        private PdfDocument document;
        private PdfPage page;

        private XFont labelFont = new XFont("Verdana", 16, XFontStyle.Bold);
        private XFont subTextFont = new XFont("Verdana", 12, XFontStyle.Regular);

        // The spacing to apply between the Folder: text and the path
        private const int spacing = 8;

        private const double leftPadding = 30;    

        //private int currentLine = 0;
        private double currentYOffset = 0;

        public PDFManager()
        {
            document = new PdfDocument();
            page = document.AddPage();
        }

        public void GenerateReport(string folderPath, int totalFiles, int filesStamped, Dictionary<string, int> fileTypes, List<string> errors)
        {
            XGraphics graphics = XGraphics.FromPdfPage(page);

            WriteDate(graphics);
            WriteFolder(graphics, "C:\\Users\\Jordan Mryyan\\Images");
            WriteNumber(graphics, "Total Files: ", 1837);
            WriteNumber(graphics, "Total Files Time Stamped:", 1772);
            WriteFileBreakdown(graphics, fileTypes);
            WriteErrors(graphics, errors);
        }

        private void WriteDate(XGraphics graphics)
        {
            // Create Fonts used in the PDF
            XFont headerFont = new XFont("Verdana", 18, XFontStyle.Bold);

            // Draw the text
            graphics.DrawString("Image Time Stamp Report - " + DateTime.Now.ToString("g"),
                                headerFont,
                                XBrushes.Black, 
                                new XRect(0, 0, page.Width, page.Height / 7),
                                XStringFormats.Center);

            graphics.DrawLine(XPens.Black, 0, page.Height / 8, page.Width, page.Height / 8);

            currentYOffset = page.Height / 8 + spacing;
        }

        private void WriteFolder(XGraphics graphics, string folderPath)
        {
            XFont pathFont = new XFont("Arial", 16, XFontStyle.Regular);

            graphics.DrawString("Folder: " ,
                                labelFont,
                                XBrushes.Black,
                                new XRect(leftPadding, currentYOffset, page.Width, page.Height),
                                XStringFormats.TopLeft);

            graphics.DrawString(folderPath,
                    pathFont,
                    XBrushes.Black,
                    new XRect(FontHelper.GetWidth(graphics, labelFont, "Folder: ") + spacing + leftPadding, currentYOffset, page.Width, page.Height),
                    XStringFormats.TopLeft);

            currentYOffset += labelFont.Height + spacing;
        }

        private void WriteNumber(XGraphics graphics,string label, int numFiles)
        {
            XFont numberFont = new XFont("Verdana", 16, XFontStyle.Regular);

            graphics.DrawString(label,
                    labelFont,
                    XBrushes.Black,
                    new XRect(leftPadding, currentYOffset, page.Width, page.Height),
                    XStringFormats.TopLeft);

            graphics.DrawString(numFiles.ToString("N0"),
                                numberFont,
                                XBrushes.Black,
                                new XRect(FontHelper.GetWidth(graphics, labelFont, label) + spacing + leftPadding, currentYOffset, page.Width, page.Height),
                                XStringFormats.TopLeft);

            currentYOffset += labelFont.Height + spacing;
        }

        private void WriteFileBreakdown(XGraphics graphics, Dictionary<string, int> fileTypes)
        {
            string label = "File Breakdown: ";

            graphics.DrawString(label,
                                labelFont,
                                XBrushes.Black,
                                new XRect(leftPadding, currentYOffset, page.Width, page.Height),
                                XStringFormats.TopLeft);

            currentYOffset += labelFont.Height + spacing;

            foreach (KeyValuePair<string, int> fileType in fileTypes)
            {
                graphics.DrawString(fileType.Key + ": " + fileType.Value.ToString("N0"),
                                    subTextFont,
                                    XBrushes.Black,
                                    new XRect(leftPadding * 2, currentYOffset, page.Width, page.Height),
                                    XStringFormats.TopLeft);
                currentYOffset += subTextFont.Height + spacing;
            }
        }

        private void WriteErrors(XGraphics graphics, List<string> errorPaths)
        { 
            string label = errorPaths.Count.ToString() + " Errors: ";

            graphics.DrawString(label,
                    labelFont,
                    XBrushes.Red,
                    new XRect(leftPadding, currentYOffset, page.Width, page.Height),
                    XStringFormats.TopLeft);

            currentYOffset += labelFont.Height + spacing;

            foreach (string error in errorPaths)
            {
                graphics.DrawString(error,
                                    subTextFont,
                                    XBrushes.Black,
                                    new XRect(leftPadding * 2, currentYOffset, page.Width, page.Height),
                                    XStringFormats.TopLeft);
                currentYOffset += subTextFont.Height + spacing;
            }
        }

        public void Save(string savePath, bool openOnSave = false)
        {
            document.Save(savePath);

            if (openOnSave)
                Process.Start(savePath);
        }
    }
}
