using Domain.Models;
using Infastructure.Repositories;

namespace Application.Reviews;

public class ReviewService
{
    private readonly ReviewRepository _reviewRepo;

    public ReviewService(ReviewRepository reviewRepo) => _reviewRepo = reviewRepo;

    public async Task<Review> CreateReviewAsync(int productId, int userId, int rating, string? comment)
    {
        if (rating < 1 || rating > 5) throw new Exception("Rating must be between 1 and 5");

        var result = Review.Create(productId, userId, rating, comment);
        if (result.error is not null) throw new Exception(result.error);

        await _reviewRepo.CreateAsync(result.review!);
        return result.review!;
    }

    public async Task DeleteReviewAsync(int reviewId) =>
        await _reviewRepo.DeleteAsync(reviewId);
}