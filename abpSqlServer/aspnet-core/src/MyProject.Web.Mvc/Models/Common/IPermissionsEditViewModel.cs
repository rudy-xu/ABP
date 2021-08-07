using System.Collections.Generic;
using MyProject.Roles.Dto;

namespace MyProject.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}