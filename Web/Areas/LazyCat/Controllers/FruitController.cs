using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using AdminLab.Models;
using AdminLab.Controllers;

namespace AdminLab.Areas.LazyCat.Controllers;
[Area("LazyCat")]
public class FruitController : BaseController<ApplicationDbContext, Fruit>
{
    public FruitController(ApplicationDbContext context) : base(context)
    {
    }
}
