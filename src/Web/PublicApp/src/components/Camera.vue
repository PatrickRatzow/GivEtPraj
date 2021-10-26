<script setup lang="ts">
import { useImages } from "@/compositions/images";
import { useCreateCaseStore } from "@/stores/create-case";

const createCase = useCreateCaseStore();
const router = useRouter();
const images = useImages();
</script>
<template>
  <div class="flex flex-col justify-between items-center h-full ion-padding">
    <div class="grid grid-cols-3 gap-4 w-full auto-rows-fr">
      <template v-for="idx in 6" :key="idx">
        <div class="bg-gray-100 dark:bg-opacity-5 text-center text-xl rounded-md relative">
          <i class="fas fa-plus text-black dark:text-white my-4" @click="images.takePicture(idx - 1)"></i>
          <img
            v-if="createCase.images[idx - 1]"
            class="absolute inset-0 object-cover w-full h-full"
            :src="images.getImageAsDataUrl(idx - 1)"
          />
        </div>
      </template>
    </div>
    <div class="w-full">
      <ion-button expand="block" @click="router.push('/opret-praj/afslut')">Continue</ion-button>
    </div>
  </div>
</template>
