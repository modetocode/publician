using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// Scroll rect with functionallity to update content when an end of the content is reached.
/// </summary>
public class DynamicContentScrollRect : ScrollRect {

    [SerializeField]
    private ContentUpdater contentUpdater;

    public ContentUpdater ContentUpdater {
        get { return this.contentUpdater; }
        set { this.contentUpdater = value; }
    }

    protected override void Start() {
        base.Start();
        Assert.IsNotNull(contentUpdater);
        this.onValueChanged.AddListener(TrackScrollingProgress);
    }

    private void TrackScrollingProgress(Vector2 position) {
        if (IsEndOfContentReached(position)) {
            Debug.Log("Update content");
            this.contentUpdater.UpdateContent();
        }
    }

    private bool IsEndOfContentReached(Vector2 position) {
        return Mathf.Abs(position.y) < Constants.Math.FloatPositiveEpsilon;
    }

    void Destroy() {
        this.onValueChanged.RemoveListener(TrackScrollingProgress);
    }
}