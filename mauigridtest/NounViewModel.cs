using CommunityToolkit.Mvvm.ComponentModel;
using mauigridtest.Models;
using mauigridtest.Repositories;
using mauigridtest.Data;
using Microsoft.Extensions.Logging;

namespace mauigridtest;

public partial class NounViewModel : ObservableObject
{
    private readonly NounRepository _nounRepository;
    private readonly ILogger<NounViewModel> _logger;
    private readonly DataInitService _dataInitService;
    private readonly Color _defaultButtonColor = Colors.LightBlue;

    private static TimeSpan DelayWhenCorrectAnswer = TimeSpan.FromMilliseconds(1500);
    private static TimeSpan DelayWhenWrongAnswer = TimeSpan.FromSeconds(3);

    [ObservableProperty]
    private GameNoun currentNoun;

    public NounViewModel(NounRepository nounRepository, ILogger<NounViewModel> logger, DataInitService dataInitService)
    {
        _nounRepository = nounRepository;
        _logger = logger;
        _dataInitService = dataInitService;

        Task.Run(async () => await Init().ConfigureAwait(false));
    }

    public IList<Button> Buttons { get; set; } = new List<Button>();

    public async void CheckAnswer(string chosenGender)
    {
        var correctGender = CurrentNoun.Gender;
        try
        {
            //_mediaElement.Play();
        }
        catch (Exception e)
        {
        }


        HighlightGender(correctGender, Colors.Green);
        if (chosenGender != correctGender)
        {
            HighlightGender(chosenGender, Colors.Red);
        }

        HideOtherButtons();
        //_nounTextLabel.Text = $"{correctGender} {_nounViewModel.CurrentNoun.Text}";

        if (chosenGender != correctGender)
        {
            await Task.Delay(DelayWhenWrongAnswer);
        }
        else
        {
            await Task.Delay(DelayWhenCorrectAnswer);
        }

        //try
        //{
        //    _mediaElement.Stop();
        //}
        //catch (Exception e)
        //{
        //}

        await MoveToNextNoun();
        ResetButtons();
    }

    private async Task Init()
    {
        var shouldInit = false;
        if (shouldInit)
        {
            await _dataInitService.Init();
        }

        var databaseName = "german_grammar_game.db";
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, databaseName);

        var shouldCopyDbToAppDirectory = !File.Exists(dbPath);
        if (shouldCopyDbToAppDirectory)
        {
            var databasePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, databaseName);

            var stream = await FileSystem.OpenAppPackageFileAsync(databaseName);

            using MemoryStream memoryStream = new MemoryStream();

            await stream.CopyToAsync(memoryStream);

            await File.WriteAllBytesAsync(databasePath, memoryStream.ToArray());
        }

        await MoveToNextNoun();
    }

    private async Task MoveToNextNoun()
    {
        var next = await _nounRepository.GetNext();
        CurrentNoun = next;

        //_nounViewModel.ButtonsVisible["der"] = !_nounViewModel.ButtonsVisible["der"];

        //_nounTextLabel.Text = _currentNoun.Text;

        var audioPrefix = "https://deutschegrammatikblob.blob.core.windows.net/audio/";
        var imagePrefix = "https://deutschegrammatikblob.blob.core.windows.net/images/";

        //_mediaElement.Source = MediaSource.FromUri($"{audioPrefix}{_currentNoun.AudioResource}");
        //_image.Source = ImageSource.FromUri(new Uri(imagePrefix + _currentNoun.ImageSource));
        return;

        //if (LocalResources)
        //{
        //    //_mediaElement.Source = MediaSource.FromResource("BigBuckBunny.mp4");
        //    _mediaElement.Source = MediaSource.FromResource(_currentNoun.AudioResource);
        //    _image.Source = ImageSource.FromFile(_currentNoun.ImageSource);
        //}
        //else
        //{
        //    var prefix = $"{FileSystem.Current.AppDataDirectory}\\";
        //    _mediaElement.Source = MediaSource.FromFile($"{prefix}{_currentNoun.AudioResource}");
        //    _image.Source = ImageSource.FromFile($"{prefix}{_currentNoun.ImageSource}");
        //}
    }

    private void HighlightGender(string gender, Color color)
    {
        var button = Buttons.Single(b => b.Text == gender);
        button.BackgroundColor = color;
    }

    private void HideOtherButtons()
    {
        var buttonToHide = Buttons.Where(b => b.BackgroundColor.Equals(_defaultButtonColor));
        foreach (var button in buttonToHide)
        {
            button.IsVisible = false;
        }
    }

    private void ResetButtons()
    {
        foreach (var button in Buttons)
        {
            ResetButton(button);
        }
    }

    private void ResetButton(Button button)
    {
        button.BackgroundColor = _defaultButtonColor;
        button.IsVisible = true;
    }

    private static void PlaySound()
    {
        try
        {
            //_mediaElement.Play();
        }
        catch (Exception e)
        {
        }
    }
}