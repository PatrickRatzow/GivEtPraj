// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
//self.addEventListener('fetch', () => { });

var cacheName = 'v1.0';
let data = [];
let num = 0;

var cacheAssets = [
    'index.html',
    'about.html',
    '/js/main.js',
    '/css/style.css'
]

// installation
self.addEventListener('install', e => {
    console.log('Service Worker: Installed');
    e.waitUntil(
        caches
            .open(cacheName)
            .then(cache => {
                console.log('Service Worker: Caching Files');
                cache.addAll(cacheAssets);
            })
            .then(() => self.skipWaiting())
    );
});

self.onnotificationclick = (event) => {
    event.notification.close();
    if (event.notification.data) {
        ({ data } = event.notification);
    }

    if (num < data.length) {
        event.waitUntil(new Promise(function (resolve, reject) {
            setInterval(() => {
                self.registration.showNotification(data.num);
                resolve();
            }, 3000);
        }))
    }
};