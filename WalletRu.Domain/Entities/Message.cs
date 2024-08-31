namespace WalletRu.Domain.Entities;

public class Message
{
    public long Id { get; set; }
    public string SerialNumber { get; set; }
    public string Body { get; set; }
    public string Timestamp { get; set; }
    public DateTime CreatedAt { get; set; }
}