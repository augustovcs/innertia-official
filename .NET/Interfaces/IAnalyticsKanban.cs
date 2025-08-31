using Task.DTO;
using Task.Models;

namespace Analytics.Interfaces;

public interface IAnalyticsKanban
{
    Task<int> QuantityTasks(int status);
}