namespace Domain.Models;

public class Bank
{
    private const int MaxCardNumberLength = 20;
    private const int MaxBankNameLength = 100;

    private Bank(int userId, string cardNumber, string bankName)
    {
        UserId = userId;
        CardNumber = cardNumber;
        BankName = bankName;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string CardNumber { get; private set; }
    public string BankName { get; private set; }

    public static (Bank? bank, string error) Create(int userId, string cardNumber, string bankName)
    {
        if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length > MaxCardNumberLength)
            return (null, "Invalid card number");

        if (string.IsNullOrWhiteSpace(bankName) || bankName.Length > MaxBankNameLength)
            return (null, "Invalid bank name");

        return (new Bank(userId, cardNumber, bankName), string.Empty);
    }
}