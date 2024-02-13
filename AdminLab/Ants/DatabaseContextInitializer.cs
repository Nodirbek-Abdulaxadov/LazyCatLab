using AdminLab.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using System.Text;

namespace AdminLab.Ants;

public class DatabaseContextInitializer<T> where T : DbContext
{
    public static T? DatabaseContext { get; set; }
    public static List<IEntityType> entityTypes = new List<IEntityType>();
    public static void Initialize(T context)
    {
        DatabaseContext = context;
        entityTypes = context.Model.GetEntityTypes()
                                   .Where(x => !x.ClrType.IsGenericType)
                                   .ToList();
        CreateLazyCatArea();
        CreateLayout();
        CreateControllers();
        CreateIndexViews();
    }

    private static void CreateLazyCatArea()
    {
        var areaPath = Path.Combine(Directory.GetCurrentDirectory(), "Areas");
        if (!Directory.Exists(areaPath))
        {
            Directory.CreateDirectory(areaPath);
        }

        var lazyCatAreaPath = Path.Combine(areaPath, "LazyCat");
        if (!Directory.Exists(lazyCatAreaPath))
        {
            Directory.CreateDirectory(lazyCatAreaPath);
        }

        var modelsFolder = Path.Combine(lazyCatAreaPath, "Models");
        if (!Directory.Exists(modelsFolder))
        {
            Directory.CreateDirectory(modelsFolder);
        }

        var controllersFolder = Path.Combine(lazyCatAreaPath, "Controllers");
        if (!Directory.Exists(controllersFolder))
        {
            Directory.CreateDirectory(controllersFolder);
        }

        var viewsFolder = Path.Combine(lazyCatAreaPath, "Views");
        if (!Directory.Exists(viewsFolder))
        {
            Directory.CreateDirectory(viewsFolder);
        }

        var sharedFolder = Path.Combine(viewsFolder, "Shared"); // Change this line
        if (!Directory.Exists(sharedFolder))
        {
            Directory.CreateDirectory(sharedFolder);
        }

        Console.WriteLine("LazyCat area created successfully.");
    }

    private static void CreateControllers()
    {
        foreach (var model in entityTypes)
        {
            CreateController(model);
        }
    }

    private static void CreateController(IEntityType model)
    {
        var controllerClass = new StringBuilder();
        controllerClass.AppendLine("using Microsoft.AspNetCore.Mvc;");
        controllerClass.AppendLine($"using {typeof(T).Namespace};");
        controllerClass.AppendLine($"using {model.ClrType.Namespace};");
        controllerClass.AppendLine("using AdminLab.Controllers;");
        controllerClass.AppendLine();
        controllerClass.AppendLine("namespace AdminLab.Areas.LazyCat.Controllers;");
        controllerClass.AppendLine("[Area(\"LazyCat\")]");
        controllerClass.AppendLine($"public class {model.DisplayName()}Controller : BaseController<{DatabaseContext!.GetType().Name}, {model.DisplayName()}>");
        controllerClass.AppendLine("{");
        controllerClass.AppendLine($"    public {model.DisplayName()}Controller({DatabaseContext.GetType().Name} context) : base(context)");
        controllerClass.AppendLine("    {");
        controllerClass.AppendLine("    }");
        controllerClass.AppendLine("}");

        var controllersFolder = Path.Combine(Directory.GetCurrentDirectory(), "Areas", "LazyCat", "Controllers");
        if (!Directory.Exists(controllersFolder))
        {
            Directory.CreateDirectory(controllersFolder);
        }

        var controllerClassPath = Path.Combine(controllersFolder, $"{model.DisplayName()}Controller.cs");
        if (!File.Exists(controllerClassPath))
        {
            File.WriteAllText(controllerClassPath, controllerClass.ToString());
        }

        Console.WriteLine($"Controller class {model.DisplayName()}Controller created successfully.");
    }

    private static void CreateLayout()
    {
        var models = entityTypes.Select(x => x.DisplayName()).ToList();
        var layout = BaseViews.Layout(models);
        var layoutPath = Path.Combine(Directory.GetCurrentDirectory(), "Areas", "LazyCat", "Views", "Shared", "_Layout.cshtml");

        if (!File.Exists(layoutPath))
        {
            File.WriteAllText(layoutPath, layout);
        }

        Console.WriteLine("Layout created successfully.");

        var viewImports = BaseViews.ViewImports(entityTypes.Select(x => x.ClrType.Namespace).ToList());
        var viewImportsPath = Path.Combine(Directory.GetCurrentDirectory(), "Areas", "LazyCat", "Views", "_ViewImports.cshtml");

        if (!File.Exists(viewImportsPath))
        {
            File.WriteAllText(viewImportsPath, viewImports);
        }

        Console.WriteLine("View imports created successfully.");

        var viewStart = BaseViews.ViewStart();
        var viewStartPath = Path.Combine(Directory.GetCurrentDirectory(), "Areas", "LazyCat", "Views", "_ViewStart.cshtml");

        if (!File.Exists(viewStartPath))
        {
            File.WriteAllText(viewStartPath, viewStart);
        }

        Console.WriteLine("View imports and view start created successfully.");
    }

    private static void CreateIndexViews()
    {
        foreach (var model in entityTypes)
        {
            var index = BaseViews.CreateIndexView(model);

            var modelsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Areas", "LazyCat", "Views", model.DisplayName());
            if (!Directory.Exists(modelsFolder))
            {
                Directory.CreateDirectory(modelsFolder);
            }

            var indexPath = Path.Combine(modelsFolder, "Index.cshtml");
            if (!File.Exists(indexPath))
            {
                File.WriteAllText(indexPath, index);
            }
        }
    }
}