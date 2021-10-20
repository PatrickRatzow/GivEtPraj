<script setup lang="ts">
import ReloadPWA from "./components/ReloadPWA.vue";
</script>

<template>
  <ReloadPWA />
  <div class="p-4 bg-gray-200">
    <router-link class="mr-4" to="/">Home</router-link>
    <router-link class="mr-4" to="/about">About</router-link>
    <router-link class="mr-4" to="/camera">Camera</router-link>
    <router-link to="/category">Category</router-link>
  </div>
  <div class="p-2">
    <router-view></router-view>
  </div>
</template>

<style>
#app {
  font-family: Montserrat, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  overflow-x: hidden;
  min-height: 100vh;
}

.router-link-exact-active {
  color: rgb(59, 130, 246);
}
</style>
