using Application.Common.Models.UserModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<UserProperty> Authenticate(string username, string password);
        Task<IList<UserModel>> GetAll(CancellationToken cancellationToken);

        Task<UserModel> GetById(int id);
        Task<UserProperty> Create(UserProperty user, string password, CancellationToken cancellationToken);
        Task<UpdateModel> Update(UserProperty user, CancellationToken cancellationToken, string password = null);
        Task<int> Delete(int id, CancellationToken cancellationToken);
    }
}
