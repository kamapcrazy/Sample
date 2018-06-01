using DinkToPdf;
using DinkToPdf.Contracts;
using Sample.PDF.Services.Models;
using System;
using System.IO;

namespace Sample.PDF.Services
{
    public class PdfService: IPdfService
    {
        private readonly IConverter _converter;

        public PdfService()
        {
            _converter = new SynchronizedConverter(new PdfTools());
        }

        public Stream GenerateReport(string htmlContent)
        {
            var doc = new HtmlToPdfDocument
            {
                GlobalSettings =
                {
                    Margins = new MarginSettings {Top = 10},
                    PageOffset = 0
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = htmlContent,
                        FooterSettings = new FooterSettings
                        {
                            FontSize = 9,
                            Left = $"Print Date: {DateTime.Now:MM/dd/yyy h:mm:ss tt}",
                            Right = "Page: [page] ([toPage])"
                        },
                        PagesCount = true
                    }
                }
            };

            var docSettings = new DocSettings(ColorEnum.Color, OrientationEnum.Portrait, PaperKindEnum.A4);

            AdjustGlobalSettings(doc, docSettings);

            return new MemoryStream(_converter.Convert(doc));
        }

        private void AdjustGlobalSettings(HtmlToPdfDocument doc, DocSettings settings)
        {
            switch (settings.Color)
            {
                case ColorEnum.Color:
                    doc.GlobalSettings.ColorMode = ColorMode.Color;
                    break;
                case ColorEnum.Grayscale:
                    doc.GlobalSettings.ColorMode = ColorMode.Grayscale;
                    break;
                default:
                    doc.GlobalSettings.ColorMode = ColorMode.Color;
                    break;
            }

            switch (settings.Orientation)
            {
                case OrientationEnum.Landscape:
                    doc.GlobalSettings.Orientation = Orientation.Landscape;
                    break;
                case OrientationEnum.Portrait:
                    doc.GlobalSettings.Orientation = Orientation.Portrait;
                    break;
                default:
                    doc.GlobalSettings.Orientation = Orientation.Landscape;
                    break;
            }

            switch (settings.PaperKind)
            {
                case PaperKindEnum.Custom:
                    doc.GlobalSettings.PaperSize = PaperKind.Custom;
                    break;
                case PaperKindEnum.Letter:
                    doc.GlobalSettings.PaperSize = PaperKind.Letter;
                    break;
                case PaperKindEnum.A3:
                    doc.GlobalSettings.PaperSize = PaperKind.A3;
                    break;
                case PaperKindEnum.A4:
                    doc.GlobalSettings.PaperSize = PaperKind.A4;
                    break;
                case PaperKindEnum.A5:
                    doc.GlobalSettings.PaperSize = PaperKind.A5;
                    break;
                default:
                    doc.GlobalSettings.PaperSize = PaperKind.A4;
                    break;
            }
        }
    }
}
