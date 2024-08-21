using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using mauigridtest.Models;
using mauigridtest.Repositories;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace mauigridtest
{
    public partial class MainPage : ContentPage
    {
        private readonly NounRepository _nounRepository;
        private static NounViewModel _nounViewModel = new();

        private static TimeSpan DelayWhenCorrectAnswer = TimeSpan.FromMilliseconds(1500);
        private static TimeSpan DelayWhenWrongAnswer = TimeSpan.FromSeconds(3);

        private readonly Label _nounTextLabel;

        private enum Row { Image, Text, Audio, Gender }
        private enum Column { Der, Die, Das }

        private readonly IList<Button> _buttons;

        private readonly Color _defaultButtonColor = Colors.LightBlue;
        private readonly Image _image;
        private readonly MediaElement _mediaElement;

        private const bool LocalResources = true;

        public MainPage(NounRepository nounRepository, NounViewModel viewModel)
        {
            viewModel.CurrentNoun = new Noun() { Text = "Loading", ImageSource = "cat.png" };

            _nounViewModel = viewModel;
            _nounViewModel.SomeText = "loaddd";
            BindingContext = viewModel;

            _nounRepository = nounRepository;

            _image = new Image()
                .Row(Row.Image)
                .Column(Column.Der, Column.Das);
            //.Bind(Image.SourceProperty, static (NounViewModel vm) => ImageSource.FromFile(vm.CurrentNoun.ImageSource));

            _image.HorizontalOptions = LayoutOptions.Center;
            _image.VerticalOptions = LayoutOptions.Center;

            _nounTextLabel = new Label()
                .Row(Row.Text)
                .Column(Column.Der, Column.Das)
                //.Bind(Label.TextProperty, nameof(viewModel.SomeText));
                .Bind(Label.TextProperty, static (NounViewModel vm) => vm.CurrentNoun.Text,
                    handlers: new (Func<NounViewModel, object?>, string)[]
                    {
                        (vm => vm, nameof(NounViewModel.CurrentNoun)),
                        (vm => vm.CurrentNoun, nameof(NounViewModel.CurrentNoun.Text))
                    });
            //.Bind(Label.TextProperty, static (NounViewModel vm) => vm.CurrentNoun.Text);

            _nounTextLabel.FontSize(75);

            _nounTextLabel.HorizontalOptions = LayoutOptions.Center;

            _buttons = CreateGenderButtons();

            _mediaElement = new MediaElement()
                .Row(Row.Audio)
                .Column(Column.Die);

            _mediaElement.ShouldAutoPlay = false;
            _mediaElement.ShouldShowPlaybackControls = false;
            _mediaElement.IsVisible = false;

            Content = new Grid
            {
                RowDefinitions = Rows.Define(
                    (Row.Image, Stars(3)),
                    (Row.Text, Stars(2)),
                    (Row.Audio, Stars(0)),
                    (Row.Gender, Stars(1))),

                ColumnDefinitions = Columns.Define(
                    (Column.Der, Star),
                    (Column.Die, Star),
                    (Column.Das, Star)),

                Children =
                {
                    _image,
                    _mediaElement,
                    _nounTextLabel,
                    _buttons[0],
                    _buttons[1],
                    _buttons[2],
                }
            };

            Task.Run(async () => await MoveToNextNoun().ConfigureAwait(false));
        }

        private IList<Button> CreateGenderButtons()
        {
            return new List<Button>
            {
                CreateGenderButton("der", Column.Der),
                CreateGenderButton("die", Column.Die),
                CreateGenderButton("das", Column.Das)
            };
        }

        private Button CreateGenderButton(string gender, Column column)
        {
            var button = new Button()
                .Text(gender)
                .Row(Row.Gender)
                .FontSize(50)
                .BackgroundColor(_defaultButtonColor)
                .Column(column);

            button.Command = new Command(() => CheckAnswer(gender));

            return button;
        }

        private async Task MoveToNextNoun()
        {
            var next = await _nounRepository.GetNext();
            _nounViewModel.CurrentNoun = next;
            _nounViewModel.SomeText = next.Text;

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

        async void CheckAnswer(string chosenGender)
        {
            var correctGender = _nounViewModel.CurrentNoun.Gender;
            try
            {
                _mediaElement.Play();
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

            try
            {
                _mediaElement.Stop();
            }
            catch (Exception e)
            {
            }

            await MoveToNextNoun();
            ResetButtons();
        }

        private void HighlightGender(string gender, Color color)
        {
            var button = _buttons.Single(b => b.Text == gender);
            button.BackgroundColor(color);
        }

        private void HideOtherButtons()
        {
            var buttonToHide = _buttons.Where(b => b.BackgroundColor.Equals(_defaultButtonColor));
            foreach (var button in buttonToHide)
            {
                button.IsVisible = false;
            }
        }

        private void ResetButtons()
        {
            foreach (var button in _buttons)
            {
                ResetButton(button);
            }
        }

        private void ResetButton(Button button)
        {
            button.BackgroundColor(_defaultButtonColor);
            button.IsVisible(true);
        }
    }
}
