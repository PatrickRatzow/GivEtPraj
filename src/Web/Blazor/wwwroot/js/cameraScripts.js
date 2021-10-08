document.addEventListener("DOMContentLoaded", function () {
    let video = document.getElementById('video');
    let flipBtn = document.querySelector('#camFlip-btn');
});


let shouldFaceUser = true;
let defaultsOpts = { audio: false, video: true }
defaultsOpts.video = { facingMode: shouldFaceUser ? 'user' : 'environment' }
let stream = null;

function startVideo() {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia(defaultsOpts)
            .then(function (_stream) {
                stream = _stream;
                video.srcObject = stream;
                video.play();
            })
            .catch(function (error) {
                console.log(error)
            });
    }
}


function takePhoto() {
    let canvas = document.createElement('canvas');
    canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
    let image_data_url = canvas.toDataURL('image/png');

    // data url of the image
    return image_data_url
}

function flipCamera() {
    if (stream == null) return
    // we need to flip, stop everything
    stream.getTracks().forEach(t => {
        t.stop();
    });
    // toggle / flip
    shouldFaceUser = !shouldFaceUser;
    startVideo();
}
