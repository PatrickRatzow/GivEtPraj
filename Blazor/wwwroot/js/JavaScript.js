var x = document.getElementById("demo");
var latitude;
var longitude;


function startVideo() {
    var video = document.getElementById('video');

    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
            try {
                video.srcObject = stream;
            } catch (error) {
                video.src = window.URL.createObjectURL(stream);
            }
            video.play();
        });
    }
}

function takePhoto() {
    let canvas = document.getElementById('canvas');
    canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
    let image_data_url = canvas.toDataURL('image/jpeg');

    // data url of the image
    return image_data_url
}

function pingWorker() {
    if ('serviceWorker' in navigator) {
        window.addEventListener('load', () => {
            console.log('App is loader');
            navigator.serviceWorker.register('../service-worker.js')
                .then(() => {
                    console.log("Service Worker registerd");
                })
        })
    }
}

function notify() {
    const items = ["First", "End of the break", "Second"];
    const options = { data: items };

    if ('serviceWorker' in navigator) {
        Notification.requestPermission()
            .then(() => navigator.serviceWorker.register('sw.js'))
            .then(() => navigator.serviceWorker.ready
                .then((s) => {
                    s.showNotification('Click to begin', options);
                }))
            .catch(e => console.log(e.message));
    }
}