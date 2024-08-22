using CsvHelper.Configuration;
using CsvHelper;
using SQLite;
using System.Globalization;
using System.Linq;
using mauigridtest.Models;
using Microsoft.Extensions.Logging;

namespace mauigridtest.Data;

public class DataInitService : IAsyncDisposable
{
    private readonly ILogger<DataInitService> _logger;

    public DataInitService(ILogger<DataInitService> logger)
    {
        _logger = logger;
    }

    //private const string DbName = "MyDatabase.db3";
    private const string DbName = "german_grammar_game.db";
    //private static string DbPath => "german_grammar_game2.db";
    private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);

    private SQLiteAsyncConnection? _connection;

    public SQLiteAsyncConnection Database =>
        (_connection ??= new SQLiteAsyncConnection(DbPath,
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

    public async Task Init()
    {
        await CreateWiktionaryNouns();
        await CreateGameNouns();
    }

    private async Task CreateGameNouns()
    {
        var records = await GetRecords<CsvNoun>("substantiv.csv", ";");
        await Database.CreateTableAsync<GameNoun>();

        foreach (var csvNoun in records)
        {
            var gameNounQuery = Database.Table<GameNoun>();
            var wiktionaryNounQuery = Database.Table<WiktionaryNoun>();

            var existingGameNoun = await gameNounQuery.FirstOrDefaultAsync(n => n.Singular == csvNoun.Noun);
            if (existingGameNoun != null)
            {
                _logger.LogInformation("GameNouns already contained '{Noun}'", csvNoun.Noun);
                continue;
            }

            var wiktionaryNoun = await wiktionaryNounQuery.FirstOrDefaultAsync(n => n.Lemma == csvNoun.Noun);
            if (wiktionaryNoun == null)
            {
                _logger.LogWarning("WiktionaryNouns did not contain '{Noun}'", csvNoun.Noun);
                continue;
            }

            try
            {
                var gameNoun = new GameNoun(wiktionaryNoun.Lemma, wiktionaryNoun.Genus, wiktionaryNoun.NominativPlural);
                await Database.InsertAsync(gameNoun);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error importing record {record}", wiktionaryNoun.Lemma);
                _logger.LogError(e, e.Message);
                continue;
            }
        }
    }

    private async Task CreateWiktionaryNouns()
    {
        var records = await GetRecords<WiktionaryNoun>("raw-data.csv");

        var recordsCount = records.Count;
        _logger.LogInformation("Total number of records {TotalNumberOfRecords}", recordsCount);
        var batchSize = 1000;

        await Database.CreateTableAsync<WiktionaryNoun>();

        for (var i = 0; i < recordsCount; i = i + batchSize)
        {
            if (i > 0 && i % 1000 == 0)
            {
                _logger.LogInformation("Imported {i} records", i);
            }

            var endIndex = Math.Min(i + batchSize, recordsCount) - 1;


            var batchRecords = records.Take(new Range(i, endIndex)).ToHashSet<WiktionaryNoun>(new ItemComparer());
            var nounsInBatch = batchRecords.Select(r => r.Lemma).ToList();

            var table = Database.Table<WiktionaryNoun>();
            var existingNouns = (await table.Where(n => nounsInBatch.Contains(n.Lemma)).ToListAsync()).Select(n => n.Lemma);
            var recordsToInsert = batchRecords.Where(r => !existingNouns.Contains(r.Lemma)).ToList();

            if (!recordsToInsert.Any())
            {
                continue;
            }

            try
            {
                await Database.InsertAllAsync(recordsToInsert);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error importing records {records}", recordsToInsert);
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }

    private static async Task<IList<T>> GetRecords<T>(string path, string delimiter = ",") where T : class
    {
        var baseDirectory = AppContext.BaseDirectory;

        var stream = await FileSystem.Current.OpenAppPackageFileAsync(path);

        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = delimiter });

        return csv.GetRecords<T>().ToList();
    }

    public async ValueTask DisposeAsync() => await _connection?.CloseAsync();
}