<script setup lang="ts">
import { create, fileTrayFull, settings } from "ionicons/icons";
import { useQueueKeys } from "@/compositions/queue-keys";
import { useMainStore } from "@/stores/main";
import { useTutorial } from "@/compositions/tutorial";

const main = useMainStore();
const tutorial = useTutorial();
const { t } = useI18n();
const { hasKey, createKey } = useQueueKeys();

onBeforeMount(async () => {
  await tutorial.loadCache();
});

onMounted(() => {
  setTimeout(createKey, 10000);
});
</script>

<template>
  <main :class="{ dark: main.activeTheme }">
    <div class="hidden absolute w-full h-screen md:flex flex-col justify-between">
      <div class="toolbar"></div>
      <div class="bg-container"></div>
      <div class="tab-bar"></div>
    </div>
    <ion-app class="max-w-screen-md ml-[50%] w-full transform -translate-x-1/2">
      <ion-page>
        <ion-content v-if="!main.hasSeenTutorial">
          <ion-button @click="tutorial.setTutorialSeen(true)">skip tutorial</ion-button>
        </ion-content>
        <ion-tabs v-else>
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

main {
  background: #fff;
  min-height: 100vh;
}

main.dark {
  background: #121212;
}

.ios main.dark {
  background: #121212;
}

.grecaptcha-badge {
  visibility: hidden;
}
</style>

<style scoped>
:root {
  --toolbar-height: 56px;
  --toolbar-background: var(--ion-toolbar-background, #fff);
  --tab-bar: 56px;
  --tab-bar-background: var(--ion-tab-bar-background, var(--ion-background-color, #fff));
}
.ios main .toolbar {
  --toolbar-height: 44px;
  --toolbar-background: var(--ion-toolbar-background, var(--ion-color-step-50, #f7f7f7));
}
.ios main .tab-bar {
  --tab-bar: 50px;
  --tab-bar-background: var(--ion-tab-bar-background, var(--ion-color-step-50, #f7f7f7));
}

.toolbar {
  min-height: var(--toolbar-height);
  background: var(--toolbar-background);
}

.tab-bar {
  min-height: var(--tab-bar);
  background: var(--tab-bar-background);
}

.bg-container {
  height: 100%;
  background: red;
  margin-left: 50%;
  @apply max-w-screen-md transform -translate-x-1/2;
}
</style>
