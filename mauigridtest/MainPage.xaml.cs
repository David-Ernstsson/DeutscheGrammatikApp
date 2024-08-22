using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using mauigridtest.Models;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace mauigridtest
{
    public partial class MainPage : ContentPage
    {
        private static NounViewModel _nounViewModel;

        private enum Row { Image, Text, Audio, Gender }
        private enum Column { Der, Die, Das }

        private readonly MediaElement _mediaElement;

        public MainPage(NounViewModel viewModel)
        {
            viewModel.CurrentNoun = new GameNoun { Singular = "Loading" };

            _nounViewModel = viewModel;
            BindingContext = viewModel;

            var image = new Image()
                .Row(Row.Image)
                .Column(Column.Der, Column.Das)
                .Bind(Image.SourceProperty, static (NounViewModel vm) => vm.CurrentNoun.ImagePath,
                    handlers:
                    [
                        (vm => vm, nameof(NounViewModel.CurrentNoun)),
                        (vm => vm.CurrentNoun, nameof(NounViewModel.CurrentNoun.ImagePath))
                    ]);

            image.HorizontalOptions = LayoutOptions.Center;
            image.VerticalOptions = LayoutOptions.Center;

            var nounTextLabel = new Label()
                .Row(Row.Text)
                .Column(Column.Der, Column.Das)
                //.Bind(Label.TextProperty, nameof(viewModel.SomeText));
                .Bind(Label.TextProperty, static (NounViewModel vm) => vm.CurrentNoun.Singular,
                    handlers:
                    [
                        (vm => vm, nameof(NounViewModel.CurrentNoun)),
                        (vm => vm.CurrentNoun, nameof(NounViewModel.CurrentNoun.Singular))
                    ]);
            //.Bind(Label.TextProperty, static (NounViewModel vm) => vm.CurrentNoun.Text);

            nounTextLabel.FontSize(75);

            nounTextLabel.HorizontalOptions = LayoutOptions.Center;

            IList<Button> buttons = CreateGenderButtons();

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
                    image,
                    _mediaElement,
                    nounTextLabel,
                    buttons[0],
                    buttons[1],
                    buttons[2],
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
                .FontSize(50)
                .Column(column);

            _nounViewModel.Buttons.Add(button);

            button.Command = new Command(() => _nounViewModel.CheckAnswer(gender));

            return button;
        }

    }
}
