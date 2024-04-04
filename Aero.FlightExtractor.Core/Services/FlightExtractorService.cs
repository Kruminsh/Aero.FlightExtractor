using Aero.FlightExtractor.Core.ErrorHandling;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.ExtractionResults;

namespace Aero.FlightExtractor.Core.Services
{
    /// <summary>
    /// Flight Extractor service
    /// </summary>
    public sealed class FlightExtractorService(IChapterSpecProvider chapterSpecProvider, IDocumentAccessor documentAccessor) : IFlightExtractorService
    {
        private readonly IDocumentAccessor _documentAccessor = documentAccessor;
        private readonly IReadOnlyCollection<IChapterSpecification> _chapterSpecifications = chapterSpecProvider.GetChapterSpecifications();

        /// <inheritdoc />
        public FlightExtractionResult ExtractFlightData(string documentPath)
        {
            try
            {
                using var document = _documentAccessor.Open(documentPath);
                var extractedChapters = ExtractChaptersFromDocument(document);
                return FlightExtractionResult.CreateFromChapterExtractions(extractedChapters);
            }
            catch (Exception ex)
            {
                return FlightExtractionResult.CreateFromErrors(new[]
                {
                    new ExtractionError
                    {
                        Message = $"Unexpected error during flight extraction: {ex.Message}",
                    }
                });
            }
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
