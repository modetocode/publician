using UnityEngine;
using UnityEngine.UI;

public class SearchItemViewComponent : ListItemViewComponent {

    [SerializeField]
    private Text textLabel;

    private SearchItem searchItem;

    protected override void OnInitialized() {
        base.OnInitialized();
        this.searchItem = this.listItem as SearchItem;
        this.UpdateGui();
    }

    private void UpdateGui() {
        this.textLabel.text = this.searchItem.Title;
    }
}
