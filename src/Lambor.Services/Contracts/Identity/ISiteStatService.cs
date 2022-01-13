using System.Collections.Generic;
using System.Threading.Tasks;
using Lambor.Entities.Identity;
using System.Security.Claims;
using Lambor.ViewModels.Identity;

namespace Lambor.Services.Contracts.Identity
{
    public interface ISiteStatService
    {
        Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake);

        Task<List<User>> GetTodayBirthdayListAsync();

        Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal);

        Task<AgeStatViewModel> GetUsersAverageAge();
    }
}