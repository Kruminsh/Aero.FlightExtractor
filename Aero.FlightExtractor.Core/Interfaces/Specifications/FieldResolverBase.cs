using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;

namespace Aero.FlightExtractor.Core.Interfaces.Specifications
{
    /// <summary>
    /// Base abstract field resolver class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FieldResolverBase<T> : IFieldResolver<T>
    {
        public abstract T? ResolveFrom(IPage page);

        object? IFieldResolver.ResolveFrom(IPage page) => ResolveFrom(page);
    }
}
