<script setup lang="ts">
import { useMainStore } from "@/stores/main";

const route = useRoute();
const router = useRouter();
const main = useMainStore();
const { t } = useI18n();

const redirectTo404 = () => router.replace("/404-not-found");

const currentCase = ref<Case | undefined>();
const fetchCase = (id: string | string[]) => {
  if (id == undefined) return;
  if (id.length == 0) return;

  const idNum = parseInt(id as string);
  if (isNaN(idNum)) return redirectTo404();

  currentCase.value = main.cases.find((c) => c.id == idNum);
  if (currentCase.value == undefined) return redirectTo404();
};

watch(() => route.params.id, fetchCase);
fetchCase(route.params.id);

const lastUpdatedAt = (): string => {
  if (currentCase.value?.updatedAt != undefined) return currentCase.value.updatedAt.toLocaleString();

  return currentCase.value?.createdAt.toLocaleString() as string;
};
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <back-button url="/my-praj"></back-button>
      </ion-buttons>
      <ion-title>{{ t("case.title") }} </ion-title>
    </ion-toolbar>
    <ion-content class="ion-padding">
      <ion-list>
        <ion-item class="flex flex-row">
          <status-indicator :status="currentCase!.status" />
          <ion-label>
            <h3>{{ currentCase!.status.name }}</h3>
            <p>{{ t("case.last-updated-at", { time: lastUpdatedAt() }) }}</p>
          </ion-label>
        </ion-item>

        <ion-list-header>{{ t("create-case.overview.category") }}</ion-list-header>
        <category-row :category="currentCase!.category"></category-row>

        <ion-list-header>{{ t("create-case.overview.sub-categories") }}</ion-list-header>
        <ion-item v-for="(sub, idx) in currentCase!.subCategories" :key="idx">
          <ion-label>{{ sub.name }}</ion-label>
        </ion-item>

        <ion-list-header>{{ t("create-case.overview.pictures") }}</ion-list-header>
        <ion-img v-for="(picture, idx) in currentCase!.images" :key="idx" :src="picture"></ion-img>

        <ion-list-header>{{ t("create-case.overview.comment.title") }}</ion-list-header>
        <ion-item>{{currentCase!.comment}}</ion-item>
      </ion-list>
    </ion-content>
  </ion-page>
</template>
