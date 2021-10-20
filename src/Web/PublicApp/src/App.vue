<script setup lang="ts">
import ReloadPWA from "./components/ReloadPWA.vue";
</script>

<template>
  <ReloadPWA />
  <router-view />
</template>

<style>
#app {
  font-family: Montserrat, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  overflow-x: hidden;
  min-height: 100vh;
}
</style>
