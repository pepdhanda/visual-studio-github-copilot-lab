// Theme management with localStorage persistence
window.themeManager = {
    init: function() {
        // Load theme from localStorage or default to light
        let savedTheme = 'light';
        try {
            savedTheme = localStorage.getItem('theme') || 'light';
        } catch (e) {
            console.warn('localStorage not available, using default theme');
        }
        this.setTheme(savedTheme);
    },
    
    toggleTheme: function() {
        const currentTheme = document.documentElement.getAttribute('data-theme') || 'light';
        const newTheme = currentTheme === 'light' ? 'dark' : 'light';
        this.setTheme(newTheme);
        return newTheme;
    },
    
    setTheme: function(theme) {
        document.documentElement.setAttribute('data-theme', theme);
        try {
            localStorage.setItem('theme', theme);
        } catch (e) {
            console.warn('localStorage not available, theme will not persist');
        }
    },
    
    getTheme: function() {
        return document.documentElement.getAttribute('data-theme') || 'light';
    }
};

// Initialize theme on page load
window.themeManager.init();
