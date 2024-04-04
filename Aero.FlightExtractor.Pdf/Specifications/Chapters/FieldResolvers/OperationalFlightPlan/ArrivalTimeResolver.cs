using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using System.Globalization;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for Arrival Time field in Operational Flight Plan chapter
    /// </summary>
    internal sealed class ArrivalTimeResolver : FieldResolverBase<TimeSpan?>
    {
        public override TimeSpan? ResolveFrom(IPage page)
        {
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "STA:") is IPageElement staLabel)
            {
                var labelIndex = words.IndexOf(staLabel);
                var timeText = words[labelIndex + 1];
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
