using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using AdminLab.Controllers;

namespace AdminLab.Areas.LazyCat.Controllers;
[Area("LazyCat")]
public class IdentityRoleController : BaseController<ApplicationDbContext, IdentityRole>
{
    public IdentityRoleController(ApplicationDbContext context) : base(context)
    {
    }
}
