using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models;
using Aero.FlightExtractor.Pdf.DocumentNavigation;
using System.Globalization;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.CrewBriefing
{
    /// <summary>
    /// Flight Identity resolver for Crew Briefing chapter
    /// </summary>
    public class FlightIdentityResolver : FieldResolverBase<FlightIdentity>
    {
        public override FlightIdentity? ResolveFrom(IPage page)
        {
            // TODO: Optimize
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "DEP") is IPageElement depLabel)
            {
                var idx = words.IndexOf(depLabel);
                if (words[idx + 1].Text != "ARR") return null;

                var dateWord = (PdfWord)words[idx - 1];
                if (words.FirstOrDefault(x => x.Location.Bottom < dateWord.Location.Bottom) is IPageElement flightNrWord)
                {
                    var dateTime = DateTime.ParseExact(dateWord.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);
                    var flightDate = DateOnly.FromDateTime(dateTime);
                    return new FlightIdentity(flightNrWord.Text, flightDate);
                }
            }

            return default;
        }
    }
}
