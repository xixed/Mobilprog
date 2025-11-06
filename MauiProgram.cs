using Microsoft.Extensions.Logging;
using Mobilprog.View;
using Mobilprog.ViewModel;

namespace Mobilprog
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            

            builder.Services.AddSingleton<Database>(s => new Database(Path.Combine(FileSystem.AppDataDirectory, "clones.db3")));
            builder.Services.AddTransient<CloneViewModel>();
            builder.Services.AddTransient<ClonePage>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
