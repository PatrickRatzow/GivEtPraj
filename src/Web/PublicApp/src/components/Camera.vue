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
    <div class="flex flex-col items-center h-5/6 w-full">
      <ion-button class="my-8" @click="takePicture">Upload/Take picture</ion-button>

      <ion-content :scroll-events="true">
        <img
          v-for="(image, i) in caseStore.images"
          :key="i"
          :src="`data:image/jpeg;base64,${image.base64String}`"
          class="px-8 pb-5"
        />
      </ion-content>
    </div>
    <ion-button class="flex flex-row my-6" @click="uploadPictures">Continue</ion-button>
  </div>
</template>
