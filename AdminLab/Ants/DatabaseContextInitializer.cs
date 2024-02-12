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
        //CreateModelClasses();
        CreateControllers();
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

    private static void CreateModelClasses()
    {
        foreach (var model in entityTypes)
        {
            //if model is not generic
            if (model.ClrType.IsGenericType) continue;
            
            CreateModelClass(model);
        }
    }

    private static void CreateModelClass(IEntityType entityType)
    {

        var modelClass = new StringBuilder();
        modelClass.AppendLine("using System;");
        modelClass.AppendLine("using System.Collections.Generic;");
        modelClass.AppendLine("using System.ComponentModel.DataAnnotations;");
        modelClass.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
        modelClass.AppendLine("using AdminLab.Models.Enums;");
        modelClass.AppendLine();
        modelClass.AppendLine("namespace AdminLab.Models;");
        modelClass.AppendLine($"public class {entityType.DisplayName()}");
        modelClass.AppendLine("{");
        foreach (var property in entityType.GetProperties())
        {
            if (property.ClrType.IsEnum)
            {
                CreateEnumClass(property);
                if (property.IsNullable)
                {
                    modelClass.AppendLine($"    public {property.ClrType.Name}? {property.Name} {{ get; set; }}");
                }
                else
                {
                    modelClass.AppendLine($"    public {property.ClrType.Name} {property.Name} {{ get; set; }}");
                }
            }
            else
            {
                if (property.IsNullable)
                {
                    if (property.ClrType.Name.Contains("Nullable"))
                    {
                        modelClass.AppendLine($"    public DateTimeOffset? {property.Name} {{ get; set; }}");
                    }
                    else
                    {
                        modelClass.AppendLine($"    public {property.ClrType.Name}? {property.Name} {{ get; set; }}");
                    }
                }
                else
                {
                    if (property.ClrType.Name.Contains("Nullable"))
                    {
                        modelClass.AppendLine($"    public DateTimeOffset {property.Name} {{ get; set; }}");
                    }
                    else
                    {
                        modelClass.AppendLine($"    public {property.ClrType.Name} {property.Name} {{ get; set; }}");
                    }
                }
            }
        }
        modelClass.AppendLine("}");

        var modelsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Areas", "LazyCat", "Models");

        var modelClassPath = Path.Combine(modelsFolder, $"{entityType.DisplayName()}.cs");
        if (!File.Exists(modelClassPath))
        {
            File.WriteAllText(modelClassPath, modelClass.ToString());
        }

        Console.WriteLine($"Model class {entityType.DisplayName()} created successfully.");
    }

    private static void CreateEnumClass(IProperty property)
    {
        var enumClass = new StringBuilder();
        enumClass.AppendLine("using System;");
        enumClass.AppendLine("using System.Collections.Generic;");
        enumClass.AppendLine("using System.ComponentModel.DataAnnotations;");
        enumClass.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
        enumClass.AppendLine();
        enumClass.AppendLine("namespace AdminLab.Models.Enums;");
        enumClass.AppendLine($"public enum {property.Name}");
        enumClass.AppendLine("{");
        foreach (var value in Enum.GetValues(property.ClrType))
        {
            enumClass.AppendLine($"    {value},");
        }
        enumClass.AppendLine("}");

        var modelsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Areas", "LazyCat", "Models", "Enums");

        if (!Directory.Exists(modelsFolder))
        {
            Directory.CreateDirectory(modelsFolder);
        }

        var enumClassPath = Path.Combine(modelsFolder, $"{property.Name}.cs");
        if (!File.Exists(enumClassPath))
        {
            File.WriteAllText(enumClassPath, enumClass.ToString());
        }

        Console.WriteLine($"Enum class {property.Name} created successfully.");
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
        controllerClass.AppendLine($"using AdminLab.Models;");
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
}