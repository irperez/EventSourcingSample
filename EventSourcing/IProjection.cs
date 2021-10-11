namespace EventSourcing
{
    public interface IProjection
    {
        void When(IEventData @event);
    }

}
