'use strict';

const applicationServerPublicKey = 'BFT8Qom635-BGZB88CrAGF9WB20eH2fl7MWzh03UmnWhJqdQTlgVzAXVfHCR4etLnSH17wiFkdkzUHTOb7BHyLI';

let isSubscribed = false;
let swRegistration = null;

function urlB64ToUint8Array(base64String) {
  const padding = '='.repeat((4 - base64String.length % 4) % 4);
  const base64 = (base64String + padding)
    .replace(/\-/g, '+')
    .replace(/_/g, '/');

  const rawData = window.atob(base64);
  const outputArray = new Uint8Array(rawData.length);

  for (let i = 0; i < rawData.length; ++i) {
    outputArray[i] = rawData.charCodeAt(i);
  }
  return outputArray;
}



if ('serviceWorker' in navigator && 'PushManager' in window) {
  console.log('Service Worker and Push is supported');

  navigator.serviceWorker.register('Scripts/Shared/sw.js')
    .then(function (swReg) {
      console.log('Service Worker is registered', swReg);

      swRegistration = swReg;
    })
    .catch(function (error) {
      console.error('Service Worker Error', error);
    });
} else {
  console.warn('Push messaging is not supported');
}






function initializeUI() {

  subscribeUser();


  // Set the initial subscription value
  swRegistration.pushManager.getSubscription()
    .then(function (subscription) {
      isSubscribed = !(subscription === null);

      updateSubscriptionOnServer(subscription);

      if (isSubscribed) {
        console.log('User IS subscribed.');
      } else {
        console.log('User is NOT subscribed.');
      }
    });
}

navigator.serviceWorker.register('Scripts/Shared/sw.js')
  .then(function (swReg) {
    console.log('Service Worker is registered', swReg);

    swRegistration = swReg;
    initializeUI();
  })





function subscribeUser() {
  const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
  swRegistration.pushManager.subscribe({
    userVisibleOnly: true,
    applicationServerKey: applicationServerKey
  })
    .then(function (subscription) {
      console.log('User is subscribed.');

      updateSubscriptionOnServer(subscription);

      isSubscribed = true;
    })
    .catch(function (err) {
      console.log('Failed to subscribe the user: ', err);
    });
}






const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
swRegistration.pushManager.subscribe({
  userVisibleOnly: true,
  applicationServerKey: applicationServerKey
})



swRegistration.pushManager.subscribe({
  userVisibleOnly: true,
  applicationServerKey: applicationServerKey
})
  .then(function (subscription) {
    console.log('User is subscribed.');

    updateSubscriptionOnServer(subscription);

    isSubscribed = true;

  })
  .catch(function (err) {
    console.log('Failed to subscribe the user: ', err);
  })






function updateSubscriptionOnServer(subscription) {
  // TODO: Send subscription to application server

  console.log('Send Subscription:');
  console.log(subscription);
  console.log('------------------------------------------------------------')
  console.log(JSON.stringify(subscription));
  console.log('------------------------------------------------------------')

  return fetch('/Api/SaveSubscription/', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(subscription)
  })
    .then(function (response) {

      console.log('Save Subscription Response:');
      console.log(response);

      //if (!response.ok) {
      //  throw new Error('Bad status code from server.');
      //}

      return response.json();
    })
    .then(function (responseData) {
      console.log('Save Subscription Another Response:');
      console.log(responseData);

      //if (!(responseData.data && responseData.data.success)) {
      //  throw new Error('Bad response from server.');
      //}
    });

  //const subscriptionJson = document.querySelector('.js-subscription-json');
  //const subscriptionDetails =
  //  document.querySelector('.js-subscription-details');

  //if (subscription) {
  //  subscriptionJson.textContent = JSON.stringify(subscription);
  //  subscriptionDetails.classList.remove('is-invisible');
  //} else {
  //  subscriptionDetails.classList.add('is-invisible');
  //}
}




//const title = 'Новину Додано!';
//const options = {
//  body: '',
//  //icon: 'images/icon.png'
//};

//const notificationPromise = self.registration.showNotification(title, options);
//event.waitUntil(notificationPromise);






//var $form = $('#create-news-form');
//var $title = $('#Title');

//$form.submit(function () {
//  var self = $(this);
//  console.log('Submit');

//  var title = 'Новина!';
//  var options = {
//    body: $title.val()
//  };
//});

//var notificationPromise = self.registration.showNotification(title, options);
//event.waitUntil(notificationPromise);
