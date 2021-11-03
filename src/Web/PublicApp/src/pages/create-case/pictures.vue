<script setup lang="ts">
import { useImages } from "@/compositions/images";
import { useCreateCaseStore } from "@/stores/create-case";

const createCase = useCreateCaseStore();
const router = useRouter();
const images = useImages();
const { t } = useI18n();
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <back-button url="/create-praj/category"></back-button>
      </ion-buttons>
      <ion-title>{{ t("create-case.pictures.title") }} </ion-title>
    </ion-toolbar>
    <ion-content class="ion-padding">
      <div class="flex flex-col justify-between items-center h-full">
        <div class="grid grid-cols-3 gap-4 w-full auto-rows-fr">
          <template v-for="idx in 6" :key="idx">
            <div class="bg-gray-100 dark:bg-opacity-5 text-center text-xl rounded-md relative">
              <i class="fas fa-plus text-black dark:text-white my-12" @click="images.takePicture(idx - 1)"></i>
              <img
                v-if="createCase.images[idx - 1]"
                class="absolute inset-0 w-full h-full"
                :src="images.getImageAsDataUrl(idx - 1)"
                @click="router.push(`/create-praj/pictures/${idx}`)"
              />
            </div>
          </template>
        </div>
        <div class="w-full">
          <ion-button expand="block" @click="router.push('/create-praj/checkout')">{{
            t("navigation.next")
          }}</ion-button>
        </div>
      </div>
    </ion-content>
  </ion-page>
</template>
