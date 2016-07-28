using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for fetching content.
/// </summary>
public interface IContentFetcher {

    event Action<ContentFetchingType> ContentStartedFetching;
    event Action<IList<IContentItem>> ContentFetched;

}
