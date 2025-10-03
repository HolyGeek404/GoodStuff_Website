using System.Net.Http.Json;
using GoodStuff.Website.Application.Services.Interfaces;
using GoodStuff.Website.Domain.Entities.User;
using GoodStuff.Website.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GoodStuff.Website.Infrastructure.Api;

public class UserApiClient(
    HttpClient client,
    IConfiguration configuration,
    IRequestMessageBuilder requestMessageBuilder,
    ILogger<UserApiClient> logger)
{
    private readonly string _scope = configuration.GetSection("GoodStuffUserApi")["Scope"]!;

    public async Task<User> SignUpAsync(User model)
    {
        try
        {
            var request = await requestMessageBuilder.BuildPost(_scope, "user/signup", model);
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                logger.LogError("Sign up failed. Status: {StatusCode}, Error: {ErrorMessage}", response.StatusCode, errorMessage);

                throw new HttpRequestException($"Sign up failed. Status: {response.StatusCode}. Message: {errorMessage}");
            }

            var success = await response.Content.ReadFromJsonAsync<bool>();
            if (success) return model;
            
            logger.LogError("SignUp response returned false for user {Email}", model.Email.Value);
            throw new InvalidOperationException("SignUp succeeded but returned false.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Couldn't sign up user {Email}. Error: {Message}", model.Email.Value, e.Message);
            throw;
        }
    }

    public async Task<User> SignInAsync(Email email, Password password)
    {
        try
        {
            var request = await requestMessageBuilder.BuildGet(_scope, $"user/signin?email={email.Value}&password={password.Value}");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<User>();
                return content ?? throw new InvalidOperationException("Couldn't get user");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            logger.LogError("Couldn't sign in {Email}. Http Code: {ResponseStatusCode}. Error Message: {ErrorMessage}",email.Value, response.StatusCode, errorMessage);
            throw new HttpRequestException(errorMessage);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Couldn't sign in user {Email}. Error: {EMessage}", email.Value, e.Message);
            throw;
        }
    }
}