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

    // Apply theme (light/dark/auto)
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