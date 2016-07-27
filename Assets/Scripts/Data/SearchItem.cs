using System;
using UnityEngine;

/// <summary>
/// Contains the data of one search item result.
/// </summary>
[Serializable]
public class SearchItem : IContentItem {

    [SerializeField]
    private ItemTitle title;

    public string Title {
        get { return this.title.Finnish; }
    }
}