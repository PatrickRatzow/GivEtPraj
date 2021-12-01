<script setup lang="ts">
import { create, fileTrayFull, settings } from "ionicons/icons";
import { useAuth } from "@/compositions/auth";
import { useMainStore } from "@/stores/main";
import { useTutorial } from "@/compositions/tutorial";

const main = useMainStore();
const tutorial = useTutorial();
const { t } = useI18n();
const { authorize } = useAuth();

onBeforeMount(async () => {
  await tutorial.loadCache();
});

onMounted(() => {
  setTimeout(authorize, 10000);
});
</script>

<template>
  <main :class="{ dark: main.activeTheme }">
    <div class="hidden absolute w-screen h-screen md:flex flex-col justify-between">
      <div class="w-screen toolbar z-10"></div>
      <div class="w-screen bg-cover z-0"></div>
      <div class="w-screen absolute h-full w-[774px] bg-cover-border left-1/2 transform -translate-x-1/2 z-20"></div>
      <div class="w-screen tab-bar z-10"></div>
    </div>
    <ion-app class="max-w-screen-md ml-[50%] w-full transform -translate-x-1/2 z-20">
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
  max-width: 600px;
}

main {
  background: #fff;
  min-height: 100vh;
  min-width: 100vw;
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
.ios main .toolbar {
  --toolbar-height: 44px;
}
.ios main .tab-bar {
  --tab-bar: 50px;
}

.toolbar {
  min-height: var(--toolbar-height, 56px);
  background: var(--ion-toolbar-background, var(--ion-color-step-50, #f7f7f7));
}

.tab-bar {
  min-height: var(--tab-bar, 56px);
  background: var(--ion-tab-bar-background, var(--ion-color-step-50, #f7f7f7));
}

html.md main:not(.dark) .toolbar,
html.md main:not(.dark) .tab-bar {
  background: white;
}

.bg-cover {
  height: 100%;
  background: var(--ion-background-color, #fff);
}

.bg-cover-border {
  background: linear-gradient(
    0deg,
    var(--ion-toolbar-background, var(--ion-color-step-50, #f7f7f7)),
    var(--ion-tab-bar-background, var(--ion-color-step-50, #f7f7f7))
  );
}
</style>
