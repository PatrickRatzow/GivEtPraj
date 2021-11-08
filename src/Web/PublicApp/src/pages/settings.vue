<script setup lang="ts">
import DarkMode1 from "@/components/DarkMode.vue";
const setting = ref(false);
const language = ref(false);
const theme = ref(false);

const router = useRouter();
const { t } = useI18n();

const toggleDarkModeHandler = (s: string) => {
  document.body.classList.toggle(s);
};
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
        <dark-mode1 />
      </ion-list>
    </ion-content>
  </ion-page>
</template>
