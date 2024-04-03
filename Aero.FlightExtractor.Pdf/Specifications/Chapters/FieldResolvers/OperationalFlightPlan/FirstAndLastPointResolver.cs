using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.OperationalFlightPlan
{
    public class FirstAndLastPointResolver : FieldResolverBase<string?>
    {
        public override string? ResolveFrom(IPage page)
        {
            var pageText = page.Text;
            int start = pageText.IndexOf("ATC Route");
            if (start == -1) return null;

            var fromATC = pageText.Substring(start);
            int end = fromATC.IndexOf("To ALTN1:");
            var atcSubstring = fromATC.Substring(0, end);

            var toDestIdx = atcSubstring.IndexOf("To DEST:");
            string[] words = atcSubstring.Substring(toDestIdx + "To DEST:".Length).Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 1)
            {
                return $"{words[0]} - {words[^1]}";
            }

            return default;
        }
    }
}
