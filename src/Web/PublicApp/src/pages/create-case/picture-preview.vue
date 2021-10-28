<script setup lang="ts">
import { useImages } from "@/compositions/images";

const route = useRoute();
const images = useImages();
const router = useRouter();
const { t } = useI18n();

const redirectTo404 = () => router.replace("/404-not-found");

const image = ref<string | undefined>();
const fetchImage = (id: string | string[]) => {
  if (id == undefined) return;
  if (id.length == 0) return;

  const idNum = parseInt(id as string);
  if (isNaN(idNum)) return redirectTo404();

  image.value = images.getImageAsDataUrl(idNum - 1);
  if (image.value == undefined) return redirectTo404();
};

const deleteImage = () => {
  const id = parseInt(route.params.id as string);
  images.removePicture(id - 1);
  router.back();
};

watch(() => route.params.id, fetchImage);
fetchImage(route.params.id);
</script>

<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-buttons slot="start">
          <back-button url="/opret-praj/billeder"></back-button>
        </ion-buttons>
        <ion-buttons slot="end">
          <ion-button class="flex justify-center" @click="deleteImage">
            <i class="fas fa-trash mr-1"></i>
            <ion-label>{{ t("navigation.delete") }}</ion-label>
          </ion-button>
        </ion-buttons>
      </ion-toolbar>
    </ion-header>
    <ion-content>
      <ion-img :src="image" />
    </ion-content>
  </ion-page>
</template>
