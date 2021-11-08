<script setup lang="ts">
const { t } = useI18n();
</script>

<template>
  <ion-page fullscreen class="flex justify-center items-center h-full">
    <div class="text-black dark:text-white text-center">
      <i class="fas fa-exclamation-circle text-red-500 text-6xl mb-4"></i>
      <h1 class="text-2xl">{{ t("create-case.map.internet-error.no-internet") }}</h1>
      <p class="h-full">{{ t("create-case.map.internet-error.internet-msg") }}</p>
    </div>
  </ion-page>
</template>
