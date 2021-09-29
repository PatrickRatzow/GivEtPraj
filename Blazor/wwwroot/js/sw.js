let data = [];
let num = 0;

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