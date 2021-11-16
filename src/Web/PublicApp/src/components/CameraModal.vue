<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";
import { useImages } from "@/compositions/images";

defineProps<{ index: number }>();
const createCase = useCreateCaseStore();
const images = useImages();
const video = ref<HTMLVideoElement | null>(null);
const canvas = ref<HTMLCanvasElement | null>(null);

watchEffect(() => {
  if (navigator.mediaDevices.getUserMedia) {
    navigator.mediaDevices
      .getUserMedia({ video: true })
      .then(function (stream) {
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        video.value!.srcObject = stream;
      })
      .catch(function (err0r) {
        console.log("Something went wrong!");
      });
  }
});

const takeImg = (index: number) => {
  if (canvas.value == null) return;
  if (video.value == null) return;

  canvas.value.width = video.value.videoWidth;
  canvas.value.height = video.value.videoHeight;

  const context = canvas.value.getContext("2d");
  if (context == null) return;
  context.drawImage(video.value, 0, 0, canvas.value.width, canvas.value.height);
  images.addPicture(index - 1, {
    base64String: canvas.value.toDataURL("image/jpeg").replaceAll("data:image/jpeg;base64,", ""),
    format: "jpeg",
    saved: true,
  });
};
const switchCamera = () => {
  const b = 1;
};

const accessFileSystem = () => {
  const b = 1;
};
</script>

<template>
  <div class="flex flex-col h-full bg-black">
    <div class="flex flex-col h-full justify-center">
      <video ref="video" autoplay="true"></video>
    </div>
    <div class="flex flex-row justify-between px-12 pb-12">
      <button @click="accessFileSystem">
        <i class="fas fa-photo-video text-white text-4xl"></i>
      </button>
      <button @click="takeImg(index)">
        <i class="fas fa-circle text-white text-7xl pb-12 pt-3"></i>
      </button>
      <button @click="switchCamera">
        <i class="fas fa-sync text-white text-4xl"></i>
      </button>
    </div>
    <canvas ref="canvas" hidden="true"></canvas>
  </div>
</template>
