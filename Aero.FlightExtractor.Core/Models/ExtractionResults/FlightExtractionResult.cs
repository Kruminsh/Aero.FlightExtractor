using Aero.FlightExtractor.Core.ErrorHandling;
using Aero.FlightExtractor.Core.Models.Chapters;

namespace Aero.FlightExtractor.Core.Models.ExtractionResults
{
    /// <summary>
    /// Result model for flight extraction
    /// </summary>
    public sealed class FlightExtractionResult
    {
        public IReadOnlyCollection<FlightData> Flights { get; set; } = Array.Empty<FlightData>();
        public IReadOnlyCollection<ExtractionError> Errors { get; set; } = Array.Empty<ExtractionError>();

        private FlightExtractionResult() { }

        public static FlightExtractionResult CreateFromChapterExtractions(IReadOnlyCollection<ChapterExtractionResult> chapterExtractions)
        {
            var flightDictionary = new Dictionary<FlightIdentity, List<ChapterBase>>();
            foreach (var extraction in chapterExtractions)
            {
                if (extraction.Chapter != null)
                {
                    var chapter = extraction.Chapter;
                    if (flightDictionary.TryGetValue(chapter.Flight, out var chapters))
                    {
                        chapters.Add(chapter);
                    }
                    else
                    {
                        flightDictionary.Add(chapter.Flight, [chapter]);
                    }
                }
            }

            return new FlightExtractionResult
            {
                Flights = flightDictionary.Select(x => new FlightData(x.Key, x.Value)).ToList(),
                Errors = chapterExtractions.SelectMany(x => x.Errors).ToList()
            };
        }

        public static FlightExtractionResult CreateFromErrors(IReadOnlyCollection<ExtractionError> extractionErrors)
        {
            return new FlightExtractionResult
            {
                Errors = extractionErrors
            };
        }
    }
}
