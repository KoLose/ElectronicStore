using System.Text.RegularExpressions;

namespace Domain.Models;

public class Franchise
{
    private const int MaxNameLength = 100;
    private const int MaxAddressLength = 200;
    private static readonly Regex PhoneRegex = new(@"^(\+7|8)\d{10}$", RegexOptions.Compiled);

    private Franchise(string name, string address, string phone)
    {
        Name = name;
        Address = address;
        Phone = phone;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Phone { get; private set; }

    public static (Franchise? franchise, string error) Create(string name, string address, string phone)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
            return (null, "Invalid name");

        if (string.IsNullOrWhiteSpace(address) || address.Length > MaxAddressLength)
            return (null, "Invalid address");

        var normalizedPhone = Regex.Replace(phone ?? "", @"[\s\-\(\)]", "");
        if (!PhoneRegex.IsMatch(normalizedPhone))
            return (null, "Invalid Russian phone number");

        return (new Franchise(name, address, normalizedPhone), string.Empty);
    }
}