using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters.Fields;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.CrewBriefing
{
    /// <summary>
    /// Crew member field resolver for Crew Briefing chapter
    /// </summary>
    public class CrewMemberResolver : FieldResolverBase<IReadOnlyCollection<CrewMember>>
    {
        public override IReadOnlyCollection<CrewMember>? ResolveFrom(IPage page)
        {
            try
            {
                if (page.Text.Contains("Flight Crew Briefing"))
                {
                    var words = page.GetPageElements().ToList();
                    if (words.FirstOrDefault(x => x.Text == "Func" && words[words.IndexOf(x) + 1].Text == "3LC") is IPageElement firstWord)
                    {
                        var firstColLabel = words.FirstOrDefault(x => x.Text == "Func");
                        var secondColLabel = words.FirstOrDefault(x => x.Text == "3LC");
                        if (firstColLabel != null && secondColLabel != null &&
                            words.IndexOf(secondColLabel) - words.IndexOf(firstColLabel) == 1)
                        {
                            var crewList = new List<CrewMember>();
                            var functions = new List<IPageElement>();
                            // Assumption: There is always at least 1 crew member
                            var firstFunction = words.First(x => x.Location.Top < firstColLabel.Location.Bottom
                                    && x.Location.Right < secondColLabel.Location.Left);
                            functions.Add(firstFunction);

                            var nextFunction = words.FirstOrDefault(x => x.Location.Top < firstFunction.Location.Bottom 
                                    && x.Location.Right < secondColLabel.Location.Left);
                            while (nextFunction?.Location.Left == firstFunction.Location.Left)
                            {
                                functions.Add(nextFunction);
                                nextFunction = words.FirstOrDefault(x => x.Location.Top < nextFunction.Location.Bottom
                                    && x.Location.Right < secondColLabel.Location.Left);
                            }

                            foreach(var functionEl in functions)
                            {
                                // TODO: Extract names
                                crewList.Add(new CrewMember("N/A", functionEl.Text));
                            }

                            return crewList;
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                throw new FieldExtractionException("Failed to resolve crew members", page.Number, "Crew", ex);
            }
                
            return default;
        }
    }
}
