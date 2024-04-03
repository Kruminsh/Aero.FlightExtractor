using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using UglyToad.PdfPig.Content;

namespace Aero.FlightExtractor.Pdf.DocumentNavigation
{
    public sealed class PdfPage(Page page) : IPage
    {
        public int Number => page.Number;
        public string Text => page.Text;

        public IEnumerable<IPageElement> GetPageElements()
        {
            foreach (var word in page.GetWords())
            {
                yield return new PdfWord(word);
            }
        }
    }
}
