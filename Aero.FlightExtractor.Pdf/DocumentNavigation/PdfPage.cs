using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using UglyToad.PdfPig.Content;

namespace Aero.FlightExtractor.Pdf.DocumentNavigation
{
    /// <summary>
    /// PdfPig Page Wrapper
    /// </summary>
    public sealed class PdfPage(Page page) : IPage
    {
        private bool _allElementsIteratedOnce = false;
        private ICollection<IPageElement> _pageElements = Array.Empty<IPageElement>();

        public int Number => page.Number;
        public string Text => page.Text;

        public IEnumerable<IPageElement> GetPageElements()
        {
            return _allElementsIteratedOnce ? ReadElementsFromCache() : ReadElementsFromPDF();
        }

        private IEnumerable<IPageElement> ReadElementsFromPDF()
        {
            _pageElements = new List<IPageElement>();
            foreach (var word in page.GetWords())
            {
                var pdfWord = new PdfWord(word);
                _pageElements.Add(pdfWord);
                yield return pdfWord;
            }

            _allElementsIteratedOnce = true;
        }

        private IEnumerable<IPageElement> ReadElementsFromCache()
        {
            foreach (var word in _pageElements)
            {
                yield return word;
            }
        }
    }
}
