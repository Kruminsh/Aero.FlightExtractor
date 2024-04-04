﻿using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using UglyToad.PdfPig.Content;

namespace Aero.FlightExtractor.Pdf.DocumentNavigation
{
    /// <summary>
    /// PdfPig Page Wrapper
    /// </summary>
    public sealed class PdfPage(Page page) : IPage
    {
        private bool _elemantsIteratedOnce = false;
        private List<IPageElement> _pageElements;

        public int Number => page.Number;
        public string Text => page.Text;

        public IEnumerable<IPageElement> GetPageElements()
        {
            return _elemantsIteratedOnce ? ReadElementsFromCache() : ReadElementsFromPDF();
        }

        private IEnumerable<IPageElement> ReadElementsFromPDF()
        {
            _pageElements = new List<IPageElement>();
            foreach (var word in page.GetWords())
            {
                var pdfWord = new PdfWord(word);
                yield return pdfWord;
                _pageElements.Add(pdfWord);
            }

            _elemantsIteratedOnce = true;
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
