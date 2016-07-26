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

    void Awake() {
        this.searchButton.onClick.AddListener(SearchApi);
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
        yield return request.Send();
        if (request.isError) {
            Debug.Log(request.error);
        }
        else {
            SearchResult result = JsonUtility.FromJson<SearchResult>(request.downloadHandler.text);
            searchList.Initialize(result.data);
            //for (int i = 0; i < result.data.Length; i++) {
            //    Debug.Log(result.data[i].Title);
            //}
        }
    }
}
