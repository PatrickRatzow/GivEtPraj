<script lang="ts">
export default {
  created() {
    const url = "/404-not-found";
    if (window.location.pathname === url) return;

    window.location.pathname = url;
  },
};
</script>

<template>
  <ion-page fullscreen class="flex justify-center items-center h-full">
    <div class="text-black dark:text-white text-center">
      <i class="fas fa-question text-6xl mb-4"></i>
      <h1 class="text-2xl">404</h1>
      <p class="text-lg">Ain't nothing here</p>
    </div>
  </ion-page>
</template>
