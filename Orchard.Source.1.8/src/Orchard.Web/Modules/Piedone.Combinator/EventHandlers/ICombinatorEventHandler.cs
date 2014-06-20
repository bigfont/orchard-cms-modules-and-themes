﻿using Orchard.Events;

namespace Piedone.Combinator.EventHandlers
{
    public interface ICombinatorEventHandler : IEventHandler
    {
        void ConfigurationChanged();
        void CacheEmptied();
    }
}
