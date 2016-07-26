using System.Collections.Generic;
using UnityEngine;

public class SearchItemsListViewComponent : MonoBehaviour {

    [SerializeField]
    private SearchItemViewComponent itemViewTemplate;
    [SerializeField]
    private Transform containerTransform;

    private IList<IListItem> listItems;
    private IList<ListItemViewComponent> listViewItems;

    public void Initialize(IList<IListItem> listItems) {
        this.listItems = listItems;
        this.UpdateViews();
    }

    private void UpdateViews() {
        this.ClearViews();
        this.listViewItems = new ListItemViewComponent[listItems.Count];
        for (int i = 0; i < listItems.Count; i++) {
            this.listViewItems[i] = GameObject.Instantiate(itemViewTemplate);
            this.listViewItems[i].transform.SetParent(containerTransform, false);
            this.listViewItems[i].Initialize(listItems[i]);
        }
    }

    private void ClearViews() {
        if (this.listViewItems == null) {
            return;
        }

        for (int i = 0; i < this.listViewItems.Count; i++) {
            Destroy(this.listViewItems[i].gameObject);
        }

        this.listViewItems = null;
    }
}
