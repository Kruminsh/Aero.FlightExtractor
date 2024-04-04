using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Services;
using Moq;
using Xunit;

namespace Aero.FlightExtractor.Core.Tests.Services
{
    public class FlightExtractorServiceTest
    {
        private readonly FlightExtractorService _flightExtractorService;
        private Mock<IChapterSpecProvider> _chapterSpecProviderMock;
        private Mock<IDocumentAccessor> _documentAccessorMock;

        public FlightExtractorServiceTest()
        {
            _chapterSpecProviderMock = new Mock<IChapterSpecProvider>();
            _documentAccessorMock = new Mock<IDocumentAccessor>();

            _flightExtractorService = new FlightExtractorService(_chapterSpecProviderMock.Object, _documentAccessorMock.Object);
        }

        [Fact]
        public void ExtractFlightData_WhenDocumentOpenFailed_ReturnErrors()
        {
            // Arrange
            const string filePath = "file";
            _documentAccessorMock.Setup(x => x.Open(It.Is<string>(s => s == filePath)))
                .Throws<Exception>();

            // Act
            var result = _flightExtractorService.ExtractFlightData(filePath);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Flights);
            Assert.NotEmpty(result.Errors);
        }
    }
}
