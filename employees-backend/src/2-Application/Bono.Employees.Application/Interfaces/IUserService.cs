using Bono.Employees.Application.ViewModels;
using Bono.Employees.Domain.Validations;

namespace Bono.Employees.Application.Interfaces
{
    public interface IUserService : IService<UserViewModel>
    {
        ValidationResult Authenticate(UserAuthenticateRequestViewModel user);
    }
}
