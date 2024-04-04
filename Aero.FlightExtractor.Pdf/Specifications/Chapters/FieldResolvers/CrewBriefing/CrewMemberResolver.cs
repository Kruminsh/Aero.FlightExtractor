using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters.Fields;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.CrewBriefing
{
    /// <summary>
    /// Crew member field resolver for Crew Briefing chapter
    /// </summary>
    internal sealed class CrewMemberResolver : FieldResolverBase<IReadOnlyCollection<CrewMember>>
    {
        public override IReadOnlyCollection<CrewMember>? ResolveFrom(IPage page)
        {
            try
            {
                if (page.Text.Contains("Flight Crew Briefing"))
                {
                    var elements = page.GetPageElements().ToList();

                    var funcLabel = elements.FirstOrDefault(x => x.Text == "Func");
                    var lcLabel = elements.FirstOrDefault(x => x.Text == "3LC");
                    var nameLabel = elements.FirstOrDefault(x => x.Text == "Name");
                    if (funcLabel != null && lcLabel != null && nameLabel != null)
                    {
                        var funcIndex = elements.IndexOf(funcLabel);
                        var lcIndex = elements.IndexOf(lcLabel);
                        var nameIndex = elements.IndexOf(nameLabel);

                        // Columns should be in correct order
                        if (lcIndex - funcIndex == 1 &&  nameIndex - lcIndex == 1) 
                        {
                            var lastColumnLabels = new[] { elements[nameIndex + 1], elements[nameIndex + 2] };
                            var lastColumnLeftBorder = lastColumnLabels.Select(x => x.Location.Left).Min();

                            // Extract all "functions" under Func column
                            var functions = new List<IPageElement>();
                            var firstFunction = elements.First(x => x.Location.Top < funcLabel.Location.Bottom && x.Location.Right < lcLabel.Location.Left);
                            functions.Add(firstFunction);

                            var nextFunction = elements.FirstOrDefault(x => x.Location.Top < firstFunction.Location.Bottom && x.Location.Right < lcLabel.Location.Left);
                            while (nextFunction?.Location.Left == firstFunction.Location.Left)
                            {
                                functions.Add(nextFunction);
                                nextFunction = elements.FirstOrDefault(x => x.Location.Top < nextFunction.Location.Bottom && x.Location.Right < lcLabel.Location.Left);
                            }
                            var bottomBorderElement = nextFunction;

                            var crewMembers = new List<CrewMember>();
                            for (int i = 0; i < functions.Count; i++)
                            {
                                var bottomBorder = i < functions.Count - 1 ? functions[i + 1].Location.Top : bottomBorderElement!.Location.Top;

                                // TODO: Maybe make a common extension method for looking for Elements in specific geometrical window ?
                                var nameElements = elements
                                    .Where(x => x.Location.Left > lcLabel.Location.Right &&
                                                    x.Location.Bottom <= functions[i].Location.Bottom &&
                                                    x.Location.Right < lastColumnLeftBorder &&
                                                    x.Location.Bottom > bottomBorder)
                                    .Select(x => x.Text)
                                    .ToList();

                                var fullName = string.Join(" ", nameElements);
                                crewMembers.Add(new CrewMember(fullName, functions[i].Text));
                            }

                            return crewMembers;
                        }
                    }
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
