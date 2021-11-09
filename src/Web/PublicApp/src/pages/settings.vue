<script setup lang="ts">
const language = ref(false);

const router = useRouter();
const { t } = useI18n();
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button></ion-back-button>
      </ion-buttons>
      <ion-title>{{ t("settings.title") }} </ion-title>
    </ion-toolbar>
    <ion-content>
      <ion-list>
        <toggle v-model:setting="language" title="Language Settings" />
        <p>
          {{ language ? "da" : "en" }}
        </p>
        <dark-mode />
      </ion-list>
    </ion-content>
  </ion-page>
</template>
