using Orchard.Events;
using Orchard.Tokens;

namespace Nwazet.ZenGallery.Tokens {
    public interface ITokenProvider : IEventHandler {
        void Describe(DescribeContext context);
        void Evaluate(EvaluateContext context);
    }
}