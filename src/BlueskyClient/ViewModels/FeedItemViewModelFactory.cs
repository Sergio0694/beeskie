﻿using Bluesky.NET.Models;
using BlueskyClient.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlueskyClient.ViewModels;

public class FeedItemViewModelFactory : IFeedItemViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;

    public FeedItemViewModelFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public FeedItemViewModel CreateViewModel(FeedItem feedItem)
    {
        return new FeedItemViewModel(
            feedItem,
            _serviceProvider.GetRequiredService<IPostSubmissionService>(),
            _serviceProvider.GetRequiredService<IDialogService>());
    }
}
