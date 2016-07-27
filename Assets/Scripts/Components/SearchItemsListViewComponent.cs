using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Responsible for displaying a list of search content items.
/// </summary>
public class SearchItemsListViewComponent : MonoBehaviour {

    [SerializeField]
    private SearchItemViewComponent itemViewTemplate;
    [SerializeField]
    private Transform containerTransform;

    private IList<IContentItem> listItems;
    private IList<ListItemViewComponent> listViewItems;
    private IContentFetcher contentFetcher;

    public void Initialize(IContentFetcher contentFetcher) {
        Assert.IsNotNull(contentFetcher);
        if (this.contentFetcher != null) {
            this.UnsubsribeFromFetchingStarted();
        }

        this.contentFetcher = contentFetcher;
        this.contentFetcher.ContentStartedFetching += OnContentStartedFetching;
    }

    void Destroy() {
        if (this.contentFetcher != null) {
            this.UnsubsribeFromFetchingStarted();
        }
    }

    private void OnContentStartedFetching() {
        //TODO show progress bar
        this.contentFetcher.ContentFetched += DisplayContent;
    }

    private void DisplayContent(IList<IContentItem> contentItems) {
        this.contentFetcher.ContentFetched -= DisplayContent;
        for (int i = 0; i < contentItems.Count; i++) {
            this.AddContentItem(contentItems[i]);
        }
    }

    private void AddContentItem(IContentItem contentItem) {
        //TODO add object pool
        ListItemViewComponent newViewObject = Instantiate(itemViewTemplate);
        newViewObject.transform.SetParent(containerTransform, false);
        newViewObject.Initialize(contentItem);

        if (this.listItems == null) {
            this.listItems = new List<IContentItem>();

        }

        this.listItems.Add(contentItem);

        if (this.listViewItems == null) {
            this.listViewItems = new List<ListItemViewComponent>();
        }

        this.listViewItems.Add(newViewObject);
    }

    private void UnsubsribeFromFetchingStarted() {
        this.contentFetcher.ContentStartedFetching -= OnContentStartedFetching;
    }
}
