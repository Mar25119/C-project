public class SubscriptionService
{
    private readonly SubscriptionRepository _subscriptionRepository;

    public SubscriptionService(SubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public void Subscribe(int userId, string type)
    {
        _subscriptionRepository.Subscribe(userId, type);
    }

    public bool IsPremium(int userId)
    {
        return _subscriptionRepository.IsPremium(userId);
    }
}