using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;

public class SearchAPIComponent : MonoBehaviour {

    [SerializeField]
    private Button searchButton;
    [SerializeField]
    private InputField searchInputField;
    [SerializeField]
    private SearchItemsListViewComponent searchList;
    [SerializeField]
    private GameObject loadingBarObject;

    void Awake() {
        this.searchButton.onClick.AddListener(SearchApi);
        this.loadingBarObject.SetActive(false);
    }

    void Destroy() {
        this.searchButton.onClick.RemoveListener(SearchApi);
    }

    private void SearchApi() {
        string queryString = searchInputField.text;
        if (queryString.Equals(string.Empty)) {
            return;
        }

        this.StartCoroutine(SendRequest(queryString));
    }

    private IEnumerator SendRequest(string query) {
        string apiUrl =
            Constants.API.UrlWithKey +
            Constants.API.LimitArgumentString + Constants.Search.NumberOfItemsPerPage +
            Constants.API.QueryArgumentString + query;

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        loadingBarObject.SetActive(true);
        searchList.Clear();
        yield return request.Send();
        loadingBarObject.SetActive(false);
        if (request.isError) {
            //TODO handle the case where there is an error fetching data
            Debug.Log(request.error);
        }
        else {
            SearchResult result = JsonUtility.FromJson<SearchResult>(request.downloadHandler.text);
            //TODO handle the case when there are 0 results.
            searchList.Initialize(result.data);
        }
    }
}
