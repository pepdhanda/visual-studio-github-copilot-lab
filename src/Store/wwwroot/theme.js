// Theme management with localStorage persistence
window.themeManager = {
    init: function() {
        // Load theme from localStorage or default to light
        const savedTheme = localStorage.getItem('theme') || 'light';
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
        localStorage.setItem('theme', theme);
    },
    
    getTheme: function() {
        return document.documentElement.getAttribute('data-theme') || 'light';
    }
};

// Initialize theme on page load
window.themeManager.init();
