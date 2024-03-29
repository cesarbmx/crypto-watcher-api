﻿using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.Shared.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeEnableWatcherConflict
    {
        public static EnableWatcherConflict GetFake()
        {
            return new EnableWatcherConflict(EnableWatcherConflictReason.WATCHER_ALREADY_ENABLED, WatcherMessage.WatcherAlreadyEnabled);
        }
    }
}
