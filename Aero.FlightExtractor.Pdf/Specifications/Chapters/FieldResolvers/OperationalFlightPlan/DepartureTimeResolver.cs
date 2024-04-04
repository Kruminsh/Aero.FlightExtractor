using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using System.Globalization;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for Departure Time field in Operational Flight Plan chapter
    /// </summary>
    public class DepartureTimeResolver : FieldResolverBase<TimeSpan?>
    {
        public override TimeSpan? ResolveFrom(IPage page)
        {
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "STD:") is IPageElement stdLabel)
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
}
