namespace Cinema.AI.Interfaces
{
    public interface IChatBot
    {
        Task<string> ChatAsync(string message);
    }
}
