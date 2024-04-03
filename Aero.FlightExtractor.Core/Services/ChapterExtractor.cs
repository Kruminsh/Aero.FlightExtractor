using Aero.FlightExtractor.Core.ErrorHandling;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters;
using Aero.FlightExtractor.Core.Models.ExtractionResults;

namespace Aero.FlightExtractor.Core.Services
{
    /// <summary>
    /// Chapter data extractor
    /// </summary>
    /// <typeparam name="T">Chapter type</typeparam>
    public sealed class ChapterExtractor<T> : IChapterExtractor where T : ChapterBase, new()
    {
        private readonly IReadOnlyDictionary<string, IFieldResolver> _fieldResolvers;
        private readonly Dictionary<string, object?> _extractedData = [];
        private readonly List<ExtractionError> _extractionErrors = [];
        private readonly T _chapter;

        private ChapterExtractor(IChapterSpecification<T> specification)
        {
            _chapter = new T();
            _fieldResolvers = specification.GetFieldResolvers(_chapter);
        }

        public static ChapterExtractor<T> Initialize(IChapterSpecification<T> specification)
        {
            return new(specification);
        }

        public ChapterExtractor<T> ExtractFieldDataFrom(IPage page)
        {
            foreach (var resolver in _fieldResolvers.Where(x => !_extractedData.ContainsKey(x.Key)))
            {
                try
                {
                    var fieldResolver = resolver.Value;
                    var value = fieldResolver.ResolveFrom(page);
                    if (value is not null)
                    {
                        _extractedData.Add(resolver.Key, value);
                    }
                }
                catch (Exception ex)
                {
                    _extractionErrors.Add(new ExtractionError
                    {
                        PageNumber = page.Number,
                        ChapterObject = typeof(T).Name,
                        Message = ex.Message,
                        FieldName = resolver.Key
                    });
                }
            }

            return this;
        }

        public bool AllFieldsExtracted()
        {
            return _extractedData.Count == _fieldResolvers.Count;
        }

        public ChapterExtractionResult Finalize()
        {
            PopulateChapterProperties();
            return ChapterExtractionResult.Create(IsChapterValid() ? _chapter : null, _extractionErrors);
        }

        private bool IsChapterValid()
        {
            var isFlightIdentified = _chapter.Flight is not null && _chapter.Flight.IsValid();
            if (!isFlightIdentified)
            {
                _extractionErrors.Add(new ExtractionError
                {
                    Message = "Unable to identify flight",
                    ChapterObject = typeof(T).Name,
                });
            }

            return isFlightIdentified;
        }

        private void PopulateChapterProperties()
        {
            try
            {
                foreach (var propertyInfo in typeof(T).GetProperties())
                {
                    if (_extractedData.TryGetValue(propertyInfo.Name, out var value))
                    {
                        propertyInfo.SetValue(_chapter, value);
                    }
                }
            }
            catch (Exception ex)
            {
                _extractionErrors.Add(new ExtractionError
                {
                    ChapterObject = typeof(T).Name,
                    Message = $"Unable to assign values to chapter object: {ex.Message}",
                });
            }
        }

        public IChapterExtractor Initialize(IChapterSpecification chapterSpecification) => Initialize(chapterSpecification);

        IChapterExtractor IChapterExtractor.ExtractFieldDataFrom(IPage page) => ExtractFieldDataFrom(page);
    }
}
