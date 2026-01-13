// appearance-manager.js
// This file manages loading and applying appearance settings from localStorage
// Extended with comprehensive Telerik/Kendo UI component overrides

(function () {
    'use strict';

    // Default settings
    const defaults = {
        theme: 'light',
        accent: 'blue',
        density: 'comfortable',
        fontSize: 14,
        font: 'System Default',
        animations: true,
        reducedMotion: false,
        highContrast: false,
        gridLines: true
    };

    // Accent color mappings
    const accentColors = {
        blue: { primary: '#0c5ea8', light: '#e6f0fb', hover: '#0a4d8a' },
        indigo: { primary: '#4f46e5', light: '#e0e7ff', hover: '#4338ca' },
        purple: { primary: '#7c3aed', light: '#ede9fe', hover: '#6d28d9' },
        pink: { primary: '#db2777', light: '#fce7f3', hover: '#be185d' },
        red: { primary: '#dc2626', light: '#fee2e2', hover: '#b91c1c' },
        orange: { primary: '#ea580c', light: '#ffedd5', hover: '#c2410c' },
        yellow: { primary: '#ca8a04', light: '#fef3c7', hover: '#a16207' },
        green: { primary: '#16a34a', light: '#dcfce7', hover: '#15803d' },
        teal: { primary: '#0d9488', light: '#ccfbf1', hover: '#0f766e' },
        cyan: { primary: '#0891b2', light: '#cffafe', hover: '#0e7490' }
    };

    // Density spacing multipliers
    const densitySettings = {
        compact: { multiplier: 0.75, lineHeight: 1.3 },
        comfortable: { multiplier: 1, lineHeight: 1.5 },
        spacious: { multiplier: 1.25, lineHeight: 1.7 }
    };

    // Font family mappings
    const fontFamilies = {
        'System Default': '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif',
        'Helvetica Neue': "'Helvetica Neue', Helvetica, Arial, sans-serif",
        'Segoe UI': "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
        'Inter': "'Inter', system-ui, sans-serif",
        'Roboto': "'Roboto', sans-serif",
        'Open Sans': "'Open Sans', sans-serif",

        // Fun / playful
        'Comic Sans MS': "'Comic Sans MS', 'Comic Sans', cursive",
        'Papyrus': "'Papyrus', fantasy",
        'Chalkboard SE': "'Chalkboard SE', 'Comic Sans MS', cursive",
        'Marker Felt': "'Marker Felt', 'Comic Sans MS', cursive",

        // Serif classics
        'Times New Roman': "'Times New Roman', Times, serif",
        'Georgia': "Georgia, serif",
        'Garamond': "'Garamond', serif",
        'Baskerville': "'Baskerville', serif",
        'Didot': "'Didot', serif",

        // Sans-serif staples
        'Arial': "Arial, Helvetica, sans-serif",
        'Verdana': "Verdana, Geneva, sans-serif",
        'Tahoma': "Tahoma, Geneva, sans-serif",
        'Trebuchet MS': "'Trebuchet MS', Helvetica, sans-serif",
        'Futura': "'Futura', sans-serif",
        'Gill Sans': "'Gill Sans', Calibri, sans-serif",

        // Monospace / dev-oriented
        'Consolas': "'Consolas', 'Courier New', monospace",
        'Courier New': "'Courier New', Courier, monospace",
        'JetBrains Mono': "'JetBrains Mono', monospace",
        'Menlo': "'Menlo', monospace",
        'Fira Code': "'Fira Code', monospace",

        // Decorative / system oddballs
        'Copperplate': "'Copperplate', 'Copperplate Gothic Light', serif",
        'Impact': "Impact, Charcoal, sans-serif",
        'Lucida Handwriting': "'Lucida Handwriting', cursive",
        'Brush Script MT': "'Brush Script MT', cursive",
        'American Typewriter': "'American Typewriter', serif",
        'Snell Roundhand': "'Snell Roundhand', cursive"
    };



    // Border radius mappings
    const borderRadiusSettings = {
        'none': {
            xs: '0',
            sm: '0',
            md: '0',
            lg: '0',
            xl: '0',
            full: '0'
        },
        'subtle': {
            xs: '2px',
            sm: '3px',
            md: '4px',
            lg: '6px',
            xl: '8px',
            full: '9999px'
        },
        'default': {
            xs: '4px',
            sm: '6px',
            md: '8px',
            lg: '12px',
            xl: '16px',
            full: '9999px'
        },
        'rounded': {
            xs: '6px',
            sm: '8px',
            md: '12px',
            lg: '16px',
            xl: '24px',
            full: '9999px'
        },
        'extra': {
            xs: '8px',
            sm: '12px',
            md: '16px',
            lg: '24px',
            xl: '32px',
            full: '9999px'
        },
        'pill': {
            xs: '9999px',
            sm: '9999px',
            md: '9999px',
            lg: '9999px',
            xl: '9999px',
            full: '9999px'
        }
    };

    // Load settings from localStorage
    function loadSettings() {
        const settings = { ...defaults };

        try {
            const keys = [
                'theme', 'accent', 'customAccent', 'density', 'fontSize', 'font',
                'animations', 'animationSpeed', 'reducedMotion', 'highContrast',
                'gridLines', 'borderRadius'
            ];

            keys.forEach(key => {
                const stored = localStorage.getItem(`ibn-${key}`);
                if (stored !== null) {
                    try {
                        settings[key] = JSON.parse(stored);
                    } catch {
                        settings[key] = stored;
                    }
                }
            });
        } catch (e) {
            console.error('Error loading appearance settings:', e);
        }

        return settings;
    }

    // Apply theme (light/dark/dim/auto)
    function applyTheme(theme) {
        const root = document.documentElement;

        // Normalize theme string - trim whitespace and handle potential issues
        theme = String(theme).trim().toLowerCase();

        if (theme === 'auto') {
            // Check system preference
            const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
            theme = prefersDark ? 'dark' : 'light';
        }

        // Dark themes
        const darkThemes = ['dark', 'dim', 'catppuccin', 'catppuccin-mocha', 'catppuccin-frappe', 'catppuccin-macchiato', 'solarized-dark', 'nord', 'dracula', 'midnight'];

        if (theme === 'dark') {
            // Custom app variables
            root.style.setProperty('--app-bg', '#0f172a');
            root.style.setProperty('--card-bg', '#1a1f2e');
            root.style.setProperty('--border', '#2d3748');
            root.style.setProperty('--border-strong', '#4b5563');
            root.style.setProperty('--text', '#f1f5f9');
            root.style.setProperty('--muted', '#94a3b8');
            root.style.setProperty('--row-hover', '#374151');
            root.style.setProperty('--nav-bg', '#1a1f2e');
            root.style.setProperty('--nav-border', '#2d3748');
            root.style.setProperty('--hover-bg', '#1f2937');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#1a1f2e');
            root.style.setProperty('--kendo-color-base-hover', '#1f2937');
            root.style.setProperty('--kendo-color-base-active', '#374151');
            root.style.setProperty('--kendo-color-on-base', '#f1f5f9');
            root.style.setProperty('--kendo-color-surface', '#0f172a');
            root.style.setProperty('--kendo-color-surface-alt', '#1a1f2e');
            root.style.setProperty('--kendo-color-border', '#2d3748');
            root.style.setProperty('--kendo-color-border-alt', '#4b5563');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#f1f5f9');
            root.style.setProperty('--kendo-color-subtle', '#94a3b8');
            root.style.setProperty('--kendo-color-disabled', '#6b7280');

            // Component states
            root.style.setProperty('--kendo-color-focus', '#3b82f6');
            root.style.setProperty('--kendo-color-error', '#ef4444');
            root.style.setProperty('--kendo-color-success', '#22c55e');
            root.style.setProperty('--kendo-color-warning', '#f59e0b');
            root.style.setProperty('--kendo-color-info', '#3b82f6');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#1a1f2e');
            root.style.setProperty('--kendo-grid-text', '#f1f5f9');
            root.style.setProperty('--kendo-grid-border', '#2d3748');
            root.style.setProperty('--kendo-grid-header-bg', '#0f172a');
            root.style.setProperty('--kendo-grid-header-text', '#f1f5f9');
            root.style.setProperty('--kendo-grid-header-border', '#2d3748');
            root.style.setProperty('--kendo-grid-footer-bg', '#0f172a');
            root.style.setProperty('--kendo-grid-footer-text', '#f1f5f9');
            root.style.setProperty('--kendo-grid-footer-border', '#2d3748');
            root.style.setProperty('--kendo-grid-alt-bg', '#151d2c');
            root.style.setProperty('--kendo-grid-hover-bg', '#374151');
            root.style.setProperty('--kendo-grid-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-grid-selected-text', '#f1f5f9');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#0f172a');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#1a1f2e');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#1a1f2e');
            root.style.setProperty('--kendo-button-text', '#f1f5f9');
            root.style.setProperty('--kendo-button-border', '#2d3748');
            root.style.setProperty('--kendo-button-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-button-hover-border', '#374151');
            root.style.setProperty('--kendo-button-active-bg', '#374151');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(59, 130, 246, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#1a1f2e');
            root.style.setProperty('--kendo-button-disabled-text', '#6b7280');
            root.style.setProperty('--kendo-button-disabled-border', '#2d3748');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#1a1f2e');
            root.style.setProperty('--kendo-input-text', '#f1f5f9');
            root.style.setProperty('--kendo-input-border', '#2d3748');
            root.style.setProperty('--kendo-input-hover-border', '#4b5563');
            root.style.setProperty('--kendo-input-focus-border', '#3b82f6');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(59, 130, 246, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#0f172a');
            root.style.setProperty('--kendo-input-disabled-text', '#6b7280');
            root.style.setProperty('--kendo-input-placeholder-text', '#94a3b8');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#1a1f2e');
            root.style.setProperty('--kendo-picker-text', '#f1f5f9');
            root.style.setProperty('--kendo-picker-border', '#2d3748');
            root.style.setProperty('--kendo-picker-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-picker-hover-border', '#4b5563');
            root.style.setProperty('--kendo-picker-focus-border', '#3b82f6');
            root.style.setProperty('--kendo-list-item-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-list-item-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-list-item-focus-bg', '#374151');
            root.style.setProperty('--kendo-popup-bg', '#1a1f2e');
            root.style.setProperty('--kendo-popup-border', '#2d3748');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#1a1f2e');
            root.style.setProperty('--kendo-calendar-text', '#f1f5f9');
            root.style.setProperty('--kendo-calendar-border', '#2d3748');
            root.style.setProperty('--kendo-calendar-header-bg', '#0f172a');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-calendar-weekend-text', '#94a3b8');
            root.style.setProperty('--kendo-calendar-other-month-text', '#6b7280');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#1a1f2e');
            root.style.setProperty('--kendo-tabstrip-text', '#f1f5f9');
            root.style.setProperty('--kendo-tabstrip-border', '#2d3748');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#94a3b8');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#f1f5f9');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#1a1f2e');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#f1f5f9');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#1a1f2e');
            root.style.setProperty('--kendo-tabstrip-content-border', '#2d3748');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#1a1f2e');
            root.style.setProperty('--kendo-window-text', '#f1f5f9');
            root.style.setProperty('--kendo-window-border', '#2d3748');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.5)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#0f172a');
            root.style.setProperty('--kendo-window-titlebar-text', '#f1f5f9');
            root.style.setProperty('--kendo-window-titlebar-border', '#2d3748');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#1a1f2e');
            root.style.setProperty('--kendo-menu-text', '#f1f5f9');
            root.style.setProperty('--kendo-menu-border', '#2d3748');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-menu-item-hover-text', '#f1f5f9');
            root.style.setProperty('--kendo-menu-item-active-bg', '#374151');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#6b7280');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#1a1f2e');
            root.style.setProperty('--kendo-treeview-text', '#f1f5f9');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#374151');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#1a1f2e');
            root.style.setProperty('--kendo-pager-text', '#f1f5f9');
            root.style.setProperty('--kendo-pager-border', '#2d3748');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#1e3a5f');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#1a1f2e');
            root.style.setProperty('--kendo-toolbar-text', '#f1f5f9');
            root.style.setProperty('--kendo-toolbar-border', '#2d3748');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#1f2937');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#1f2937');
            root.style.setProperty('--kendo-chip-text', '#f1f5f9');
            root.style.setProperty('--kendo-chip-border', '#374151');
            root.style.setProperty('--kendo-chip-hover-bg', '#374151');
            root.style.setProperty('--kendo-chip-selected-bg', '#1e3a5f');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#1a1f2e');
            root.style.setProperty('--kendo-card-text', '#f1f5f9');
            root.style.setProperty('--kendo-card-border', '#2d3748');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.2)');
            root.style.setProperty('--kendo-card-header-bg', '#0f172a');
            root.style.setProperty('--kendo-card-header-border', '#2d3748');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#1a1f2e');
            root.style.setProperty('--kendo-notification-text', '#f1f5f9');
            root.style.setProperty('--kendo-notification-border', '#2d3748');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#0f172a');
            root.style.setProperty('--kendo-tooltip-text', '#f1f5f9');
            root.style.setProperty('--kendo-tooltip-border', '#2d3748');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.4)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#1a1f2e');
            root.style.setProperty('--kendo-splitter-border', '#2d3748');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#0f172a');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#1f2937');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#1a1f2e');
            root.style.setProperty('--kendo-upload-text', '#f1f5f9');
            root.style.setProperty('--kendo-upload-border', '#2d3748');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#0f172a');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#1f2937');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#1a1f2e');
            root.style.setProperty('--kendo-editor-text', '#f1f5f9');
            root.style.setProperty('--kendo-editor-border', '#2d3748');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#0f172a');
            root.style.setProperty('--kendo-editor-content-bg', '#1a1f2e');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#1a1f2e');
            root.style.setProperty('--kendo-scheduler-text', '#f1f5f9');
            root.style.setProperty('--kendo-scheduler-border', '#2d3748');
            root.style.setProperty('--kendo-scheduler-header-bg', '#0f172a');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-scheduler-event-bg', '#1e3a5f');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#0a0f1a');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#1a1f2e');
            root.style.setProperty('--kendo-gantt-text', '#f1f5f9');
            root.style.setProperty('--kendo-gantt-border', '#2d3748');
            root.style.setProperty('--kendo-gantt-header-bg', '#0f172a');
            root.style.setProperty('--kendo-gantt-alt-bg', '#151d2c');
            root.style.setProperty('--kendo-gantt-task-bg', '#1e3a5f');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#1a1f2e');
            root.style.setProperty('--kendo-panelbar-text', '#f1f5f9');
            root.style.setProperty('--kendo-panelbar-border', '#2d3748');
            root.style.setProperty('--kendo-panelbar-header-bg', '#0f172a');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#1f2937');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#1a1f2e');
            root.style.setProperty('--kendo-listview-text', '#f1f5f9');
            root.style.setProperty('--kendo-listview-border', '#2d3748');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#1f2937');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#1e3a5f');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#2d3748');
            root.style.setProperty('--kendo-slider-selection-bg', '#3b82f6');
            root.style.setProperty('--kendo-slider-handle-bg', '#f1f5f9');
            root.style.setProperty('--kendo-slider-handle-border', '#2d3748');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffffff');
        } else if (theme === 'dim') {
            // ===== DIM THEME - A softer middle ground between light and dark =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#2d3748');
            root.style.setProperty('--card-bg', '#3d4a5c');
            root.style.setProperty('--border', '#4a5568');
            root.style.setProperty('--border-strong', '#606b7d');
            root.style.setProperty('--text', '#e2e8f0');
            root.style.setProperty('--muted', '#a0aec0');
            root.style.setProperty('--row-hover', '#4a5568');
            root.style.setProperty('--nav-bg', '#3d4a5c');
            root.style.setProperty('--nav-border', '#4a5568');
            root.style.setProperty('--hover-bg', '#4a5568');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#3d4a5c');
            root.style.setProperty('--kendo-color-base-hover', '#4a5568');
            root.style.setProperty('--kendo-color-base-active', '#5a6678');
            root.style.setProperty('--kendo-color-on-base', '#e2e8f0');
            root.style.setProperty('--kendo-color-surface', '#2d3748');
            root.style.setProperty('--kendo-color-surface-alt', '#3d4a5c');
            root.style.setProperty('--kendo-color-border', '#4a5568');
            root.style.setProperty('--kendo-color-border-alt', '#606b7d');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#e2e8f0');
            root.style.setProperty('--kendo-color-subtle', '#a0aec0');
            root.style.setProperty('--kendo-color-disabled', '#718096');

            // Component states
            root.style.setProperty('--kendo-color-focus', '#63b3ed');
            root.style.setProperty('--kendo-color-error', '#fc8181');
            root.style.setProperty('--kendo-color-success', '#68d391');
            root.style.setProperty('--kendo-color-warning', '#f6ad55');
            root.style.setProperty('--kendo-color-info', '#63b3ed');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#3d4a5c');
            root.style.setProperty('--kendo-grid-text', '#e2e8f0');
            root.style.setProperty('--kendo-grid-border', '#4a5568');
            root.style.setProperty('--kendo-grid-header-bg', '#2d3748');
            root.style.setProperty('--kendo-grid-header-text', '#e2e8f0');
            root.style.setProperty('--kendo-grid-header-border', '#4a5568');
            root.style.setProperty('--kendo-grid-footer-bg', '#2d3748');
            root.style.setProperty('--kendo-grid-footer-text', '#e2e8f0');
            root.style.setProperty('--kendo-grid-footer-border', '#4a5568');
            root.style.setProperty('--kendo-grid-alt-bg', '#364152');
            root.style.setProperty('--kendo-grid-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-grid-selected-bg', '#3182ce');
            root.style.setProperty('--kendo-grid-selected-text', '#ffffff');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#2d3748');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#3d4a5c');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#3d4a5c');
            root.style.setProperty('--kendo-button-text', '#e2e8f0');
            root.style.setProperty('--kendo-button-border', '#4a5568');
            root.style.setProperty('--kendo-button-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-button-hover-border', '#5a6678');
            root.style.setProperty('--kendo-button-active-bg', '#5a6678');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(99, 179, 237, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#3d4a5c');
            root.style.setProperty('--kendo-button-disabled-text', '#718096');
            root.style.setProperty('--kendo-button-disabled-border', '#4a5568');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#3d4a5c');
            root.style.setProperty('--kendo-input-text', '#e2e8f0');
            root.style.setProperty('--kendo-input-border', '#4a5568');
            root.style.setProperty('--kendo-input-hover-border', '#606b7d');
            root.style.setProperty('--kendo-input-focus-border', '#63b3ed');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(99, 179, 237, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#2d3748');
            root.style.setProperty('--kendo-input-disabled-text', '#718096');
            root.style.setProperty('--kendo-input-placeholder-text', '#a0aec0');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#3d4a5c');
            root.style.setProperty('--kendo-picker-text', '#e2e8f0');
            root.style.setProperty('--kendo-picker-border', '#4a5568');
            root.style.setProperty('--kendo-picker-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-picker-hover-border', '#606b7d');
            root.style.setProperty('--kendo-picker-focus-border', '#63b3ed');
            root.style.setProperty('--kendo-list-item-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-list-item-selected-bg', '#3182ce');
            root.style.setProperty('--kendo-list-item-focus-bg', '#5a6678');
            root.style.setProperty('--kendo-popup-bg', '#3d4a5c');
            root.style.setProperty('--kendo-popup-border', '#4a5568');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.25)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#3d4a5c');
            root.style.setProperty('--kendo-calendar-text', '#e2e8f0');
            root.style.setProperty('--kendo-calendar-border', '#4a5568');
            root.style.setProperty('--kendo-calendar-header-bg', '#2d3748');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#3182ce');
            root.style.setProperty('--kendo-calendar-weekend-text', '#a0aec0');
            root.style.setProperty('--kendo-calendar-other-month-text', '#718096');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#3d4a5c');
            root.style.setProperty('--kendo-tabstrip-text', '#e2e8f0');
            root.style.setProperty('--kendo-tabstrip-border', '#4a5568');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#a0aec0');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#e2e8f0');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#3d4a5c');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#e2e8f0');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#3d4a5c');
            root.style.setProperty('--kendo-tabstrip-content-border', '#4a5568');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#3d4a5c');
            root.style.setProperty('--kendo-window-text', '#e2e8f0');
            root.style.setProperty('--kendo-window-border', '#4a5568');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.35)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#2d3748');
            root.style.setProperty('--kendo-window-titlebar-text', '#e2e8f0');
            root.style.setProperty('--kendo-window-titlebar-border', '#4a5568');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#3d4a5c');
            root.style.setProperty('--kendo-menu-text', '#e2e8f0');
            root.style.setProperty('--kendo-menu-border', '#4a5568');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-menu-item-hover-text', '#e2e8f0');
            root.style.setProperty('--kendo-menu-item-active-bg', '#5a6678');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#718096');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#3d4a5c');
            root.style.setProperty('--kendo-treeview-text', '#e2e8f0');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#3182ce');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#5a6678');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#3d4a5c');
            root.style.setProperty('--kendo-pager-text', '#e2e8f0');
            root.style.setProperty('--kendo-pager-border', '#4a5568');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#3182ce');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#3d4a5c');
            root.style.setProperty('--kendo-toolbar-text', '#e2e8f0');
            root.style.setProperty('--kendo-toolbar-border', '#4a5568');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#4a5568');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#4a5568');
            root.style.setProperty('--kendo-chip-text', '#e2e8f0');
            root.style.setProperty('--kendo-chip-border', '#5a6678');
            root.style.setProperty('--kendo-chip-hover-bg', '#5a6678');
            root.style.setProperty('--kendo-chip-selected-bg', '#3182ce');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#3d4a5c');
            root.style.setProperty('--kendo-card-text', '#e2e8f0');
            root.style.setProperty('--kendo-card-border', '#4a5568');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.15)');
            root.style.setProperty('--kendo-card-header-bg', '#2d3748');
            root.style.setProperty('--kendo-card-header-border', '#4a5568');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#3d4a5c');
            root.style.setProperty('--kendo-notification-text', '#e2e8f0');
            root.style.setProperty('--kendo-notification-border', '#4a5568');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.25)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#2d3748');
            root.style.setProperty('--kendo-tooltip-text', '#e2e8f0');
            root.style.setProperty('--kendo-tooltip-border', '#4a5568');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#3d4a5c');
            root.style.setProperty('--kendo-splitter-border', '#4a5568');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#2d3748');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#4a5568');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#3d4a5c');
            root.style.setProperty('--kendo-upload-text', '#e2e8f0');
            root.style.setProperty('--kendo-upload-border', '#4a5568');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#2d3748');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#4a5568');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#3d4a5c');
            root.style.setProperty('--kendo-editor-text', '#e2e8f0');
            root.style.setProperty('--kendo-editor-border', '#4a5568');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#2d3748');
            root.style.setProperty('--kendo-editor-content-bg', '#3d4a5c');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#3d4a5c');
            root.style.setProperty('--kendo-scheduler-text', '#e2e8f0');
            root.style.setProperty('--kendo-scheduler-border', '#4a5568');
            root.style.setProperty('--kendo-scheduler-header-bg', '#2d3748');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-scheduler-event-bg', '#3182ce');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#283240');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#3d4a5c');
            root.style.setProperty('--kendo-gantt-text', '#e2e8f0');
            root.style.setProperty('--kendo-gantt-border', '#4a5568');
            root.style.setProperty('--kendo-gantt-header-bg', '#2d3748');
            root.style.setProperty('--kendo-gantt-alt-bg', '#364152');
            root.style.setProperty('--kendo-gantt-task-bg', '#3182ce');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#3d4a5c');
            root.style.setProperty('--kendo-panelbar-text', '#e2e8f0');
            root.style.setProperty('--kendo-panelbar-border', '#4a5568');
            root.style.setProperty('--kendo-panelbar-header-bg', '#2d3748');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#4a5568');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#3d4a5c');
            root.style.setProperty('--kendo-listview-text', '#e2e8f0');
            root.style.setProperty('--kendo-listview-border', '#4a5568');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#4a5568');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#3182ce');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#4a5568');
            root.style.setProperty('--kendo-slider-selection-bg', '#63b3ed');
            root.style.setProperty('--kendo-slider-handle-bg', '#e2e8f0');
            root.style.setProperty('--kendo-slider-handle-border', '#4a5568');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffffff');
        } else if (theme === 'colorful') {
            // ===== COLORFUL THEME - Vibrant, energetic palette with purple/pink tints =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#faf5ff');
            root.style.setProperty('--card-bg', '#ffffff');
            root.style.setProperty('--border', '#e9d5ff');
            root.style.setProperty('--border-strong', '#d8b4fe');
            root.style.setProperty('--text', '#3b0764');
            root.style.setProperty('--muted', '#7e22ce');
            root.style.setProperty('--row-hover', '#f3e8ff');
            root.style.setProperty('--nav-bg', '#ffffff');
            root.style.setProperty('--nav-border', '#e9d5ff');
            root.style.setProperty('--hover-bg', '#fae8ff');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#ffffff');
            root.style.setProperty('--kendo-color-base-hover', '#fae8ff');
            root.style.setProperty('--kendo-color-base-active', '#f3e8ff');
            root.style.setProperty('--kendo-color-on-base', '#3b0764');
            root.style.setProperty('--kendo-color-surface', '#faf5ff');
            root.style.setProperty('--kendo-color-surface-alt', '#fdf4ff');
            root.style.setProperty('--kendo-color-border', '#e9d5ff');
            root.style.setProperty('--kendo-color-border-alt', '#d8b4fe');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#3b0764');
            root.style.setProperty('--kendo-color-subtle', '#7e22ce');
            root.style.setProperty('--kendo-color-disabled', '#c084fc');

            // Component states - more vibrant
            root.style.setProperty('--kendo-color-focus', '#a855f7');
            root.style.setProperty('--kendo-color-error', '#e11d48');
            root.style.setProperty('--kendo-color-success', '#059669');
            root.style.setProperty('--kendo-color-warning', '#d97706');
            root.style.setProperty('--kendo-color-info', '#0ea5e9');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#ffffff');
            root.style.setProperty('--kendo-grid-text', '#3b0764');
            root.style.setProperty('--kendo-grid-border', '#e9d5ff');
            root.style.setProperty('--kendo-grid-header-bg', 'linear-gradient(135deg, #f5d0fe 0%, #e9d5ff 100%)');
            root.style.setProperty('--kendo-grid-header-bg', '#f5d0fe');
            root.style.setProperty('--kendo-grid-header-text', '#701a75');
            root.style.setProperty('--kendo-grid-header-border', '#e9d5ff');
            root.style.setProperty('--kendo-grid-footer-bg', '#f5d0fe');
            root.style.setProperty('--kendo-grid-footer-text', '#701a75');
            root.style.setProperty('--kendo-grid-footer-border', '#e9d5ff');
            root.style.setProperty('--kendo-grid-alt-bg', '#fdf4ff');
            root.style.setProperty('--kendo-grid-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-grid-selected-bg', '#e879f9');
            root.style.setProperty('--kendo-grid-selected-text', '#ffffff');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#f5d0fe');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#fdf4ff');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#ffffff');
            root.style.setProperty('--kendo-button-text', '#7e22ce');
            root.style.setProperty('--kendo-button-border', '#d8b4fe');
            root.style.setProperty('--kendo-button-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-button-hover-border', '#c084fc');
            root.style.setProperty('--kendo-button-active-bg', '#f3e8ff');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(168, 85, 247, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#fdf4ff');
            root.style.setProperty('--kendo-button-disabled-text', '#c084fc');
            root.style.setProperty('--kendo-button-disabled-border', '#e9d5ff');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#ffffff');
            root.style.setProperty('--kendo-input-text', '#3b0764');
            root.style.setProperty('--kendo-input-border', '#d8b4fe');
            root.style.setProperty('--kendo-input-hover-border', '#c084fc');
            root.style.setProperty('--kendo-input-focus-border', '#a855f7');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(168, 85, 247, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#fdf4ff');
            root.style.setProperty('--kendo-input-disabled-text', '#c084fc');
            root.style.setProperty('--kendo-input-placeholder-text', '#9333ea');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#ffffff');
            root.style.setProperty('--kendo-picker-text', '#3b0764');
            root.style.setProperty('--kendo-picker-border', '#d8b4fe');
            root.style.setProperty('--kendo-picker-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-picker-hover-border', '#c084fc');
            root.style.setProperty('--kendo-picker-focus-border', '#a855f7');
            root.style.setProperty('--kendo-list-item-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-list-item-selected-bg', '#e879f9');
            root.style.setProperty('--kendo-list-item-selected-text', '#ffffff');
            root.style.setProperty('--kendo-list-item-focus-bg', '#f3e8ff');
            root.style.setProperty('--kendo-popup-bg', '#ffffff');
            root.style.setProperty('--kendo-popup-border', '#e9d5ff');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(147, 51, 234, 0.15)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#ffffff');
            root.style.setProperty('--kendo-calendar-text', '#3b0764');
            root.style.setProperty('--kendo-calendar-border', '#e9d5ff');
            root.style.setProperty('--kendo-calendar-header-bg', '#f5d0fe');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#a855f7');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#ffffff');
            root.style.setProperty('--kendo-calendar-weekend-text', '#c026d3');
            root.style.setProperty('--kendo-calendar-other-month-text', '#d8b4fe');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-text', '#3b0764');
            root.style.setProperty('--kendo-tabstrip-border', '#e9d5ff');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#9333ea');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#7e22ce');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#7e22ce');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-content-border', '#e9d5ff');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#ffffff');
            root.style.setProperty('--kendo-window-text', '#3b0764');
            root.style.setProperty('--kendo-window-border', '#e9d5ff');
            root.style.setProperty('--kendo-window-shadow', 'rgba(147, 51, 234, 0.2)');
            root.style.setProperty('--kendo-window-titlebar-bg', 'linear-gradient(135deg, #f0abfc 0%, #c084fc 100%)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#f0abfc');
            root.style.setProperty('--kendo-window-titlebar-text', '#701a75');
            root.style.setProperty('--kendo-window-titlebar-border', '#e9d5ff');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#ffffff');
            root.style.setProperty('--kendo-menu-text', '#3b0764');
            root.style.setProperty('--kendo-menu-border', '#e9d5ff');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-menu-item-hover-text', '#7e22ce');
            root.style.setProperty('--kendo-menu-item-active-bg', '#f3e8ff');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#d8b4fe');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#ffffff');
            root.style.setProperty('--kendo-treeview-text', '#3b0764');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#e879f9');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#ffffff');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#f3e8ff');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#ffffff');
            root.style.setProperty('--kendo-pager-text', '#3b0764');
            root.style.setProperty('--kendo-pager-border', '#e9d5ff');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#a855f7');
            root.style.setProperty('--kendo-pager-item-selected-text', '#ffffff');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#ffffff');
            root.style.setProperty('--kendo-toolbar-text', '#3b0764');
            root.style.setProperty('--kendo-toolbar-border', '#e9d5ff');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#fae8ff');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#f3e8ff');
            root.style.setProperty('--kendo-chip-text', '#7e22ce');
            root.style.setProperty('--kendo-chip-border', '#e9d5ff');
            root.style.setProperty('--kendo-chip-hover-bg', '#e9d5ff');
            root.style.setProperty('--kendo-chip-selected-bg', '#a855f7');
            root.style.setProperty('--kendo-chip-selected-text', '#ffffff');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#ffffff');
            root.style.setProperty('--kendo-card-text', '#3b0764');
            root.style.setProperty('--kendo-card-border', '#e9d5ff');
            root.style.setProperty('--kendo-card-shadow', 'rgba(147, 51, 234, 0.1)');
            root.style.setProperty('--kendo-card-header-bg', '#fdf4ff');
            root.style.setProperty('--kendo-card-header-border', '#e9d5ff');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#ffffff');
            root.style.setProperty('--kendo-notification-text', '#3b0764');
            root.style.setProperty('--kendo-notification-border', '#e9d5ff');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(147, 51, 234, 0.15)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#7e22ce');
            root.style.setProperty('--kendo-tooltip-text', '#ffffff');
            root.style.setProperty('--kendo-tooltip-border', '#6b21a8');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(107, 33, 168, 0.3)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#ffffff');
            root.style.setProperty('--kendo-splitter-border', '#e9d5ff');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#f5d0fe');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#f0abfc');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#ffffff');
            root.style.setProperty('--kendo-upload-text', '#3b0764');
            root.style.setProperty('--kendo-upload-border', '#e9d5ff');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#fdf4ff');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#fae8ff');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#ffffff');
            root.style.setProperty('--kendo-editor-text', '#3b0764');
            root.style.setProperty('--kendo-editor-border', '#e9d5ff');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#fdf4ff');
            root.style.setProperty('--kendo-editor-content-bg', '#ffffff');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#ffffff');
            root.style.setProperty('--kendo-scheduler-text', '#3b0764');
            root.style.setProperty('--kendo-scheduler-border', '#e9d5ff');
            root.style.setProperty('--kendo-scheduler-header-bg', '#f5d0fe');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-scheduler-event-bg', '#e879f9');
            root.style.setProperty('--kendo-scheduler-event-text', '#ffffff');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#fdf4ff');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#ffffff');
            root.style.setProperty('--kendo-gantt-text', '#3b0764');
            root.style.setProperty('--kendo-gantt-border', '#e9d5ff');
            root.style.setProperty('--kendo-gantt-header-bg', '#f5d0fe');
            root.style.setProperty('--kendo-gantt-alt-bg', '#fdf4ff');
            root.style.setProperty('--kendo-gantt-task-bg', '#a855f7');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#ffffff');
            root.style.setProperty('--kendo-panelbar-text', '#3b0764');
            root.style.setProperty('--kendo-panelbar-border', '#e9d5ff');
            root.style.setProperty('--kendo-panelbar-header-bg', '#fdf4ff');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#fae8ff');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#ffffff');
            root.style.setProperty('--kendo-listview-text', '#3b0764');
            root.style.setProperty('--kendo-listview-border', '#e9d5ff');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#fae8ff');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#e879f9');
            root.style.setProperty('--kendo-listview-item-selected-text', '#ffffff');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#e9d5ff');
            root.style.setProperty('--kendo-slider-selection-bg', '#a855f7');
            root.style.setProperty('--kendo-slider-handle-bg', '#ffffff');
            root.style.setProperty('--kendo-slider-handle-border', '#c084fc');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#fae8ff');
        } else if (theme === 'high-contrast') {
            // ===== HIGH CONTRAST THEME - Maximum accessibility with strong contrast =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#ffffff');
            root.style.setProperty('--card-bg', '#ffffff');
            root.style.setProperty('--border', '#000000');
            root.style.setProperty('--border-strong', '#000000');
            root.style.setProperty('--text', '#000000');
            root.style.setProperty('--muted', '#000000');
            root.style.setProperty('--row-hover', '#ffff00');
            root.style.setProperty('--nav-bg', '#ffffff');
            root.style.setProperty('--nav-border', '#000000');
            root.style.setProperty('--hover-bg', '#ffff00');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#ffffff');
            root.style.setProperty('--kendo-color-base-hover', '#ffff00');
            root.style.setProperty('--kendo-color-base-active', '#ffff00');
            root.style.setProperty('--kendo-color-on-base', '#000000');
            root.style.setProperty('--kendo-color-surface', '#ffffff');
            root.style.setProperty('--kendo-color-surface-alt', '#ffffff');
            root.style.setProperty('--kendo-color-border', '#000000');
            root.style.setProperty('--kendo-color-border-alt', '#000000');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#000000');
            root.style.setProperty('--kendo-color-subtle', '#000000');
            root.style.setProperty('--kendo-color-disabled', '#595959');

            // Component states - high contrast colors
            root.style.setProperty('--kendo-color-focus', '#0000ff');
            root.style.setProperty('--kendo-color-error', '#ff0000');
            root.style.setProperty('--kendo-color-success', '#008000');
            root.style.setProperty('--kendo-color-warning', '#ff8c00');
            root.style.setProperty('--kendo-color-info', '#0000ff');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#ffffff');
            root.style.setProperty('--kendo-grid-text', '#000000');
            root.style.setProperty('--kendo-grid-border', '#000000');
            root.style.setProperty('--kendo-grid-header-bg', '#000000');
            root.style.setProperty('--kendo-grid-header-text', '#ffffff');
            root.style.setProperty('--kendo-grid-header-border', '#000000');
            root.style.setProperty('--kendo-grid-footer-bg', '#000000');
            root.style.setProperty('--kendo-grid-footer-text', '#ffffff');
            root.style.setProperty('--kendo-grid-footer-border', '#000000');
            root.style.setProperty('--kendo-grid-alt-bg', '#f0f0f0');
            root.style.setProperty('--kendo-grid-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-grid-selected-bg', '#0000ff');
            root.style.setProperty('--kendo-grid-selected-text', '#ffffff');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#000000');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#ffffff');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#ffffff');
            root.style.setProperty('--kendo-button-text', '#000000');
            root.style.setProperty('--kendo-button-border', '#000000');
            root.style.setProperty('--kendo-button-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-button-hover-border', '#000000');
            root.style.setProperty('--kendo-button-active-bg', '#ffff00');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(0, 0, 255, 0.8)');
            root.style.setProperty('--kendo-button-disabled-bg', '#ffffff');
            root.style.setProperty('--kendo-button-disabled-text', '#595959');
            root.style.setProperty('--kendo-button-disabled-border', '#595959');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#ffffff');
            root.style.setProperty('--kendo-input-text', '#000000');
            root.style.setProperty('--kendo-input-border', '#000000');
            root.style.setProperty('--kendo-input-hover-border', '#0000ff');
            root.style.setProperty('--kendo-input-focus-border', '#0000ff');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(0, 0, 255, 0.5)');
            root.style.setProperty('--kendo-input-disabled-bg', '#f0f0f0');
            root.style.setProperty('--kendo-input-disabled-text', '#595959');
            root.style.setProperty('--kendo-input-placeholder-text', '#595959');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#ffffff');
            root.style.setProperty('--kendo-picker-text', '#000000');
            root.style.setProperty('--kendo-picker-border', '#000000');
            root.style.setProperty('--kendo-picker-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-picker-hover-border', '#000000');
            root.style.setProperty('--kendo-picker-focus-border', '#0000ff');
            root.style.setProperty('--kendo-list-item-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-list-item-selected-bg', '#0000ff');
            root.style.setProperty('--kendo-list-item-selected-text', '#ffffff');
            root.style.setProperty('--kendo-list-item-focus-bg', '#ffff00');
            root.style.setProperty('--kendo-popup-bg', '#ffffff');
            root.style.setProperty('--kendo-popup-border', '#000000');
            root.style.setProperty('--kendo-popup-shadow', 'none');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#ffffff');
            root.style.setProperty('--kendo-calendar-text', '#000000');
            root.style.setProperty('--kendo-calendar-border', '#000000');
            root.style.setProperty('--kendo-calendar-header-bg', '#000000');
            root.style.setProperty('--kendo-calendar-header-text', '#ffffff');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#0000ff');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#ffffff');
            root.style.setProperty('--kendo-calendar-weekend-text', '#000000');
            root.style.setProperty('--kendo-calendar-other-month-text', '#595959');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-text', '#000000');
            root.style.setProperty('--kendo-tabstrip-border', '#000000');
            root.style.setProperty('--kendo-tabstrip-item-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-item-text', '#000000');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#000000');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#000000');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-content-border', '#000000');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#ffffff');
            root.style.setProperty('--kendo-window-text', '#000000');
            root.style.setProperty('--kendo-window-border', '#000000');
            root.style.setProperty('--kendo-window-shadow', 'none');
            root.style.setProperty('--kendo-window-titlebar-bg', '#000000');
            root.style.setProperty('--kendo-window-titlebar-text', '#ffffff');
            root.style.setProperty('--kendo-window-titlebar-border', '#000000');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#ffffff');
            root.style.setProperty('--kendo-menu-text', '#000000');
            root.style.setProperty('--kendo-menu-border', '#000000');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-menu-item-hover-text', '#000000');
            root.style.setProperty('--kendo-menu-item-active-bg', '#0000ff');
            root.style.setProperty('--kendo-menu-item-active-text', '#ffffff');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#595959');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#ffffff');
            root.style.setProperty('--kendo-treeview-text', '#000000');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#0000ff');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#ffffff');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#ffff00');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#ffffff');
            root.style.setProperty('--kendo-pager-text', '#000000');
            root.style.setProperty('--kendo-pager-border', '#000000');
            root.style.setProperty('--kendo-pager-item-bg', '#ffffff');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#0000ff');
            root.style.setProperty('--kendo-pager-item-selected-text', '#ffffff');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#ffffff');
            root.style.setProperty('--kendo-toolbar-text', '#000000');
            root.style.setProperty('--kendo-toolbar-border', '#000000');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#ffff00');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#ffffff');
            root.style.setProperty('--kendo-chip-text', '#000000');
            root.style.setProperty('--kendo-chip-border', '#000000');
            root.style.setProperty('--kendo-chip-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-chip-selected-bg', '#0000ff');
            root.style.setProperty('--kendo-chip-selected-text', '#ffffff');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#ffffff');
            root.style.setProperty('--kendo-card-text', '#000000');
            root.style.setProperty('--kendo-card-border', '#000000');
            root.style.setProperty('--kendo-card-shadow', 'none');
            root.style.setProperty('--kendo-card-header-bg', '#f0f0f0');
            root.style.setProperty('--kendo-card-header-border', '#000000');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#ffffff');
            root.style.setProperty('--kendo-notification-text', '#000000');
            root.style.setProperty('--kendo-notification-border', '#000000');
            root.style.setProperty('--kendo-notification-shadow', 'none');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#000000');
            root.style.setProperty('--kendo-tooltip-text', '#ffffff');
            root.style.setProperty('--kendo-tooltip-border', '#ffffff');
            root.style.setProperty('--kendo-tooltip-shadow', 'none');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#ffffff');
            root.style.setProperty('--kendo-splitter-border', '#000000');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#000000');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#0000ff');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#ffffff');
            root.style.setProperty('--kendo-upload-text', '#000000');
            root.style.setProperty('--kendo-upload-border', '#000000');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#f0f0f0');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#ffff00');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#ffffff');
            root.style.setProperty('--kendo-editor-text', '#000000');
            root.style.setProperty('--kendo-editor-border', '#000000');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#f0f0f0');
            root.style.setProperty('--kendo-editor-content-bg', '#ffffff');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#ffffff');
            root.style.setProperty('--kendo-scheduler-text', '#000000');
            root.style.setProperty('--kendo-scheduler-border', '#000000');
            root.style.setProperty('--kendo-scheduler-header-bg', '#000000');
            root.style.setProperty('--kendo-scheduler-header-text', '#ffffff');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-scheduler-event-bg', '#0000ff');
            root.style.setProperty('--kendo-scheduler-event-text', '#ffffff');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#f0f0f0');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#ffffff');
            root.style.setProperty('--kendo-gantt-text', '#000000');
            root.style.setProperty('--kendo-gantt-border', '#000000');
            root.style.setProperty('--kendo-gantt-header-bg', '#000000');
            root.style.setProperty('--kendo-gantt-header-text', '#ffffff');
            root.style.setProperty('--kendo-gantt-alt-bg', '#f0f0f0');
            root.style.setProperty('--kendo-gantt-task-bg', '#0000ff');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#ffffff');
            root.style.setProperty('--kendo-panelbar-text', '#000000');
            root.style.setProperty('--kendo-panelbar-border', '#000000');
            root.style.setProperty('--kendo-panelbar-header-bg', '#f0f0f0');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#ffff00');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#ffffff');
            root.style.setProperty('--kendo-listview-text', '#000000');
            root.style.setProperty('--kendo-listview-border', '#000000');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#ffff00');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#0000ff');
            root.style.setProperty('--kendo-listview-item-selected-text', '#ffffff');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#000000');
            root.style.setProperty('--kendo-slider-selection-bg', '#0000ff');
            root.style.setProperty('--kendo-slider-handle-bg', '#ffffff');
            root.style.setProperty('--kendo-slider-handle-border', '#000000');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffff00');
        } else if (theme === 'nord') {
            // ===== NORD THEME - Popular arctic, cool-toned palette =====
            // Based on https://www.nordtheme.com/
            // Custom app variables
            root.style.setProperty('--app-bg', '#2e3440');
            root.style.setProperty('--card-bg', '#3b4252');
            root.style.setProperty('--border', '#4c566a');
            root.style.setProperty('--border-strong', '#616e88');
            root.style.setProperty('--text', '#eceff4');
            root.style.setProperty('--muted', '#d8dee9');
            root.style.setProperty('--row-hover', '#434c5e');
            root.style.setProperty('--nav-bg', '#3b4252');
            root.style.setProperty('--nav-border', '#4c566a');
            root.style.setProperty('--hover-bg', '#434c5e');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#3b4252');
            root.style.setProperty('--kendo-color-base-hover', '#434c5e');
            root.style.setProperty('--kendo-color-base-active', '#4c566a');
            root.style.setProperty('--kendo-color-on-base', '#eceff4');
            root.style.setProperty('--kendo-color-surface', '#2e3440');
            root.style.setProperty('--kendo-color-surface-alt', '#3b4252');
            root.style.setProperty('--kendo-color-border', '#4c566a');
            root.style.setProperty('--kendo-color-border-alt', '#616e88');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#eceff4');
            root.style.setProperty('--kendo-color-subtle', '#d8dee9');
            root.style.setProperty('--kendo-color-disabled', '#616e88');

            // Component states - Nord frost colors
            root.style.setProperty('--kendo-color-focus', '#88c0d0');
            root.style.setProperty('--kendo-color-error', '#bf616a');
            root.style.setProperty('--kendo-color-success', '#a3be8c');
            root.style.setProperty('--kendo-color-warning', '#ebcb8b');
            root.style.setProperty('--kendo-color-info', '#81a1c1');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#3b4252');
            root.style.setProperty('--kendo-grid-text', '#eceff4');
            root.style.setProperty('--kendo-grid-border', '#4c566a');
            root.style.setProperty('--kendo-grid-header-bg', '#2e3440');
            root.style.setProperty('--kendo-grid-header-text', '#88c0d0');
            root.style.setProperty('--kendo-grid-header-border', '#4c566a');
            root.style.setProperty('--kendo-grid-footer-bg', '#2e3440');
            root.style.setProperty('--kendo-grid-footer-text', '#d8dee9');
            root.style.setProperty('--kendo-grid-footer-border', '#4c566a');
            root.style.setProperty('--kendo-grid-alt-bg', '#353b49');
            root.style.setProperty('--kendo-grid-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-grid-selected-bg', '#5e81ac');
            root.style.setProperty('--kendo-grid-selected-text', '#eceff4');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#2e3440');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#3b4252');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#3b4252');
            root.style.setProperty('--kendo-button-text', '#eceff4');
            root.style.setProperty('--kendo-button-border', '#4c566a');
            root.style.setProperty('--kendo-button-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-button-hover-border', '#616e88');
            root.style.setProperty('--kendo-button-active-bg', '#4c566a');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(136, 192, 208, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#3b4252');
            root.style.setProperty('--kendo-button-disabled-text', '#616e88');
            root.style.setProperty('--kendo-button-disabled-border', '#4c566a');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#3b4252');
            root.style.setProperty('--kendo-input-text', '#eceff4');
            root.style.setProperty('--kendo-input-border', '#4c566a');
            root.style.setProperty('--kendo-input-hover-border', '#616e88');
            root.style.setProperty('--kendo-input-focus-border', '#88c0d0');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(136, 192, 208, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#2e3440');
            root.style.setProperty('--kendo-input-disabled-text', '#616e88');
            root.style.setProperty('--kendo-input-placeholder-text', '#d8dee9');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#3b4252');
            root.style.setProperty('--kendo-picker-text', '#eceff4');
            root.style.setProperty('--kendo-picker-border', '#4c566a');
            root.style.setProperty('--kendo-picker-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-picker-hover-border', '#616e88');
            root.style.setProperty('--kendo-picker-focus-border', '#88c0d0');
            root.style.setProperty('--kendo-list-item-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-list-item-selected-bg', '#5e81ac');
            root.style.setProperty('--kendo-list-item-selected-text', '#eceff4');
            root.style.setProperty('--kendo-list-item-focus-bg', '#4c566a');
            root.style.setProperty('--kendo-popup-bg', '#3b4252');
            root.style.setProperty('--kendo-popup-border', '#4c566a');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#3b4252');
            root.style.setProperty('--kendo-calendar-text', '#eceff4');
            root.style.setProperty('--kendo-calendar-border', '#4c566a');
            root.style.setProperty('--kendo-calendar-header-bg', '#2e3440');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#5e81ac');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#eceff4');
            root.style.setProperty('--kendo-calendar-weekend-text', '#d08770');
            root.style.setProperty('--kendo-calendar-other-month-text', '#616e88');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#3b4252');
            root.style.setProperty('--kendo-tabstrip-text', '#eceff4');
            root.style.setProperty('--kendo-tabstrip-border', '#4c566a');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#d8dee9');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#eceff4');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#3b4252');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#88c0d0');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#3b4252');
            root.style.setProperty('--kendo-tabstrip-content-border', '#4c566a');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#3b4252');
            root.style.setProperty('--kendo-window-text', '#eceff4');
            root.style.setProperty('--kendo-window-border', '#4c566a');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.4)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#2e3440');
            root.style.setProperty('--kendo-window-titlebar-text', '#88c0d0');
            root.style.setProperty('--kendo-window-titlebar-border', '#4c566a');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#3b4252');
            root.style.setProperty('--kendo-menu-text', '#eceff4');
            root.style.setProperty('--kendo-menu-border', '#4c566a');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-menu-item-hover-text', '#eceff4');
            root.style.setProperty('--kendo-menu-item-active-bg', '#4c566a');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#616e88');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#3b4252');
            root.style.setProperty('--kendo-treeview-text', '#eceff4');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#5e81ac');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#eceff4');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#4c566a');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#3b4252');
            root.style.setProperty('--kendo-pager-text', '#eceff4');
            root.style.setProperty('--kendo-pager-border', '#4c566a');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#5e81ac');
            root.style.setProperty('--kendo-pager-item-selected-text', '#eceff4');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#3b4252');
            root.style.setProperty('--kendo-toolbar-text', '#eceff4');
            root.style.setProperty('--kendo-toolbar-border', '#4c566a');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#434c5e');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#434c5e');
            root.style.setProperty('--kendo-chip-text', '#eceff4');
            root.style.setProperty('--kendo-chip-border', '#4c566a');
            root.style.setProperty('--kendo-chip-hover-bg', '#4c566a');
            root.style.setProperty('--kendo-chip-selected-bg', '#5e81ac');
            root.style.setProperty('--kendo-chip-selected-text', '#eceff4');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#3b4252');
            root.style.setProperty('--kendo-card-text', '#eceff4');
            root.style.setProperty('--kendo-card-border', '#4c566a');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.2)');
            root.style.setProperty('--kendo-card-header-bg', '#2e3440');
            root.style.setProperty('--kendo-card-header-border', '#4c566a');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#3b4252');
            root.style.setProperty('--kendo-notification-text', '#eceff4');
            root.style.setProperty('--kendo-notification-border', '#4c566a');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#2e3440');
            root.style.setProperty('--kendo-tooltip-text', '#eceff4');
            root.style.setProperty('--kendo-tooltip-border', '#4c566a');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.4)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#3b4252');
            root.style.setProperty('--kendo-splitter-border', '#4c566a');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#2e3440');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#434c5e');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#3b4252');
            root.style.setProperty('--kendo-upload-text', '#eceff4');
            root.style.setProperty('--kendo-upload-border', '#4c566a');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#2e3440');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#434c5e');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#3b4252');
            root.style.setProperty('--kendo-editor-text', '#eceff4');
            root.style.setProperty('--kendo-editor-border', '#4c566a');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#2e3440');
            root.style.setProperty('--kendo-editor-content-bg', '#3b4252');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#3b4252');
            root.style.setProperty('--kendo-scheduler-text', '#eceff4');
            root.style.setProperty('--kendo-scheduler-border', '#4c566a');
            root.style.setProperty('--kendo-scheduler-header-bg', '#2e3440');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-scheduler-event-bg', '#5e81ac');
            root.style.setProperty('--kendo-scheduler-event-text', '#eceff4');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#2e3440');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#3b4252');
            root.style.setProperty('--kendo-gantt-text', '#eceff4');
            root.style.setProperty('--kendo-gantt-border', '#4c566a');
            root.style.setProperty('--kendo-gantt-header-bg', '#2e3440');
            root.style.setProperty('--kendo-gantt-alt-bg', '#353b49');
            root.style.setProperty('--kendo-gantt-task-bg', '#5e81ac');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#3b4252');
            root.style.setProperty('--kendo-panelbar-text', '#eceff4');
            root.style.setProperty('--kendo-panelbar-border', '#4c566a');
            root.style.setProperty('--kendo-panelbar-header-bg', '#2e3440');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#434c5e');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#3b4252');
            root.style.setProperty('--kendo-listview-text', '#eceff4');
            root.style.setProperty('--kendo-listview-border', '#4c566a');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#434c5e');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#5e81ac');
            root.style.setProperty('--kendo-listview-item-selected-text', '#eceff4');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#4c566a');
            root.style.setProperty('--kendo-slider-selection-bg', '#88c0d0');
            root.style.setProperty('--kendo-slider-handle-bg', '#eceff4');
            root.style.setProperty('--kendo-slider-handle-border', '#4c566a');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffffff');
        } else if (theme === 'sepia') {
            // ===== SEPIA THEME - Warm, paper-like tones for comfortable reading =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#f4ecd8');
            root.style.setProperty('--card-bg', '#faf6eb');
            root.style.setProperty('--border', '#d4c4a8');
            root.style.setProperty('--border-strong', '#bfae8e');
            root.style.setProperty('--text', '#5c4b37');
            root.style.setProperty('--muted', '#8b7355');
            root.style.setProperty('--row-hover', '#efe5d0');
            root.style.setProperty('--nav-bg', '#faf6eb');
            root.style.setProperty('--nav-border', '#d4c4a8');
            root.style.setProperty('--hover-bg', '#f5edd9');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#faf6eb');
            root.style.setProperty('--kendo-color-base-hover', '#f5edd9');
            root.style.setProperty('--kendo-color-base-active', '#efe5d0');
            root.style.setProperty('--kendo-color-on-base', '#5c4b37');
            root.style.setProperty('--kendo-color-surface', '#f4ecd8');
            root.style.setProperty('--kendo-color-surface-alt', '#faf6eb');
            root.style.setProperty('--kendo-color-border', '#d4c4a8');
            root.style.setProperty('--kendo-color-border-alt', '#bfae8e');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#5c4b37');
            root.style.setProperty('--kendo-color-subtle', '#8b7355');
            root.style.setProperty('--kendo-color-disabled', '#b8a890');

            // Component states - warm tones
            root.style.setProperty('--kendo-color-focus', '#a67c52');
            root.style.setProperty('--kendo-color-error', '#c25450');
            root.style.setProperty('--kendo-color-success', '#6b8e5e');
            root.style.setProperty('--kendo-color-warning', '#c9a227');
            root.style.setProperty('--kendo-color-info', '#6b8fb5');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#faf6eb');
            root.style.setProperty('--kendo-grid-text', '#5c4b37');
            root.style.setProperty('--kendo-grid-border', '#d4c4a8');
            root.style.setProperty('--kendo-grid-header-bg', '#efe5d0');
            root.style.setProperty('--kendo-grid-header-text', '#5c4b37');
            root.style.setProperty('--kendo-grid-header-border', '#d4c4a8');
            root.style.setProperty('--kendo-grid-footer-bg', '#efe5d0');
            root.style.setProperty('--kendo-grid-footer-text', '#5c4b37');
            root.style.setProperty('--kendo-grid-footer-border', '#d4c4a8');
            root.style.setProperty('--kendo-grid-alt-bg', '#f7f1e3');
            root.style.setProperty('--kendo-grid-hover-bg', '#f0e6d2');
            root.style.setProperty('--kendo-grid-selected-bg', '#d4b896');
            root.style.setProperty('--kendo-grid-selected-text', '#3d2e1f');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#efe5d0');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#faf6eb');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#faf6eb');
            root.style.setProperty('--kendo-button-text', '#5c4b37');
            root.style.setProperty('--kendo-button-border', '#d4c4a8');
            root.style.setProperty('--kendo-button-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-button-hover-border', '#bfae8e');
            root.style.setProperty('--kendo-button-active-bg', '#efe5d0');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(166, 124, 82, 0.4)');
            root.style.setProperty('--kendo-button-disabled-bg', '#faf6eb');
            root.style.setProperty('--kendo-button-disabled-text', '#b8a890');
            root.style.setProperty('--kendo-button-disabled-border', '#d4c4a8');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#faf6eb');
            root.style.setProperty('--kendo-input-text', '#5c4b37');
            root.style.setProperty('--kendo-input-border', '#d4c4a8');
            root.style.setProperty('--kendo-input-hover-border', '#bfae8e');
            root.style.setProperty('--kendo-input-focus-border', '#a67c52');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(166, 124, 82, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#f4ecd8');
            root.style.setProperty('--kendo-input-disabled-text', '#b8a890');
            root.style.setProperty('--kendo-input-placeholder-text', '#8b7355');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#faf6eb');
            root.style.setProperty('--kendo-picker-text', '#5c4b37');
            root.style.setProperty('--kendo-picker-border', '#d4c4a8');
            root.style.setProperty('--kendo-picker-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-picker-hover-border', '#bfae8e');
            root.style.setProperty('--kendo-picker-focus-border', '#a67c52');
            root.style.setProperty('--kendo-list-item-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-list-item-selected-bg', '#d4b896');
            root.style.setProperty('--kendo-list-item-selected-text', '#3d2e1f');
            root.style.setProperty('--kendo-list-item-focus-bg', '#efe5d0');
            root.style.setProperty('--kendo-popup-bg', '#faf6eb');
            root.style.setProperty('--kendo-popup-border', '#d4c4a8');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(92, 75, 55, 0.15)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#faf6eb');
            root.style.setProperty('--kendo-calendar-text', '#5c4b37');
            root.style.setProperty('--kendo-calendar-border', '#d4c4a8');
            root.style.setProperty('--kendo-calendar-header-bg', '#efe5d0');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#a67c52');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#ffffff');
            root.style.setProperty('--kendo-calendar-weekend-text', '#a67c52');
            root.style.setProperty('--kendo-calendar-other-month-text', '#b8a890');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#faf6eb');
            root.style.setProperty('--kendo-tabstrip-text', '#5c4b37');
            root.style.setProperty('--kendo-tabstrip-border', '#d4c4a8');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#8b7355');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#5c4b37');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#faf6eb');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#5c4b37');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#faf6eb');
            root.style.setProperty('--kendo-tabstrip-content-border', '#d4c4a8');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#faf6eb');
            root.style.setProperty('--kendo-window-text', '#5c4b37');
            root.style.setProperty('--kendo-window-border', '#d4c4a8');
            root.style.setProperty('--kendo-window-shadow', 'rgba(92, 75, 55, 0.2)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#efe5d0');
            root.style.setProperty('--kendo-window-titlebar-text', '#5c4b37');
            root.style.setProperty('--kendo-window-titlebar-border', '#d4c4a8');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#faf6eb');
            root.style.setProperty('--kendo-menu-text', '#5c4b37');
            root.style.setProperty('--kendo-menu-border', '#d4c4a8');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-menu-item-hover-text', '#5c4b37');
            root.style.setProperty('--kendo-menu-item-active-bg', '#efe5d0');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#b8a890');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#faf6eb');
            root.style.setProperty('--kendo-treeview-text', '#5c4b37');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#d4b896');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#3d2e1f');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#efe5d0');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#faf6eb');
            root.style.setProperty('--kendo-pager-text', '#5c4b37');
            root.style.setProperty('--kendo-pager-border', '#d4c4a8');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#a67c52');
            root.style.setProperty('--kendo-pager-item-selected-text', '#ffffff');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#faf6eb');
            root.style.setProperty('--kendo-toolbar-text', '#5c4b37');
            root.style.setProperty('--kendo-toolbar-border', '#d4c4a8');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#f5edd9');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#efe5d0');
            root.style.setProperty('--kendo-chip-text', '#5c4b37');
            root.style.setProperty('--kendo-chip-border', '#d4c4a8');
            root.style.setProperty('--kendo-chip-hover-bg', '#e5d9c0');
            root.style.setProperty('--kendo-chip-selected-bg', '#a67c52');
            root.style.setProperty('--kendo-chip-selected-text', '#ffffff');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#faf6eb');
            root.style.setProperty('--kendo-card-text', '#5c4b37');
            root.style.setProperty('--kendo-card-border', '#d4c4a8');
            root.style.setProperty('--kendo-card-shadow', 'rgba(92, 75, 55, 0.1)');
            root.style.setProperty('--kendo-card-header-bg', '#f7f1e3');
            root.style.setProperty('--kendo-card-header-border', '#d4c4a8');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#faf6eb');
            root.style.setProperty('--kendo-notification-text', '#5c4b37');
            root.style.setProperty('--kendo-notification-border', '#d4c4a8');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(92, 75, 55, 0.15)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#5c4b37');
            root.style.setProperty('--kendo-tooltip-text', '#faf6eb');
            root.style.setProperty('--kendo-tooltip-border', '#3d2e1f');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(92, 75, 55, 0.3)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#faf6eb');
            root.style.setProperty('--kendo-splitter-border', '#d4c4a8');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#efe5d0');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#e5d9c0');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#faf6eb');
            root.style.setProperty('--kendo-upload-text', '#5c4b37');
            root.style.setProperty('--kendo-upload-border', '#d4c4a8');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#f7f1e3');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#f0e6d2');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#faf6eb');
            root.style.setProperty('--kendo-editor-text', '#5c4b37');
            root.style.setProperty('--kendo-editor-border', '#d4c4a8');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#f7f1e3');
            root.style.setProperty('--kendo-editor-content-bg', '#faf6eb');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#faf6eb');
            root.style.setProperty('--kendo-scheduler-text', '#5c4b37');
            root.style.setProperty('--kendo-scheduler-border', '#d4c4a8');
            root.style.setProperty('--kendo-scheduler-header-bg', '#efe5d0');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-scheduler-event-bg', '#d4b896');
            root.style.setProperty('--kendo-scheduler-event-text', '#3d2e1f');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#f7f1e3');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#faf6eb');
            root.style.setProperty('--kendo-gantt-text', '#5c4b37');
            root.style.setProperty('--kendo-gantt-border', '#d4c4a8');
            root.style.setProperty('--kendo-gantt-header-bg', '#efe5d0');
            root.style.setProperty('--kendo-gantt-alt-bg', '#f7f1e3');
            root.style.setProperty('--kendo-gantt-task-bg', '#a67c52');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#faf6eb');
            root.style.setProperty('--kendo-panelbar-text', '#5c4b37');
            root.style.setProperty('--kendo-panelbar-border', '#d4c4a8');
            root.style.setProperty('--kendo-panelbar-header-bg', '#f7f1e3');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#f0e6d2');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#faf6eb');
            root.style.setProperty('--kendo-listview-text', '#5c4b37');
            root.style.setProperty('--kendo-listview-border', '#d4c4a8');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#f5edd9');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#d4b896');
            root.style.setProperty('--kendo-listview-item-selected-text', '#3d2e1f');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#d4c4a8');
            root.style.setProperty('--kendo-slider-selection-bg', '#a67c52');
            root.style.setProperty('--kendo-slider-handle-bg', '#faf6eb');
            root.style.setProperty('--kendo-slider-handle-border', '#bfae8e');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffffff');
        } else if (theme === 'midnight') {
            // ===== MIDNIGHT THEME - True OLED black for battery saving and pure dark =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#000000');
            root.style.setProperty('--card-bg', '#0a0a0a');
            root.style.setProperty('--border', '#1a1a1a');
            root.style.setProperty('--border-strong', '#2a2a2a');
            root.style.setProperty('--text', '#e4e4e7');
            root.style.setProperty('--muted', '#a1a1aa');
            root.style.setProperty('--row-hover', '#18181b');
            root.style.setProperty('--nav-bg', '#0a0a0a');
            root.style.setProperty('--nav-border', '#1a1a1a');
            root.style.setProperty('--hover-bg', '#18181b');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#0a0a0a');
            root.style.setProperty('--kendo-color-base-hover', '#18181b');
            root.style.setProperty('--kendo-color-base-active', '#27272a');
            root.style.setProperty('--kendo-color-on-base', '#e4e4e7');
            root.style.setProperty('--kendo-color-surface', '#000000');
            root.style.setProperty('--kendo-color-surface-alt', '#0a0a0a');
            root.style.setProperty('--kendo-color-border', '#1a1a1a');
            root.style.setProperty('--kendo-color-border-alt', '#2a2a2a');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#e4e4e7');
            root.style.setProperty('--kendo-color-subtle', '#a1a1aa');
            root.style.setProperty('--kendo-color-disabled', '#52525b');

            // Component states
            root.style.setProperty('--kendo-color-focus', '#60a5fa');
            root.style.setProperty('--kendo-color-error', '#f87171');
            root.style.setProperty('--kendo-color-success', '#4ade80');
            root.style.setProperty('--kendo-color-warning', '#fbbf24');
            root.style.setProperty('--kendo-color-info', '#38bdf8');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#0a0a0a');
            root.style.setProperty('--kendo-grid-text', '#e4e4e7');
            root.style.setProperty('--kendo-grid-border', '#1a1a1a');
            root.style.setProperty('--kendo-grid-header-bg', '#000000');
            root.style.setProperty('--kendo-grid-header-text', '#e4e4e7');
            root.style.setProperty('--kendo-grid-header-border', '#1a1a1a');
            root.style.setProperty('--kendo-grid-footer-bg', '#000000');
            root.style.setProperty('--kendo-grid-footer-text', '#e4e4e7');
            root.style.setProperty('--kendo-grid-footer-border', '#1a1a1a');
            root.style.setProperty('--kendo-grid-alt-bg', '#050505');
            root.style.setProperty('--kendo-grid-hover-bg', '#18181b');
            root.style.setProperty('--kendo-grid-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-grid-selected-text', '#e4e4e7');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#000000');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#0a0a0a');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#0a0a0a');
            root.style.setProperty('--kendo-button-text', '#e4e4e7');
            root.style.setProperty('--kendo-button-border', '#2a2a2a');
            root.style.setProperty('--kendo-button-hover-bg', '#18181b');
            root.style.setProperty('--kendo-button-hover-border', '#3f3f46');
            root.style.setProperty('--kendo-button-active-bg', '#27272a');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(96, 165, 250, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#0a0a0a');
            root.style.setProperty('--kendo-button-disabled-text', '#52525b');
            root.style.setProperty('--kendo-button-disabled-border', '#1a1a1a');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#0a0a0a');
            root.style.setProperty('--kendo-input-text', '#e4e4e7');
            root.style.setProperty('--kendo-input-border', '#2a2a2a');
            root.style.setProperty('--kendo-input-hover-border', '#3f3f46');
            root.style.setProperty('--kendo-input-focus-border', '#60a5fa');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(96, 165, 250, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#000000');
            root.style.setProperty('--kendo-input-disabled-text', '#52525b');
            root.style.setProperty('--kendo-input-placeholder-text', '#71717a');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#0a0a0a');
            root.style.setProperty('--kendo-picker-text', '#e4e4e7');
            root.style.setProperty('--kendo-picker-border', '#2a2a2a');
            root.style.setProperty('--kendo-picker-hover-bg', '#18181b');
            root.style.setProperty('--kendo-picker-hover-border', '#3f3f46');
            root.style.setProperty('--kendo-picker-focus-border', '#60a5fa');
            root.style.setProperty('--kendo-list-item-hover-bg', '#18181b');
            root.style.setProperty('--kendo-list-item-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-list-item-selected-text', '#e4e4e7');
            root.style.setProperty('--kendo-list-item-focus-bg', '#27272a');
            root.style.setProperty('--kendo-popup-bg', '#0a0a0a');
            root.style.setProperty('--kendo-popup-border', '#1a1a1a');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.8)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#0a0a0a');
            root.style.setProperty('--kendo-calendar-text', '#e4e4e7');
            root.style.setProperty('--kendo-calendar-border', '#1a1a1a');
            root.style.setProperty('--kendo-calendar-header-bg', '#000000');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#18181b');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#e4e4e7');
            root.style.setProperty('--kendo-calendar-weekend-text', '#a1a1aa');
            root.style.setProperty('--kendo-calendar-other-month-text', '#52525b');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#0a0a0a');
            root.style.setProperty('--kendo-tabstrip-text', '#e4e4e7');
            root.style.setProperty('--kendo-tabstrip-border', '#1a1a1a');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#a1a1aa');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#18181b');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#e4e4e7');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#0a0a0a');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#e4e4e7');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#0a0a0a');
            root.style.setProperty('--kendo-tabstrip-content-border', '#1a1a1a');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#0a0a0a');
            root.style.setProperty('--kendo-window-text', '#e4e4e7');
            root.style.setProperty('--kendo-window-border', '#1a1a1a');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.9)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#000000');
            root.style.setProperty('--kendo-window-titlebar-text', '#e4e4e7');
            root.style.setProperty('--kendo-window-titlebar-border', '#1a1a1a');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#0a0a0a');
            root.style.setProperty('--kendo-menu-text', '#e4e4e7');
            root.style.setProperty('--kendo-menu-border', '#1a1a1a');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#18181b');
            root.style.setProperty('--kendo-menu-item-hover-text', '#e4e4e7');
            root.style.setProperty('--kendo-menu-item-active-bg', '#27272a');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#52525b');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#0a0a0a');
            root.style.setProperty('--kendo-treeview-text', '#e4e4e7');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#18181b');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#e4e4e7');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#27272a');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#0a0a0a');
            root.style.setProperty('--kendo-pager-text', '#e4e4e7');
            root.style.setProperty('--kendo-pager-border', '#1a1a1a');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#18181b');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-pager-item-selected-text', '#e4e4e7');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#0a0a0a');
            root.style.setProperty('--kendo-toolbar-text', '#e4e4e7');
            root.style.setProperty('--kendo-toolbar-border', '#1a1a1a');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#18181b');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#18181b');
            root.style.setProperty('--kendo-chip-text', '#e4e4e7');
            root.style.setProperty('--kendo-chip-border', '#2a2a2a');
            root.style.setProperty('--kendo-chip-hover-bg', '#27272a');
            root.style.setProperty('--kendo-chip-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-chip-selected-text', '#e4e4e7');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#0a0a0a');
            root.style.setProperty('--kendo-card-text', '#e4e4e7');
            root.style.setProperty('--kendo-card-border', '#1a1a1a');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.5)');
            root.style.setProperty('--kendo-card-header-bg', '#000000');
            root.style.setProperty('--kendo-card-header-border', '#1a1a1a');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#0a0a0a');
            root.style.setProperty('--kendo-notification-text', '#e4e4e7');
            root.style.setProperty('--kendo-notification-border', '#1a1a1a');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.6)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#18181b');
            root.style.setProperty('--kendo-tooltip-text', '#e4e4e7');
            root.style.setProperty('--kendo-tooltip-border', '#2a2a2a');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.7)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#0a0a0a');
            root.style.setProperty('--kendo-splitter-border', '#1a1a1a');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#000000');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#18181b');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#0a0a0a');
            root.style.setProperty('--kendo-upload-text', '#e4e4e7');
            root.style.setProperty('--kendo-upload-border', '#1a1a1a');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#000000');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#18181b');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#0a0a0a');
            root.style.setProperty('--kendo-editor-text', '#e4e4e7');
            root.style.setProperty('--kendo-editor-border', '#1a1a1a');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#000000');
            root.style.setProperty('--kendo-editor-content-bg', '#0a0a0a');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#0a0a0a');
            root.style.setProperty('--kendo-scheduler-text', '#e4e4e7');
            root.style.setProperty('--kendo-scheduler-border', '#1a1a1a');
            root.style.setProperty('--kendo-scheduler-header-bg', '#000000');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#18181b');
            root.style.setProperty('--kendo-scheduler-event-bg', '#1e3a5f');
            root.style.setProperty('--kendo-scheduler-event-text', '#e4e4e7');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#050505');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#0a0a0a');
            root.style.setProperty('--kendo-gantt-text', '#e4e4e7');
            root.style.setProperty('--kendo-gantt-border', '#1a1a1a');
            root.style.setProperty('--kendo-gantt-header-bg', '#000000');
            root.style.setProperty('--kendo-gantt-alt-bg', '#050505');
            root.style.setProperty('--kendo-gantt-task-bg', '#1e3a5f');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#0a0a0a');
            root.style.setProperty('--kendo-panelbar-text', '#e4e4e7');
            root.style.setProperty('--kendo-panelbar-border', '#1a1a1a');
            root.style.setProperty('--kendo-panelbar-header-bg', '#000000');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#18181b');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#0a0a0a');
            root.style.setProperty('--kendo-listview-text', '#e4e4e7');
            root.style.setProperty('--kendo-listview-border', '#1a1a1a');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#18181b');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#1e3a5f');
            root.style.setProperty('--kendo-listview-item-selected-text', '#e4e4e7');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#2a2a2a');
            root.style.setProperty('--kendo-slider-selection-bg', '#60a5fa');
            root.style.setProperty('--kendo-slider-handle-bg', '#e4e4e7');
            root.style.setProperty('--kendo-slider-handle-border', '#2a2a2a');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffffff');
        }
        else if (theme === 'dracula') {
            // ===== DRACULA THEME - Popular dark theme with distinctive purple/pink/cyan palette =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#282a36');
            root.style.setProperty('--card-bg', '#343746');
            root.style.setProperty('--border', '#44475a');
            root.style.setProperty('--border-strong', '#6272a4');
            root.style.setProperty('--text', '#f8f8f2');
            root.style.setProperty('--muted', '#a0a0a6');
            root.style.setProperty('--row-hover', '#44475a');
            root.style.setProperty('--nav-bg', '#343746');
            root.style.setProperty('--nav-border', '#44475a');
            root.style.setProperty('--hover-bg', '#44475a');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#343746');
            root.style.setProperty('--kendo-color-base-hover', '#44475a');
            root.style.setProperty('--kendo-color-base-active', '#6272a4');
            root.style.setProperty('--kendo-color-on-base', '#f8f8f2');
            root.style.setProperty('--kendo-color-surface', '#282a36');
            root.style.setProperty('--kendo-color-surface-alt', '#343746');
            root.style.setProperty('--kendo-color-border', '#44475a');
            root.style.setProperty('--kendo-color-border-alt', '#6272a4');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#f8f8f2');
            root.style.setProperty('--kendo-color-subtle', '#a0a0a6');
            root.style.setProperty('--kendo-color-disabled', '#6272a4');

            // Component states - Dracula colors
            root.style.setProperty('--kendo-color-focus', '#bd93f9');
            root.style.setProperty('--kendo-color-error', '#ff5555');
            root.style.setProperty('--kendo-color-success', '#50fa7b');
            root.style.setProperty('--kendo-color-warning', '#ffb86c');
            root.style.setProperty('--kendo-color-info', '#8be9fd');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#343746');
            root.style.setProperty('--kendo-grid-text', '#f8f8f2');
            root.style.setProperty('--kendo-grid-border', '#44475a');
            root.style.setProperty('--kendo-grid-header-bg', '#282a36');
            root.style.setProperty('--kendo-grid-header-text', '#bd93f9');
            root.style.setProperty('--kendo-grid-header-border', '#44475a');
            root.style.setProperty('--kendo-grid-footer-bg', '#282a36');
            root.style.setProperty('--kendo-grid-footer-text', '#f8f8f2');
            root.style.setProperty('--kendo-grid-footer-border', '#44475a');
            root.style.setProperty('--kendo-grid-alt-bg', '#2d2f3a');
            root.style.setProperty('--kendo-grid-hover-bg', '#44475a');
            root.style.setProperty('--kendo-grid-selected-bg', '#bd93f9');
            root.style.setProperty('--kendo-grid-selected-text', '#282a36');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#282a36');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#343746');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#343746');
            root.style.setProperty('--kendo-button-text', '#f8f8f2');
            root.style.setProperty('--kendo-button-border', '#44475a');
            root.style.setProperty('--kendo-button-hover-bg', '#44475a');
            root.style.setProperty('--kendo-button-hover-border', '#6272a4');
            root.style.setProperty('--kendo-button-active-bg', '#6272a4');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(189, 147, 249, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#343746');
            root.style.setProperty('--kendo-button-disabled-text', '#6272a4');
            root.style.setProperty('--kendo-button-disabled-border', '#44475a');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#343746');
            root.style.setProperty('--kendo-input-text', '#f8f8f2');
            root.style.setProperty('--kendo-input-border', '#44475a');
            root.style.setProperty('--kendo-input-hover-border', '#6272a4');
            root.style.setProperty('--kendo-input-focus-border', '#bd93f9');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(189, 147, 249, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#282a36');
            root.style.setProperty('--kendo-input-disabled-text', '#6272a4');
            root.style.setProperty('--kendo-input-placeholder-text', '#a0a0a6');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#343746');
            root.style.setProperty('--kendo-picker-text', '#f8f8f2');
            root.style.setProperty('--kendo-picker-border', '#44475a');
            root.style.setProperty('--kendo-picker-hover-bg', '#44475a');
            root.style.setProperty('--kendo-picker-hover-border', '#6272a4');
            root.style.setProperty('--kendo-picker-focus-border', '#bd93f9');
            root.style.setProperty('--kendo-list-item-hover-bg', '#44475a');
            root.style.setProperty('--kendo-list-item-selected-bg', '#bd93f9');
            root.style.setProperty('--kendo-list-item-selected-text', '#282a36');
            root.style.setProperty('--kendo-list-item-focus-bg', '#6272a4');
            root.style.setProperty('--kendo-popup-bg', '#343746');
            root.style.setProperty('--kendo-popup-border', '#44475a');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.4)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#343746');
            root.style.setProperty('--kendo-calendar-text', '#f8f8f2');
            root.style.setProperty('--kendo-calendar-border', '#44475a');
            root.style.setProperty('--kendo-calendar-header-bg', '#282a36');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#44475a');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#bd93f9');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#282a36');
            root.style.setProperty('--kendo-calendar-weekend-text', '#ff79c6');
            root.style.setProperty('--kendo-calendar-other-month-text', '#6272a4');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#343746');
            root.style.setProperty('--kendo-tabstrip-text', '#f8f8f2');
            root.style.setProperty('--kendo-tabstrip-border', '#44475a');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#a0a0a6');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#44475a');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#f8f8f2');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#343746');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#bd93f9');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#343746');
            root.style.setProperty('--kendo-tabstrip-content-border', '#44475a');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#343746');
            root.style.setProperty('--kendo-window-text', '#f8f8f2');
            root.style.setProperty('--kendo-window-border', '#44475a');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.5)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#282a36');
            root.style.setProperty('--kendo-window-titlebar-text', '#ff79c6');
            root.style.setProperty('--kendo-window-titlebar-border', '#44475a');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#343746');
            root.style.setProperty('--kendo-menu-text', '#f8f8f2');
            root.style.setProperty('--kendo-menu-border', '#44475a');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#44475a');
            root.style.setProperty('--kendo-menu-item-hover-text', '#f8f8f2');
            root.style.setProperty('--kendo-menu-item-active-bg', '#6272a4');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#6272a4');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#343746');
            root.style.setProperty('--kendo-treeview-text', '#f8f8f2');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#44475a');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#bd93f9');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#282a36');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#6272a4');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#343746');
            root.style.setProperty('--kendo-pager-text', '#f8f8f2');
            root.style.setProperty('--kendo-pager-border', '#44475a');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#44475a');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#bd93f9');
            root.style.setProperty('--kendo-pager-item-selected-text', '#282a36');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#343746');
            root.style.setProperty('--kendo-toolbar-text', '#f8f8f2');
            root.style.setProperty('--kendo-toolbar-border', '#44475a');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#44475a');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#44475a');
            root.style.setProperty('--kendo-chip-text', '#f8f8f2');
            root.style.setProperty('--kendo-chip-border', '#6272a4');
            root.style.setProperty('--kendo-chip-hover-bg', '#6272a4');
            root.style.setProperty('--kendo-chip-selected-bg', '#bd93f9');
            root.style.setProperty('--kendo-chip-selected-text', '#282a36');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#343746');
            root.style.setProperty('--kendo-card-text', '#f8f8f2');
            root.style.setProperty('--kendo-card-border', '#44475a');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.3)');
            root.style.setProperty('--kendo-card-header-bg', '#282a36');
            root.style.setProperty('--kendo-card-header-border', '#44475a');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#343746');
            root.style.setProperty('--kendo-notification-text', '#f8f8f2');
            root.style.setProperty('--kendo-notification-border', '#44475a');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.4)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#282a36');
            root.style.setProperty('--kendo-tooltip-text', '#f8f8f2');
            root.style.setProperty('--kendo-tooltip-border', '#44475a');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.5)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#343746');
            root.style.setProperty('--kendo-splitter-border', '#44475a');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#282a36');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#44475a');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#343746');
            root.style.setProperty('--kendo-upload-text', '#f8f8f2');
            root.style.setProperty('--kendo-upload-border', '#44475a');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#282a36');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#44475a');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#343746');
            root.style.setProperty('--kendo-editor-text', '#f8f8f2');
            root.style.setProperty('--kendo-editor-border', '#44475a');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#282a36');
            root.style.setProperty('--kendo-editor-content-bg', '#343746');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#343746');
            root.style.setProperty('--kendo-scheduler-text', '#f8f8f2');
            root.style.setProperty('--kendo-scheduler-border', '#44475a');
            root.style.setProperty('--kendo-scheduler-header-bg', '#282a36');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#44475a');
            root.style.setProperty('--kendo-scheduler-event-bg', '#bd93f9');
            root.style.setProperty('--kendo-scheduler-event-text', '#282a36');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#282a36');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#343746');
            root.style.setProperty('--kendo-gantt-text', '#f8f8f2');
            root.style.setProperty('--kendo-gantt-border', '#44475a');
            root.style.setProperty('--kendo-gantt-header-bg', '#282a36');
            root.style.setProperty('--kendo-gantt-alt-bg', '#2d2f3a');
            root.style.setProperty('--kendo-gantt-task-bg', '#bd93f9');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#343746');
            root.style.setProperty('--kendo-panelbar-text', '#f8f8f2');
            root.style.setProperty('--kendo-panelbar-border', '#44475a');
            root.style.setProperty('--kendo-panelbar-header-bg', '#282a36');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#44475a');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#343746');
            root.style.setProperty('--kendo-listview-text', '#f8f8f2');
            root.style.setProperty('--kendo-listview-border', '#44475a');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#44475a');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#bd93f9');
            root.style.setProperty('--kendo-listview-item-selected-text', '#282a36');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#44475a');
            root.style.setProperty('--kendo-slider-selection-bg', '#bd93f9');
            root.style.setProperty('--kendo-slider-handle-bg', '#f8f8f2');
            root.style.setProperty('--kendo-slider-handle-border', '#44475a');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffffff');
        } else if (theme === 'solarized-light') {
            // ===== SOLARIZED LIGHT THEME - Scientifically designed for reduced eye strain =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#fdf6e3');
            root.style.setProperty('--card-bg', '#eee8d5');
            root.style.setProperty('--border', '#93a1a1');
            root.style.setProperty('--border-strong', '#839496');
            root.style.setProperty('--text', '#657b83');
            root.style.setProperty('--muted', '#93a1a1');
            root.style.setProperty('--row-hover', '#eee8d5');
            root.style.setProperty('--nav-bg', '#eee8d5');
            root.style.setProperty('--nav-border', '#93a1a1');
            root.style.setProperty('--hover-bg', '#eee8d5');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#eee8d5');
            root.style.setProperty('--kendo-color-base-hover', '#e4ddc4');
            root.style.setProperty('--kendo-color-base-active', '#d9d2b3');
            root.style.setProperty('--kendo-color-on-base', '#657b83');
            root.style.setProperty('--kendo-color-surface', '#fdf6e3');
            root.style.setProperty('--kendo-color-surface-alt', '#eee8d5');
            root.style.setProperty('--kendo-color-border', '#93a1a1');
            root.style.setProperty('--kendo-color-border-alt', '#839496');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#657b83');
            root.style.setProperty('--kendo-color-subtle', '#93a1a1');
            root.style.setProperty('--kendo-color-disabled', '#93a1a1');

            // Component states - Solarized accent colors
            root.style.setProperty('--kendo-color-focus', '#268bd2');
            root.style.setProperty('--kendo-color-error', '#dc322f');
            root.style.setProperty('--kendo-color-success', '#859900');
            root.style.setProperty('--kendo-color-warning', '#b58900');
            root.style.setProperty('--kendo-color-info', '#268bd2');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#eee8d5');
            root.style.setProperty('--kendo-grid-text', '#657b83');
            root.style.setProperty('--kendo-grid-border', '#93a1a1');
            root.style.setProperty('--kendo-grid-header-bg', '#fdf6e3');
            root.style.setProperty('--kendo-grid-header-text', '#586e75');
            root.style.setProperty('--kendo-grid-header-border', '#93a1a1');
            root.style.setProperty('--kendo-grid-footer-bg', '#fdf6e3');
            root.style.setProperty('--kendo-grid-footer-text', '#657b83');
            root.style.setProperty('--kendo-grid-footer-border', '#93a1a1');
            root.style.setProperty('--kendo-grid-alt-bg', '#f7f0d7');
            root.style.setProperty('--kendo-grid-hover-bg', '#e4ddc4');
            root.style.setProperty('--kendo-grid-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-grid-selected-text', '#fdf6e3');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#fdf6e3');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#eee8d5');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#eee8d5');
            root.style.setProperty('--kendo-button-text', '#657b83');
            root.style.setProperty('--kendo-button-border', '#93a1a1');
            root.style.setProperty('--kendo-button-hover-bg', '#e4ddc4');
            root.style.setProperty('--kendo-button-hover-border', '#839496');
            root.style.setProperty('--kendo-button-active-bg', '#d9d2b3');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(38, 139, 210, 0.4)');
            root.style.setProperty('--kendo-button-disabled-bg', '#eee8d5');
            root.style.setProperty('--kendo-button-disabled-text', '#93a1a1');
            root.style.setProperty('--kendo-button-disabled-border', '#93a1a1');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#fdf6e3');
            root.style.setProperty('--kendo-input-text', '#657b83');
            root.style.setProperty('--kendo-input-border', '#93a1a1');
            root.style.setProperty('--kendo-input-hover-border', '#839496');
            root.style.setProperty('--kendo-input-focus-border', '#268bd2');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(38, 139, 210, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#eee8d5');
            root.style.setProperty('--kendo-input-disabled-text', '#93a1a1');
            root.style.setProperty('--kendo-input-placeholder-text', '#93a1a1');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#fdf6e3');
            root.style.setProperty('--kendo-picker-text', '#657b83');
            root.style.setProperty('--kendo-picker-border', '#93a1a1');
            root.style.setProperty('--kendo-picker-hover-bg', '#eee8d5');
            root.style.setProperty('--kendo-picker-hover-border', '#839496');
            root.style.setProperty('--kendo-picker-focus-border', '#268bd2');
            root.style.setProperty('--kendo-list-item-hover-bg', '#eee8d5');
            root.style.setProperty('--kendo-list-item-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-list-item-selected-text', '#fdf6e3');
            root.style.setProperty('--kendo-list-item-focus-bg', '#e4ddc4');
            root.style.setProperty('--kendo-popup-bg', '#fdf6e3');
            root.style.setProperty('--kendo-popup-border', '#93a1a1');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.1)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#fdf6e3');
            root.style.setProperty('--kendo-calendar-text', '#657b83');
            root.style.setProperty('--kendo-calendar-border', '#93a1a1');
            root.style.setProperty('--kendo-calendar-header-bg', '#eee8d5');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#eee8d5');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#fdf6e3');
            root.style.setProperty('--kendo-calendar-weekend-text', '#2aa198');
            root.style.setProperty('--kendo-calendar-other-month-text', '#93a1a1');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#eee8d5');
            root.style.setProperty('--kendo-tabstrip-text', '#657b83');
            root.style.setProperty('--kendo-tabstrip-border', '#93a1a1');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#93a1a1');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#e4ddc4');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#657b83');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#eee8d5');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#268bd2');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#eee8d5');
            root.style.setProperty('--kendo-tabstrip-content-border', '#93a1a1');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#eee8d5');
            root.style.setProperty('--kendo-window-text', '#657b83');
            root.style.setProperty('--kendo-window-border', '#93a1a1');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.15)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#fdf6e3');
            root.style.setProperty('--kendo-window-titlebar-text', '#586e75');
            root.style.setProperty('--kendo-window-titlebar-border', '#93a1a1');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#eee8d5');
            root.style.setProperty('--kendo-menu-text', '#657b83');
            root.style.setProperty('--kendo-menu-border', '#93a1a1');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#e4ddc4');
            root.style.setProperty('--kendo-menu-item-hover-text', '#657b83');
            root.style.setProperty('--kendo-menu-item-active-bg', '#d9d2b3');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#93a1a1');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#eee8d5');
            root.style.setProperty('--kendo-treeview-text', '#657b83');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#e4ddc4');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#fdf6e3');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#d9d2b3');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#eee8d5');
            root.style.setProperty('--kendo-pager-text', '#657b83');
            root.style.setProperty('--kendo-pager-border', '#93a1a1');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#e4ddc4');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-pager-item-selected-text', '#fdf6e3');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#eee8d5');
            root.style.setProperty('--kendo-toolbar-text', '#657b83');
            root.style.setProperty('--kendo-toolbar-border', '#93a1a1');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#e4ddc4');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#e4ddc4');
            root.style.setProperty('--kendo-chip-text', '#657b83');
            root.style.setProperty('--kendo-chip-border', '#93a1a1');
            root.style.setProperty('--kendo-chip-hover-bg', '#d9d2b3');
            root.style.setProperty('--kendo-chip-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-chip-selected-text', '#fdf6e3');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#eee8d5');
            root.style.setProperty('--kendo-card-text', '#657b83');
            root.style.setProperty('--kendo-card-border', '#93a1a1');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.08)');
            root.style.setProperty('--kendo-card-header-bg', '#fdf6e3');
            root.style.setProperty('--kendo-card-header-border', '#93a1a1');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#eee8d5');
            root.style.setProperty('--kendo-notification-text', '#657b83');
            root.style.setProperty('--kendo-notification-border', '#93a1a1');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.12)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#586e75');
            root.style.setProperty('--kendo-tooltip-text', '#fdf6e3');
            root.style.setProperty('--kendo-tooltip-border', '#657b83');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.2)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#eee8d5');
            root.style.setProperty('--kendo-splitter-border', '#93a1a1');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#fdf6e3');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#e4ddc4');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#eee8d5');
            root.style.setProperty('--kendo-upload-text', '#657b83');
            root.style.setProperty('--kendo-upload-border', '#93a1a1');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#fdf6e3');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#e4ddc4');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#eee8d5');
            root.style.setProperty('--kendo-editor-text', '#657b83');
            root.style.setProperty('--kendo-editor-border', '#93a1a1');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#fdf6e3');
            root.style.setProperty('--kendo-editor-content-bg', '#eee8d5');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#eee8d5');
            root.style.setProperty('--kendo-scheduler-text', '#657b83');
            root.style.setProperty('--kendo-scheduler-border', '#93a1a1');
            root.style.setProperty('--kendo-scheduler-header-bg', '#fdf6e3');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#e4ddc4');
            root.style.setProperty('--kendo-scheduler-event-bg', '#268bd2');
            root.style.setProperty('--kendo-scheduler-event-text', '#fdf6e3');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#fdf6e3');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#eee8d5');
            root.style.setProperty('--kendo-gantt-text', '#657b83');
            root.style.setProperty('--kendo-gantt-border', '#93a1a1');
            root.style.setProperty('--kendo-gantt-header-bg', '#fdf6e3');
            root.style.setProperty('--kendo-gantt-alt-bg', '#f7f0d7');
            root.style.setProperty('--kendo-gantt-task-bg', '#268bd2');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#eee8d5');
            root.style.setProperty('--kendo-panelbar-text', '#657b83');
            root.style.setProperty('--kendo-panelbar-border', '#93a1a1');
            root.style.setProperty('--kendo-panelbar-header-bg', '#fdf6e3');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#e4ddc4');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#eee8d5');
            root.style.setProperty('--kendo-listview-text', '#657b83');
            root.style.setProperty('--kendo-listview-border', '#93a1a1');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#e4ddc4');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-listview-item-selected-text', '#fdf6e3');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#93a1a1');
            root.style.setProperty('--kendo-slider-selection-bg', '#268bd2');
            root.style.setProperty('--kendo-slider-handle-bg', '#fdf6e3');
            root.style.setProperty('--kendo-slider-handle-border', '#93a1a1');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#eee8d5');
        } else if (theme === 'solarized-dark') {
            // ===== SOLARIZED DARK THEME - Dark variant with scientifically designed colors =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#002b36');
            root.style.setProperty('--card-bg', '#073642');
            root.style.setProperty('--border', '#586e75');
            root.style.setProperty('--border-strong', '#657b83');
            root.style.setProperty('--text', '#839496');
            root.style.setProperty('--muted', '#657b83');
            root.style.setProperty('--row-hover', '#073642');
            root.style.setProperty('--nav-bg', '#073642');
            root.style.setProperty('--nav-border', '#586e75');
            root.style.setProperty('--hover-bg', '#073642');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#073642');
            root.style.setProperty('--kendo-color-base-hover', '#094150');
            root.style.setProperty('--kendo-color-base-active', '#0e4c5e');
            root.style.setProperty('--kendo-color-on-base', '#839496');
            root.style.setProperty('--kendo-color-surface', '#002b36');
            root.style.setProperty('--kendo-color-surface-alt', '#073642');
            root.style.setProperty('--kendo-color-border', '#586e75');
            root.style.setProperty('--kendo-color-border-alt', '#657b83');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#839496');
            root.style.setProperty('--kendo-color-subtle', '#657b83');
            root.style.setProperty('--kendo-color-disabled', '#586e75');

            // Component states - Solarized accent colors
            root.style.setProperty('--kendo-color-focus', '#268bd2');
            root.style.setProperty('--kendo-color-error', '#dc322f');
            root.style.setProperty('--kendo-color-success', '#859900');
            root.style.setProperty('--kendo-color-warning', '#b58900');
            root.style.setProperty('--kendo-color-info', '#268bd2');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#073642');
            root.style.setProperty('--kendo-grid-text', '#839496');
            root.style.setProperty('--kendo-grid-border', '#586e75');
            root.style.setProperty('--kendo-grid-header-bg', '#002b36');
            root.style.setProperty('--kendo-grid-header-text', '#93a1a1');
            root.style.setProperty('--kendo-grid-header-border', '#586e75');
            root.style.setProperty('--kendo-grid-footer-bg', '#002b36');
            root.style.setProperty('--kendo-grid-footer-text', '#839496');
            root.style.setProperty('--kendo-grid-footer-border', '#586e75');
            root.style.setProperty('--kendo-grid-alt-bg', '#03313d');
            root.style.setProperty('--kendo-grid-hover-bg', '#094150');
            root.style.setProperty('--kendo-grid-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-grid-selected-text', '#002b36');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#002b36');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#073642');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#073642');
            root.style.setProperty('--kendo-button-text', '#839496');
            root.style.setProperty('--kendo-button-border', '#586e75');
            root.style.setProperty('--kendo-button-hover-bg', '#094150');
            root.style.setProperty('--kendo-button-hover-border', '#657b83');
            root.style.setProperty('--kendo-button-active-bg', '#0e4c5e');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(38, 139, 210, 0.4)');
            root.style.setProperty('--kendo-button-disabled-bg', '#073642');
            root.style.setProperty('--kendo-button-disabled-text', '#586e75');
            root.style.setProperty('--kendo-button-disabled-border', '#586e75');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#073642');
            root.style.setProperty('--kendo-input-text', '#839496');
            root.style.setProperty('--kendo-input-border', '#586e75');
            root.style.setProperty('--kendo-input-hover-border', '#657b83');
            root.style.setProperty('--kendo-input-focus-border', '#268bd2');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(38, 139, 210, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#002b36');
            root.style.setProperty('--kendo-input-disabled-text', '#586e75');
            root.style.setProperty('--kendo-input-placeholder-text', '#657b83');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#073642');
            root.style.setProperty('--kendo-picker-text', '#839496');
            root.style.setProperty('--kendo-picker-border', '#586e75');
            root.style.setProperty('--kendo-picker-hover-bg', '#094150');
            root.style.setProperty('--kendo-picker-hover-border', '#657b83');
            root.style.setProperty('--kendo-picker-focus-border', '#268bd2');
            root.style.setProperty('--kendo-list-item-hover-bg', '#094150');
            root.style.setProperty('--kendo-list-item-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-list-item-selected-text', '#002b36');
            root.style.setProperty('--kendo-list-item-focus-bg', '#0e4c5e');
            root.style.setProperty('--kendo-popup-bg', '#073642');
            root.style.setProperty('--kendo-popup-border', '#586e75');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#073642');
            root.style.setProperty('--kendo-calendar-text', '#839496');
            root.style.setProperty('--kendo-calendar-border', '#586e75');
            root.style.setProperty('--kendo-calendar-header-bg', '#002b36');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#094150');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#002b36');
            root.style.setProperty('--kendo-calendar-weekend-text', '#2aa198');
            root.style.setProperty('--kendo-calendar-other-month-text', '#586e75');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#073642');
            root.style.setProperty('--kendo-tabstrip-text', '#839496');
            root.style.setProperty('--kendo-tabstrip-border', '#586e75');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#657b83');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#094150');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#839496');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#073642');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#268bd2');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#073642');
            root.style.setProperty('--kendo-tabstrip-content-border', '#586e75');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#073642');
            root.style.setProperty('--kendo-window-text', '#839496');
            root.style.setProperty('--kendo-window-border', '#586e75');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.4)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#002b36');
            root.style.setProperty('--kendo-window-titlebar-text', '#93a1a1');
            root.style.setProperty('--kendo-window-titlebar-border', '#586e75');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#073642');
            root.style.setProperty('--kendo-menu-text', '#839496');
            root.style.setProperty('--kendo-menu-border', '#586e75');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#094150');
            root.style.setProperty('--kendo-menu-item-hover-text', '#839496');
            root.style.setProperty('--kendo-menu-item-active-bg', '#0e4c5e');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#586e75');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#073642');
            root.style.setProperty('--kendo-treeview-text', '#839496');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#094150');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#002b36');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#0e4c5e');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#073642');
            root.style.setProperty('--kendo-pager-text', '#839496');
            root.style.setProperty('--kendo-pager-border', '#586e75');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#094150');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-pager-item-selected-text', '#002b36');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#073642');
            root.style.setProperty('--kendo-toolbar-text', '#839496');
            root.style.setProperty('--kendo-toolbar-border', '#586e75');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#094150');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#094150');
            root.style.setProperty('--kendo-chip-text', '#839496');
            root.style.setProperty('--kendo-chip-border', '#586e75');
            root.style.setProperty('--kendo-chip-hover-bg', '#0e4c5e');
            root.style.setProperty('--kendo-chip-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-chip-selected-text', '#002b36');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#073642');
            root.style.setProperty('--kendo-card-text', '#839496');
            root.style.setProperty('--kendo-card-border', '#586e75');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.2)');
            root.style.setProperty('--kendo-card-header-bg', '#002b36');
            root.style.setProperty('--kendo-card-header-border', '#586e75');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#073642');
            root.style.setProperty('--kendo-notification-text', '#839496');
            root.style.setProperty('--kendo-notification-border', '#586e75');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#002b36');
            root.style.setProperty('--kendo-tooltip-text', '#839496');
            root.style.setProperty('--kendo-tooltip-border', '#586e75');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.4)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#073642');
            root.style.setProperty('--kendo-splitter-border', '#586e75');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#002b36');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#094150');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#073642');
            root.style.setProperty('--kendo-upload-text', '#839496');
            root.style.setProperty('--kendo-upload-border', '#586e75');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#002b36');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#094150');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#073642');
            root.style.setProperty('--kendo-editor-text', '#839496');
            root.style.setProperty('--kendo-editor-border', '#586e75');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#002b36');
            root.style.setProperty('--kendo-editor-content-bg', '#073642');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#073642');
            root.style.setProperty('--kendo-scheduler-text', '#839496');
            root.style.setProperty('--kendo-scheduler-border', '#586e75');
            root.style.setProperty('--kendo-scheduler-header-bg', '#002b36');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#094150');
            root.style.setProperty('--kendo-scheduler-event-bg', '#268bd2');
            root.style.setProperty('--kendo-scheduler-event-text', '#002b36');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#002b36');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#073642');
            root.style.setProperty('--kendo-gantt-text', '#839496');
            root.style.setProperty('--kendo-gantt-border', '#586e75');
            root.style.setProperty('--kendo-gantt-header-bg', '#002b36');
            root.style.setProperty('--kendo-gantt-alt-bg', '#03313d');
            root.style.setProperty('--kendo-gantt-task-bg', '#268bd2');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#073642');
            root.style.setProperty('--kendo-panelbar-text', '#839496');
            root.style.setProperty('--kendo-panelbar-border', '#586e75');
            root.style.setProperty('--kendo-panelbar-header-bg', '#002b36');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#094150');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#073642');
            root.style.setProperty('--kendo-listview-text', '#839496');
            root.style.setProperty('--kendo-listview-border', '#586e75');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#094150');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#268bd2');
            root.style.setProperty('--kendo-listview-item-selected-text', '#002b36');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#586e75');
            root.style.setProperty('--kendo-slider-selection-bg', '#268bd2');
            root.style.setProperty('--kendo-slider-handle-bg', '#839496');
            root.style.setProperty('--kendo-slider-handle-border', '#586e75');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#93a1a1');
        } else if (theme === 'monochrome') {
            // ===== MONOCHROME THEME - Pure grayscale for zero color distraction =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#ffffff');
            root.style.setProperty('--card-bg', '#ffffff');
            root.style.setProperty('--border', '#d0d0d0');
            root.style.setProperty('--border-strong', '#a0a0a0');
            root.style.setProperty('--text', '#1a1a1a');
            root.style.setProperty('--muted', '#6a6a6a');
            root.style.setProperty('--row-hover', '#f5f5f5');
            root.style.setProperty('--nav-bg', '#ffffff');
            root.style.setProperty('--nav-border', '#d0d0d0');
            root.style.setProperty('--hover-bg', '#f5f5f5');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#ffffff');
            root.style.setProperty('--kendo-color-base-hover', '#f5f5f5');
            root.style.setProperty('--kendo-color-base-active', '#e8e8e8');
            root.style.setProperty('--kendo-color-on-base', '#1a1a1a');
            root.style.setProperty('--kendo-color-surface', '#ffffff');
            root.style.setProperty('--kendo-color-surface-alt', '#fafafa');
            root.style.setProperty('--kendo-color-border', '#d0d0d0');
            root.style.setProperty('--kendo-color-border-alt', '#a0a0a0');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#1a1a1a');
            root.style.setProperty('--kendo-color-subtle', '#6a6a6a');
            root.style.setProperty('--kendo-color-disabled', '#a0a0a0');

            // Component states - grayscale only
            root.style.setProperty('--kendo-color-focus', '#4a4a4a');
            root.style.setProperty('--kendo-color-error', '#3a3a3a');
            root.style.setProperty('--kendo-color-success', '#5a5a5a');
            root.style.setProperty('--kendo-color-warning', '#6a6a6a');
            root.style.setProperty('--kendo-color-info', '#4a4a4a');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#ffffff');
            root.style.setProperty('--kendo-grid-text', '#1a1a1a');
            root.style.setProperty('--kendo-grid-border', '#d0d0d0');
            root.style.setProperty('--kendo-grid-header-bg', '#f0f0f0');
            root.style.setProperty('--kendo-grid-header-text', '#1a1a1a');
            root.style.setProperty('--kendo-grid-header-border', '#d0d0d0');
            root.style.setProperty('--kendo-grid-footer-bg', '#f0f0f0');
            root.style.setProperty('--kendo-grid-footer-text', '#1a1a1a');
            root.style.setProperty('--kendo-grid-footer-border', '#d0d0d0');
            root.style.setProperty('--kendo-grid-alt-bg', '#fafafa');
            root.style.setProperty('--kendo-grid-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-grid-selected-bg', '#e0e0e0');
            root.style.setProperty('--kendo-grid-selected-text', '#1a1a1a');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#f0f0f0');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#ffffff');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#ffffff');
            root.style.setProperty('--kendo-button-text', '#1a1a1a');
            root.style.setProperty('--kendo-button-border', '#d0d0d0');
            root.style.setProperty('--kendo-button-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-button-hover-border', '#a0a0a0');
            root.style.setProperty('--kendo-button-active-bg', '#e8e8e8');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(74, 74, 74, 0.3)');
            root.style.setProperty('--kendo-button-disabled-bg', '#fafafa');
            root.style.setProperty('--kendo-button-disabled-text', '#a0a0a0');
            root.style.setProperty('--kendo-button-disabled-border', '#d0d0d0');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#ffffff');
            root.style.setProperty('--kendo-input-text', '#1a1a1a');
            root.style.setProperty('--kendo-input-border', '#d0d0d0');
            root.style.setProperty('--kendo-input-hover-border', '#a0a0a0');
            root.style.setProperty('--kendo-input-focus-border', '#4a4a4a');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(74, 74, 74, 0.2)');
            root.style.setProperty('--kendo-input-disabled-bg', '#fafafa');
            root.style.setProperty('--kendo-input-disabled-text', '#a0a0a0');
            root.style.setProperty('--kendo-input-placeholder-text', '#6a6a6a');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#ffffff');
            root.style.setProperty('--kendo-picker-text', '#1a1a1a');
            root.style.setProperty('--kendo-picker-border', '#d0d0d0');
            root.style.setProperty('--kendo-picker-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-picker-hover-border', '#a0a0a0');
            root.style.setProperty('--kendo-picker-focus-border', '#4a4a4a');
            root.style.setProperty('--kendo-list-item-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-list-item-selected-bg', '#e0e0e0');
            root.style.setProperty('--kendo-list-item-selected-text', '#1a1a1a');
            root.style.setProperty('--kendo-list-item-focus-bg', '#e8e8e8');
            root.style.setProperty('--kendo-popup-bg', '#ffffff');
            root.style.setProperty('--kendo-popup-border', '#d0d0d0');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.1)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#ffffff');
            root.style.setProperty('--kendo-calendar-text', '#1a1a1a');
            root.style.setProperty('--kendo-calendar-border', '#d0d0d0');
            root.style.setProperty('--kendo-calendar-header-bg', '#f0f0f0');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#6a6a6a');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#ffffff');
            root.style.setProperty('--kendo-calendar-weekend-text', '#6a6a6a');
            root.style.setProperty('--kendo-calendar-other-month-text', '#a0a0a0');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-text', '#1a1a1a');
            root.style.setProperty('--kendo-tabstrip-border', '#d0d0d0');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#6a6a6a');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#1a1a1a');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#1a1a1a');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-content-border', '#d0d0d0');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#ffffff');
            root.style.setProperty('--kendo-window-text', '#1a1a1a');
            root.style.setProperty('--kendo-window-border', '#d0d0d0');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.15)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#f0f0f0');
            root.style.setProperty('--kendo-window-titlebar-text', '#1a1a1a');
            root.style.setProperty('--kendo-window-titlebar-border', '#d0d0d0');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#ffffff');
            root.style.setProperty('--kendo-menu-text', '#1a1a1a');
            root.style.setProperty('--kendo-menu-border', '#d0d0d0');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-menu-item-hover-text', '#1a1a1a');
            root.style.setProperty('--kendo-menu-item-active-bg', '#e8e8e8');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#a0a0a0');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#ffffff');
            root.style.setProperty('--kendo-treeview-text', '#1a1a1a');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#e0e0e0');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#1a1a1a');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#e8e8e8');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#ffffff');
            root.style.setProperty('--kendo-pager-text', '#1a1a1a');
            root.style.setProperty('--kendo-pager-border', '#d0d0d0');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#6a6a6a');
            root.style.setProperty('--kendo-pager-item-selected-text', '#ffffff');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#ffffff');
            root.style.setProperty('--kendo-toolbar-text', '#1a1a1a');
            root.style.setProperty('--kendo-toolbar-border', '#d0d0d0');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#f5f5f5');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#f0f0f0');
            root.style.setProperty('--kendo-chip-text', '#1a1a1a');
            root.style.setProperty('--kendo-chip-border', '#d0d0d0');
            root.style.setProperty('--kendo-chip-hover-bg', '#e8e8e8');
            root.style.setProperty('--kendo-chip-selected-bg', '#6a6a6a');
            root.style.setProperty('--kendo-chip-selected-text', '#ffffff');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#ffffff');
            root.style.setProperty('--kendo-card-text', '#1a1a1a');
            root.style.setProperty('--kendo-card-border', '#d0d0d0');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.08)');
            root.style.setProperty('--kendo-card-header-bg', '#fafafa');
            root.style.setProperty('--kendo-card-header-border', '#d0d0d0');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#ffffff');
            root.style.setProperty('--kendo-notification-text', '#1a1a1a');
            root.style.setProperty('--kendo-notification-border', '#d0d0d0');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.12)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#1a1a1a');
            root.style.setProperty('--kendo-tooltip-text', '#ffffff');
            root.style.setProperty('--kendo-tooltip-border', '#3a3a3a');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.2)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#ffffff');
            root.style.setProperty('--kendo-splitter-border', '#d0d0d0');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#f0f0f0');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#e8e8e8');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#ffffff');
            root.style.setProperty('--kendo-upload-text', '#1a1a1a');
            root.style.setProperty('--kendo-upload-border', '#d0d0d0');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#fafafa');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#f5f5f5');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#ffffff');
            root.style.setProperty('--kendo-editor-text', '#1a1a1a');
            root.style.setProperty('--kendo-editor-border', '#d0d0d0');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#fafafa');
            root.style.setProperty('--kendo-editor-content-bg', '#ffffff');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#ffffff');
            root.style.setProperty('--kendo-scheduler-text', '#1a1a1a');
            root.style.setProperty('--kendo-scheduler-border', '#d0d0d0');
            root.style.setProperty('--kendo-scheduler-header-bg', '#f0f0f0');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-scheduler-event-bg', '#d0d0d0');
            root.style.setProperty('--kendo-scheduler-event-text', '#1a1a1a');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#fafafa');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#ffffff');
            root.style.setProperty('--kendo-gantt-text', '#1a1a1a');
            root.style.setProperty('--kendo-gantt-border', '#d0d0d0');
            root.style.setProperty('--kendo-gantt-header-bg', '#f0f0f0');
            root.style.setProperty('--kendo-gantt-alt-bg', '#fafafa');
            root.style.setProperty('--kendo-gantt-task-bg', '#6a6a6a');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#ffffff');
            root.style.setProperty('--kendo-panelbar-text', '#1a1a1a');
            root.style.setProperty('--kendo-panelbar-border', '#d0d0d0');
            root.style.setProperty('--kendo-panelbar-header-bg', '#fafafa');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#f5f5f5');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#ffffff');
            root.style.setProperty('--kendo-listview-text', '#1a1a1a');
            root.style.setProperty('--kendo-listview-border', '#d0d0d0');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#f5f5f5');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#e0e0e0');
            root.style.setProperty('--kendo-listview-item-selected-text', '#1a1a1a');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#d0d0d0');
            root.style.setProperty('--kendo-slider-selection-bg', '#6a6a6a');
            root.style.setProperty('--kendo-slider-handle-bg', '#ffffff');
            root.style.setProperty('--kendo-slider-handle-border', '#a0a0a0');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#f5f5f5');
        } else if (theme === 'catppuccin' || theme === 'catppuccin-mocha') {
            // ===== CATPPUCCIN MOCHA THEME - Trendy pastel dark theme =====
            // Custom app variables
            root.style.setProperty('--app-bg', '#1e1e2e');
            root.style.setProperty('--card-bg', '#313244');
            root.style.setProperty('--border', '#45475a');
            root.style.setProperty('--border-strong', '#585b70');
            root.style.setProperty('--text', '#cdd6f4');
            root.style.setProperty('--muted', '#bac2de');
            root.style.setProperty('--row-hover', '#45475a');
            root.style.setProperty('--nav-bg', '#313244');
            root.style.setProperty('--nav-border', '#45475a');
            root.style.setProperty('--hover-bg', '#45475a');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#313244');
            root.style.setProperty('--kendo-color-base-hover', '#45475a');
            root.style.setProperty('--kendo-color-base-active', '#585b70');
            root.style.setProperty('--kendo-color-on-base', '#cdd6f4');
            root.style.setProperty('--kendo-color-surface', '#1e1e2e');
            root.style.setProperty('--kendo-color-surface-alt', '#313244');
            root.style.setProperty('--kendo-color-border', '#45475a');
            root.style.setProperty('--kendo-color-border-alt', '#585b70');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#cdd6f4');
            root.style.setProperty('--kendo-color-subtle', '#bac2de');
            root.style.setProperty('--kendo-color-disabled', '#6c7086');

            // Component states - Catppuccin colors
            root.style.setProperty('--kendo-color-focus', '#89b4fa');
            root.style.setProperty('--kendo-color-error', '#f38ba8');
            root.style.setProperty('--kendo-color-success', '#a6e3a1');
            root.style.setProperty('--kendo-color-warning', '#f9e2af');
            root.style.setProperty('--kendo-color-info', '#89dceb');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#313244');
            root.style.setProperty('--kendo-grid-text', '#cdd6f4');
            root.style.setProperty('--kendo-grid-border', '#45475a');
            root.style.setProperty('--kendo-grid-header-bg', '#1e1e2e');
            root.style.setProperty('--kendo-grid-header-text', '#b4befe');
            root.style.setProperty('--kendo-grid-header-border', '#45475a');
            root.style.setProperty('--kendo-grid-footer-bg', '#1e1e2e');
            root.style.setProperty('--kendo-grid-footer-text', '#cdd6f4');
            root.style.setProperty('--kendo-grid-footer-border', '#45475a');
            root.style.setProperty('--kendo-grid-alt-bg', '#282838');
            root.style.setProperty('--kendo-grid-hover-bg', '#45475a');
            root.style.setProperty('--kendo-grid-selected-bg', '#89b4fa');
            root.style.setProperty('--kendo-grid-selected-text', '#1e1e2e');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#1e1e2e');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#313244');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#313244');
            root.style.setProperty('--kendo-button-text', '#cdd6f4');
            root.style.setProperty('--kendo-button-border', '#45475a');
            root.style.setProperty('--kendo-button-hover-bg', '#45475a');
            root.style.setProperty('--kendo-button-hover-border', '#585b70');
            root.style.setProperty('--kendo-button-active-bg', '#585b70');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(137, 180, 250, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#313244');
            root.style.setProperty('--kendo-button-disabled-text', '#6c7086');
            root.style.setProperty('--kendo-button-disabled-border', '#45475a');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#313244');
            root.style.setProperty('--kendo-input-text', '#cdd6f4');
            root.style.setProperty('--kendo-input-border', '#45475a');
            root.style.setProperty('--kendo-input-hover-border', '#585b70');
            root.style.setProperty('--kendo-input-focus-border', '#89b4fa');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(137, 180, 250, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#1e1e2e');
            root.style.setProperty('--kendo-input-disabled-text', '#6c7086');
            root.style.setProperty('--kendo-input-placeholder-text', '#a6adc8');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#313244');
            root.style.setProperty('--kendo-picker-text', '#cdd6f4');
            root.style.setProperty('--kendo-picker-border', '#45475a');
            root.style.setProperty('--kendo-picker-hover-bg', '#45475a');
            root.style.setProperty('--kendo-picker-hover-border', '#585b70');
            root.style.setProperty('--kendo-picker-focus-border', '#89b4fa');
            root.style.setProperty('--kendo-list-item-hover-bg', '#45475a');
            root.style.setProperty('--kendo-list-item-selected-bg', '#89b4fa');
            root.style.setProperty('--kendo-list-item-selected-text', '#1e1e2e');
            root.style.setProperty('--kendo-list-item-focus-bg', '#585b70');
            root.style.setProperty('--kendo-popup-bg', '#313244');
            root.style.setProperty('--kendo-popup-border', '#45475a');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#313244');
            root.style.setProperty('--kendo-calendar-text', '#cdd6f4');
            root.style.setProperty('--kendo-calendar-border', '#45475a');
            root.style.setProperty('--kendo-calendar-header-bg', '#1e1e2e');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#45475a');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#89b4fa');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#1e1e2e');
            root.style.setProperty('--kendo-calendar-weekend-text', '#f5c2e7');
            root.style.setProperty('--kendo-calendar-other-month-text', '#6c7086');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#313244');
            root.style.setProperty('--kendo-tabstrip-text', '#cdd6f4');
            root.style.setProperty('--kendo-tabstrip-border', '#45475a');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#bac2de');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#45475a');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#cdd6f4');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#313244');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#b4befe');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#313244');
            root.style.setProperty('--kendo-tabstrip-content-border', '#45475a');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#313244');
            root.style.setProperty('--kendo-window-text', '#cdd6f4');
            root.style.setProperty('--kendo-window-border', '#45475a');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.4)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#1e1e2e');
            root.style.setProperty('--kendo-window-titlebar-text', '#cba6f7');
            root.style.setProperty('--kendo-window-titlebar-border', '#45475a');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#313244');
            root.style.setProperty('--kendo-menu-text', '#cdd6f4');
            root.style.setProperty('--kendo-menu-border', '#45475a');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#45475a');
            root.style.setProperty('--kendo-menu-item-hover-text', '#cdd6f4');
            root.style.setProperty('--kendo-menu-item-active-bg', '#585b70');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#6c7086');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#313244');
            root.style.setProperty('--kendo-treeview-text', '#cdd6f4');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#45475a');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#89b4fa');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#1e1e2e');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#585b70');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#313244');
            root.style.setProperty('--kendo-pager-text', '#cdd6f4');
            root.style.setProperty('--kendo-pager-border', '#45475a');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#45475a');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#89b4fa');
            root.style.setProperty('--kendo-pager-item-selected-text', '#1e1e2e');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#313244');
            root.style.setProperty('--kendo-toolbar-text', '#cdd6f4');
            root.style.setProperty('--kendo-toolbar-border', '#45475a');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#45475a');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#45475a');
            root.style.setProperty('--kendo-chip-text', '#cdd6f4');
            root.style.setProperty('--kendo-chip-border', '#585b70');
            root.style.setProperty('--kendo-chip-hover-bg', '#585b70');
            root.style.setProperty('--kendo-chip-selected-bg', '#89b4fa');
            root.style.setProperty('--kendo-chip-selected-text', '#1e1e2e');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#313244');
            root.style.setProperty('--kendo-card-text', '#cdd6f4');
            root.style.setProperty('--kendo-card-border', '#45475a');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.2)');
            root.style.setProperty('--kendo-card-header-bg', '#1e1e2e');
            root.style.setProperty('--kendo-card-header-border', '#45475a');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#313244');
            root.style.setProperty('--kendo-notification-text', '#cdd6f4');
            root.style.setProperty('--kendo-notification-border', '#45475a');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#1e1e2e');
            root.style.setProperty('--kendo-tooltip-text', '#cdd6f4');
            root.style.setProperty('--kendo-tooltip-border', '#45475a');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.4)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#313244');
            root.style.setProperty('--kendo-splitter-border', '#45475a');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#1e1e2e');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#45475a');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#313244');
            root.style.setProperty('--kendo-upload-text', '#cdd6f4');
            root.style.setProperty('--kendo-upload-border', '#45475a');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#1e1e2e');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#45475a');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#313244');
            root.style.setProperty('--kendo-editor-text', '#cdd6f4');
            root.style.setProperty('--kendo-editor-border', '#45475a');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#1e1e2e');
            root.style.setProperty('--kendo-editor-content-bg', '#313244');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#313244');
            root.style.setProperty('--kendo-scheduler-text', '#cdd6f4');
            root.style.setProperty('--kendo-scheduler-border', '#45475a');
            root.style.setProperty('--kendo-scheduler-header-bg', '#1e1e2e');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#45475a');
            root.style.setProperty('--kendo-scheduler-event-bg', '#b4befe');
            root.style.setProperty('--kendo-scheduler-event-text', '#1e1e2e');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#1e1e2e');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#313244');
            root.style.setProperty('--kendo-gantt-text', '#cdd6f4');
            root.style.setProperty('--kendo-gantt-border', '#45475a');
            root.style.setProperty('--kendo-gantt-header-bg', '#1e1e2e');
            root.style.setProperty('--kendo-gantt-alt-bg', '#282838');
            root.style.setProperty('--kendo-gantt-task-bg', '#89b4fa');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#313244');
            root.style.setProperty('--kendo-panelbar-text', '#cdd6f4');
            root.style.setProperty('--kendo-panelbar-border', '#45475a');
            root.style.setProperty('--kendo-panelbar-header-bg', '#1e1e2e');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#45475a');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#313244');
            root.style.setProperty('--kendo-listview-text', '#cdd6f4');
            root.style.setProperty('--kendo-listview-border', '#45475a');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#45475a');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#89b4fa');
            root.style.setProperty('--kendo-listview-item-selected-text', '#1e1e2e');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#45475a');
            root.style.setProperty('--kendo-slider-selection-bg', '#89b4fa');
            root.style.setProperty('--kendo-slider-handle-bg', '#cdd6f4');
            root.style.setProperty('--kendo-slider-handle-border', '#45475a');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffffff');
        } else if (theme === 'catppuccin-latte') {
            // Custom app variables
            root.style.setProperty('--app-bg', '#eff1f5');
            root.style.setProperty('--card-bg', '#e6e9ef');
            root.style.setProperty('--border', '#ccd0da');
            root.style.setProperty('--border-strong', '#bcc0cc');
            root.style.setProperty('--text', '#4c4f69');
            root.style.setProperty('--muted', '#6c6f85');
            root.style.setProperty('--row-hover', '#dce0e8');
            root.style.setProperty('--nav-bg', '#e6e9ef');
            root.style.setProperty('--nav-border', '#ccd0da');
            root.style.setProperty('--hover-bg', '#dce0e8');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#e6e9ef');
            root.style.setProperty('--kendo-color-base-hover', '#dce0e8');
            root.style.setProperty('--kendo-color-base-active', '#ccd0da');
            root.style.setProperty('--kendo-color-on-base', '#4c4f69');
            root.style.setProperty('--kendo-color-surface', '#eff1f5');
            root.style.setProperty('--kendo-color-surface-alt', '#e6e9ef');
            root.style.setProperty('--kendo-color-border', '#ccd0da');
            root.style.setProperty('--kendo-color-border-alt', '#bcc0cc');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#4c4f69');
            root.style.setProperty('--kendo-color-subtle', '#6c6f85');
            root.style.setProperty('--kendo-color-disabled', '#9ca0b0');

            // Component states - Catppuccin Latte colors
            root.style.setProperty('--kendo-color-focus', '#1e66f5');
            root.style.setProperty('--kendo-color-error', '#d20f39');
            root.style.setProperty('--kendo-color-success', '#40a02b');
            root.style.setProperty('--kendo-color-warning', '#df8e1d');
            root.style.setProperty('--kendo-color-info', '#209fb5');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#e6e9ef');
            root.style.setProperty('--kendo-grid-text', '#4c4f69');
            root.style.setProperty('--kendo-grid-border', '#ccd0da');
            root.style.setProperty('--kendo-grid-header-bg', '#eff1f5');
            root.style.setProperty('--kendo-grid-header-text', '#5c5f77');
            root.style.setProperty('--kendo-grid-header-border', '#ccd0da');
            root.style.setProperty('--kendo-grid-footer-bg', '#eff1f5');
            root.style.setProperty('--kendo-grid-footer-text', '#4c4f69');
            root.style.setProperty('--kendo-grid-footer-border', '#ccd0da');
            root.style.setProperty('--kendo-grid-alt-bg', '#dce0e8');
            root.style.setProperty('--kendo-grid-hover-bg', '#ccd0da');
            root.style.setProperty('--kendo-grid-selected-bg', '#1e66f5');
            root.style.setProperty('--kendo-grid-selected-text', '#eff1f5');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#eff1f5');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#e6e9ef');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#e6e9ef');
            root.style.setProperty('--kendo-button-text', '#4c4f69');
            root.style.setProperty('--kendo-button-border', '#ccd0da');
            root.style.setProperty('--kendo-button-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-button-hover-border', '#bcc0cc');
            root.style.setProperty('--kendo-button-active-bg', '#ccd0da');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(30, 102, 245, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#e6e9ef');
            root.style.setProperty('--kendo-button-disabled-text', '#9ca0b0');
            root.style.setProperty('--kendo-button-disabled-border', '#ccd0da');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#e6e9ef');
            root.style.setProperty('--kendo-input-text', '#4c4f69');
            root.style.setProperty('--kendo-input-border', '#ccd0da');
            root.style.setProperty('--kendo-input-hover-border', '#bcc0cc');
            root.style.setProperty('--kendo-input-focus-border', '#1e66f5');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(30, 102, 245, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#eff1f5');
            root.style.setProperty('--kendo-input-disabled-text', '#9ca0b0');
            root.style.setProperty('--kendo-input-placeholder-text', '#7c7f93');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#e6e9ef');
            root.style.setProperty('--kendo-picker-text', '#4c4f69');
            root.style.setProperty('--kendo-picker-border', '#ccd0da');
            root.style.setProperty('--kendo-picker-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-picker-hover-border', '#bcc0cc');
            root.style.setProperty('--kendo-picker-focus-border', '#1e66f5');
            root.style.setProperty('--kendo-list-item-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-list-item-selected-bg', '#1e66f5');
            root.style.setProperty('--kendo-list-item-selected-text', '#eff1f5');
            root.style.setProperty('--kendo-list-item-focus-bg', '#ccd0da');
            root.style.setProperty('--kendo-popup-bg', '#e6e9ef');
            root.style.setProperty('--kendo-popup-border', '#ccd0da');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.15)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#e6e9ef');
            root.style.setProperty('--kendo-calendar-text', '#4c4f69');
            root.style.setProperty('--kendo-calendar-border', '#ccd0da');
            root.style.setProperty('--kendo-calendar-header-bg', '#eff1f5');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#1e66f5');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#eff1f5');
            root.style.setProperty('--kendo-calendar-weekend-text', '#ea76cb');
            root.style.setProperty('--kendo-calendar-other-month-text', '#9ca0b0');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#e6e9ef');
            root.style.setProperty('--kendo-tabstrip-text', '#4c4f69');
            root.style.setProperty('--kendo-tabstrip-border', '#ccd0da');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#6c6f85');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#4c4f69');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#e6e9ef');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#5c5f77');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#e6e9ef');
            root.style.setProperty('--kendo-tabstrip-content-border', '#ccd0da');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#e6e9ef');
            root.style.setProperty('--kendo-window-text', '#4c4f69');
            root.style.setProperty('--kendo-window-border', '#ccd0da');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.2)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#eff1f5');
            root.style.setProperty('--kendo-window-titlebar-text', '#8839ef');
            root.style.setProperty('--kendo-window-titlebar-border', '#ccd0da');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#e6e9ef');
            root.style.setProperty('--kendo-menu-text', '#4c4f69');
            root.style.setProperty('--kendo-menu-border', '#ccd0da');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-menu-item-hover-text', '#4c4f69');
            root.style.setProperty('--kendo-menu-item-active-bg', '#ccd0da');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#9ca0b0');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#e6e9ef');
            root.style.setProperty('--kendo-treeview-text', '#4c4f69');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#1e66f5');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#eff1f5');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#ccd0da');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#e6e9ef');
            root.style.setProperty('--kendo-pager-text', '#4c4f69');
            root.style.setProperty('--kendo-pager-border', '#ccd0da');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#1e66f5');
            root.style.setProperty('--kendo-pager-item-selected-text', '#eff1f5');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#e6e9ef');
            root.style.setProperty('--kendo-toolbar-text', '#4c4f69');
            root.style.setProperty('--kendo-toolbar-border', '#ccd0da');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#dce0e8');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#dce0e8');
            root.style.setProperty('--kendo-chip-text', '#4c4f69');
            root.style.setProperty('--kendo-chip-border', '#ccd0da');
            root.style.setProperty('--kendo-chip-hover-bg', '#ccd0da');
            root.style.setProperty('--kendo-chip-selected-bg', '#1e66f5');
            root.style.setProperty('--kendo-chip-selected-text', '#eff1f5');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#e6e9ef');
            root.style.setProperty('--kendo-card-text', '#4c4f69');
            root.style.setProperty('--kendo-card-border', '#ccd0da');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.1)');
            root.style.setProperty('--kendo-card-header-bg', '#eff1f5');
            root.style.setProperty('--kendo-card-header-border', '#ccd0da');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#e6e9ef');
            root.style.setProperty('--kendo-notification-text', '#4c4f69');
            root.style.setProperty('--kendo-notification-border', '#ccd0da');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.15)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#eff1f5');
            root.style.setProperty('--kendo-tooltip-text', '#4c4f69');
            root.style.setProperty('--kendo-tooltip-border', '#ccd0da');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.2)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#e6e9ef');
            root.style.setProperty('--kendo-splitter-border', '#ccd0da');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#eff1f5');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#dce0e8');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#e6e9ef');
            root.style.setProperty('--kendo-upload-text', '#4c4f69');
            root.style.setProperty('--kendo-upload-border', '#ccd0da');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#eff1f5');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#dce0e8');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#e6e9ef');
            root.style.setProperty('--kendo-editor-text', '#4c4f69');
            root.style.setProperty('--kendo-editor-border', '#ccd0da');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#eff1f5');
            root.style.setProperty('--kendo-editor-content-bg', '#e6e9ef');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#e6e9ef');
            root.style.setProperty('--kendo-scheduler-text', '#4c4f69');
            root.style.setProperty('--kendo-scheduler-border', '#ccd0da');
            root.style.setProperty('--kendo-scheduler-header-bg', '#eff1f5');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-scheduler-event-bg', '#7287fd');
            root.style.setProperty('--kendo-scheduler-event-text', '#eff1f5');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#eff1f5');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#e6e9ef');
            root.style.setProperty('--kendo-gantt-text', '#4c4f69');
            root.style.setProperty('--kendo-gantt-border', '#ccd0da');
            root.style.setProperty('--kendo-gantt-header-bg', '#eff1f5');
            root.style.setProperty('--kendo-gantt-alt-bg', '#dce0e8');
            root.style.setProperty('--kendo-gantt-task-bg', '#1e66f5');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#e6e9ef');
            root.style.setProperty('--kendo-panelbar-text', '#4c4f69');
            root.style.setProperty('--kendo-panelbar-border', '#ccd0da');
            root.style.setProperty('--kendo-panelbar-header-bg', '#eff1f5');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#dce0e8');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#e6e9ef');
            root.style.setProperty('--kendo-listview-text', '#4c4f69');
            root.style.setProperty('--kendo-listview-border', '#ccd0da');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#dce0e8');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#1e66f5');
            root.style.setProperty('--kendo-listview-item-selected-text', '#eff1f5');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#ccd0da');
            root.style.setProperty('--kendo-slider-selection-bg', '#1e66f5');
            root.style.setProperty('--kendo-slider-handle-bg', '#4c4f69');
            root.style.setProperty('--kendo-slider-handle-border', '#ccd0da');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#5c5f77');

            // ===== CATPPUCCIN FRAPP� THEME - Subdued dark theme =====
        } else if (theme === 'catppuccin-frappe') {
            // Custom app variables
            root.style.setProperty('--app-bg', '#303446');
            root.style.setProperty('--card-bg', '#414559');
            root.style.setProperty('--border', '#51576d');
            root.style.setProperty('--border-strong', '#626880');
            root.style.setProperty('--text', '#c6d0f5');
            root.style.setProperty('--muted', '#b5bfe2');
            root.style.setProperty('--row-hover', '#51576d');
            root.style.setProperty('--nav-bg', '#414559');
            root.style.setProperty('--nav-border', '#51576d');
            root.style.setProperty('--hover-bg', '#51576d');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#414559');
            root.style.setProperty('--kendo-color-base-hover', '#51576d');
            root.style.setProperty('--kendo-color-base-active', '#626880');
            root.style.setProperty('--kendo-color-on-base', '#c6d0f5');
            root.style.setProperty('--kendo-color-surface', '#303446');
            root.style.setProperty('--kendo-color-surface-alt', '#414559');
            root.style.setProperty('--kendo-color-border', '#51576d');
            root.style.setProperty('--kendo-color-border-alt', '#626880');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#c6d0f5');
            root.style.setProperty('--kendo-color-subtle', '#b5bfe2');
            root.style.setProperty('--kendo-color-disabled', '#737994');

            // Component states - Catppuccin Frapp� colors
            root.style.setProperty('--kendo-color-focus', '#8caaee');
            root.style.setProperty('--kendo-color-error', '#e78284');
            root.style.setProperty('--kendo-color-success', '#a6d189');
            root.style.setProperty('--kendo-color-warning', '#e5c890');
            root.style.setProperty('--kendo-color-info', '#81c8be');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#414559');
            root.style.setProperty('--kendo-grid-text', '#c6d0f5');
            root.style.setProperty('--kendo-grid-border', '#51576d');
            root.style.setProperty('--kendo-grid-header-bg', '#303446');
            root.style.setProperty('--kendo-grid-header-text', '#babbf1');
            root.style.setProperty('--kendo-grid-header-border', '#51576d');
            root.style.setProperty('--kendo-grid-footer-bg', '#303446');
            root.style.setProperty('--kendo-grid-footer-text', '#c6d0f5');
            root.style.setProperty('--kendo-grid-footer-border', '#51576d');
            root.style.setProperty('--kendo-grid-alt-bg', '#383c51');
            root.style.setProperty('--kendo-grid-hover-bg', '#51576d');
            root.style.setProperty('--kendo-grid-selected-bg', '#8caaee');
            root.style.setProperty('--kendo-grid-selected-text', '#303446');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#303446');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#414559');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#414559');
            root.style.setProperty('--kendo-button-text', '#c6d0f5');
            root.style.setProperty('--kendo-button-border', '#51576d');
            root.style.setProperty('--kendo-button-hover-bg', '#51576d');
            root.style.setProperty('--kendo-button-hover-border', '#626880');
            root.style.setProperty('--kendo-button-active-bg', '#626880');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(140, 170, 238, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#414559');
            root.style.setProperty('--kendo-button-disabled-text', '#737994');
            root.style.setProperty('--kendo-button-disabled-border', '#51576d');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#414559');
            root.style.setProperty('--kendo-input-text', '#c6d0f5');
            root.style.setProperty('--kendo-input-border', '#51576d');
            root.style.setProperty('--kendo-input-hover-border', '#626880');
            root.style.setProperty('--kendo-input-focus-border', '#8caaee');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(140, 170, 238, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#303446');
            root.style.setProperty('--kendo-input-disabled-text', '#737994');
            root.style.setProperty('--kendo-input-placeholder-text', '#a5adce');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#414559');
            root.style.setProperty('--kendo-picker-text', '#c6d0f5');
            root.style.setProperty('--kendo-picker-border', '#51576d');
            root.style.setProperty('--kendo-picker-hover-bg', '#51576d');
            root.style.setProperty('--kendo-picker-hover-border', '#626880');
            root.style.setProperty('--kendo-picker-focus-border', '#8caaee');
            root.style.setProperty('--kendo-list-item-hover-bg', '#51576d');
            root.style.setProperty('--kendo-list-item-selected-bg', '#8caaee');
            root.style.setProperty('--kendo-list-item-selected-text', '#303446');
            root.style.setProperty('--kendo-list-item-focus-bg', '#626880');
            root.style.setProperty('--kendo-popup-bg', '#414559');
            root.style.setProperty('--kendo-popup-border', '#51576d');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#414559');
            root.style.setProperty('--kendo-calendar-text', '#c6d0f5');
            root.style.setProperty('--kendo-calendar-border', '#51576d');
            root.style.setProperty('--kendo-calendar-header-bg', '#303446');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#51576d');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#8caaee');
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#303446');
            root.style.setProperty('--kendo-calendar-weekend-text', '#f4b8e4');
            root.style.setProperty('--kendo-calendar-other-month-text', '#737994');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#414559');
            root.style.setProperty('--kendo-tabstrip-text', '#c6d0f5');
            root.style.setProperty('--kendo-tabstrip-border', '#51576d');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#b5bfe2');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#51576d');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#c6d0f5');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#414559');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#babbf1');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#414559');
            root.style.setProperty('--kendo-tabstrip-content-border', '#51576d');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#414559');
            root.style.setProperty('--kendo-window-text', '#c6d0f5');
            root.style.setProperty('--kendo-window-border', '#51576d');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.4)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#303446');
            root.style.setProperty('--kendo-window-titlebar-text', '#ca9ee6');
            root.style.setProperty('--kendo-window-titlebar-border', '#51576d');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#414559');
            root.style.setProperty('--kendo-menu-text', '#c6d0f5');
            root.style.setProperty('--kendo-menu-border', '#51576d');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#51576d');
            root.style.setProperty('--kendo-menu-item-hover-text', '#c6d0f5');
            root.style.setProperty('--kendo-menu-item-active-bg', '#626880');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#737994');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#414559');
            root.style.setProperty('--kendo-treeview-text', '#c6d0f5');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#51576d');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#8caaee');
            root.style.setProperty('--kendo-treeview-item-selected-text', '#303446');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#626880');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#414559');
            root.style.setProperty('--kendo-pager-text', '#c6d0f5');
            root.style.setProperty('--kendo-pager-border', '#51576d');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#51576d');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#8caaee');
            root.style.setProperty('--kendo-pager-item-selected-text', '#303446');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#414559');
            root.style.setProperty('--kendo-toolbar-text', '#c6d0f5');
            root.style.setProperty('--kendo-toolbar-border', '#51576d');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#51576d');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#51576d');
            root.style.setProperty('--kendo-chip-text', '#c6d0f5');
            root.style.setProperty('--kendo-chip-border', '#626880');
            root.style.setProperty('--kendo-chip-hover-bg', '#626880');
            root.style.setProperty('--kendo-chip-selected-bg', '#8caaee');
            root.style.setProperty('--kendo-chip-selected-text', '#303446');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#414559');
            root.style.setProperty('--kendo-card-text', '#c6d0f5');
            root.style.setProperty('--kendo-card-border', '#51576d');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.2)');
            root.style.setProperty('--kendo-card-header-bg', '#303446');
            root.style.setProperty('--kendo-card-header-border', '#51576d');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#414559');
            root.style.setProperty('--kendo-notification-text', '#c6d0f5');
            root.style.setProperty('--kendo-notification-border', '#51576d');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#303446');
            root.style.setProperty('--kendo-tooltip-text', '#c6d0f5');
            root.style.setProperty('--kendo-tooltip-border', '#51576d');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.4)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#414559');
            root.style.setProperty('--kendo-splitter-border', '#51576d');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#303446');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#51576d');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#414559');
            root.style.setProperty('--kendo-upload-text', '#c6d0f5');
            root.style.setProperty('--kendo-upload-border', '#51576d');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#303446');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#51576d');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#414559');
            root.style.setProperty('--kendo-editor-text', '#c6d0f5');
            root.style.setProperty('--kendo-editor-border', '#51576d');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#303446');
            root.style.setProperty('--kendo-editor-content-bg', '#414559');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#414559');
            root.style.setProperty('--kendo-scheduler-text', '#c6d0f5');
            root.style.setProperty('--kendo-scheduler-border', '#51576d');
            root.style.setProperty('--kendo-scheduler-header-bg', '#303446');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#51576d');
            root.style.setProperty('--kendo-scheduler-event-bg', '#babbf1');
            root.style.setProperty('--kendo-scheduler-event-text', '#303446');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#303446');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#414559');
            root.style.setProperty('--kendo-gantt-text', '#c6d0f5');
            root.style.setProperty('--kendo-gantt-border', '#51576d');
            root.style.setProperty('--kendo-gantt-header-bg', '#303446');
            root.style.setProperty('--kendo-gantt-alt-bg', '#383c51');
            root.style.setProperty('--kendo-gantt-task-bg', '#8caaee');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#414559');
            root.style.setProperty('--kendo-panelbar-text', '#c6d0f5');
            root.style.setProperty('--kendo-panelbar-border', '#51576d');
            root.style.setProperty('--kendo-panelbar-header-bg', '#303446');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#51576d');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#414559');
            root.style.setProperty('--kendo-listview-text', '#c6d0f5');
            root.style.setProperty('--kendo-listview-border', '#51576d');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#51576d');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#8caaee');
            root.style.setProperty('--kendo-listview-item-selected-text', '#303446');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#51576d');
            root.style.setProperty('--kendo-slider-selection-bg', '#8caaee');
            root.style.setProperty('--kendo-slider-handle-bg', '#c6d0f5');
            root.style.setProperty('--kendo-slider-handle-border', '#51576d');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffffff');

            // ===== CATPPUCCIN MACCHIATO THEME - Medium contrast dark theme =====
        } else if (theme === 'catppuccin-macchiato') {
            // Official Catppuccin Macchiato Palette
            // Base: #24273a, Mantle: #1e2030, Crust: #181926
            // Surface 0: #363a4f, Surface 1: #494d64, Surface 2: #5b6078
            // Overlay 0: #6e738d, Overlay 1: #8087a2, Overlay 2: #939ab7
            // Subtext 0: #a5adcb, Subtext 1: #b8c0e0, Text: #cad3f5
            // Lavender: #b7bdf8, Blue: #8aadf4, Sapphire: #7dc4e4, Sky: #91d7e3
            // Teal: #8bd5ca, Green: #a6da95, Yellow: #eed49f, Peach: #f5a97f
            // Maroon: #ee99a0, Red: #ed8796, Mauve: #c6a0f6, Pink: #f5bde6
            // Flamingo: #f0c6c6, Rosewater: #f4dbd6

            // Custom app variables
            root.style.setProperty('--app-bg', '#24273a');        // Base
            root.style.setProperty('--card-bg', '#363a4f');       // Surface 0
            root.style.setProperty('--border', '#494d64');        // Surface 1
            root.style.setProperty('--border-strong', '#5b6078'); // Surface 2
            root.style.setProperty('--text', '#cad3f5');          // Text
            root.style.setProperty('--muted', '#b8c0e0');         // Subtext 1
            root.style.setProperty('--row-hover', '#494d64');     // Surface 1
            root.style.setProperty('--nav-bg', '#363a4f');        // Surface 0
            root.style.setProperty('--nav-border', '#494d64');    // Surface 1
            root.style.setProperty('--hover-bg', '#494d64');      // Surface 1

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#363a4f');        // Surface 0
            root.style.setProperty('--kendo-color-base-hover', '#494d64');  // Surface 1
            root.style.setProperty('--kendo-color-base-active', '#5b6078'); // Surface 2
            root.style.setProperty('--kendo-color-on-base', '#cad3f5');     // Text
            root.style.setProperty('--kendo-color-surface', '#24273a');     // Base
            root.style.setProperty('--kendo-color-surface-alt', '#363a4f'); // Surface 0
            root.style.setProperty('--kendo-color-border', '#494d64');      // Surface 1
            root.style.setProperty('--kendo-color-border-alt', '#5b6078');  // Surface 2

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#cad3f5'); // Text
            root.style.setProperty('--kendo-color-subtle', '#b8c0e0');         // Subtext 1
            root.style.setProperty('--kendo-color-disabled', '#6e738d');       // Overlay 0

            // Component states - Catppuccin Macchiato colors
            root.style.setProperty('--kendo-color-focus', '#8aadf4');   // Blue
            root.style.setProperty('--kendo-color-error', '#ed8796');   // Red
            root.style.setProperty('--kendo-color-success', '#a6da95'); // Green
            root.style.setProperty('--kendo-color-warning', '#eed49f'); // Yellow
            root.style.setProperty('--kendo-color-info', '#91d7e3');    // Sky

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#363a4f');        // Surface 0
            root.style.setProperty('--kendo-grid-text', '#cad3f5');      // Text
            root.style.setProperty('--kendo-grid-border', '#494d64');    // Surface 1
            root.style.setProperty('--kendo-grid-header-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-grid-header-text', '#b7bdf8'); // Lavender
            root.style.setProperty('--kendo-grid-header-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-grid-footer-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-grid-footer-text', '#cad3f5'); // Text
            root.style.setProperty('--kendo-grid-footer-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-grid-alt-bg', '#1e2030');    // Mantle (alternating rows)
            root.style.setProperty('--kendo-grid-hover-bg', '#494d64');  // Surface 1
            root.style.setProperty('--kendo-grid-selected-bg', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-grid-selected-text', '#181926'); // Crust
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#363a4f'); // Surface 0

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#363a4f');        // Surface 0
            root.style.setProperty('--kendo-button-text', '#cad3f5');      // Text
            root.style.setProperty('--kendo-button-border', '#494d64');    // Surface 1
            root.style.setProperty('--kendo-button-hover-bg', '#494d64');  // Surface 1
            root.style.setProperty('--kendo-button-hover-border', '#5b6078'); // Surface 2
            root.style.setProperty('--kendo-button-active-bg', '#5b6078'); // Surface 2
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(138, 173, 244, 0.5)'); // Blue with opacity
            root.style.setProperty('--kendo-button-disabled-bg', '#363a4f'); // Surface 0
            root.style.setProperty('--kendo-button-disabled-text', '#6e738d'); // Overlay 0
            root.style.setProperty('--kendo-button-disabled-border', '#494d64'); // Surface 1

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#363a4f');        // Surface 0
            root.style.setProperty('--kendo-input-text', '#cad3f5');      // Text
            root.style.setProperty('--kendo-input-border', '#494d64');    // Surface 1
            root.style.setProperty('--kendo-input-hover-border', '#5b6078'); // Surface 2
            root.style.setProperty('--kendo-input-focus-border', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(138, 173, 244, 0.25)'); // Blue with opacity
            root.style.setProperty('--kendo-input-disabled-bg', '#24273a'); // Base
            root.style.setProperty('--kendo-input-disabled-text', '#6e738d'); // Overlay 0
            root.style.setProperty('--kendo-input-placeholder-text', '#a5adcb'); // Subtext 0

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#363a4f');       // Surface 0
            root.style.setProperty('--kendo-picker-text', '#cad3f5');     // Text
            root.style.setProperty('--kendo-picker-border', '#494d64');   // Surface 1
            root.style.setProperty('--kendo-picker-hover-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-picker-hover-border', '#5b6078'); // Surface 2
            root.style.setProperty('--kendo-picker-focus-border', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-list-item-hover-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-list-item-selected-bg', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-list-item-selected-text', '#181926'); // Crust
            root.style.setProperty('--kendo-list-item-focus-bg', '#5b6078'); // Surface 2
            root.style.setProperty('--kendo-popup-bg', '#363a4f');        // Surface 0
            root.style.setProperty('--kendo-popup-border', '#494d64');    // Surface 1
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#363a4f');     // Surface 0
            root.style.setProperty('--kendo-calendar-text', '#cad3f5');   // Text
            root.style.setProperty('--kendo-calendar-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-calendar-header-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-calendar-cell-selected-text', '#181926'); // Crust
            root.style.setProperty('--kendo-calendar-weekend-text', '#f5bde6'); // Pink
            root.style.setProperty('--kendo-calendar-other-month-text', '#6e738d'); // Overlay 0

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#363a4f');     // Surface 0
            root.style.setProperty('--kendo-tabstrip-text', '#cad3f5');   // Text
            root.style.setProperty('--kendo-tabstrip-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#b8c0e0'); // Subtext 1
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#cad3f5'); // Text
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#363a4f'); // Surface 0
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#b7bdf8'); // Lavender
            root.style.setProperty('--kendo-tabstrip-content-bg', '#363a4f'); // Surface 0
            root.style.setProperty('--kendo-tabstrip-content-border', '#494d64'); // Surface 1

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#363a4f');       // Surface 0
            root.style.setProperty('--kendo-window-text', '#cad3f5');     // Text
            root.style.setProperty('--kendo-window-border', '#494d64');   // Surface 1
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.4)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-window-titlebar-text', '#c6a0f6'); // Mauve
            root.style.setProperty('--kendo-window-titlebar-border', '#494d64'); // Surface 1

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#363a4f');         // Surface 0
            root.style.setProperty('--kendo-menu-text', '#cad3f5');       // Text
            root.style.setProperty('--kendo-menu-border', '#494d64');     // Surface 1
            root.style.setProperty('--kendo-menu-item-hover-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-menu-item-hover-text', '#cad3f5'); // Text
            root.style.setProperty('--kendo-menu-item-active-bg', '#5b6078'); // Surface 2
            root.style.setProperty('--kendo-menu-item-disabled-text', '#6e738d'); // Overlay 0

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#363a4f');     // Surface 0
            root.style.setProperty('--kendo-treeview-text', '#cad3f5');   // Text
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-treeview-item-selected-text', '#181926'); // Crust
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#5b6078'); // Surface 2

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#363a4f');        // Surface 0
            root.style.setProperty('--kendo-pager-text', '#cad3f5');      // Text
            root.style.setProperty('--kendo-pager-border', '#494d64');    // Surface 1
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-pager-item-selected-bg', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-pager-item-selected-text', '#181926'); // Crust

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#363a4f');      // Surface 0
            root.style.setProperty('--kendo-toolbar-text', '#cad3f5');    // Text
            root.style.setProperty('--kendo-toolbar-border', '#494d64');  // Surface 1
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#494d64'); // Surface 1

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#494d64');         // Surface 1
            root.style.setProperty('--kendo-chip-text', '#cad3f5');       // Text
            root.style.setProperty('--kendo-chip-border', '#5b6078');     // Surface 2
            root.style.setProperty('--kendo-chip-hover-bg', '#5b6078');   // Surface 2
            root.style.setProperty('--kendo-chip-selected-bg', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-chip-selected-text', '#181926'); // Crust

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#363a4f');         // Surface 0
            root.style.setProperty('--kendo-card-text', '#cad3f5');       // Text
            root.style.setProperty('--kendo-card-border', '#494d64');     // Surface 1
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.2)');
            root.style.setProperty('--kendo-card-header-bg', '#1e2030');  // Mantle
            root.style.setProperty('--kendo-card-header-border', '#494d64'); // Surface 1

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#363a4f');  // Surface 0
            root.style.setProperty('--kendo-notification-text', '#cad3f5'); // Text
            root.style.setProperty('--kendo-notification-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.3)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#1e2030');      // Mantle
            root.style.setProperty('--kendo-tooltip-text', '#cad3f5');    // Text
            root.style.setProperty('--kendo-tooltip-border', '#494d64');  // Surface 1
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.4)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#363a4f');     // Surface 0
            root.style.setProperty('--kendo-splitter-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#494d64'); // Surface 1

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#363a4f');       // Surface 0
            root.style.setProperty('--kendo-upload-text', '#cad3f5');     // Text
            root.style.setProperty('--kendo-upload-border', '#494d64');   // Surface 1
            root.style.setProperty('--kendo-upload-dropzone-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#494d64'); // Surface 1

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#363a4f');       // Surface 0
            root.style.setProperty('--kendo-editor-text', '#cad3f5');     // Text
            root.style.setProperty('--kendo-editor-border', '#494d64');   // Surface 1
            root.style.setProperty('--kendo-editor-toolbar-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-editor-content-bg', '#363a4f'); // Surface 0

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#363a4f');    // Surface 0
            root.style.setProperty('--kendo-scheduler-text', '#cad3f5');  // Text
            root.style.setProperty('--kendo-scheduler-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-scheduler-header-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-scheduler-event-bg', '#b7bdf8'); // Lavender
            root.style.setProperty('--kendo-scheduler-event-text', '#181926'); // Crust
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#1e2030'); // Mantle

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#363a4f');        // Surface 0
            root.style.setProperty('--kendo-gantt-text', '#cad3f5');      // Text
            root.style.setProperty('--kendo-gantt-border', '#494d64');    // Surface 1
            root.style.setProperty('--kendo-gantt-header-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-gantt-alt-bg', '#1e2030');    // Mantle
            root.style.setProperty('--kendo-gantt-task-bg', '#8aadf4');   // Blue

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#363a4f');     // Surface 0
            root.style.setProperty('--kendo-panelbar-text', '#cad3f5');   // Text
            root.style.setProperty('--kendo-panelbar-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-panelbar-header-bg', '#1e2030'); // Mantle
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#494d64'); // Surface 1

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#363a4f');     // Surface 0
            root.style.setProperty('--kendo-listview-text', '#cad3f5');   // Text
            root.style.setProperty('--kendo-listview-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-listview-item-hover-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-listview-item-selected-bg', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-listview-item-selected-text', '#181926'); // Crust

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-slider-selection-bg', '#8aadf4'); // Blue
            root.style.setProperty('--kendo-slider-handle-bg', '#cad3f5'); // Text
            root.style.setProperty('--kendo-slider-handle-border', '#494d64'); // Surface 1
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#ffffff');
        }
        else {
            // Custom app variables (light)
            root.style.setProperty('--app-bg', '#fff');
            root.style.setProperty('--card-bg', '#fff');
            root.style.setProperty('--border', '#e5e7eb');
            root.style.setProperty('--border-strong', '#d1d5db');
            root.style.setProperty('--text', '#111827');
            root.style.setProperty('--muted', '#6b7280');
            root.style.setProperty('--row-hover', '#f1f5f9');
            root.style.setProperty('--nav-bg', '#ffffff');
            root.style.setProperty('--nav-border', '#e5e7eb');
            root.style.setProperty('--hover-bg', '#f8f9fa');

            // ===== Core Kendo/Telerik Theme Variables =====
            root.style.setProperty('--kendo-color-base', '#ffffff');
            root.style.setProperty('--kendo-color-base-hover', '#f8f9fa');
            root.style.setProperty('--kendo-color-base-active', '#e5e7eb');
            root.style.setProperty('--kendo-color-on-base', '#111827');
            root.style.setProperty('--kendo-color-surface', '#ffffff');
            root.style.setProperty('--kendo-color-surface-alt', '#f9fafb');
            root.style.setProperty('--kendo-color-border', '#e5e7eb');
            root.style.setProperty('--kendo-color-border-alt', '#d1d5db');

            // Text colors
            root.style.setProperty('--kendo-color-on-app-surface', '#111827');
            root.style.setProperty('--kendo-color-subtle', '#6b7280');
            root.style.setProperty('--kendo-color-disabled', '#9ca3af');

            // Component states
            root.style.setProperty('--kendo-color-focus', '#3b82f6');
            root.style.setProperty('--kendo-color-error', '#ef4444');
            root.style.setProperty('--kendo-color-success', '#22c55e');
            root.style.setProperty('--kendo-color-warning', '#f59e0b');
            root.style.setProperty('--kendo-color-info', '#3b82f6');

            // ===== Grid Component =====
            root.style.setProperty('--kendo-grid-bg', '#ffffff');
            root.style.setProperty('--kendo-grid-text', '#111827');
            root.style.setProperty('--kendo-grid-border', '#e5e7eb');
            root.style.setProperty('--kendo-grid-header-bg', '#f3f4f6');
            root.style.setProperty('--kendo-grid-header-text', '#111827');
            root.style.setProperty('--kendo-grid-header-border', '#e5e7eb');
            root.style.setProperty('--kendo-grid-footer-bg', '#f3f4f6');
            root.style.setProperty('--kendo-grid-footer-text', '#111827');
            root.style.setProperty('--kendo-grid-footer-border', '#e5e7eb');
            root.style.setProperty('--kendo-grid-alt-bg', '#f9fafb');
            root.style.setProperty('--kendo-grid-hover-bg', '#f1f5f9');
            root.style.setProperty('--kendo-grid-selected-bg', '#dbeafe');
            root.style.setProperty('--kendo-grid-selected-text', '#111827');
            root.style.setProperty('--kendo-grid-grouping-header-bg', '#f3f4f6');
            root.style.setProperty('--kendo-grid-filter-cell-bg', '#ffffff');

            // ===== Button Component =====
            root.style.setProperty('--kendo-button-bg', '#ffffff');
            root.style.setProperty('--kendo-button-text', '#111827');
            root.style.setProperty('--kendo-button-border', '#e5e7eb');
            root.style.setProperty('--kendo-button-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-button-hover-border', '#d1d5db');
            root.style.setProperty('--kendo-button-active-bg', '#e5e7eb');
            root.style.setProperty('--kendo-button-focus-shadow', 'rgba(59, 130, 246, 0.5)');
            root.style.setProperty('--kendo-button-disabled-bg', '#f9fafb');
            root.style.setProperty('--kendo-button-disabled-text', '#9ca3af');
            root.style.setProperty('--kendo-button-disabled-border', '#e5e7eb');

            // ===== Input/TextBox Component =====
            root.style.setProperty('--kendo-input-bg', '#ffffff');
            root.style.setProperty('--kendo-input-text', '#111827');
            root.style.setProperty('--kendo-input-border', '#d1d5db');
            root.style.setProperty('--kendo-input-hover-border', '#9ca3af');
            root.style.setProperty('--kendo-input-focus-border', '#3b82f6');
            root.style.setProperty('--kendo-input-focus-shadow', 'rgba(59, 130, 246, 0.25)');
            root.style.setProperty('--kendo-input-disabled-bg', '#f9fafb');
            root.style.setProperty('--kendo-input-disabled-text', '#9ca3af');
            root.style.setProperty('--kendo-input-placeholder-text', '#6b7280');

            // ===== Dropdown/ComboBox/MultiSelect =====
            root.style.setProperty('--kendo-picker-bg', '#ffffff');
            root.style.setProperty('--kendo-picker-text', '#111827');
            root.style.setProperty('--kendo-picker-border', '#d1d5db');
            root.style.setProperty('--kendo-picker-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-picker-hover-border', '#9ca3af');
            root.style.setProperty('--kendo-picker-focus-border', '#3b82f6');
            root.style.setProperty('--kendo-list-item-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-list-item-selected-bg', '#dbeafe');
            root.style.setProperty('--kendo-list-item-focus-bg', '#f1f5f9');
            root.style.setProperty('--kendo-popup-bg', '#ffffff');
            root.style.setProperty('--kendo-popup-border', '#e5e7eb');
            root.style.setProperty('--kendo-popup-shadow', 'rgba(0, 0, 0, 0.1)');

            // ===== DatePicker/TimePicker/DateTimePicker =====
            root.style.setProperty('--kendo-calendar-bg', '#ffffff');
            root.style.setProperty('--kendo-calendar-text', '#111827');
            root.style.setProperty('--kendo-calendar-border', '#e5e7eb');
            root.style.setProperty('--kendo-calendar-header-bg', '#f9fafb');
            root.style.setProperty('--kendo-calendar-cell-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-calendar-cell-selected-bg', '#dbeafe');
            root.style.setProperty('--kendo-calendar-weekend-text', '#6b7280');
            root.style.setProperty('--kendo-calendar-other-month-text', '#9ca3af');

            // ===== TabStrip Component =====
            root.style.setProperty('--kendo-tabstrip-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-text', '#111827');
            root.style.setProperty('--kendo-tabstrip-border', '#e5e7eb');
            root.style.setProperty('--kendo-tabstrip-item-bg', 'transparent');
            root.style.setProperty('--kendo-tabstrip-item-text', '#6b7280');
            root.style.setProperty('--kendo-tabstrip-item-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-tabstrip-item-hover-text', '#111827');
            root.style.setProperty('--kendo-tabstrip-item-selected-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-item-selected-text', '#111827');
            root.style.setProperty('--kendo-tabstrip-content-bg', '#ffffff');
            root.style.setProperty('--kendo-tabstrip-content-border', '#e5e7eb');

            // ===== Window/Dialog Component =====
            root.style.setProperty('--kendo-window-bg', '#ffffff');
            root.style.setProperty('--kendo-window-text', '#111827');
            root.style.setProperty('--kendo-window-border', '#e5e7eb');
            root.style.setProperty('--kendo-window-shadow', 'rgba(0, 0, 0, 0.15)');
            root.style.setProperty('--kendo-window-titlebar-bg', '#f9fafb');
            root.style.setProperty('--kendo-window-titlebar-text', '#111827');
            root.style.setProperty('--kendo-window-titlebar-border', '#e5e7eb');

            // ===== Menu/ContextMenu Component =====
            root.style.setProperty('--kendo-menu-bg', '#ffffff');
            root.style.setProperty('--kendo-menu-text', '#111827');
            root.style.setProperty('--kendo-menu-border', '#e5e7eb');
            root.style.setProperty('--kendo-menu-item-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-menu-item-hover-text', '#111827');
            root.style.setProperty('--kendo-menu-item-active-bg', '#e5e7eb');
            root.style.setProperty('--kendo-menu-item-disabled-text', '#9ca3af');

            // ===== TreeView Component =====
            root.style.setProperty('--kendo-treeview-bg', '#ffffff');
            root.style.setProperty('--kendo-treeview-text', '#111827');
            root.style.setProperty('--kendo-treeview-item-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-treeview-item-selected-bg', '#dbeafe');
            root.style.setProperty('--kendo-treeview-item-focus-bg', '#f1f5f9');

            // ===== Pager Component =====
            root.style.setProperty('--kendo-pager-bg', '#ffffff');
            root.style.setProperty('--kendo-pager-text', '#111827');
            root.style.setProperty('--kendo-pager-border', '#e5e7eb');
            root.style.setProperty('--kendo-pager-item-bg', 'transparent');
            root.style.setProperty('--kendo-pager-item-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-pager-item-selected-bg', '#dbeafe');

            // ===== Toolbar Component =====
            root.style.setProperty('--kendo-toolbar-bg', '#ffffff');
            root.style.setProperty('--kendo-toolbar-text', '#111827');
            root.style.setProperty('--kendo-toolbar-border', '#e5e7eb');
            root.style.setProperty('--kendo-toolbar-item-hover-bg', '#f8f9fa');

            // ===== Chip/Badge Component =====
            root.style.setProperty('--kendo-chip-bg', '#f3f4f6');
            root.style.setProperty('--kendo-chip-text', '#111827');
            root.style.setProperty('--kendo-chip-border', '#e5e7eb');
            root.style.setProperty('--kendo-chip-hover-bg', '#e5e7eb');
            root.style.setProperty('--kendo-chip-selected-bg', '#dbeafe');

            // ===== Card Component =====
            root.style.setProperty('--kendo-card-bg', '#ffffff');
            root.style.setProperty('--kendo-card-text', '#111827');
            root.style.setProperty('--kendo-card-border', '#e5e7eb');
            root.style.setProperty('--kendo-card-shadow', 'rgba(0, 0, 0, 0.1)');
            root.style.setProperty('--kendo-card-header-bg', '#f9fafb');
            root.style.setProperty('--kendo-card-header-border', '#e5e7eb');

            // ===== Notification/Alert Component =====
            root.style.setProperty('--kendo-notification-bg', '#ffffff');
            root.style.setProperty('--kendo-notification-text', '#111827');
            root.style.setProperty('--kendo-notification-border', '#e5e7eb');
            root.style.setProperty('--kendo-notification-shadow', 'rgba(0, 0, 0, 0.15)');

            // ===== Tooltip Component =====
            root.style.setProperty('--kendo-tooltip-bg', '#111827');
            root.style.setProperty('--kendo-tooltip-text', '#ffffff');
            root.style.setProperty('--kendo-tooltip-border', '#374151');
            root.style.setProperty('--kendo-tooltip-shadow', 'rgba(0, 0, 0, 0.2)');

            // ===== Splitter Component =====
            root.style.setProperty('--kendo-splitter-bg', '#ffffff');
            root.style.setProperty('--kendo-splitter-border', '#e5e7eb');
            root.style.setProperty('--kendo-splitter-splitbar-bg', '#f3f4f6');
            root.style.setProperty('--kendo-splitter-splitbar-hover-bg', '#e5e7eb');

            // ===== Upload Component =====
            root.style.setProperty('--kendo-upload-bg', '#ffffff');
            root.style.setProperty('--kendo-upload-text', '#111827');
            root.style.setProperty('--kendo-upload-border', '#e5e7eb');
            root.style.setProperty('--kendo-upload-dropzone-bg', '#f9fafb');
            root.style.setProperty('--kendo-upload-dropzone-hover-bg', '#f3f4f6');

            // ===== Editor Component =====
            root.style.setProperty('--kendo-editor-bg', '#ffffff');
            root.style.setProperty('--kendo-editor-text', '#111827');
            root.style.setProperty('--kendo-editor-border', '#e5e7eb');
            root.style.setProperty('--kendo-editor-toolbar-bg', '#f9fafb');
            root.style.setProperty('--kendo-editor-content-bg', '#ffffff');

            // ===== Scheduler Component =====
            root.style.setProperty('--kendo-scheduler-bg', '#ffffff');
            root.style.setProperty('--kendo-scheduler-text', '#111827');
            root.style.setProperty('--kendo-scheduler-border', '#e5e7eb');
            root.style.setProperty('--kendo-scheduler-header-bg', '#f3f4f6');
            root.style.setProperty('--kendo-scheduler-cell-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-scheduler-event-bg', '#dbeafe');
            root.style.setProperty('--kendo-scheduler-nonwork-bg', '#f9fafb');

            // ===== Gantt Component =====
            root.style.setProperty('--kendo-gantt-bg', '#ffffff');
            root.style.setProperty('--kendo-gantt-text', '#111827');
            root.style.setProperty('--kendo-gantt-border', '#e5e7eb');
            root.style.setProperty('--kendo-gantt-header-bg', '#f3f4f6');
            root.style.setProperty('--kendo-gantt-alt-bg', '#f9fafb');
            root.style.setProperty('--kendo-gantt-task-bg', '#dbeafe');

            // ===== PanelBar Component =====
            root.style.setProperty('--kendo-panelbar-bg', '#ffffff');
            root.style.setProperty('--kendo-panelbar-text', '#111827');
            root.style.setProperty('--kendo-panelbar-border', '#e5e7eb');
            root.style.setProperty('--kendo-panelbar-header-bg', '#f9fafb');
            root.style.setProperty('--kendo-panelbar-header-hover-bg', '#f3f4f6');

            // ===== ListView Component =====
            root.style.setProperty('--kendo-listview-bg', '#ffffff');
            root.style.setProperty('--kendo-listview-text', '#111827');
            root.style.setProperty('--kendo-listview-border', '#e5e7eb');
            root.style.setProperty('--kendo-listview-item-hover-bg', '#f8f9fa');
            root.style.setProperty('--kendo-listview-item-selected-bg', '#dbeafe');

            // ===== Slider/RangeSlider Component =====
            root.style.setProperty('--kendo-slider-track-bg', '#e5e7eb');
            root.style.setProperty('--kendo-slider-selection-bg', '#3b82f6');
            root.style.setProperty('--kendo-slider-handle-bg', '#ffffff');
            root.style.setProperty('--kendo-slider-handle-border', '#d1d5db');
            root.style.setProperty('--kendo-slider-handle-hover-bg', '#f8f9fa');
        }

        // Set body background
        document.body.style.backgroundColor = getComputedStyle(root).getPropertyValue('--app-bg');
    }

    // Apply accent color

    function applyAccent(accentName, customAccent) {
        const root = document.documentElement;
        let colors;

        if (accentName === 'custom' && customAccent) {
            // Generate color palette from custom color
            colors = {
                primary: customAccent,
                light: lightenColor(customAccent, 85),
                hover: adjustColor(customAccent, -20)
            };
        } else {
            colors = accentColors[accentName] || accentColors.blue;
        }

        // Custom app variables
        root.style.setProperty('--accent', colors.primary);
        root.style.setProperty('--accent-weak', colors.light);
        root.style.setProperty('--accent-hover', colors.hover);

        // Kendo/Telerik theme variables
        root.style.setProperty('--kendo-color-primary', colors.primary);
        root.style.setProperty('--kendo-color-primary-hover', colors.hover);
        root.style.setProperty('--kendo-color-primary-active', colors.hover);
        root.style.setProperty('--kendo-color-on-primary', '#ffffff');
        root.style.setProperty('--kendo-color-primary-subtle', colors.light);

        // RGB values for rgba() usage
        const rgb = hexToRgb(colors.primary);
        if (rgb) {
            root.style.setProperty('--accent-rgb', `${rgb.r}, ${rgb.g}, ${rgb.b}`);
        }
    }

    function applyBorderRadius(radiusName) {
        const root = document.documentElement;
        const radii = borderRadiusSettings[radiusName] || borderRadiusSettings.default;

        // Custom app variables
        root.style.setProperty('--radius-xs', radii.xs);
        root.style.setProperty('--radius-sm', radii.sm);
        root.style.setProperty('--radius-md', radii.md);
        root.style.setProperty('--radius-lg', radii.lg);
        root.style.setProperty('--radius-xl', radii.xl);
        root.style.setProperty('--radius-full', radii.full);

        // Kendo/Telerik border radius variables
        root.style.setProperty('--kendo-border-radius-sm', radii.sm);
        root.style.setProperty('--kendo-border-radius-md', radii.md);
        root.style.setProperty('--kendo-border-radius-lg', radii.lg);

        // Component-specific radii
        root.style.setProperty('--kendo-button-border-radius', radii.md);
        root.style.setProperty('--kendo-input-border-radius', radii.md);
        root.style.setProperty('--kendo-card-border-radius', radii.lg);
        root.style.setProperty('--kendo-popup-border-radius', radii.md);
        root.style.setProperty('--kendo-window-border-radius', radii.lg);
        root.style.setProperty('--kendo-chip-border-radius', radii.full);
        root.style.setProperty('--kendo-badge-border-radius', radii.full);
        root.style.setProperty('--kendo-notification-border-radius', radii.md);
        root.style.setProperty('--kendo-tooltip-border-radius', radii.sm);
        root.style.setProperty('--kendo-menu-popup-border-radius', radii.md);
        root.style.setProperty('--kendo-tabstrip-border-radius', radii.md);
        root.style.setProperty('--kendo-panelbar-border-radius', radii.md);

        // Add class for CSS-based overrides
        root.setAttribute('data-radius', radiusName);
    }

    function applyAnimationSpeed(speed) {
        const root = document.documentElement;

        // Base durations (in ms)
        const baseDurations = {
            fast: 100,
            normal: 150,
            slow: 200,
            slower: 300,
            animation: 200
        };

        // Calculate adjusted durations (speed > 1 = faster, speed < 1 = slower)
        const multiplier = 1 / speed;

        root.style.setProperty('--transition-fast', `${Math.round(baseDurations.fast * multiplier)}ms`);
        root.style.setProperty('--transition-normal', `${Math.round(baseDurations.normal * multiplier)}ms`);
        root.style.setProperty('--transition-slow', `${Math.round(baseDurations.slow * multiplier)}ms`);
        root.style.setProperty('--transition-slower', `${Math.round(baseDurations.slower * multiplier)}ms`);
        root.style.setProperty('--animation-duration', `${Math.round(baseDurations.animation * multiplier)}ms`);

        // Kendo animation durations
        root.style.setProperty('--kendo-transition-duration', `${Math.round(baseDurations.normal * multiplier)}ms`);
        root.style.setProperty('--kendo-animation-duration', `${Math.round(baseDurations.animation * multiplier)}ms`);

        root.setAttribute('data-animation-speed', speed.toString());
    }

    // Convert hex to RGB
    function hexToRgb(hex) {
        const result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
        return result ? {
            r: parseInt(result[1], 16),
            g: parseInt(result[2], 16),
            b: parseInt(result[3], 16)
        } : null;
    }

    function adjustColor(hex, amount) {
        const rgb = hexToRgb(hex);
        if (!rgb) return hex;

        const adjust = (value) => Math.max(0, Math.min(255, value + amount));
        const r = adjust(rgb.r).toString(16).padStart(2, '0');
        const g = adjust(rgb.g).toString(16).padStart(2, '0');
        const b = adjust(rgb.b).toString(16).padStart(2, '0');

        return `#${r}${g}${b}`;
    }
    function lightenColor(hex, percent) {
        const rgb = hexToRgb(hex);
        if (!rgb) return hex;

        const lighten = (value) => Math.round(value + (255 - value) * (percent / 100));
        const r = lighten(rgb.r).toString(16).padStart(2, '0');
        const g = lighten(rgb.g).toString(16).padStart(2, '0');
        const b = lighten(rgb.b).toString(16).padStart(2, '0');

        return `#${r}${g}${b}`;
    }


    // Apply density
    function applyDensity(densityName) {
        const root = document.documentElement;
        const density = densitySettings[densityName] || densitySettings.comfortable;

        // Adjust spacing scale
        const baseSpacing = 4;
        for (let i = 0; i <= 30; i++) {
            const value = Math.round(baseSpacing * i * density.multiplier);
            root.style.setProperty(`--spacing-${i}`, `${value}px`);
            root.style.setProperty(`--kendo-spacing-${i}`, `${value}px`);
        }

        // Line height
        root.style.setProperty('--base-line-height', density.lineHeight.toString());
        root.style.setProperty('--kendo-line-height', density.lineHeight.toString());

        // Component-specific padding
        const componentPadding = {
            compact: { x: '8px', y: '4px', cellX: '6px', cellY: '4px' },
            comfortable: { x: '12px', y: '8px', cellX: '12px', cellY: '8px' },
            spacious: { x: '16px', y: '12px', cellX: '16px', cellY: '12px' }
        };

        const padding = componentPadding[densityName] || componentPadding.comfortable;

        // Grid
        root.style.setProperty('--kendo-grid-cell-padding-x', padding.cellX);
        root.style.setProperty('--kendo-grid-cell-padding-y', padding.cellY);
        root.style.setProperty('--kendo-grid-header-padding-x', padding.cellX);
        root.style.setProperty('--kendo-grid-header-padding-y', padding.cellY);

        // Buttons
        root.style.setProperty('--kendo-button-padding-x', padding.x);
        root.style.setProperty('--kendo-button-padding-y', padding.y);

        // Inputs
        root.style.setProperty('--kendo-input-padding-x', padding.x);
        root.style.setProperty('--kendo-input-padding-y', padding.y);

        // List items
        root.style.setProperty('--kendo-list-item-padding-x', padding.x);
        root.style.setProperty('--kendo-list-item-padding-y', padding.y);

        // Menu items
        root.style.setProperty('--kendo-menu-item-padding-x', padding.x);
        root.style.setProperty('--kendo-menu-item-padding-y', padding.y);

        // Tree items
        root.style.setProperty('--kendo-treeview-item-padding-x', padding.x);
        root.style.setProperty('--kendo-treeview-item-padding-y', padding.y);

        root.setAttribute('data-density', densityName);
    }

    // Apply font settings
    function applyFontSettings(fontName, fontSize) {
        const root = document.documentElement;
        const fontFamily = fontFamilies[fontName] || fontFamilies['System Default'];

        root.style.setProperty('--base-font-family', fontFamily);
        root.style.setProperty('--base-font-size', `${fontSize}px`);
        root.style.setProperty('--kendo-font-family', fontFamily);
        root.style.setProperty('--kendo-font-size', `${fontSize}px`);
        root.style.setProperty('--kendo-font-size-xs', `${fontSize - 2}px`);
        root.style.setProperty('--kendo-font-size-sm', `${fontSize - 1}px`);
        root.style.setProperty('--kendo-font-size-md', `${fontSize}px`);
        root.style.setProperty('--kendo-font-size-lg', `${fontSize + 2}px`);
        root.style.setProperty('--kendo-font-size-xl', `${fontSize + 4}px`);

        document.body.style.fontFamily = fontFamily;
        document.body.style.fontSize = `${fontSize}px`;
    }

    function applyReducedMotion(enabled) {
        if (enabled) {
            document.documentElement.classList.add('reduce-motion');
        } else {
            document.documentElement.classList.remove('reduce-motion');
        }
    }

    // Apply animations setting
    function applyAnimations(enabled, speed = 1.0) {
        const root = document.documentElement;

        if (!enabled) {
            root.classList.add('no-animations');
            root.style.setProperty('--transition-duration', '0ms');
            root.style.setProperty('--transition-fast', '0ms');
            root.style.setProperty('--transition-normal', '0ms');
            root.style.setProperty('--transition-slow', '0ms');
            root.style.setProperty('--animation-duration', '0ms');
            root.style.setProperty('--kendo-transition-duration', '0ms');
            root.style.setProperty('--kendo-animation-duration', '0ms');
        } else {
            root.classList.remove('no-animations');
            applyAnimationSpeed(speed);
        }
    }

    // Apply reduced motion
    function applyReducedMotion(enabled) {
        if (enabled) {
            document.documentElement.classList.add('reduce-motion');
        } else {
            document.documentElement.classList.remove('reduce-motion');
        }
    }

    // Apply high contrast
    function applyHighContrast(enabled) {
        if (enabled) {
            document.documentElement.classList.add('high-contrast');
        } else {
            document.documentElement.classList.remove('high-contrast');
        }
    }

    // Apply grid lines setting
    function applyGridLines(enabled) {
        const root = document.documentElement;

        if (enabled) {
            root.classList.add('show-grid-lines');
            root.style.setProperty('--kendo-grid-border-width', '1px');
            root.style.setProperty('--grid-cell-border-width', '1px');
        } else {
            root.classList.remove('show-grid-lines');
            root.style.setProperty('--kendo-grid-border-width', '0px');
            root.style.setProperty('--grid-cell-border-width', '0px');
        }
    }

    // Main function to apply all settings
    function applyAppearanceSettings() {
        const settings = loadSettings();

        // Apply all settings
        applyTheme(settings.theme);
        applyAccent(settings.accent, settings.customAccent);
        applyDensity(settings.density);
        applyFontSettings(settings.font, settings.fontSize);
        applyBorderRadius(settings.borderRadius);
        applyAnimations(settings.animations, settings.animationSpeed);
        applyReducedMotion(settings.reducedMotion);
        applyGridLines(settings.gridLines);

        console.log('Extended appearance settings applied:', settings);
    }

    // Listen for system theme changes if using auto
    function watchSystemTheme() {
        const darkModeQuery = window.matchMedia('(prefers-color-scheme: dark)');
        darkModeQuery.addEventListener('change', () => {
            const settings = loadSettings();
            if (settings.theme === 'auto') {
                applyTheme('auto');
            }
        });
    }


    // Listen for storage events (when settings change in another tab)
    window.addEventListener('storage', (e) => {
        if (e.key && e.key.startsWith('ibn-')) {
            applyAppearanceSettings();
        }
    });

    // Initialize on DOM ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', () => {
            applyAppearanceSettings();
            watchSystemTheme();
        });
    } else {
        applyAppearanceSettings();
        watchSystemTheme();
    }

    // Expose for manual refresh
    window.refreshAppearance = applyAppearanceSettings;

    // Expose individual functions for programmatic use
    window.appearanceManager = {
        applyTheme,
        applyAccent,
        applyDensity,
        applyBorderRadius,
        applyAnimationSpeed,
        applyAnimations,
        applyFontSettings,
        applyReducedMotion,
        applyGridLines,
        loadSettings,
        refresh: applyAppearanceSettings
    };

})();

