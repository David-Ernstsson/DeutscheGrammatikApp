using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using mauigridtest.Models;
using System.Runtime.CompilerServices;
using mauigridtest.Repositories;

namespace mauigridtest;

public partial class NounViewModel : ObservableObject
{
    private readonly NounRepository _nounRepository;
    private readonly Color _defaultButtonColor = Colors.LightBlue;

    private static TimeSpan DelayWhenCorrectAnswer = TimeSpan.FromMilliseconds(1500);
    private static TimeSpan DelayWhenWrongAnswer = TimeSpan.FromSeconds(3);

    [ObservableProperty]
    private Noun currentNoun;

    public NounViewModel(NounRepository nounRepository)
    {
        _nounRepository = nounRepository;

        Task.Run(async () => await MoveToNextNoun().ConfigureAwait(false));
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