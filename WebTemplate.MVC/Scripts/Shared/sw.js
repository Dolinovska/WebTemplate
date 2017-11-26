'use strict';

self.addEventListener('push', function (event) {
  var txt = event.data.text();

  console.log('[Service Worker] Push Received.');
  console.log(`[Service Worker] Push had this data: "${txt}"`);

  //const title = 'Новина!';
  //const options = {
  //  body: txt,
  //  //icon: 'images/icon.png',
  //  //badge: 'images/badge.png'
  //};

  event.waitUntil(self.registration.showNotification('Новина!', { body: txt }));
});



self.addEventListener('notificationclick', function (event) {
  console.log('[Service Worker] Notification click Received.');

  event.notification.close();

  event.waitUntil(
    clients.openWindow('https://developers.google.com/web/')
  );
});