using System;
using UnityEngine;

/// <summary>
/// Contains the data of the content item title.
/// </summary>
[Serializable]
public class ItemTitle {

    [SerializeField]
    private string fi;

    public string Finnish {
        get { return this.fi; }
    }
}
