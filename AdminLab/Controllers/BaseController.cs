using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminLab.Controllers;

public class BaseController<TDbContext, TModel> 
    : Controller where TModel 
        : class where TDbContext 
        : DbContext
{
    private readonly TDbContext _context;
    private DbSet<TModel> _modelSet;

    public BaseController(TDbContext context)
    {
        _context = context;
        _modelSet = _context.Set<TModel>();
    }

    public IActionResult Index()
    {
        var list = _modelSet.ToList();
        return View(list);
    }
}