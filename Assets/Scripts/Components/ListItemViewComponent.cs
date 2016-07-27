using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Responsible for showing the view of a content item.
/// </summary>
public abstract class ListItemViewComponent : MonoBehaviour {

    protected IContentItem contentItem;

    public void Initialize(IContentItem contentItem) {
        Assert.IsNotNull(contentItem);
        this.contentItem = contentItem;
        this.OnInitialized();
    }

    protected virtual void OnInitialized() {
    }
}
