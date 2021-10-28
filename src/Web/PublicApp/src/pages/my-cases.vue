<script setup lang="ts">
import { useMainStore } from "@/stores/main";

const main = useMainStore();
const router = useRouter();
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
        <ion-item
          v-for="currentCase in main.cases"
          :key="currentCase.id"
          @click="router.push(`/praj/${currentCase.id}`)"
        >
          <status-indicator :status="currentCase.status"> </status-indicator>
          <ion-label>
            <h3>{{ currentCase.id }}</h3>
            <p>{{ currentCase.category.name }}</p>
          </ion-label>
        </ion-item>
      </ion-list>
    </ion-content>
  </ion-page>
</template>
