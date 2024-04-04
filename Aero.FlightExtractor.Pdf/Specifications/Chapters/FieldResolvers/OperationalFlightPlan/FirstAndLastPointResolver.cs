using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for First and Last Navigation point field in Operational Flight Plan chapter
    /// </summary>
    internal sealed class FirstAndLastPointResolver : FieldResolverBase<string?>
    {
        public override string? ResolveFrom(IPage page)
        {
            var elements = page.GetPageElements().ToList();
            if (elements.FirstOrDefault(x => x.Text == "ATC") is IPageElement atcWord) 
            {
                var atcIndex = elements.IndexOf(atcWord);
                if (elements[atcIndex + 1].Text != "Route") return default;

                var restOfElements = elements.Skip(atcIndex + 2).ToList();
                var toDestLabel = restOfElements.FirstOrDefault(x => x.Text == "DEST:");
                var toAltnLabel = restOfElements.FirstOrDefault(x => x.Text == "ALTN1:");
                if (toDestLabel != null && toAltnLabel != null) 
                {
                    var startIndex = restOfElements.IndexOf(toDestLabel);
                    var endIndex = restOfElements.IndexOf(toAltnLabel) - 1; // - 1 due to "To" before "ALTN1"

                    var fullRoute = restOfElements.Skip(startIndex + 1).Take(endIndex - startIndex - 1).ToList();

                    if (fullRoute.Count > 1)
                    {
                        return $"{fullRoute[0].Text} - {fullRoute[^1].Text}";
                    }
                }
            }

            return default;
        }
    }
}
