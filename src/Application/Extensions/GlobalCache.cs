using System.Collections.Concurrent;

namespace Application.Extensions
{
    public static class GlobalCache
    {
        public static ConcurrentDictionary<int, string> TokenStorages = new();
    }
}
