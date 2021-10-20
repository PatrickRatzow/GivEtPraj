<script setup lang="ts">
import { Camera, CameraResultType } from "@capacitor/camera";
import axios from "../utils/axios";
import { useCreateCaseStore } from "@/stores";

const caseStore = useCreateCaseStore();

const takePicture = async () => {
  const image = await Camera.getPhoto({
    quality: 90,
    allowEditing: true,
    resultType: CameraResultType.Base64,
  });

  caseStore.$patch((state) => {
    state.images.push(image);
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    (state as any).hasChanged = true;
  });
};

interface CameraRequest {
  images: string[];
}

const uploadPictures = async (): Promise<boolean> => {
  const data: CameraRequest = { images: caseStore.images.map((i) => i.base64String as string) };
  const resp = await axios.post("camera", data);

  return resp.status === 200;
};
</script>
<template>
  <div class="container">
    <div class="image-content">
      <button @click="takePicture">A button!</button>
      <img v-for="(image, i) in caseStore.images" :key="i" :src="`data:image/jpeg;base64,${image.base64String}`" />
      <button @click="uploadPictures">A button!</button>
    </div>
  </div>
</template>
