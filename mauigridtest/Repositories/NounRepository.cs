using mauigridtest.Data;
using mauigridtest.Models;

namespace mauigridtest.Repositories;

public class NounRepository
{
    private readonly DatabaseContext _databaseContext;

    public NounRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<GameNoun>> GetAll()
    {
        var nouns = await _databaseContext.GetAllAsync<GameNoun>();
        return nouns.ToList();
    }

    public async Task<GameNoun> GetNext()
    {
        var count = (await GetAll()).Count;

        Random random = new();
        var next = random.Next(count - 1);

        return (await GetAll())[next];
    }
}