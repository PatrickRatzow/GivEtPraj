<script setup lang="ts">
import { useImages } from "@/compositions/images";
import { useCreateCaseStore } from "@/stores/create-case";
import { close } from "ionicons/icons";

const createCase = useCreateCaseStore();
const router = useRouter();
const images = useImages();
const { t } = useI18n();

const currentIndex = ref(0);
const viewCameraModal = ref(false);

const openCameraModal = (index: number) => {
  if (createCase.images[index - 1]) return;

  currentIndex.value = index;
  viewCameraModal.value = true;
};
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
            <div
              class="bg-gray-100 dark:bg-opacity-5 text-center text-xl rounded-md relative"
              @click="openCameraModal(idx)"
            >
              <i class="fas fa-plus text-black dark:text-white my-12"></i>
              <img
                v-if="createCase.images[idx - 1] != undefined"
                class="absolute inset-0 w-full h-full object-cover"
                :src="images.getImageAsDataUrl(idx - 1)"
                @click="router.push(`/create-praj/pictures/${idx}`)"
              />
            </div>
          </template>
        </div>
        <div class="w-full">
          <ion-button expand="block" @click="router.push('/create-praj/checkout')">
            {{ t("navigation.next") }}
          </ion-button>
        </div>
      </div>

      <ion-modal :is-open="viewCameraModal" @didDismiss="viewCameraModal = false">
        <ion-header translucent>
          <ion-toolbar>
            <ion-title>{{ t("create-case.pictures.modal.title") }}</ion-title>
            <ion-buttons slot="end">
              <ion-button @click="viewCameraModal = false">
                <ion-icon :icon="close"></ion-icon>
              </ion-button>
            </ion-buttons>
          </ion-toolbar>
        </ion-header>
        <ion-content fullscreen overflow-scroll="false">
          <camera-modal :index="currentIndex" @pictureTaken="viewCameraModal = false"></camera-modal>
        </ion-content>
      </ion-modal>
    </ion-content>
  </ion-page>
</template>
