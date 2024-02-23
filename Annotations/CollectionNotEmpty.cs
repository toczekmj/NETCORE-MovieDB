using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Annotations;

public class CollectionNotEmpty : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is ICollection
        {
            Count: > 0
        };
    }
}