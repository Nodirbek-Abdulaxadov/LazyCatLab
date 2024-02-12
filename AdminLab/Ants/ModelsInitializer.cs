using Microsoft.EntityFrameworkCore;

namespace AdminLab.Ants;

public class ModelsInitializer<T> where T : DbContext
{
    private T? DatabaseContext { get; set; }

    public ModelsInitializer()
    {
        DatabaseContext = DatabaseContextInitializer<T>.DatabaseContext;
    }

    private void GetModelNames()
    {
        var modelNames = DatabaseContext?.Model.GetEntityTypes().Select(e => e.Name);
        foreach (var modelName in modelNames)
        {
            Console.WriteLine(modelName);
        }
    }
}