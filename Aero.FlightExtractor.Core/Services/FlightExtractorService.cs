using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.ExtractionResults;

namespace Aero.FlightExtractor.Core.Services
{
    /// <summary>
    /// Flight Extractor service
    /// </summary>
    public class FlightExtractorService : IFlightExtractorService
    {
        private readonly IDocumentAccessor _documentAccessor;
        private readonly IReadOnlyCollection<IChapterSpecification> _chapterSpecifications;

        public FlightExtractorService(IChapterSpecProvider chapterSpecProvider, IDocumentAccessor documentAccessor)
        {
            _chapterSpecifications = chapterSpecProvider.GetChapterSpecifications();
            _documentAccessor = documentAccessor;
        }

        public FlightExtractionResult ExtractFlightData(string documentPath)
        {
            using var document = _documentAccessor.Open(documentPath);
            var extractedChapters = ExtractChaptersFromDocument(document);
            return FlightExtractionResult.Create(extractedChapters);
        }

        private List<ChapterExtractionResult> ExtractChaptersFromDocument(IDocument document)
        {
            IChapterExtractor? activeExtractor = null;
            var extractedChapters = new List<ChapterExtractionResult>();
            foreach (var page in document.GetPages())
            {
                ProcessPage(page, ref activeExtractor, extractedChapters);
            }
            FinalizeActiveExtractor(activeExtractor, extractedChapters);
            return extractedChapters;
        }

        private void ProcessPage(IPage page, ref IChapterExtractor? activeExtractor, ICollection<ChapterExtractionResult> results)
        {
            var newChapterSpecification = _chapterSpecifications.FirstOrDefault(spec => spec.BeginsIn(page));
            if (newChapterSpecification != null)
            {
                FinalizeActiveExtractor(activeExtractor, results);
                activeExtractor = newChapterSpecification.CreateExtractor();
            }

            activeExtractor?.ExtractFieldDataFrom(page);
        }

        private void FinalizeActiveExtractor(IChapterExtractor? extractor, ICollection<ChapterExtractionResult> results)
        {
            if (extractor != null) 
            {
                results.Add(extractor.Finalize());
            }
        }
    }
}
