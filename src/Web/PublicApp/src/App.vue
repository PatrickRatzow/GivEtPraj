<script setup lang="ts">
import { create, fileTrayFull } from "ionicons/icons";

const { t } = useI18n();
</script>

<template>
  <ion-app>
    <ion-page>
      <ion-tabs>
        <ion-router-outlet />

        <ion-tab-bar slot="bottom">
          <ion-tab-button tab="create-case" href="/opret-praj">
            <ion-icon :icon="create"></ion-icon>
            <ion-label>{{ t("tabs.create-case") }}</ion-label>
          </ion-tab-button>

          <ion-tab-button tab="history" href="/mine-prajs">
            <ion-icon :icon="fileTrayFull"></ion-icon>
            <ion-label>{{ t("tabs.my-cases") }}</ion-label>
          </ion-tab-button>
        </ion-tab-bar>
      </ion-tabs>
    </ion-page>

    <!-- <ReloadPWA /> -->
  </ion-app>
</template>

<style>
#app {
  font-family: Montserrat, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  overflow-x: hidden;
  min-height: 100vh;
}

.grecaptcha-badge {
  visibility: hidden;
}
</style>
