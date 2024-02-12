using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using AdminLab.Models;
using AdminLab.Controllers;

namespace AdminLab.Areas.LazyCat.Controllers;
[Area("LazyCat")]
public class IdentityRoleController : BaseController<ApplicationDbContext, IdentityRole>
{
    public IdentityRoleController(ApplicationDbContext context) : base(context)
    {
    }
}
