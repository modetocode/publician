using System;
using UnityEngine;

/// <summary>
/// Containts the mata data of the search result.
/// </summary>
[Serializable]
public class MetaData {

    [SerializeField]
    private int offset;

    [SerializeField]
    private int count;

    public int Offset {
        get { return this.offset; }
    }

    public int Count {
        get { return this.count; }
    }
}