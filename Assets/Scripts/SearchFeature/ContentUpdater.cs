using System;
using UnityEngine;

/// <summary>
/// Serializable class of content updater because interfaces could not be serialized.
/// </summary>
[Serializable]
public abstract class ContentUpdater : MonoBehaviour, IContentUpdater {
    public abstract void UpdateContent();
}
