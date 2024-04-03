using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters.Fields;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.CrewBriefing
{
    public class PassengersResolver : FieldResolverBase<Passengers>
    {
        public override Passengers? ResolveFrom(IPage page)
        {
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "PAX") is IPageElement paxLabel)
            {
                if (words.FirstOrDefault(x => x.Text == "C/Y" && x.Location.Bottom < paxLabel.Location.Bottom) is IPageElement cyLabel)
                {
                    var timeLabel = words[words.IndexOf(cyLabel) - 1];
                    if (words.FirstOrDefault(x =>x.Location.Left > timeLabel.Location.Right && x.Location.Top < timeLabel.Location.Bottom) is IPageElement passengers)
                    {
                        string[] parts = passengers.Text.Split('/');

                        if (parts.Length == 2)
                        {
                            var ecoExtract = int.TryParse(parts[0], out int economy);
                            var busExtract = int.TryParse(parts[1], out int business);
                            return new Passengers(ecoExtract ? economy : null, busExtract ? business : null);
                        }
                    }
                }
            }

            return null;
        }
    }
}
