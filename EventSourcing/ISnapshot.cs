using System;



namespace EventSourcing
{
    public interface ISnapshot
    {
        Type Handles { get; }
        void Handle(IAggregate<Guid> aggregate);
    }

}
