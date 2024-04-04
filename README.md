# Aero.FlightExtractor
.NET Library for Flight Data extraction from documents with a pre-defined structure and fields.
Currently supported document extraction implementations: PDF.

#### Project Structure
* Aero.FlightExtractor.Core - contains abstractions and core service implementations that remain regardless of document and chapter specification implementations
* Aero.FlightExtractor.Core.Tests - Tests for core project
* Aero.FlightExtractor.Pdf - Document navigation, chapter specifications and field resolvers for PDF extraction implementation. Utilizes [PdfPig](https://github.com/UglyToad/PdfPig) for PDF processing.
* Aero.FlightExtractor.Pdf.DependencyInjection - extensions for registering PDF flight extractor into .NET IoC container.
* Aero.FlightExtractor.Pdf.Tests - Tests for Pdf project
* Aero.FlightExtractor.ConsoleDemo - Demo Console application that uses Aero.FlightExtractor.Pdf to extract flight data from a PDF file.