namespace Domain.Models;

public class Payment
{
    private Payment(int orderId, int bankId, decimal amount)
    {
        OrderId = orderId;
        BankId = bankId;
        Amount = amount;
    }

    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public int BankId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaymentDate { get; private set; } = DateTime.UtcNow;

    public static (Payment? payment, string error) Create(int orderId, int bankId, decimal amount)
    {
        if (amount <= 0)
            return (null, "Amount must be positive");

        return (new Payment(orderId, bankId, amount), string.Empty);
    }
}