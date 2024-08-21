using SQLite;

namespace mauigridtest.Data;

public class DatabaseContext : IAsyncDisposable
{
    private const string DbName = "MyDatabase.db3";
    private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);

    private SQLiteAsyncConnection _connection;
    private SQLiteAsyncConnection Database =>
        (_connection ??= new SQLiteAsyncConnection(DbPath,
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

    public async ValueTask DisposeAsync() => await _connection?.CloseAsync();

    public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
    {
        var table = await GetTableAsync<TTable>();
        return await table.ToListAsync();
    }

    public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
    {
        //await CreateTableIfNotExists<TTable>();
        //return await Database.InsertAsync(item) > 0;
        return await Execute<TTable, bool>(async () => await Database.InsertAsync(item) > 0);
    }

    private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        return Database.Table<TTable>();
    }

    private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
    {
        await Database.CreateTableAsync<TTable>();
    }

    private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>> action) where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        return await action();
    }
}