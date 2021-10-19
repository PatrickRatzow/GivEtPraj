<script setup lang="ts">
import { Camera, CameraResultType } from "@capacitor/camera";

const takePicture = async () => {
  const image = await Camera.getPhoto({
    quality: 90,
    allowEditing: true,
    resultType: CameraResultType.Uri,
  });

  // image.webPath will contain a path that can be set as an image src.
  // You can access the original file using image.path, which can be
  // passed to the Filesystem API to read the raw data of the image,
  // if desired (or pass resultType: CameraResultType.Base64 to getPhoto)
  var imageUrl = image.webPath;

  // Can be set to the src of an image now
  //This gives an error;
  //imageElement.src = imageUrl;
};

let viewGalleryModal = false;

function greet() {
  alert("HELLO");
}
</script>
<template>
  <div class="container">
    <div class="balck-screen"></div>
    <div class="video">
      <video class="w-screen"></video>
    </div>

    <div class="content">
      <div class="flex flex-row justify-between mx-8 mb-10 items-center">
        <button class="rounded-full z-10" type="button">
          <i class="text-black fal fa-sync text-4xl"></i>
        </button>
        <button
          id="click-photo"
          class="bg-white-500 rounded-full w-16 h-16 border-4 border-black z-10"
          type="button"
        ></button>
        <button
          id="flipCam-btn"
          class="rounded-full w-14 h-14 z-10 bg-black border-2 border-gray-200"
          type="button"
        ></button>
      </div>
    </div>

    <div>
      <p>Hello there</p>
    </div>
    <button @click="takePicture">A button!</button>
  </div>
</template>
