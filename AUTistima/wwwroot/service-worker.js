// Service Worker para AUTistima PWA
// Versão do cache - incrementar a cada deploy
const CACHE_VERSION = 'v1.0.2';
const CACHE_NAME = `autistima-${CACHE_VERSION}`;

// Arquivos essenciais para cache offline
const STATIC_ASSETS = [
    '/',
    '/css/site.css',
    '/js/site.js',
    '/lib/bootstrap/dist/css/bootstrap.min.css',
    '/lib/bootstrap/dist/js/bootstrap.bundle.min.js',
    '/manifest.json',
    '/offline.html',
    '/icons/icon-192x192.png',
    '/icons/icon-512x512.png'
];

// Páginas importantes para cache
const PAGES_TO_CACHE = [
    '/Glossario',
    '/Manejos',
    '/Acolhimento',
    '/Home/Sobre'
];

// Instalação do Service Worker
self.addEventListener('install', (event) => {
    console.log('[SW] Instalando Service Worker...');
    
    event.waitUntil(
        caches.open(CACHE_NAME)
            .then((cache) => {
                console.log('[SW] Cacheando arquivos estáticos...');
                return cache.addAll(STATIC_ASSETS);
            })
            .then(() => {
                console.log('[SW] Service Worker instalado com sucesso!');
                return self.skipWaiting();
            })
            .catch((error) => {
                console.error('[SW] Erro na instalação:', error);
            })
    );
});

// Ativação - limpa caches antigos
self.addEventListener('activate', (event) => {
    console.log('[SW] Ativando Service Worker...');
    
    event.waitUntil(
        caches.keys()
            .then((cacheNames) => {
                return Promise.all(
                    cacheNames
                        .filter((name) => name.startsWith('autistima-') && name !== CACHE_NAME)
                        .map((name) => {
                            console.log('[SW] Removendo cache antigo:', name);
                            return caches.delete(name);
                        })
                );
            })
            .then(() => {
                console.log('[SW] Service Worker ativado!');
                return self.clients.claim();
            })
    );
});

// Estratégia de fetch: Network First com fallback para cache
self.addEventListener('fetch', (event) => {
    const { request } = event;
    const url = new URL(request.url);
    
    // Ignorar requisições não-GET e de outros domínios
    if (request.method !== 'GET' || url.origin !== location.origin) {
        return;
    }
    
    // Ignorar requisições de API (sempre rede)
    if (url.pathname.startsWith('/api/')) {
        return;
    }
    
    // Para arquivos estáticos: Cache First
    if (isStaticAsset(url.pathname)) {
        event.respondWith(cacheFirst(request));
        return;
    }
    
    // Para páginas HTML: Network First com fallback
    event.respondWith(networkFirst(request));
});

// Verifica se é um arquivo estático
function isStaticAsset(pathname) {
    const staticExtensions = ['.css', '.js', '.png', '.jpg', '.jpeg', '.gif', '.svg', '.woff', '.woff2', '.ico'];
    return staticExtensions.some(ext => pathname.endsWith(ext)) || pathname.startsWith('/lib/');
}

// Estratégia Cache First
async function cacheFirst(request) {
    const cachedResponse = await caches.match(request);
    if (cachedResponse) {
        return cachedResponse;
    }
    
    try {
        const networkResponse = await fetch(request);
        if (networkResponse.ok) {
            const cache = await caches.open(CACHE_NAME);
            cache.put(request, networkResponse.clone());
        }
        return networkResponse;
    } catch (error) {
        console.error('[SW] Erro no fetch:', error);
        return new Response('Offline', { status: 503 });
    }
}

// Estratégia Network First
async function networkFirst(request) {
    try {
        const networkResponse = await fetch(request);
        
        if (networkResponse.ok) {
            // Cachear a resposta para uso offline
            const cache = await caches.open(CACHE_NAME);
            cache.put(request, networkResponse.clone());
        }
        
        return networkResponse;
    } catch (error) {
        console.log('[SW] Rede indisponível, buscando no cache...');
        
        const cachedResponse = await caches.match(request);
        if (cachedResponse) {
            return cachedResponse;
        }
        
        // Retorna página offline se disponível
        const offlinePage = await caches.match('/offline.html');
        if (offlinePage) {
            return offlinePage;
        }
        
        return new Response('Você está offline e esta página não está em cache.', {
            status: 503,
            headers: { 'Content-Type': 'text/html; charset=utf-8' }
        });
    }
}

// =====================================================
// PUSH NOTIFICATIONS
// =====================================================

self.addEventListener('push', (event) => {
    console.log('[SW] Push recebido:', event);
    
    let data = {
        title: 'AUTistima',
        body: 'Você tem uma nova notificação!',
        icon: '/icons/icon-192x192.png',
        badge: '/icons/icon-72x72.png',
        tag: 'autistima-notification',
        data: { url: '/' }
    };
    
    if (event.data) {
        try {
            const payload = event.data.json();
            data = { ...data, ...payload };
        } catch (e) {
            data.body = event.data.text();
        }
    }
    
    const options = {
        body: data.body,
        icon: data.icon || '/icons/icon-192x192.png',
        badge: data.badge || '/icons/icon-72x72.png',
        tag: data.tag || 'autistima-notification',
        vibrate: [100, 50, 100],
        data: data.data || { url: '/' },
        actions: [
            { action: 'open', title: 'Abrir', icon: '/icons/icon-96x96.png' },
            { action: 'close', title: 'Fechar' }
        ],
        requireInteraction: false
    };
    
    event.waitUntil(
        self.registration.showNotification(data.title, options)
    );
});

// Clique na notificação
self.addEventListener('notificationclick', (event) => {
    console.log('[SW] Clique na notificação:', event);
    
    event.notification.close();
    
    if (event.action === 'close') {
        return;
    }
    
    const urlToOpen = event.notification.data?.url || '/';
    
    event.waitUntil(
        clients.matchAll({ type: 'window', includeUncontrolled: true })
            .then((windowClients) => {
                // Se já tem uma janela aberta, foca nela
                for (const client of windowClients) {
                    if (client.url.includes(self.location.origin) && 'focus' in client) {
                        client.navigate(urlToOpen);
                        return client.focus();
                    }
                }
                // Senão, abre uma nova
                if (clients.openWindow) {
                    return clients.openWindow(urlToOpen);
                }
            })
    );
});

// Fechamento da notificação
self.addEventListener('notificationclose', (event) => {
    console.log('[SW] Notificação fechada:', event);
});

// Background Sync (para enviar dados quando voltar online)
self.addEventListener('sync', (event) => {
    console.log('[SW] Sync event:', event.tag);
    
    if (event.tag === 'sync-notifications') {
        event.waitUntil(syncNotifications());
    }
});

async function syncNotifications() {
    try {
        const response = await fetch('/api/notificacoes/sync', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }
        });
        console.log('[SW] Notificações sincronizadas');
    } catch (error) {
        console.error('[SW] Erro ao sincronizar:', error);
    }
}

console.log('[SW] Service Worker carregado - AUTistima PWA');
