public class ReviewService
{
    private readonly ReviewRepository _reviewRepository;

    public ReviewService(ReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public void AddReview(int userId, int contentId, string text, int rating)
    {
        _reviewRepository.AddReview(userId, contentId, text, rating);
    }

    public List<Review> GetReviewsByContent(int contentId)
    {
        return _reviewRepository.GetReviewsByContent(contentId);
    }
}