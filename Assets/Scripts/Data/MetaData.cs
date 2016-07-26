using System;
using UnityEngine;

[Serializable]
public class MetaData {

    public int offset;

    [SerializeField]
    private int count;

    public int Count {
        get { return this.count; }
    }
}