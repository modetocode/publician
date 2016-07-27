using System;
using UnityEngine;

/// <summary>
/// Contains the result data of a search in the API.
/// </summary>
[Serializable]
public class SearchResult {

    [SerializeField]
    private MetaData meta;
    [SerializeField]
    private SearchItem[] data;

    public MetaData MetaData {
        get { return this.meta; }
    }

    public SearchItem[] SearchData {
        get { return this.data; }
    }
}
