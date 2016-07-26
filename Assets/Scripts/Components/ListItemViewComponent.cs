using UnityEngine;

public abstract class ListItemViewComponent : MonoBehaviour {

    protected IListItem listItem;

    public void Initialize(IListItem listItem) {
        this.listItem = listItem;
        this.OnInitialized();
    }

    protected virtual void OnInitialized() {
    }
}
