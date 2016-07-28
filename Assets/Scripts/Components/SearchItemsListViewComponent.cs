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
    [SerializeField]
    private GameObject fetchingContentObject;

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

    void Awake() {
        Assert.IsNotNull(this.itemViewTemplate);
        Assert.IsNotNull(this.containerTransform);
        Assert.IsNotNull(this.fetchingContentObject);
        this.fetchingContentObject.SetActive(false);
    }

    void Destroy() {
        if (this.contentFetcher != null) {
            this.UnsubsribeFromFetchingStarted();
        }
    }

    private void OnContentStartedFetching(ContentFetchingType contentFetchedType) {
        if (contentFetchedType == ContentFetchingType.NewContent) {
            this.RemoveAllContentItems();
        }

        this.fetchingContentObject.transform.SetAsLastSibling();
        this.fetchingContentObject.SetActive(true);
        this.contentFetcher.ContentFetched += DisplayContent;
    }

    private void DisplayContent(IList<IContentItem> contentItems) {
        this.contentFetcher.ContentFetched -= DisplayContent;
        this.fetchingContentObject.SetActive(false);
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

    private void RemoveAllContentItems() {
        if (this.listItems == null) {
            return;
        }

        for (int i = 0; i < this.listViewItems.Count; i++) {
            Destroy(this.listViewItems[i].gameObject);
        }

        this.listViewItems.Clear();
        this.listItems.Clear();
    }

    private void UnsubsribeFromFetchingStarted() {
        this.contentFetcher.ContentStartedFetching -= OnContentStartedFetching;
    }
}
