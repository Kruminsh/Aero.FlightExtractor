using Aero.FlightExtractor.Core.ErrorHandling;
using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters;
using Aero.FlightExtractor.Core.Models.ExtractionResults;

namespace Aero.FlightExtractor.Core.Services
{
    public sealed class ChapterProcessor<T> : IChapterProcessor where T : ChapterBase, new()
    {
        private readonly IReadOnlyDictionary<string, IFieldResolver> _fieldResolvers;
        private readonly Dictionary<string, object?> _extractedData = [];
        private readonly List<ExtractionError> _extractionErrors = [];
        private readonly T _chapter;

        private ChapterProcessor(IChapterSpecification<T> specification)
        {
            _chapter = new T();
            _fieldResolvers = specification.GetFieldResolvers(_chapter);
        }

        public static ChapterProcessor<T> Initialize(IChapterSpecification<T> specification)
        {
            return new(specification);
        }

        public ChapterProcessor<T> ExtractFieldsIfAny(IPage page)
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
                catch (FieldExtractionException ex)
                {
                    _extractionErrors.Add(new ExtractionError
                    {
                        PageNumber = page.Number,
                        ChapterObject = nameof(T),
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

            return ChapterExtractionResult.Create(_chapter, _extractionErrors);
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
                    ChapterObject = nameof(T),
                    Message = $"Unable to assign values to chapter object: {ex.Message}",
                });
            }
        }

        public IChapterProcessor Initialize(IChapterSpecification chapterSpecification) => Initialize(chapterSpecification);

        IChapterProcessor IChapterProcessor.ExtractFieldsIfAny(IPage page) => ExtractFieldsIfAny(page);
    }
}
