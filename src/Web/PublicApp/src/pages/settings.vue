<script setup lang="ts">
import { useTutorial } from "@/compositions/tutorial";

const language = ref(false);

const tutorial = useTutorial();

const router = useRouter();
const { t } = useI18n();
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button></ion-back-button>
      </ion-buttons>
      <ion-title>{{ t("settings.title") }}</ion-title>
    </ion-toolbar>
    <ion-content>
      <ion-list>
        <toggle v-model:setting="language" :title="t('settings.english')" />
        <dark-mode />
        <ion-button @click="tutorial.setTutorialSeen(false)">{{ t("settings.view-tutorial") }}</ion-button>
      </ion-list>
    </ion-content>
  </ion-page>
</template>
