using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Annotations;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class CollectionHasMinimumElements : ValidationAttribute
{
    private readonly int _max;
    private readonly int _min;
    
    public CollectionHasMinimumElements(int min = 0, int max = int.MaxValue)
    {
        _min = min;
        _max = max;
    }
    
    public override bool IsValid(object? value)
    {
        return value is ICollection
        {
            Count: > 0
        };
    }
}