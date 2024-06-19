namespace MovieApi.Filters.Helpers;

public interface IApiKeyValidator
{
    public bool IsValid(string apiKey);
}

public class ApiKeyValidator : IApiKeyValidator
{
    private readonly IConfiguration _configuration;

    public ApiKeyValidator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool IsValid(string apiKey)
    {
        var localApiKey = _configuration.GetValue<string>(ApiKeyConstants.ApiKeySectionName);
        return apiKey.Equals(localApiKey);
    }
}