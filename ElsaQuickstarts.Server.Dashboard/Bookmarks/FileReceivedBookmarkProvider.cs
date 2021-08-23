using Elsa.Services;
using ElsaQuickstarts.Server.Dashboard.Activity;
using System.Collections.Generic;

namespace ElsaQuickstarts.Server.Dashboard.Bookmarks
{
    public class FileReceivedBookmarkProvider : BookmarkProvider<FileReceivedBookmark, FileReceived>
    {
        public override IEnumerable<BookmarkResult> GetBookmarks(BookmarkProviderContext<FileReceived> context)
        {
            return new[] { Result(new FileReceivedBookmark()) };
        }
    }
}
