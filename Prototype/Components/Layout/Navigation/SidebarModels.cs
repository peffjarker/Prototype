using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Prototype.Components.Layout.Navigation
{
    public class SidebarSection
    {
        /// <summary>
        /// Display title of the section ("Option", "Status", etc.)
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Optional stable key used internally and for routing/query sync.
        /// Not stored in the database.
        /// If left null, it will auto-generate a slug from <see cref="Title"/>.
        /// </summary>
        [NotMapped]
        [JsonIgnore] // optional, so it doesn’t leak through APIs if serialized
        public string? SectionKey
        {
            get => _sectionKey ?? Slugify(Title);
            set => _sectionKey = value;
        }

        private string? _sectionKey;

        /// <summary>
        /// Whether this section represents a legend (static color-coded info list)
        /// rather than clickable navigation items.
        /// </summary>
        public bool IsLegend { get; set; } = false;

        /// <summary>
        /// The list of items shown within this section.
        /// </summary>
        public List<SidebarItem> Items { get; set; } = new();

        /// <summary>
        /// Utility to normalize a title into a URL/key-safe slug.
        /// </summary>
        private static string Slugify(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var sb = new StringBuilder(input.Length);
            bool lastDash = false;

            foreach (var ch in input.Trim().ToLowerInvariant())
            {
                if (char.IsLetterOrDigit(ch))
                {
                    sb.Append(ch);
                    lastDash = false;
                }
                else if (!lastDash)
                {
                    sb.Append('-');
                    lastDash = true;
                }
            }

            return sb.ToString().Trim('-');
        }
    }

    public class SidebarItem
    {
        /// <summary>Visible text label for the item.</summary>
        public string Text { get; set; } = "";

        /// <summary>Optional unique identifier for the item (not required).</summary>
        public string? Key { get; set; }

        /// <summary>Optional HTML/SVG icon markup or symbol name.</summary>
        public string? Icon { get; set; }

        /// <summary>Optional color (hex) used for legends or accent dots.</summary>
        public string? ColorHex { get; set; }

        /// <summary>True if this item is currently selected (UI use only).</summary>
        public bool Selected { get; set; }

        /// <summary>Optional URL this item navigates to.</summary>
        public string? Url { get; set; }
    }
}
