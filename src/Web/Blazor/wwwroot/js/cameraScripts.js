document.addEventListener("DOMContentLoaded", function () {
    var video = document.getElementById('video');
    let flipBtn = document.querySelector('#camFlip-btn');
});


let shouldFaceUser = true;
let defaultsOpts = { audio: false, video: true }
defaultsOpts.video = { facingMode: shouldFaceUser ? 'user' : 'environment' }
let stream = null;
let videoSettings;

async function startVideo() {
    if (navigator.mediaDevices?.getUserMedia) {
        const _stream = await navigator.mediaDevices.getUserMedia(defaultsOpts);

        stream = _stream;

        const videoTracks = stream.getVideoTracks();
        console.log({ videoTracks})
        const videoTrack = videoTracks[videoTracks.length - 1];
        console.log({ videoTrack })
        videoSettings = videoTrack.getSettings();
        console.log({ videoSettings })
         
        video.srcObject = stream;
        video.play();
    }
}


function takePhoto() {
    let video = document.getElementById('video');
    let canvas = document.createElement('canvas');


    canvas.height = videoSettings.height;
    canvas.width = videoSettings.width;
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
