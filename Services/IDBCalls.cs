using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Services
{
    public interface IDBCalls
    {
        Task<IEnumerable<Visitors>> VisitorsLoggedInAsync();
    }
}