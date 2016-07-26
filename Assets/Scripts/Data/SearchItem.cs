using System;
using UnityEngine;

[Serializable]
public class SearchItem : IListItem {

    [SerializeField]
    private ItemTitle title;

    public string Title { get { return this.title.fi; } }
}