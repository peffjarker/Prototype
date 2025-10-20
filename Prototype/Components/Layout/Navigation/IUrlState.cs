// Services/IUrlState.cs
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IUrlState
    {
        /// <summary>Case-insensitive snapshot of the current query.</summary>
        IReadOnlyDictionary<string, string?> Current { get; }

        /// <summary>Raised after the URL changes (user navigation or our own Set/Navigate).</summary>
        event Action? Changed;

        /// <summary>Build a href by merging the current query with optional overrides.</summary>
        string BuildHref(string baseHref, IReadOnlyDictionary<string, string?>? overrides = null);

        /// <summary>Merge overrides into the current query (null removes a key) and write to URL.</summary>
        void Set(params (string key, string? value)[] overrides);

        /// <summary>Navigate to a path while preserving current query (with optional overrides).</summary>
        void Navigate(string path, IReadOnlyDictionary<string, string?>? overrides = null);
    }
}
