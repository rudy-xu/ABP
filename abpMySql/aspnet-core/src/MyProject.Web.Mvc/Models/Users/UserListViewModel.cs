using System.Collections.Generic;
using MyProject.Roles.Dto;

namespace MyProject.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
