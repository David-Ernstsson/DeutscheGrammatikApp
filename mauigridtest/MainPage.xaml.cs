using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace mauigridtest
{
    public partial class MainPage : ContentPage
    {
        private readonly NounRepository _nounRepository;

        private Noun _currentNoun;

        private static TimeSpan DelayWhenCorrectAnswer = TimeSpan.FromMilliseconds(1500);
        private static TimeSpan DelayWhenWrongAnswer = TimeSpan.FromSeconds(3);

        private readonly Label _nounTextLabel;

        private enum Row { Image, Text, Audio, FileAudio, DownloadMedia,Gender }
        private enum Column { Der, Die, Das }

        private readonly IList<Button> _buttons;

        private readonly Color _defaultButtonColor = Colors.Blue;
        private readonly Image _image;
        private readonly MediaElement _mediaElement;
        private readonly MediaElement _fromFilemediaElement;

        public MainPage(NounRepository nounRepository)
        {
            _nounRepository = nounRepository;
            _currentNoun = _nounRepository.GetNext();

            _image = new Image()
                .Source(ImageSource.FromFile(_currentNoun.ImageSource))
                .Row(Row.Image)
                .Column(Column.Der, Column.Das);

            _image.HorizontalOptions = LayoutOptions.Center;
            _image.VerticalOptions = LayoutOptions.Center;

            _nounTextLabel = new Label()
                .Text(_currentNoun.Text)
                .Row(Row.Text)
                .Column(Column.Der, Column.Das);

            _nounTextLabel.FontSize(100);

            _nounTextLabel.HorizontalOptions = LayoutOptions.Center;

            _buttons = CreateGenderButtons();

            _mediaElement = new MediaElement()
                .Row(Row.Audio)
                .Column(Column.Die);

            _mediaElement.ShouldAutoPlay = false;
            _mediaElement.ShouldShowPlaybackControls = false;

            _fromFilemediaElement = new MediaElement()
                .Row(Row.FileAudio)
                .Column(Column.Die);

            _fromFilemediaElement.ShouldAutoPlay = false;
            _fromFilemediaElement.ShouldShowPlaybackControls = true;

            var downloadMediaButton = new Button()
                .Text("Download")
                .Row(Row.DownloadMedia)
                .BackgroundColor(_defaultButtonColor)
                .FontSize(50)
                .Column(Column.Die);

            downloadMediaButton.Command = new Command(DownloadMedia);

            Content = new Grid
            {
                RowDefinitions = Rows.Define(
                    (Row.Image, Stars(3)),
                    (Row.Text, Stars(2)),
                    (Row.Audio, Stars(1)),
                    (Row.FileAudio, Stars(1)),
                    (Row.DownloadMedia, Stars(1)),
                    (Row.Gender, Stars(1))),

                ColumnDefinitions = Columns.Define(
                    (Column.Der, Star),
                    (Column.Die, Star),
                    (Column.Das, Star)),

                Children =
                {
                    _image,
                    _mediaElement,
                    _fromFilemediaElement,
                    _nounTextLabel,
                    downloadMediaButton,
                    _buttons[0],
                    _buttons[1],
                    _buttons[2],
                }
            };

            MoveToNextNoun();
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
                .BackgroundColor(_defaultButtonColor)
                .FontSize(50)
                .Column(column);

            button.Command = new Command(() => CheckAnswer(gender));

            return button;
        }

        private void MoveToNextNoun()
        {
            var mainDir = FileSystem.Current.AppDataDirectory;


            _currentNoun = _nounRepository.GetNext();

            _nounTextLabel.Text = _currentNoun.Text;
            _image.Source = ImageSource.FromFile($"{mainDir}\\{_currentNoun.ImageSource}");
            var mediaElementSource = MediaSource.FromResource(_currentNoun.AudiResource);

            _mediaElement.Source = mediaElementSource;

            _fromFilemediaElement.Source = MediaSource.FromFile($"{mainDir}\\BigBuckBunny.mp4");
        }

        async void DownloadMedia()
        {
            // Create an output filename
            using (var http = new HttpClient())
            {
                var stream = await http.GetStreamAsync("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");
                var path = Path.Combine(FileSystem.Current.AppDataDirectory, "BigBuckBunny.mp4");
                await using Stream streamToWriteTo = File.Open(path, FileMode.Create);
                await stream.CopyToAsync(streamToWriteTo);
            }
        }

        async void CheckAnswer(string chosenGender)
        {
            var correctGender = _currentNoun.Gender;
            _mediaElement.Play();

            HighlightGender(correctGender, Colors.Green);
            if (chosenGender != correctGender)
            {
                HighlightGender(chosenGender, Colors.Red);
            }

            HideOtherButtons();
            _nounTextLabel.Text = $"{correctGender} {_currentNoun.Text}";

            if (chosenGender != correctGender)
            {
                await Task.Delay(DelayWhenWrongAnswer);
            }
            else
            {
                await Task.Delay(DelayWhenCorrectAnswer);
            }

            _mediaElement.Stop();

            MoveToNextNoun();
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
