﻿using BlueskyClient.Constants;
using BlueskyClient.ViewModels;
using JeniusApps.Common.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

#nullable enable

namespace BlueskyClient.Views;

public sealed partial class ShellPage : Page
{
    public ShellPage()
    {
        this.InitializeComponent();
        ViewModel = App.Services.GetRequiredService<ShellPageViewModel>();
    }

    public ShellPageViewModel ViewModel { get; }

    public string DisplayTitle => $"Beeskie {SystemInformation.Instance.ApplicationVersion.ToFormattedString().TrimEnd('0').TrimEnd('.')}";

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        App.Services.GetRequiredKeyedService<INavigator>(NavigationConstants.ContentNavigatorKey).SetFrame(ContentFrame);
        _ = ViewModel.InitializeAsync(e.Parameter as string).ConfigureAwait(false);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        ViewModel.Unitialize();
    }

    private void OnImagePreviewBackgroundKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key is VirtualKey.Escape)
        {
            ViewModel.CloseImageViewerCommand.Execute(null);
            e.Handled = true;
        }
    }

    private void OnImagePreviewBackgroundClicked(object sender, TappedRoutedEventArgs e)
    {
        if (e.OriginalSource is Grid g && g == SmokeGrid)
        {
            ViewModel.CloseImageViewerCommand.Execute(null);
            e.Handled = true;
        }
    }
}
