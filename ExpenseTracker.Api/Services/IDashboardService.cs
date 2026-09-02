using ExpenseTracker.Api.DTOs.Dashboard;

namespace ExpenseTracker.Api.Services;

public interface IDashboardService
{
    Task<DashboardSummaryResponse> GetSummaryAsync();
}