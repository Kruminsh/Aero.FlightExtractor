using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using UglyToad.PdfPig.Core;

namespace Aero.FlightExtractor.Pdf.DocumentNavigation
{
    public sealed class PdfWordLocation(PdfRectangle pdfRectangle) : ILocation
    {
        public double Bottom => pdfRectangle.Bottom;
        public double Top => pdfRectangle.Top;
        public double Left => pdfRectangle.Left;
        public double Right => pdfRectangle.Right;
    }
}
