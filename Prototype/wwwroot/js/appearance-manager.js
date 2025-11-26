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
        'Open Sans': "'Open Sans', sans-serif"
    };

    // Load settings from localStorage
    function loadSettings() {
        const settings = { ...defaults };

        try {
            const theme = localStorage.getItem('ibn-theme');
            if (theme) settings.theme = JSON.parse(theme);

            const accent = localStorage.getItem('ibn-accent');
            if (accent) settings.accent = JSON.parse(accent);

            const density = localStorage.getItem('ibn-density');
            if (density) settings.density = JSON.parse(density);

            const fontSize = localStorage.getItem('ibn-fontSize');
            if (fontSize) settings.fontSize = JSON.parse(fontSize);

            const font = localStorage.getItem('ibn-font');
            if (font) settings.font = JSON.parse(font);

            const animations = localStorage.getItem('ibn-animations');
            if (animations !== null) settings.animations = JSON.parse(animations);

            const reducedMotion = localStorage.getItem('ibn-reducedMotion');
            if (reducedMotion !== null) settings.reducedMotion = JSON.parse(reducedMotion);

            const highContrast = localStorage.getItem('ibn-highContrast');
            if (highContrast !== null) settings.highContrast = JSON.parse(highContrast);

            const gridLines = localStorage.getItem('ibn-gridLines');
            if (gridLines !== null) settings.gridLines = JSON.parse(gridLines);
        } catch (e) {
            console.error('Error loading appearance settings:', e);
        }

        return settings;
    }

    // Apply theme (light/dark/dim/auto)
    function applyTheme(theme) {
        const root = document.documentElement;

        if (theme === 'auto') {
            // Check system preference
            const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
            theme = prefersDark ? 'dark' : 'light';
        }

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
        } else {
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
    function applyAccent(accentName) {
        const root = document.documentElement;
        const colors = accentColors[accentName] || accentColors.blue;

        // Custom app variables
        root.style.setProperty('--accent', colors.primary);
        root.style.setProperty('--accent-weak', colors.light);
        root.style.setProperty('--accent-hover', colors.hover);

        // Kendo/Telerik theme variables
        root.style.setProperty('--kendo-color-primary', colors.primary);
        root.style.setProperty('--kendo-color-primary-hover', colors.hover);
        root.style.setProperty('--kendo-color-primary-active', colors.hover);
        root.style.setProperty('--kendo-color-on-primary', '#ffffff');

        // Extract RGB values from hex
        const rgb = hexToRgb(colors.primary);
        if (rgb) {
            root.style.setProperty('--accent-rgb', `${rgb.r}, ${rgb.g}, ${rgb.b}`);
        }
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

    // Apply density
    function applyDensity(densityName) {
        const root = document.documentElement;
        const density = densitySettings[densityName] || densitySettings.comfortable;

        // Adjust spacing scale
        const baseSpacing = 4; // 4px base unit
        for (let i = 0; i <= 30; i++) {
            const value = Math.round(baseSpacing * i * density.multiplier);
            root.style.setProperty(`--kendo-spacing-${i}`, `${value}px`);
        }

        // Adjust line height
        root.style.setProperty('--base-line-height', density.lineHeight);

        // Component-specific padding based on density
        const componentPadding = {
            compact: { x: '8px', y: '4px' },
            comfortable: { x: '12px', y: '8px' },
            spacious: { x: '16px', y: '12px' }
        };

        const padding = componentPadding[densityName];
        root.style.setProperty('--kendo-grid-cell-padding-x', padding.x);
        root.style.setProperty('--kendo-grid-cell-padding-y', padding.y);
        root.style.setProperty('--kendo-grid-header-padding-x', padding.x);
        root.style.setProperty('--kendo-grid-header-padding-y', padding.y);
        root.style.setProperty('--kendo-button-padding-x', padding.x);
        root.style.setProperty('--kendo-button-padding-y', padding.y);
        root.style.setProperty('--kendo-input-padding-x', padding.x);
        root.style.setProperty('--kendo-input-padding-y', padding.y);
    }

    // Apply font settings
    function applyFontSettings(fontName, fontSize) {
        const root = document.documentElement;
        const fontFamily = fontFamilies[fontName] || fontFamilies['System Default'];

        // Custom app variables
        root.style.setProperty('--base-font-family', fontFamily);
        root.style.setProperty('--base-font-size', `${fontSize}px`);

        // Kendo/Telerik typography variables
        root.style.setProperty('--kendo-font-family', fontFamily);
        root.style.setProperty('--kendo-font-size', `${fontSize}px`);
        root.style.setProperty('--kendo-font-size-sm', `${fontSize - 1}px`);
        root.style.setProperty('--kendo-font-size-lg', `${fontSize + 2}px`);

        // Apply to body
        document.body.style.fontFamily = fontFamily;
        document.body.style.fontSize = `${fontSize}px`;
    }

    // Apply animations setting
    function applyAnimations(enabled) {
        const root = document.documentElement;

        if (!enabled) {
            root.style.setProperty('--transition-duration', '0ms');
            root.style.setProperty('--animation-duration', '0ms');
        } else {
            root.style.setProperty('--transition-duration', '150ms');
            root.style.setProperty('--animation-duration', '200ms');
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
            document.documentElement.classList.add('show-grid-lines');
            root.style.setProperty('--kendo-grid-border-width', '1px');
        } else {
            document.documentElement.classList.remove('show-grid-lines');
            root.style.setProperty('--kendo-grid-border-width', '0px');
        }
    }

    // Main function to apply all settings
    function applyAppearanceSettings() {
        const settings = loadSettings();

        applyTheme(settings.theme);
        applyAccent(settings.accent);
        applyDensity(settings.density);
        applyFontSettings(settings.font, settings.fontSize);
        applyAnimations(settings.animations);
        applyReducedMotion(settings.reducedMotion);
        applyHighContrast(settings.highContrast);
        applyGridLines(settings.gridLines);

        console.log('Appearance settings applied:', settings);
    }

    // Listen for system theme changes if using auto
    function watchSystemTheme() {
        const darkModeQuery = window.matchMedia('(prefers-color-scheme: dark)');
        darkModeQuery.addEventListener('change', (e) => {
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

    // Expose function globally for manual refresh
    window.refreshAppearance = applyAppearanceSettings;

})();