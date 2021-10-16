using System;

namespace EventSourcing
{
    public interface IProjection 
    {


        void When(IEventData @event);
    }

    public interface IProjectionHandler
    {
        Type Handles { get; }
        void Handle(IAggregate<Guid> projection);
    }

}
