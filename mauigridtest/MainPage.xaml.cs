using CommunityToolkit.Maui.Markup;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace mauigridtest
{
    public partial class MainPage : ContentPage
    {
        private int _currentNounIndex = 0;

        private static TimeSpan DelayWhenCorrectAnswer = TimeSpan.FromMilliseconds(500);
        private static TimeSpan DelayWhenWrongAnswer = TimeSpan.FromSeconds(3);

        private readonly Label _nounTextLabel;

        private enum Row { Image, Text, Gender }
        private enum Column { Der, Die, Das }

        private readonly IList<Button> _buttons;

        private readonly Color _defaultButtonColor = Colors.Blue;
        private readonly Image _image;

        public MainPage()
        {
            _image = new Image()
                .Source(ImageSource.FromFile(Constants.Nouns[_currentNounIndex].ImageSource))
                .Row(Row.Image)
                .Column(Column.Der, Column.Das);

            _image.HorizontalOptions = LayoutOptions.Center;
            _image.VerticalOptions = LayoutOptions.Center;

            _nounTextLabel = new Label()
                .Text(Constants.Nouns[_currentNounIndex].Text)
                .Row(Row.Text)
                .Column(Column.Der, Column.Das);

            _nounTextLabel.FontSize(100);

            _nounTextLabel.HorizontalOptions = LayoutOptions.Center;

            _buttons = CreateGenderButtons();

            Content = new Grid
            {
                RowDefinitions = Rows.Define(
                    (Row.Image, Stars(3)),
                    (Row.Text, Stars(2)),
                    (Row.Gender, Stars(1))),

                ColumnDefinitions = Columns.Define(
                    (Column.Der, Star),
                    (Column.Die, Star),
                    (Column.Das, Star)),

                Children =
                {
                    _image,

                    _nounTextLabel,
                    _buttons[0],
                    _buttons[1],
                    _buttons[2],
                }
            };
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
            Random random = new();
            var next = random.Next(Constants.Nouns.Count);

            _currentNounIndex = next;

            _nounTextLabel.Text = Constants.Nouns[_currentNounIndex].Text;
            _image.Source = Constants.Nouns[_currentNounIndex].ImageSource;
        }

        async void CheckAnswer(string chosenGender)
        {
            var correctGender = Constants.Nouns[_currentNounIndex].Gender;

            HighlightGender(correctGender, Colors.Green);
            if (chosenGender != correctGender)
            {
                HighlightGender(chosenGender, Colors.Red);
            }

            HideOtherButtons();
            _nounTextLabel.Text = $"{correctGender} {Constants.Nouns[_currentNounIndex].Text}";

            if (chosenGender != correctGender)
            {
                await Task.Delay(DelayWhenWrongAnswer);
            }
            else
            {
                await Task.Delay(DelayWhenCorrectAnswer);
            }
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
