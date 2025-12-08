using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AUTistima.Extensions;

/// <summary>
/// Extensões para trabalhar com enums
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Obtém o nome de exibição do enum usando o atributo [Display(Name = "...")]
    /// Se não houver atributo, retorna o nome do enum com espaços entre as palavras
    /// </summary>
    public static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        
        if (field == null) return value.ToString();
        
        var attribute = field.GetCustomAttribute<DisplayAttribute>();
        
        if (attribute != null)
        {
            return attribute.Name ?? value.ToString();
        }
        
        // Fallback: adiciona espaços antes de letras maiúsculas
        return System.Text.RegularExpressions.Regex.Replace(
            value.ToString(), 
            "([a-z])([A-Z])", 
            "$1 $2"
        );
    }
}
