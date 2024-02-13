using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Domain.Entities.Identity;
using AdminLab.Controllers;

namespace AdminLab.Areas.LazyCat.Controllers;
[Area("LazyCat")]
public class ApplicationUserController : BaseController<ApplicationDbContext, ApplicationUser>
{
    public ApplicationUserController(ApplicationDbContext context) : base(context)
    {
    }
}
