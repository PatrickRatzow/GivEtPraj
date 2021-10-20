<script setup lang="ts">
import { Camera, Photo, CameraResultType } from "@capacitor/camera";
import { computed, ref } from "vue";
import axios from "../utils/axios";
import { useCreateCaseStore } from "@/stores";

const caseStore = useCreateCaseStore();
const takePicture = async () => {
  const image = await Camera.getPhoto({
    quality: 90,
    allowEditing: true,
    resultType: CameraResultType.Base64,
  });

  store.commit("addImage", image);
};
interface CameraRequest {
  images: string[];
}

const uploadPictures = async (): Promise<boolean> => {
  const data: CameraRequest = { images: images.value.map((i) => i.base64String as string) };
  const resp = await axios.post("camera", data);

  return resp.status === 200;
};
</script>
<template>
  <div class="container">
    <div class="image-content">
      <button @click="takePicture">A button!</button>
      <img v-for="(image, i) in images" :key="i" :src="`data:image/jpeg;base64,${image.base64String}`" />
      <button @click="uploadPictures">A button!</button>
    </div>
  </div>
</template>
