using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MovieApi.Model;

public class BanBadWords : ValidationAttribute
{
    private readonly bool _asterisk;

    public BanBadWords(bool asterisk = false)
    {
        _asterisk = asterisk;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string)
            return ValidationResult.Success;
        
        var commonText = value as string;

        foreach (var badWord in BadWordList.BadWords)
        {
            if (!commonText.Contains(badWord, StringComparison.OrdinalIgnoreCase)) continue;
            
            if (_asterisk)
            {
                var regex = new Regex($@"\b{Regex.Escape(badWord)}\b", RegexOptions.IgnoreCase);
                commonText = regex.Replace(commonText, Match => new string('*', Match.Length));
                validationContext.ObjectType.GetProperty(validationContext.MemberName)?
                    .SetValue(validationContext.ObjectInstance, commonText);
            }
            else
            {
                return new ValidationResult("The comment contains inappropriate language!");
            }
        }       
        return ValidationResult.Success;
    }
}


// TODO: download lists of bad words from external source
file static class BadWordList
{
    public static readonly List<string> BadWords = new()
    {
        "dupa",
        "chuj"
    };
}