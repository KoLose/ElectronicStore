namespace Domain.Models;

public class Review
{
    private const int MaxCommentLength = 2000;
    private const int MinRating = 1;
    private const int MaxRating = 5;

    private Review(int productId, int userId, int rating, string? comment)
    {
        ProductId = productId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
    }

    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int UserId { get; private set; }
    public int Rating { get; private set; }
    public string? Comment { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public static (Review? review, string error) Create(int productId, int userId, int rating, string? comment = null)
    {
        if (rating < MinRating || rating > MaxRating)
            return (null, "Invalid rating");

        if (comment?.Length > MaxCommentLength)
            return (null, "Comment too long");

        return (new Review(productId, userId, rating, comment), string.Empty);
    }
}