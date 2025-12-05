/**
 * AUTistima - JavaScript Principal
 * "Política pública em forma de afeto"
 */

// Aguarda o DOM estar pronto
document.addEventListener('DOMContentLoaded', function() {
    initializeApp();
});

function initializeApp() {
    // Inicializa tooltips do Bootstrap
    initTooltips();
    
    // Inicializa animações suaves
    initSmoothAnimations();
    
    // Marca link ativo na navbar
    highlightActiveNav();
    
    // Auto-dismiss de alertas
    initAutoDismissAlerts();
}

/**
 * Inicializa tooltips do Bootstrap
 */
function initTooltips() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    tooltipTriggerList.forEach(el => new bootstrap.Tooltip(el));
}

/**
 * Animações suaves para elementos ao entrar na viewport
 */
function initSmoothAnimations() {
    // Verifica se o usuário prefere movimento reduzido
    if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
        return;
    }
    
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };
    
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-in');
                observer.unobserve(entry.target);
            }
        });
    }, observerOptions);
    
    document.querySelectorAll('.feature-card, .card, .post-card').forEach(el => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(20px)';
        el.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
        observer.observe(el);
    });
}

/**
 * Destaca o link ativo na navegação
 */
function highlightActiveNav() {
    const currentPath = window.location.pathname.toLowerCase();
    const navLinks = document.querySelectorAll('.navbar-nav .nav-link');
    
    navLinks.forEach(link => {
        const href = link.getAttribute('href');
        if (href) {
            const linkPath = new URL(href, window.location.origin).pathname.toLowerCase();
            if (currentPath === linkPath || 
                (currentPath.startsWith(linkPath) && linkPath !== '/')) {
                link.classList.add('active');
            }
        }
    });
}

/**
 * Auto-dismiss de alertas após alguns segundos
 */
function initAutoDismissAlerts() {
    const alerts = document.querySelectorAll('.alert-success, .alert-info');
    alerts.forEach(alert => {
        setTimeout(() => {
            const bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
            bsAlert.close();
        }, 5000);
    });
}

/**
 * Função global para acolher posts via AJAX
 */
async function acolherPost(postId, button) {
    try {
        // Obtém o token anti-forgery
        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        
        const response = await fetch('/Acolhimento/Acolher', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': token || ''
            },
            body: `postId=${postId}`
        });
        
        const data = await response.json();
        
        if (data.success) {
            const countSpan = document.getElementById(`count-${postId}`);
            if (countSpan) {
                countSpan.textContent = `(${data.total})`;
            }
            
            const iconSpan = button.querySelector('.acolher-icon');
            if (data.acolhido) {
                button.classList.add('acolhido');
                if (iconSpan) iconSpan.textContent = '❤️';
            } else {
                button.classList.remove('acolhido');
                if (iconSpan) iconSpan.textContent = '🤍';
            }
            
            // Animação de feedback
            button.style.transform = 'scale(1.1)';
            setTimeout(() => {
                button.style.transform = 'scale(1)';
            }, 200);
        }
    } catch (error) {
        console.error('Erro ao acolher:', error);
    }
}

/**
 * Classe CSS para elementos animados
 */
document.head.insertAdjacentHTML('beforeend', `
<style>
    .animate-in {
        opacity: 1 !important;
        transform: translateY(0) !important;
    }
</style>
`);
