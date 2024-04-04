using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using System.Globalization;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for Time To Destination field in Operational Flight Plan chapter
    /// </summary>
    internal sealed class TimeToDestination : FieldResolverBase<TimeSpan?>
    {
        public override TimeSpan? ResolveFrom(IPage page)
        {
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "To:") is IPageElement toLabel)
            {
                var idx = words.IndexOf(toLabel);
                var to = words[idx + 1];
                if (words.FirstOrDefault(x => x.Text == $"{to.Text}:") is IPageElement toFuelLabel)
                {
                    var idx2 = words.IndexOf(toFuelLabel);
                    if (TimeSpan.TryParse(words[idx2 + 1].Text, CultureInfo.InvariantCulture, out var result))
                    {
                        return result;
                    }

                    throw new FieldExtractionException("Failed to resolve Time To Destination", page.Number, "TimeToDestination");
                }
            }

            return default;
        }
    }
}
