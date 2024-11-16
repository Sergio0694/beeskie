﻿using Bluesky.NET.Models;
using BlueskyClient.Extensions;
using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class FeedItemViewModel : ObservableObject
{
    private readonly IPostSubmissionService _postSubmissionService;

    public FeedItemViewModel(
        FeedItem feedItem,
        IPostSubmissionService postSubmissionService)
    {
        FeedItem = feedItem;
        _postSubmissionService = postSubmissionService;
        
        IsLiked = feedItem.Post.Viewer?.Like is not null;
        ReplyCount = feedItem.Post.GetReplyCount();
        RepostCount = feedItem.Post.GetRepostCount();
        LikeCount = feedItem.Post.GetLikeCount();
    }

    public FeedItem FeedItem { get; }

    [ObservableProperty]
    private bool _isLiked;

    [ObservableProperty]
    private string _replyCount = string.Empty;

    [ObservableProperty]
    private string _repostCount = string.Empty;

    [ObservableProperty]
    private string _likeCount = string.Empty;

    [RelayCommand]
    private async Task LikeAsync()
    {
        if (IsLiked)
        {
            return;
        }

        var result = await _postSubmissionService.LikeAsync(
            FeedItem.Post.Uri,
            FeedItem.Post.Cid);

        if (result)
        {
            LikeCount = (FeedItem.Post.LikeCount + 1).ToString();
        }

        IsLiked = result;
    }
}