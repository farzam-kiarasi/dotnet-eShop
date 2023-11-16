﻿using eShop.HybridApp.Services;
using eShop.WebAppComponents.Services;
using Microsoft.Extensions.Logging;

namespace eShop.HybridApp;

public static class MauiProgram
{
    // NOTE: Must have a trailing slash to ensure the full BaseAddress URL is used to resolve relative URLs
    private static string MobileBffBaseUrl = "http://localhost:61632/catalog-api/";

    internal static string ProductImageHostUrl = "https://localhost:7298";

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddHttpClient<CatalogService>(o => o.BaseAddress = new(MobileBffBaseUrl));
        builder.Services.AddSingleton<IProductImageUrlProvider, ProductImageUrlProvider>();

        return builder.Build();
    }
}
