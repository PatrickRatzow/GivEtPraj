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
      <div v-for="index in 6" :key="index">
        <div
          v-if="caseStore.images[index - 1] == null"
          class="w-full px-2 bg-green-200 border-4 border-indigo-600 rounded"
        >
          <button @click="takePicture">
            <i class="fas fa-photo-video text-5xl mx-10 my-10"></i>
          </button>
        </div>
        <img v-else class="w-full px-8" :src="`data:image/jpeg;base64,${caseStore.images[index - 1]?.base64String}`" />
      </div>
    </div>
    <ion-button class="flex flex-row my-6" @click="uploadPictures">Continue</ion-button>
  </div>
</template>
