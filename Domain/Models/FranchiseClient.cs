namespace Domain.Models;

public class FranchiseClient
{
    private FranchiseClient(int franchiseId, int userId)
    {
        FranchiseId = franchiseId;
        UserId = userId;
    }

    public int Id { get; private set; }
    public int FranchiseId { get; private set; }
    public int UserId { get; private set; }

    public static (FranchiseClient? franchiseClient, string error) Create(int franchiseId, int userId)
    {
        return (new FranchiseClient(franchiseId, userId), string.Empty);
    }
}