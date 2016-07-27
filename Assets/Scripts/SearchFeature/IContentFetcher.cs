using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for fetching content.
/// </summary>
public interface IContentFetcher {

    void FetchContent();
    event Action ContentStartedFetching;
    event Action<IList<IContentItem>> ContentFetched;

}
