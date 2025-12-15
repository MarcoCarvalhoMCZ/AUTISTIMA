using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AUTistima.Data;
using AUTistima.Models.Enums;

namespace AUTistima.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrador")] // Garante que apenas admins acessem
public class DocsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

    public DocsController(
        ApplicationDbContext context,
        IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        _context = context;
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    public IActionResult Index()
    {
        var model = new DocumentationViewModel
        {
            GeneratedAt = DateTime.Now,
            DatabaseTables = GetDatabaseTables(),
            Controllers = GetControllersInfo(),
            SystemInfo = GetSystemInfo()
        };

        return View(model);
    }

    private Dictionary<string, string> GetSystemInfo()
    {
        return new Dictionary<string, string>
        {
            { "Nome do Sistema", "AUTistima" },
            { "Versão do Framework", System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription },
            { "Sistema Operacional", System.Runtime.InteropServices.RuntimeInformation.OSDescription },
            { "Ambiente", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production" },
            { "Banco de Dados", "SQL Server" }
        };
    }

    private List<TableInfo> GetDatabaseTables()
    {
        var tables = new List<TableInfo>();
        var entityTypes = _context.Model.GetEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var table = new TableInfo
            {
                Name = entityType.GetTableName() ?? entityType.Name,
                Columns = entityType.GetProperties().Select(p => new ColumnInfo
                {
                    Name = p.Name,
                    Type = p.ClrType.Name,
                    IsPrimaryKey = p.IsPrimaryKey(),
                    IsNullable = p.IsNullable
                }).ToList()
            };
            tables.Add(table);
        }

        return tables.OrderBy(t => t.Name).ToList();
    }

    private List<ControllerInfo> GetControllersInfo()
    {
        var controllers = new List<ControllerInfo>();
        
        var actions = _actionDescriptorCollectionProvider.ActionDescriptors.Items
            .OfType<ControllerActionDescriptor>()
            .GroupBy(a => a.ControllerName);

        foreach (var group in actions)
        {
            var controllerType = group.First().ControllerTypeInfo;
            
            // Pula controllers do framework ou gerados
            if (controllerType.Namespace?.StartsWith("Microsoft") == true) continue;

            var controllerInfo = new ControllerInfo
            {
                Name = group.Key,
                Namespace = controllerType.Namespace ?? "",
                Actions = group.Select(a => new ActionInfo
                {
                    Name = a.ActionName,
                    Method = a.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.FirstOrDefault() ?? "GET",
                    Route = a.AttributeRouteInfo?.Template ?? $"{group.Key}/{a.ActionName}"
                }).DistinctBy(a => a.Name + a.Method).ToList()
            };
            
            controllers.Add(controllerInfo);
        }

        return controllers.OrderBy(c => c.Name).ToList();
    }
}

// ViewModels auxiliares
public class DocumentationViewModel
{
    public DateTime GeneratedAt { get; set; }
    public Dictionary<string, string> SystemInfo { get; set; } = new();
    public List<TableInfo> DatabaseTables { get; set; } = new();
    public List<ControllerInfo> Controllers { get; set; } = new();
}

public class TableInfo
{
    public string Name { get; set; } = "";
    public List<ColumnInfo> Columns { get; set; } = new();
}

public class ColumnInfo
{
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public bool IsPrimaryKey { get; set; }
    public bool IsNullable { get; set; }
}

public class ControllerInfo
{
    public string Name { get; set; } = "";
    public string Namespace { get; set; } = "";
    public List<ActionInfo> Actions { get; set; } = new();
}

public class ActionInfo
{
    public string Name { get; set; } = "";
    public string Method { get; set; } = "";
    public string Route { get; set; } = "";
}
