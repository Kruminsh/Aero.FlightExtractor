using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;

namespace Aero.FlightExtractor.Core.Interfaces.Specifications
{
    public abstract class FieldResolverBase<T> : IFieldResolver<T>
    {
        public abstract T? ResolveFrom(IPage page);

        object? IFieldResolver.ResolveFrom(IPage page) => ResolveFrom(page);
    }
}
