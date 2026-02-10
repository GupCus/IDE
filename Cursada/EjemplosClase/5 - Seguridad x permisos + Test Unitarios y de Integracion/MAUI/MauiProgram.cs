using Microsoft.Extensions.Logging;
using API.Clients;
using API.Auth.MAUI;

namespace MAUI;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		var app = builder.Build();

		// Register AuthService singleton
		var authService = new MAUIAuthService();
		AuthServiceProvider.Register(authService);

		return app;
	}
}
