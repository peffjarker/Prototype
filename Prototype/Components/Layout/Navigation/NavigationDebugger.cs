// Utilities/NavigationDebugger.cs
// Temporary debugging helper - add to your project to diagnose navigation issues
// Remove after debugging is complete

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    /// <summary>
    /// Temporary debugging helper to track navigation and sidebar state changes.
    /// Register as Scoped in DI and inject into components to diagnose issues.
    /// </summary>
    public class NavigationDebugger : IDisposable
    {
        private readonly NavigationManager _nav;
        private readonly List<NavigationEvent> _events = new();
        private EventHandler<LocationChangedEventArgs>? _handler;

        public NavigationDebugger(NavigationManager nav)
        {
            _nav = nav;
            _handler = OnLocationChanged;
            _nav.LocationChanged += _handler;
            
            LogEvent("NavigationDebugger", "Initialized", _nav.Uri);
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            LogEvent("Navigation", $"IsNavigationIntercepted={e.IsNavigationIntercepted}", e.Location);
        }

        public void LogPageInitialized(string pageName, string basePath)
        {
            LogEvent("PageInit", pageName, $"BasePath={basePath}, Uri={_nav.Uri}");
        }

        public void LogSidebarRebuild(string pageName, string currentPath, string basePath, bool willRebuild)
        {
            var action = willRebuild ? "REBUILDING" : "SKIPPING";
            LogEvent("SidebarRebuild", pageName, $"{action} - Current={currentPath}, Base={basePath}");
        }

        public void LogUrlStateChange(string operation, Dictionary<string, string?>? parameters = null)
        {
            var paramStr = parameters != null 
                ? string.Join(", ", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"))
                : "none";
            LogEvent("UrlState", operation, paramStr);
        }

        public void LogSidebarItemClick(string itemText, string? itemKey, string? itemUrl)
        {
            LogEvent("SidebarClick", itemText, $"Key={itemKey}, Url={itemUrl}");
        }

        private void LogEvent(string category, string action, string details)
        {
            var evt = new NavigationEvent
            {
                Timestamp = DateTime.Now,
                Category = category,
                Action = action,
                Details = details
            };
            
            _events.Add(evt);
            
            // Console output with color coding
            var color = category switch
            {
                "Navigation" => "color: blue",
                "PageInit" => "color: green",
                "SidebarRebuild" => "color: orange",
                "UrlState" => "color: purple",
                "SidebarClick" => "color: red",
                _ => "color: black"
            };
            
            Console.WriteLine($"[{evt.Timestamp:HH:mm:ss.fff}] [{category}] {action} - {details}");
        }

        public void PrintSummary()
        {
            Console.WriteLine("\n========== NAVIGATION DEBUG SUMMARY ==========");
            Console.WriteLine($"Total Events: {_events.Count}");
            Console.WriteLine("\nEvents by Category:");
            
            foreach (var group in _events.GroupBy(e => e.Category))
            {
                Console.WriteLine($"  {group.Key}: {group.Count()} events");
            }
            
            Console.WriteLine("\nRecent Events (last 20):");
            foreach (var evt in _events.TakeLast(20))
            {
                Console.WriteLine($"  [{evt.Timestamp:HH:mm:ss.fff}] {evt.Category} - {evt.Action}");
            }
            
            Console.WriteLine("==============================================\n");
        }

        public void Clear()
        {
            _events.Clear();
            Console.WriteLine("Navigation debug events cleared");
        }

        public IReadOnlyList<NavigationEvent> GetEvents() => _events.AsReadOnly();

        public void Dispose()
        {
            if (_handler != null)
                _nav.LocationChanged -= _handler;
            
            LogEvent("NavigationDebugger", "Disposed", $"Total events logged: {_events.Count}");
        }

        public class NavigationEvent
        {
            public DateTime Timestamp { get; set; }
            public string Category { get; set; } = "";
            public string Action { get; set; } = "";
            public string Details { get; set; } = "";
        }
    }
}

// ========== USAGE EXAMPLE ==========

/*
 * 1. Register in Program.cs:
 *    builder.Services.AddScoped<NavigationDebugger>();
 *
 * 2. Inject into your pages:
 *    [Inject] private NavigationDebugger? Debug { get; set; }
 *
 * 3. Add logging calls:
 *    protected override void OnInitialized()
 *    {
 *        Debug?.LogPageInitialized(GetType().Name, BasePath);
 *        base.OnInitialized();
 *    }
 *
 * 4. In PageWithSidebarBase, add:
 *    protected void RebuildSidebar()
 *    {
 *        var currentPath = GetCurrentPathOnly(Nav);
 *        var willRebuild = IsPathMatch(currentPath, BasePath);
 *        Debug?.LogSidebarRebuild(GetType().Name, currentPath, BasePath, willRebuild);
 *        // ... rest of method
 *    }
 *
 * 5. View results in browser console (F12)
 * 6. Print summary on demand:
 *    Debug?.PrintSummary();
 */
