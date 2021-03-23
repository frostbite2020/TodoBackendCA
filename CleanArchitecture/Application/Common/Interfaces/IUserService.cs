using Application.Common.Models;
using Application.Common.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<UserProperties> Authenticate(string username, string password);
        Task<IList<UserModel>> GetAll(CancellationToken cancellationToken);

        Task<UserModel> GetById(int id);
        Task<UserProperties> Create(UserProperties user, string password, CancellationToken cancellationToken);
        Task<UpdateModel> Update(UserProperties user, CancellationToken cancellationToken, string password = null);
        Task<int> Delete(int id, CancellationToken cancellationToken);
    }
}
