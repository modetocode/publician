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

    public event Action<IList<IContentItem>, ContentFetchedType> ContentFetched;
    public event Action ContentStartedFetching;

    private string queryString = String.Empty;
    private SearchResult previousSearchResult;

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
        }

        string apiUrl =
           Constants.API.UrlWithKey +
           Constants.API.LimitArgumentString + Constants.Search.NumberOfItemsPerPage +
           Constants.API.QueryArgumentString + queryString +
           Constants.API.OffsetArgumentString + offsetValue;
        if (this.ContentStartedFetching != null) {
            this.ContentStartedFetching();
        }

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.Send();
        if (request.isError) {
            this.searchInfoTextLabel.text = Constants.Strings.FetchingContentFailedMessage;
        }
        else {
            SearchResult result = JsonUtility.FromJson<SearchResult>(request.downloadHandler.text);
            if (this.ContentFetched != null) {
                ContentFetchedType fetchedType = isNewSearch ? ContentFetchedType.NewContent : ContentFetchedType.UpdatedContent;
                this.ContentFetched(result.SearchData, fetchedType);
            }

            this.previousSearchResult = result;
            if (isNewSearch) {
                this.searchInfoTextLabel.text = string.Format(Constants.Strings.SearchInfoStringTemplate, result.MetaData.Count, this.queryString);
            }
        }
    }

    public override void UpdateContent() {
        if (this.queryString.Equals(string.Empty)) {
            return;
        }

        this.StartCoroutine(FetchContentCoroutine(isNewSearch: false));
    }
}