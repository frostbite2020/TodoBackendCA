using Application.Common.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserManagements.Queries.UserGetAll
{
    public class UsersVm
    {
        public IList<UserModel> UserDatas { get; set; }
    }
}
