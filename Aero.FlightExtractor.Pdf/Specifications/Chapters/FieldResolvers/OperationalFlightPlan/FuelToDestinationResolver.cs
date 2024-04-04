using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.OperationalFlightPlan
{
    /// <summary>
    /// Fuel To Destination field resolver for Operational Flight Plan chapter
    /// </summary>
    public class FuelToDestinationResolver : FieldResolverBase<float?>
    {
        public override float? ResolveFrom(IPage page)
        {
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "To:") is IPageElement toLabel)
            {
                var idx = words.IndexOf(toLabel);
                var to = words[idx + 1];
                if (words.FirstOrDefault(x => x.Text == $"{to.Text}:") is IPageElement toFuelLabel)
                {
                    var idx2 = words.IndexOf(toFuelLabel);
                    if (float.TryParse(toFuelLabel.Text, out var fuel))
                    {
                        return fuel;
                    }

                    throw new FieldExtractionException("Failed to parse fuel to destination", page.Number, "FuelToDestination");
                }
            }

            return default;
        }
    }
}
