using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Domain.Entities;
using AdminLab.Controllers;

namespace AdminLab.Areas.LazyCat.Controllers;
[Area("LazyCat")]
public class FruitController : BaseController<ApplicationDbContext, Fruit>
{
    public FruitController(ApplicationDbContext context) : base(context)
    {
    }
}
