<script setup lang="ts">
import { useMainStore } from "@/stores/main";

const main = useMainStore();
const { t } = useI18n();
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button></ion-back-button>
      </ion-buttons>
      <ion-title>{{ t("my-cases.title") }} </ion-title>
    </ion-toolbar>
    <ion-content class="ion-padding">
      <ion-list>
        <ion-item v-for="currentCase in main.cases" :key="currentCase.id">
          <span
            :title="currentCase.status.name"
            class="w-8 h-8 rounded-full mr-2"
            :style="`background: ${currentCase.status.color};`"
          >
          </span>
          <ion-label>
            <h3>{{ currentCase.id }}</h3>
            <p>{{ currentCase.category.name }}</p>
          </ion-label>
        </ion-item>
      </ion-list>
    </ion-content>
  </ion-page>
</template>
