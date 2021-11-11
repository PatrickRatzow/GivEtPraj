<script setup lang="ts">
import { useLocale } from "@/compositions/locale";
import { useThemes } from "@/compositions/themes";
import { useTutorial } from "@/compositions/tutorial";
import { useMainStore } from "@/stores/main";
import { close } from "ionicons/icons";

<<<<<<< HEAD
=======
const main = useMainStore();
const locale = useLocale();
>>>>>>> 131aa1da59c21c0099a5a93683e622f3f5dcffd6
const tutorial = useTutorial();
const themes = useThemes();
const { t } = useI18n();

const viewPrivacyPolicy = ref(false);
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button></ion-back-button>
      </ion-buttons>
      <ion-title>{{ t("settings.title") }}</ion-title>
    </ion-toolbar>
    <ion-content fullscreen>
      <ion-list>
        <ion-list-header>{{ t("settings.general.header") }}</ion-list-header>
        <!-- TODO: Switch language -->
        <ion-item>
          <ion-label>{{ t("settings.general.english-language") }}</ion-label>
          <ion-toggle color="success" :checked="locale.language"></ion-toggle>
        </ion-item>
        <ion-item @click="themes.setTheme(!main.activeTheme)">
          <ion-label>{{ t("settings.general.dark-mode") }}</ion-label>
          <ion-toggle color="success" :checked="main.activeTheme"> </ion-toggle>
        </ion-item>
      </ion-list>
      <ion-list>
        <ion-list-header>{{ t("settings.misc.header") }}</ion-list-header>
        <ion-item @click="tutorial.setTutorialSeen(false)">
          <ion-label>{{ t("settings.misc.view-tutorial") }}</ion-label>
        </ion-item>
        <ion-item @click="viewPrivacyPolicy = true">
          <ion-label>{{ t("settings.misc.view-privacy-policy") }}</ion-label>
        </ion-item>
      </ion-list>

      <ion-modal :is-open="viewPrivacyPolicy" @didDismiss="viewPrivacyPolicy = false">
        <ion-header translucent>
          <ion-toolbar>
            <ion-title>{{ t("privacy-policy.header") }}</ion-title>
            <ion-buttons slot="end">
              <ion-button @click="viewPrivacyPolicy = false">
                <ion-icon :icon="close"></ion-icon>
              </ion-button>
            </ion-buttons>
          </ion-toolbar>
        </ion-header>
        <ion-content fullscreen class="ion-padding">
          {{ t("privacy-policy.content") }}
        </ion-content>
      </ion-modal>
    </ion-content>
  </ion-page>
</template>
