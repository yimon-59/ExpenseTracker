using System.Text.Json;
using StackExchange.Redis;

namespace ExpenseTracker.Api.Services;

public interface IRedisCacheService
{
    Task<T?> GetAsync<T>(string key);

    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration);

    Task RemoveAsync(string key);
}

public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);

        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration)
    {
        var json = JsonSerializer.Serialize(value);

        await _database.StringSetAsync(
            key,
            json,
            expiration);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}