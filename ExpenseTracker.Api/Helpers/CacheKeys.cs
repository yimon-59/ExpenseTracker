namespace ExpenseTracker.Api.Helpers
{
    public class CacheKeys
    {
        public static string DashboardSummary(int userId)
        => $"dashboard:summary:user:{userId}";
    }
}
