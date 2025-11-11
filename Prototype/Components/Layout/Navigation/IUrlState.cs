// Services/IUrlState.cs
namespace Services
{
    /// <summary>
    /// Central service for URL state management with debouncing and query parameter handling.
    /// Single source of truth for all URL operations across the application.
    /// </summary>
    public interface IUrlState
    {
        /// <summary>Current query parameters as a read-only snapshot.</summary>
        IReadOnlyDictionary<string, string?> Current { get; }

        /// <summary>Fired when the URL changes (after navigation completes).</summary>
        event Action? Changed;

        // ===== Core Operations =====

        /// <summary>
        /// Build a URL with the current query parameters, optionally overriding specific values.
        /// Does NOT navigate - just returns the computed URL.
        /// </summary>
        /// <param name="baseHref">Base path (can include query string)</param>
        /// <param name="overrides">Query parameters to override (null values remove keys)</param>
        string BuildHref(string baseHref, IReadOnlyDictionary<string, string?>? overrides = null, bool preserveCurrentState = true);

        /// <summary>
        /// Update query parameters on the current page (replaces history entry).
        /// Use this for filter changes that shouldn't create new history entries.
        /// </summary>
        void Set(params (string key, string? value)[] overrides);

        /// <summary>
        /// Navigate to a new path with optional query parameter overrides (creates history entry).
        /// Use this for actual navigation between pages.
        /// </summary>
        void Navigate(string path, IReadOnlyDictionary<string, string?>? overrides = null);

        // ===== Convenience Helpers =====

        /// <summary>Get a single query parameter value, or null if not present.</summary>
        string? Get(string key);

        /// <summary>Check if a query parameter exists and has a non-empty value.</summary>
        bool Has(string key);

        /// <summary>
        /// Toggle a value in a multi-value query parameter (comma-separated).
        /// Useful for multi-select filters.
        /// </summary>
        void Toggle(string key, string value, string separator = ",");

        /// <summary>Remove one or more query parameters from the current URL.</summary>
        void Remove(params string[] keys);

        /// <summary>
        /// Update multiple query parameters at once (replaces history).
        /// More efficient than multiple Set() calls.
        /// </summary>
        void Update(IReadOnlyDictionary<string, string?> updates);

        /// <summary>
        /// Get all values for a multi-value parameter as a list.
        /// Returns empty list if parameter doesn't exist.
        /// </summary>
        IReadOnlyList<string> GetMulti(string key, string separator = ",");
    }
}