// Dynamically calculate and set navbar height as CSS variable
(function() {
    function updateNavbarHeight() {
        const navbar = document.querySelector('.appbar');
        if (navbar) {
            const height = navbar.offsetHeight;
            document.documentElement.style.setProperty('--navbar-height', `${height}px`);
        }
    }

    // Update on load
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', updateNavbarHeight);
    } else {
        updateNavbarHeight();
    }

    // Update on resize (debounced)
    let resizeTimer;
    window.addEventListener('resize', function() {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(updateNavbarHeight, 150);
    });

    // Update after Blazor enhanced navigation
    if (window.Blazor) {
        window.Blazor.addEventListener('enhancedload', updateNavbarHeight);
    }

    // Observe DOM changes for dynamic navbar content
    const observer = new MutationObserver(function(mutations) {
        mutations.forEach(function(mutation) {
            if (mutation.type === 'childList' || mutation.type === 'attributes') {
                updateNavbarHeight();
            }
        });
    });

    // Start observing once navbar is available
    function startObserving() {
        const navbar = document.querySelector('.appbar');
        if (navbar) {
            observer.observe(navbar, {
                childList: true,
                subtree: true,
                attributes: true,
                attributeFilter: ['class', 'style']
            });
        } else {
            // Retry if navbar not found yet
            setTimeout(startObserving, 100);
        }
    }

    startObserving();
})();
