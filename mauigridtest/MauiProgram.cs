using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using mauigridtest.Data;
using mauigridtest.Repositories;
using Microsoft.Extensions.Logging;

namespace mauigridtest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<NounRepository>();
            builder.Services.AddSingleton<DatabaseContext>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<NounViewModel>();
            builder.Services.AddTransient<DataInitService>();

            return builder.Build();
        }
    }
}
