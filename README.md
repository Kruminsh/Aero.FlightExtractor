# Aero.FlightExtractor
.NET Library for Flight Data extraction from documents with a pre-defined structure and fields.
Currently supported document extraction implementations: PDF.

#### Project Structure
* Aero.FlightExtractor.Core - contains abstractions and core service implementations that remain regardless of document and chapter specification implementations
* Aero.FlightExtractor.Core.Tests - Tests for core project
* Aero.FlightExtractor.Pdf - Document navigation, chapter specifications and field resolvers for PDF extraction implementation. Utilizes [PdfPig](https://github.com/UglyToad/PdfPig) for PDF processing.
* Aero.FlightExtractor.Pdf.DependencyInjection - extensions for registering PDF flight extractor into .NET IoC container.
* Aero.FlightExtractor.Pdf.Tests - Tests for Pdf project
* Aero.FlightExtractor.ConsoleDemo - Demo Console application that uses Aero.FlightExtractor.Pdf to extract flight data from a PDF file.

#### Adding new Chapters and Fields
Steps to create new chapters:
1. Create base model that implements ChapterBase, e.g.

```csharp
public class CrewBriefing : ChapterBase
{
    public Passengers? Passengers { get; set; }
    public int? DryOperatingWeight { get; set; }
    public float? DryOperatingIndex { get; set; }
    public IReadOnlyCollection<CrewMember>? Crew { get; set; }
}
```
2. Create Specification for that model that implements IChapterSpecification and define how to determine start of that chapter in a Page as well as field resolvers, e.g.
```csharp
public class CrewBriefingSpec : IChapterSpecification<CrewBriefing>
{
    public bool BeginsIn(IPage page)
    {
        if (page.Text.Contains("Flight Crew Briefing"))
        {
            var elements = page.GetPageElements().ToList();
            var pageOne = elements.FirstOrDefault(x => x.Text == "Page" && elements[elements.IndexOf(x) + 1].Text == "1");
            return pageOne != null;
        }

        return false;
    }

    public IReadOnlyDictionary<string, IFieldResolver> GetFieldResolvers(CrewBriefing chapter)
    {
        return new Dictionary<string, IFieldResolver>()
        {
            { nameof(chapter.Flight), new FlightIdentityResolver() },
            { nameof(chapter.Passengers), new PassengersResolver() },
            { nameof(chapter.Crew), new CrewMemberResolver() },
        };
    }

    public ChapterExtractor<CrewBriefing> CreateExtractor() => ChapterExtractor<CrewBriefing>.Initialize(this);

    IChapterExtractor IChapterSpecification.CreateExtractor() => CreateExtractor();
}
```

3. Define a Field Resolver per field in chapter object that specifes how to extract field value from a page. Field Resolver needs to implement FieldResolverBase, e.g.
```csharp
public class ArrivalTimeResolver : FieldResolverBase<TimeSpan?>
{
    public override TimeSpan? ResolveFrom(IPage page)
    {
        var words = page.GetPageElements().ToList();
        if (words.FirstOrDefault(x => x.Text == "STA:") is IPageElement stdLabel)
        {
            var idx = words.IndexOf(stdLabel);
            var timeText = words[idx + 1];
            if (TimeSpan.TryParse(timeText.Text, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            throw new FieldExtractionException("Failed to resolve departure time", page.Number, "DepartureTime");
        }

        return default;
    }
}
```