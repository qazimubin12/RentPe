var cacheName = 'v1';
var url = self.location.href.split("/");

url.pop();

var path = url.join("/") + "/";

var cachedFiles = [
    path,
    path + 'Scripts/register-service-worker.js'
];

self.addEventListener('install', function (e) {
    e.waitUntil(
        caches.open(cacheName).then(function (cache) {
            Promise.all(
                cachedFiles.map(function (url) { cache.add(url) })
            );
        })
    );
});



self.addEventListener('activate', function (e) {

    e.waitUntil(
        caches.keys().then(function (cachedNames) {
            cachedNames.forEach(function (currentCache) {
                if (currentCache !== cacheName) {
                    caches.delete(currentCache);
                }
            }, this);
        })
    );
});

self.addEventListener('fetch', function (e) {
    if (cachedFiles.indexOf(e.request.url) < 0) {
        return;
    }

    e.respondWith(
        caches.match(e.request.url).then(function (resp) {
            return resp || fetch(e.request.url).then(function (response) {
                var clonedResponse = response.clone();

                if (response.redirected) {
                    return new Response(response.body);
                }

                caches.open(cacheName).then(function (cache) {
                    cache.put(e.request.url, clonedResponse);
                });
                return response;
            });
        }).catch(function () {
            return caches.match('/');
        })
    );
});