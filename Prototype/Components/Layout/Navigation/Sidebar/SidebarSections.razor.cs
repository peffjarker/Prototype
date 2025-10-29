// Prototype/Components/Layout/Navigation/SidebarSections.razor.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Services;

namespace Prototype.Components.Layout.Navigation.Sidebar
{
    /// <summary>
    /// Sidebar sections component - simplified to use UrlState for all URL operations.
    /// </summary>
    public partial class SidebarSections : ComponentBase, IDisposable
    {
        // ===== Parameters =====

        [Parameter] public IReadOnlyList<SidebarSection> Sections { get; set; } = Array.Empty<SidebarSection>();

        /// <summary>
        /// Map section keys to query parameter names.
        /// If null/missing, that section won't sync with URL query.
        /// </summary>
        [Parameter]
        public Dictionary<string, string?> SectionQueryKeys { get; set; } = new(StringComparer.OrdinalIgnoreCase)
        {
            ["option"] = null,
            ["status"] = "status",
            ["asn"] = "asn",
            ["franchise"] = "dealer"
        };

        /// <summary>Two-way selection bindings (section key -> selected value)</summary>
        [Parameter] public Dictionary<string, string?> SelectedBySection { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        [Parameter] public EventCallback<Dictionary<string, string?>> SelectedBySectionChanged { get; set; }

        [Parameter] public EventCallback<SidebarItem> OnItemSelected { get; set; }

        [Parameter] public string Width { get; set; } = "255px";
        [Parameter] public string HeaderColor { get; set; } = "#0c5ea8";
        [Parameter] public string AccentColor { get; set; } = "#f0d400";
        [Parameter] public string BackgroundColor { get; set; } = "#f2f4f8";

        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IUrlState Url { get; set; } = default!;

        // ===== Internals =====

        private string _currentPath = "";

        protected override void OnInitialized()
        {
            UpdateCurrentPath();
            Url.Changed += HandleUrlChanged;
        }

        protected override void OnParametersSet()
        {
            UpdateCurrentPath();
        }

        private void HandleUrlChanged()
        {
            UpdateCurrentPath();
            _ = InvokeAsync(StateHasChanged);
        }

        private void UpdateCurrentPath()
        {
            var abs = Nav.ToAbsoluteUri(Nav.Uri);
            var rel = Nav.ToBaseRelativePath(abs.ToString()).TrimStart('/');
            var cut = rel.IndexOfAny(new[] { '?', '#' });
            _currentPath = cut >= 0 ? rel[..cut] : rel;
        }

        // ===== Selection Logic =====

        private bool IsSelected(SidebarSection section, SidebarItem item)
        {
            var sk = (section.SectionKey ?? section.Title ?? "").Trim();
            var skKey = sk.ToLowerInvariant();

            if (section.IsLegend || string.IsNullOrWhiteSpace(sk))
                return false;

            if (section.IsFranchiseSelector)
                return false;

            // ASN: Always query-driven
            if (skKey == "asn")
            {
                var currentAsn = Url.Get("asn");
                var itemAsn = !string.IsNullOrWhiteSpace(item.Key) ? item.Key : ExtractAsnId(item.Text);

                if (!string.IsNullOrWhiteSpace(currentAsn))
                    return string.Equals(itemAsn, currentAsn, StringComparison.OrdinalIgnoreCase);

                // No ?asn= → select first ASN item
                var firstAsnItem = Sections
                    .FirstOrDefault(s => (s.SectionKey ?? s.Title ?? "").Trim().Equals("asn", StringComparison.OrdinalIgnoreCase))
                    ?.Items.FirstOrDefault();

                var firstAsn = firstAsnItem is null ? null :
                    !string.IsNullOrWhiteSpace(firstAsnItem.Key) ? firstAsnItem.Key : ExtractAsnId(firstAsnItem.Text);

                return !string.IsNullOrWhiteSpace(firstAsn) &&
                       string.Equals(itemAsn, firstAsn, StringComparison.OrdinalIgnoreCase);
            }

            // Query-driven sections (status, etc.)
            if (SectionQueryKeys.TryGetValue(sk, out var qkey) && !string.IsNullOrWhiteSpace(qkey))
            {
                var qval = Url.Get(qkey);

                if (skKey == "status")
                {
                    if (!string.IsNullOrWhiteSpace(qval))
                        return string.Equals(item.Text, qval, StringComparison.OrdinalIgnoreCase);

                    // Default: highlight "All"
                    return string.Equals(item.Text, "All", StringComparison.OrdinalIgnoreCase);
                }

                // Generic query-driven
                if (!string.IsNullOrWhiteSpace(qval))
                    return string.Equals(item.Text, qval, StringComparison.OrdinalIgnoreCase);

                return false;
            }

            // Path-driven fallback (for pure navigation items)
            if (!string.IsNullOrWhiteSpace(item.Url))
            {
                var itemPath = OnlyPath(item.Url);
                var itemHasQuery = HasQuery(item.Url);

                if (!itemHasQuery && !string.IsNullOrWhiteSpace(itemPath) &&
                    string.Equals(Norm(itemPath), Norm(_currentPath), StringComparison.Ordinal))
                {
                    return true;
                }
            }

            // Two-way binding fallback
            if (!skKey.Equals("option", StringComparison.Ordinal) &&
                SelectedBySection.TryGetValue(sk, out var chosen) &&
                !string.IsNullOrWhiteSpace(chosen))
            {
                return string.Equals(item.Text, chosen, StringComparison.OrdinalIgnoreCase);
            }

            // Legacy Selected flag
            return item.Selected;
        }

        // ===== Click Handling (simplified with UrlState) =====

        private async Task HandleSelect(SidebarSection section, SidebarItem item)
        {
            var sk = (section.SectionKey ?? section.Title ?? "").Trim();
            var skKey = sk.ToLowerInvariant();

            // Update two-way binding
            if (!string.IsNullOrWhiteSpace(sk))
            {
                var clone = new Dictionary<string, string?>(SelectedBySection, StringComparer.OrdinalIgnoreCase)
                {
                    [sk] = item.Text
                };
                if (SelectedBySectionChanged.HasDelegate)
                    await SelectedBySectionChanged.InvokeAsync(clone);
                SelectedBySection = clone;
            }

            // Navigate using UrlState
            if (skKey == "asn")
            {
                var asn = !string.IsNullOrWhiteSpace(item.Key) ? item.Key : ExtractAsnId(item.Text);
                Url.Set(("asn", asn));
            }
            else if (SectionQueryKeys.TryGetValue(sk, out var qkey) && !string.IsNullOrWhiteSpace(qkey))
            {
                Url.Set((qkey!, item.Text));
            }
            else if (!string.IsNullOrWhiteSpace(item.Url))
            {
                // Navigate to URL (UrlState will preserve current query automatically via BuildHref pattern)
                Nav.NavigateTo(item.Url, replace: false);
            }

            if (OnItemSelected.HasDelegate)
                await OnItemSelected.InvokeAsync(item);
        }

        // ===== Helper Methods (franchise change) =====

        private async Task HandleFranchiseChange(string? dealerId)
        {
            Url.Set(("dealer", dealerId));

            if (OnItemSelected.HasDelegate)
            {
                var franchiseItem = new SidebarItem { Key = dealerId, Text = dealerId ?? "All" };
                await OnItemSelected.InvokeAsync(franchiseItem);
            }
        }

        // ===== Utilities =====

        private static string OnlyPath(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return "";
            if (Uri.TryCreate(url, UriKind.Absolute, out var abs))
                return abs.AbsolutePath.TrimStart('/');

            var rel = url.TrimStart('/');
            var q = rel.IndexOf('?', StringComparison.Ordinal);
            return q >= 0 ? rel[..q] : rel;
        }

        private static bool HasQuery(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return false;
            if (Uri.TryCreate(url, UriKind.Absolute, out var abs))
                return !string.IsNullOrEmpty(abs.Query);
            return url.Contains('?', StringComparison.Ordinal);
        }

        private static string Norm(string s)
        {
            s = s.Trim();
            if (s.EndsWith("/")) s = s.TrimEnd('/');
            return s.ToLowerInvariant();
        }

        private static string ExtractAsnId(string? text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            var left = text.Split('-', 2)[0].Trim();
            return left.Replace(" ", "", StringComparison.Ordinal);
        }

        public void Dispose()
        {
            Url.Changed -= HandleUrlChanged;
        }
    }
}