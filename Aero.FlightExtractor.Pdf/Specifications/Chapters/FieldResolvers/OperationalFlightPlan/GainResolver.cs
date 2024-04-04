using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for GAIN/LOSS field in Operational Flight Plan
    /// </summary>
    public class GainResolver : FieldResolverBase<int?>
    {
        private readonly IReadOnlyCollection<string> _gainOrLoss = ["GAIN", "LOSS"];

        public override int? ResolveFrom(IPage page)
        {
            var pageElements = page.GetPageElements().ToList();
            if (pageElements.FirstOrDefault(x => x.Text == "Loss:") is IPageElement gainLabel)
            {
                var lblIndex = pageElements.IndexOf(gainLabel);
                var nextElement = pageElements[lblIndex + 1];
                if (_gainOrLoss.Contains(nextElement.Text))
                {
                    var value = pageElements[lblIndex + 2].Text;
                    var extractedGain = value.Replace("$/TON", "").Trim();
                    if (int.TryParse(extractedGain, out var gainValue))
                    {
                        switch(nextElement.Text)
                        {
                            case "GAIN":
                                return gainValue;
                            case "LOSS":
                                return gainValue * -1;
                        }
                    }

                    throw new FieldExtractionException("Failed to parse gain/loss", page.Number, "Gain");
                }
            }

            return default;
        }
    }
}
