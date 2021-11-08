<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";

const createCase = useCreateCaseStore();
const { t } = useI18n();

// createCase.category = {
//   name: "Vejskade",
//   icon: "fas fa-road",
//   subCategories: [],
// };
//createCase.subCategories = [{ name: "Hul" }, { name: "XD" }, { name: "XD2" }];
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button default-href="/create-praj/pictures"></ion-back-button>
      </ion-buttons>
      <ion-title>{{ t("create-case.overview.title") }}</ion-title>
    </ion-toolbar>
    <ion-content>
      <div class="flex flex-col justify-between h-full ion-padding">
        <ion-list>
          <ion-list-header>{{ t("create-case.overview.category") }}</ion-list-header>
          <category-row :category="createCase.category!"></category-row>
        </ion-list>
        <ion-list>
          <ion-list-header>{{ t("create-case.overview.sub-categories") }}</ion-list-header>
          <ion-item v-for="(sub, idx) in createCase.subCategories" :key="idx">
            <ion-label>{{ sub.name }}</ion-label>
          </ion-item>
        </ion-list>
        <ion-list>
          <ion-list-header>{{ t("create-case.overview.pictures") }}</ion-list-header>
          <ion-item></ion-item>
        </ion-list>
        <ion-list>
          <ion-list-header>{{ t("create-case.overview.comment.title") }}</ion-list-header>
          <ion-item>
            <ion-textarea :placeholder="t('create-case.overview.comment.placeholder')"></ion-textarea>
          </ion-item>
          <p class="float-right">0/200</p>
        </ion-list>
        <div class="w-full ion-padding">
          <ion-button expand="block">{{ t("create-case.overview.finish") }}</ion-button>
        </div>
      </div>
    </ion-content>
  </ion-page>
</template>
