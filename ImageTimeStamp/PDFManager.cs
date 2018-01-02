using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using System.Drawing;
using PdfSharp.Drawing.Layout;

namespace ImageTimeStamp
{
    static class PDFManager
    {
        private static PdfPage page;

        private static XFont labelFont = new XFont("Verdana", 16, XFontStyle.Bold);
        private static XFont subTextFont = new XFont("Verdana", 12, XFontStyle.Regular);

        // The spacing to apply between the Folder: text and the path
        private const int spacing = 8;

        private const double leftPadding = 30;    

        //private int currentLine = 0;
        private static double currentYOffset = 0;

        public static PdfDocument GenerateReport(ReportData report)
        {
            PdfDocument document = new PdfDocument();
            page = document.AddPage();
            XGraphics graphics = XGraphics.FromPdfPage(page);

            WriteDate(graphics);
            WriteFolder(graphics, report.FolderPath);
            WriteNumber(graphics, "Total Files: ", report.TotalFiles);
            WriteNumber(graphics, "Total Files Time Stamped:", report.TotalFilesStamped);
            WriteFileBreakdown(graphics, report.FileTypes);
            WriteErrors(graphics, report.Errors);
            return document;
        }

        private static void WriteDate(XGraphics graphics)
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

        private static void WriteFolder(XGraphics graphics, string folderPath)
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

        private static void WriteNumber(XGraphics graphics,string label, int numFiles)
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

        private static void WriteFileBreakdown(XGraphics graphics, Dictionary<string, int> fileTypes)
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

        private static void WriteErrors(XGraphics graphics, List<string> errorPaths)
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
                XTextFormatter test = new XTextFormatter(graphics);
                test.DrawString(error,
                                    subTextFont,
                                    XBrushes.Black,
                                    new XRect(leftPadding * 2, currentYOffset, page.Width - leftPadding * 2, page.Height),
                                    XStringFormats.TopLeft);

                if (FontHelper.GetWidth(graphics, subTextFont, error) > page.Width - leftPadding * 2)
                    currentYOffset += spacing;
                 currentYOffset += subTextFont.Height + spacing;
            }
        }

        public static void Save(PdfDocument document, string savePath, bool openOnSave = false)
        {
            document.Save(savePath);

            if (openOnSave)
                Process.Start(savePath);
        }
    }
}
