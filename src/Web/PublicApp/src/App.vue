<script setup lang="ts">
import { create, fileTrayFull, settings } from "ionicons/icons";
import { useQueueKeys } from "@/compositions/queue-keys";
import { useThemes } from "@/compositions/themes";
import { useMainStore } from "@/stores/main";

const themes = useThemes();
const main = useMainStore();
const { t } = useI18n();
const { hasKey, createKey } = useQueueKeys();

onMounted(async () => {
  await themes.loadTheme();

  if (!hasKey()) return;

  await createKey();
});
</script>

<template>
  <main :class="{ dark: main.activeTheme }">
    <ion-app>
      <ion-page>
        <ion-tabs>
          <ion-router-outlet />

          <ion-tab-bar slot="bottom">
            <ion-tab-button tab="create-praj" href="/create-praj">
              <ion-icon :icon="create"></ion-icon>
              <ion-label>{{ t("tabs.create-case") }}</ion-label>
            </ion-tab-button>

            <ion-tab-button tab="history" href="/my-prajs">
              <ion-icon :icon="fileTrayFull"></ion-icon>
              <ion-label>{{ t("tabs.my-cases") }}</ion-label>
            </ion-tab-button>

            <ion-tab-button tab="settings" href="/settings">
              <ion-icon :icon="settings"></ion-icon>
              <ion-label>{{ t("tabs.settings") }}</ion-label>
            </ion-tab-button>
          </ion-tab-bar>
        </ion-tabs>
      </ion-page>

      <!-- <ReloadPWA /> -->
    </ion-app>
  </main>
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
