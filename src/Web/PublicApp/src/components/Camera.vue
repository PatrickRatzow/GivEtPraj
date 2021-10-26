<script setup lang="ts">
import { Camera, CameraResultType } from "@capacitor/camera";
import axios from "../utils/axios";
import { useCreateCaseStore } from "@/stores/create-case";

const caseStore = useCreateCaseStore();

const takePicture = async () => {
  const image = await Camera.getPhoto({
    quality: 90,
    allowEditing: true,
    resultType: CameraResultType.Base64,
  });

  caseStore.$patch((state) => {
    state.images.push(image);
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
  <div class="flex flex-col justify-between items-center h-full">
    <div class="grid grid-cols-2 h-5/6 w-full justify-items-center pt-8">
      <img
        v-for="index in 6"
        :key="index"
        class="h-12 w-12 bg-red-200"
        :src="`data:image/jpeg;base64,${caseStore.images[index - 1]}`"
        @click="takePicture"
      />
    </div>
    <ion-button class="flex flex-row my-6" @click="uploadPictures">Continue</ion-button>
  </div>
</template>
