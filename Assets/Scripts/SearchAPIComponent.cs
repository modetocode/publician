using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;

/// <summary>
/// Main controller responsible for logic of searching the API.
/// </summary>
public class SearchAPIComponent : ContentUpdater, IContentFetcher {

    [SerializeField]
    private Button searchButton;
    [SerializeField]
    private InputField searchInputField;
    [SerializeField]
    private SearchItemsListViewComponent searchList;
    [SerializeField]
    private Text searchInfoTextLabel;

    public event Action<IList<IContentItem>> ContentFetched;
    public event Action<ContentFetchingType> ContentStartedFetching;

    private string queryString = String.Empty;
    private SearchResult previousSearchResult;
    private bool isFetchingInProgress;

    void Awake() {
        Assert.IsNotNull(this.searchButton);
        Assert.IsNotNull(this.searchInputField);
        Assert.IsNotNull(this.searchList);
        Assert.IsNotNull(this.searchInfoTextLabel);
        this.searchButton.onClick.AddListener(SearchApi);
        this.searchList.Initialize(this);
    }

    void Destroy() {
        this.searchButton.onClick.RemoveListener(SearchApi);
    }

    private void SearchApi() {
        string newQueryString = searchInputField.text;
        if (newQueryString.Equals(this.queryString)) {
            return;
        }

        this.queryString = newQueryString;
        if (this.queryString.Equals(string.Empty)) {
            return;
        }

        this.StartCoroutine(FetchContentCoroutine(isNewSearch: true));
    }

    private IEnumerator FetchContentCoroutine(bool isNewSearch) {
        int offsetValue = 0;
        if (!isNewSearch) {
            Assert.IsNotNull(this.previousSearchResult);
            offsetValue = previousSearchResult.MetaData.Offset + Constants.Search.NumberOfItemsPerPage;
            if (offsetValue >= previousSearchResult.MetaData.Count) {
                yield break;
            }
        }

        string apiUrl =
           Constants.API.UrlWithKey +
           Constants.API.LimitArgumentString + Constants.Search.NumberOfItemsPerPage +
           Constants.API.QueryArgumentString + queryString +
           Constants.API.OffsetArgumentString + offsetValue;
        if (this.ContentStartedFetching != null) {
            ContentFetchingType fetchingType = isNewSearch ? ContentFetchingType.NewContent : ContentFetchingType.UpdatedContent;
            this.ContentStartedFetching(fetchingType);
        }

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        this.isFetchingInProgress = true;
        yield return request.Send();
        this.isFetchingInProgress = false;
        string searchInfoMessage;
        IList<IContentItem> contentItems;
        if (request.isError) {
            searchInfoMessage = Constants.Strings.FetchingContentFailedMessage;
            contentItems = new IContentItem[0];

        }
        else {
            SearchResult result = JsonUtility.FromJson<SearchResult>(request.downloadHandler.text);
            searchInfoMessage = string.Format(Constants.Strings.SearchInfoStringTemplate, result.MetaData.Count, this.queryString);
            contentItems = result.SearchData;
            this.previousSearchResult = result;
        }

        this.searchInfoTextLabel.text = searchInfoMessage;
        if (this.ContentFetched != null) {
            this.ContentFetched(contentItems);
        }
    }

    public override void UpdateContent() {
        if (this.isFetchingInProgress) {
            return;
        }

        if (this.queryString.Equals(string.Empty)) {
            return;
        }

        this.StartCoroutine(FetchContentCoroutine(isNewSearch: false));
    }
}