using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.OperationalFlightPlan
{
    public class RouteFieldResolver : FieldResolverBase<string?>
    {
        public override string? ResolveFrom(IPage page)
        {
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "From:") is IPageElement fromLabel)
            {
                var idx = words.IndexOf(fromLabel);
                var fromText = words[idx + 1].Text;

                if (words.FirstOrDefault(x => x.Text == "To:") is IPageElement toLabel)
                {
                    var idx2 = words.IndexOf(toLabel);
                    var toText = words[idx2 + 1].Text;
                    return $"{fromText} - {toText}";
                }
            }

            return null;
        }
    }
}
