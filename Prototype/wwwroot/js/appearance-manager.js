// appearance-manager.js
// This file manages loading and applying appearance settings from localStorage

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

            // Kendo/Telerik dark theme variables
            root.style.setProperty('--kendo-color-base', '#1a1f2e');
            root.style.setProperty('--kendo-color-base-hover', '#1f2937');
            root.style.setProperty('--kendo-color-base-active', '#374151');
            root.style.setProperty('--kendo-color-on-base', '#f1f5f9');
            root.style.setProperty('--kendo-color-surface', '#0f172a');
            root.style.setProperty('--kendo-color-surface-alt', '#1a1f2e');
            root.style.setProperty('--kendo-color-border', '#2d3748');
            root.style.setProperty('--kendo-color-border-alt', '#4b5563');

            // Grid-specific dark variables
            root.style.setProperty('--kendo-grid-bg', '#1a1f2e');
            root.style.setProperty('--kendo-grid-text', '#f1f5f9');
            root.style.setProperty('--kendo-grid-border', '#2d3748');
            root.style.setProperty('--kendo-grid-header-bg', '#0f172a');
            root.style.setProperty('--kendo-grid-header-text', '#f1f5f9');
            root.style.setProperty('--kendo-grid-alt-bg', '#151d2c');
            root.style.setProperty('--kendo-grid-hover-bg', '#374151');
            root.style.setProperty('--kendo-grid-selected-bg', '#1e3a5f');
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

            // Kendo/Telerik light theme variables
            root.style.setProperty('--kendo-color-base', '#ffffff');
            root.style.setProperty('--kendo-color-base-hover', '#f8f9fa');
            root.style.setProperty('--kendo-color-base-active', '#e5e7eb');
            root.style.setProperty('--kendo-color-on-base', '#111827');
            root.style.setProperty('--kendo-color-surface', '#ffffff');
            root.style.setProperty('--kendo-color-surface-alt', '#f9fafb');
            root.style.setProperty('--kendo-color-border', '#e5e7eb');
            root.style.setProperty('--kendo-color-border-alt', '#d1d5db');

            // Grid-specific light variables
            root.style.setProperty('--kendo-grid-bg', '#ffffff');
            root.style.setProperty('--kendo-grid-text', '#111827');
            root.style.setProperty('--kendo-grid-border', '#e5e7eb');
            root.style.setProperty('--kendo-grid-header-bg', '#f3f4f6');
            root.style.setProperty('--kendo-grid-header-text', '#111827');
            root.style.setProperty('--kendo-grid-alt-bg', '#f9fafb');
            root.style.setProperty('--kendo-grid-hover-bg', '#f1f5f9');
            root.style.setProperty('--kendo-grid-selected-bg', '#dbeafe');
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