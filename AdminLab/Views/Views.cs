using Microsoft.EntityFrameworkCore.Metadata;
using System.Text;

namespace AdminLab.Views;

public static class BaseViews
{
    public static string Layout(List<string> models)
        => $$"""
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="utf-8" />
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>@ViewData["Title"] - AdminLab</title>
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
            <link href="~/lazy/style.css" rel="stylesheet" />
            </head>
        <body>
         <nav class="navbar navbar-expand-lg bg-body-tertiary">
          <div class="container-fluid">
            <a class="navbar-brand" href="#">LazyCatAdmin</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
              <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
              <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                  {{CreateNavlinks(models)}}
              </ul>
            </div>
          </div>
        </nav>

        <main role="main" class="container">
            @RenderBody()
        </main>

        <footer class="border-top footer text-muted">
            <div class="container">
                &copy; @DateTime.Now.Year.ToString() - AdminLab - <a asp-area=""Admin"" asp-controller=""Home"" asp-action=""Privacy"">Privacy</a>
            </div>
        </footer>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
        @RenderSection("Scripts", required: false)

        </body>
        </html>
        """;

    private static string CreateNavlinks(List<string> models)
    {
        var navlinks = new StringBuilder();
        foreach (var model in models)
        {
            navlinks.AppendLine("<li class=\"nav-item\">");
            navlinks.AppendLine($"    <a class=\"nav-link\" href=\"/lazycat/{model.ToLower()}/index\">{model}</a>");
            navlinks.AppendLine("</li>");
        }
        return navlinks.ToString();
    }

    public static string ViewImports(List<string> namespaces)
    {
        var viewImports = new StringBuilder();
        viewImports.AppendLine("@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers");
        foreach (var ns in namespaces)
        {
            viewImports.AppendLine($"@using {ns}");
        }
        return viewImports.ToString();
    }

    public static string ViewStart()
        => "@{ Layout = \"~/Areas/LazyCat/Views/Shared/_Layout.cshtml\"; }";


    public static string CreateIndexView(IEntityType model)
    {
        var modelClassName = model.DisplayName();
        var modelClassNamespace = model.ClrType.Namespace;
        string indexView = $$"""
        @using {{modelClassNamespace}}
        @model List<{{modelClassName}}>


        <div class="d-flex justify-content-between align-items-center">
            <h2>{{modelClassName}}</h2>
            <a asp-action="Create" class="btn btn-success">
                <i class="bi bi-plus"></i> Create New
            </a>
        </div>

        <table class="table table-striped">
            <thead>
                {{CreateTableHeader(model)}}
            </thead>
            <tbody>
            @foreach (var item in Model)
                {
                    <tr>
                        {{CreateTableRow(model)}}
                        <td>
                            <div class="d-flex justify-content-between">
                                <a asp-area="lazycat" asp-controller="{{modelClassName}}" asp-action="Edit" asp-route-id="@item.Id" class="btn btn-primary">
                                    <i class="bi bi-pencil"></i>
                                </a>
                                <a asp-area="lazycat" asp-controller="{{modelClassName}}" asp-action="Delete" asp-route-id="@item.Id" class="btn btn-danger ms-2">
                                    <i class="bi bi-trash"></i>
                                </a>
                             </div>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
        """;

        return indexView;
    }

    private static object CreateTableRow(IEntityType model)
    {
        var tableRow = new StringBuilder();
        foreach (var property in GetModelProperties(model))
        {
            tableRow.AppendLine($"<td>@item.{property}</td>");
        }
        return tableRow.ToString();
    }

    private static string CreateTableHeader(IEntityType model)
    {
        var tableHeader = new StringBuilder();
        tableHeader.AppendLine("<tr>");
        foreach (var property in GetModelProperties(model))
        {
            tableHeader.AppendLine($"<th>{property}</th>");
        }
        tableHeader.AppendLine("<th style=\"width: 160px;\">Action</th>");
        tableHeader.AppendLine("</tr>");
        return tableHeader.ToString();
    }

    private static List<string> GetModelProperties(IEntityType model)
    {
        var properties = new List<string>();
        foreach (var property in model.GetProperties())
        {
            properties.Add(property.Name);
        }
        return properties;
    }

}