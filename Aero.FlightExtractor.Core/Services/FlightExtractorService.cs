using Aero.FlightExtractor.Core.ErrorHandling;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models;
using Aero.FlightExtractor.Core.Models.Chapters;

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
            var extractedChapters = new List<ChapterBase>();
            IChapterProcessor? chapterProcessor = null;

            using var document = _documentAccessor.Open(documentPath);
            foreach (var page in document.GetPages())
            {
                if (_chapterSpecifications.SingleOrDefault(x => x.BeginsIn(page)) is IChapterSpecification newChapter)
                {
                    if (chapterProcessor != null) extractedChapters.Add(chapterProcessor.Finalize());
                    chapterProcessor = newChapter.CreateProcessor();
                };

                if (chapterProcessor != null)
                {
                    chapterProcessor.ExtractFieldsIfAny(page);
                    if (chapterProcessor.AllFieldsExtracted())
                    {
                        extractedChapters.Add(chapterProcessor.Finalize());
                        chapterProcessor = null;
                    }
                }
            }

            if (chapterProcessor != null) extractedChapters.Add(chapterProcessor.Finalize());

            return CreateExtractionResult(extractedChapters);
        }

        private FlightExtractionResult CreateExtractionResult(List<ChapterBase> extractedChapters)
        {
            var flightData = extractedChapters
                .GroupBy(x => x.Flight)
                .Select(x => new FlightData(x.Key, x.ToList()))
                .ToList();

            return new FlightExtractionResult
            {
                Flights = flightData.ToList(),
                Errors = new List<ExtractionError>()
            };
        }
    }
}
