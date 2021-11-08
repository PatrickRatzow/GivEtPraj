<script setup lang="ts">
const toggleDarkModeHandler = () => {
  return document.body.classList.toggle("dark");
};
</script>

<template>
  <ion-item>
    <ion-label>{{ "Dark mode" }}</ion-label>
    <ion-toggle slot="start" color="success" class="w-15 h-7.2" @ionChange="toggleDarkModeHandler()"> </ion-toggle>
  </ion-item>
</template>
