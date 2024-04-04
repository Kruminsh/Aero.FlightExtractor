using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using System.Globalization;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for Time To Alternate field in Operational Flight Plan chapter
    /// </summary>
    public class TimeToAlternateResolver : FieldResolverBase<TimeSpan?>
    {
        public override TimeSpan? ResolveFrom(IPage page)
        {
            var alternateAirdrome = GetAlternateAirdrome(page);
            if (alternateAirdrome != null)
            {
                var elements = page.GetPageElements().ToList();
                if (elements.FirstOrDefault(x => x.Text == alternateAirdrome + ":") is IPageElement fieldLabel)
                {
                    var labelIndex = elements.IndexOf(fieldLabel);
                    if (TimeSpan.TryParse(elements[labelIndex + 1].Text, CultureInfo.InvariantCulture, out var result))
                    {
                        return result;
                    }

                    throw new FieldExtractionException("Failed to resolve Time To Alternate", page.Number, "TimeToAlternate");
                }
            }

            return default;
        }

        private string? GetAlternateAirdrome(IPage page)
        {
            var altnResolver = new FirstAlternateAirdromeResolver();
            return altnResolver.ResolveFrom(page);
        }
    }
}
