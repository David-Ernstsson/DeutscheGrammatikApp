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

    public async Task<List<Noun>> GetAll()
    {
        var nouns = await _databaseContext.GetAllAsync<Noun>();
        if (nouns.Count() == 0)
        {

            foreach (var noun in Constants.Nouns)
            {
                await _databaseContext.AddItemAsync<Noun>(noun);
            }

            return await GetAll();
        }


        return nouns.ToList();
    }

    public async Task<Noun> GetNext()
    {
        Random random = new();
        var next = random.Next(Constants.Nouns.Count);

        return (await GetAll())[next];
    }
}