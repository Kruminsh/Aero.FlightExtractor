using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for Time To Alternate field in Operational Flight Plan chapter
    /// </summary>
    internal sealed class FuelToAlternateResolver : FieldResolverBase<float?>
    {
        public override float? ResolveFrom(IPage page)
        {
            var alternateAirdrome = GetAlternateAirdrome(page);
            if (alternateAirdrome != null)
            {
                var elements = page.GetPageElements().ToList();
                if (elements.FirstOrDefault(x => x.Text == alternateAirdrome + ":") is IPageElement fieldLabel)
                {
                    var labelIndex = elements.IndexOf(fieldLabel);
                    if (float.TryParse(elements[labelIndex + 2].Text, out var result))
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
