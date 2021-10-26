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
  <ion-page fullscreen class="flex justify-center items-center">
    <h1 class="text-2xl text-black mt-8 dark:text-white">404 - Ain't nothin here</h1>
  </ion-page>
</template>
