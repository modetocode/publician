using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


/// <summary>
/// Responsible for showing the view of a searh content item.
/// </summary>
public class SearchItemViewComponent : ListItemViewComponent {

    [SerializeField]
    private Text textLabel;

    private SearchItem searchItem;

    void Awake() {
        Assert.IsNotNull(this.textLabel);
    }

    protected override void OnInitialized() {
        base.OnInitialized();
        this.searchItem = this.contentItem as SearchItem;
        Assert.IsNotNull(this.searchItem);
        this.UpdateGui();
    }

    private void UpdateGui() {
        this.textLabel.text = this.searchItem.Title;
    }
}
