using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ProjektDaniel.Proces
{
    public class DocumentProcessor
    {
        public List<DateTime> ExtractDatesFromDocx(string docxFilePath)
        {
            List<DateTime> dates = new List<DateTime>();

            using (WordprocessingDocument doc = WordprocessingDocument.Open(docxFilePath, false))
            {
                var body = doc.MainDocumentPart.Document.Body;
                foreach (var paragraph in body.Elements<Paragraph>())
                {
                    DateTime date;
                    if (DateTime.TryParseExact(paragraph.InnerText, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        dates.Add(date);
                    }
                }
            }

            return dates;
        }
    }
}
