﻿using System.Collections.Generic;
using MyProject.Roles.Dto;

namespace MyProject.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
