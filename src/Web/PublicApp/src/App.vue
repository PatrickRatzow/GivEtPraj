<script setup lang="ts">
import ReloadPWA from "./components/ReloadPWA.vue";
</script>

<template>
  <ReloadPWA />
  <div class="p-4 bg-gray-200">
    <router-link class="mr-4" to="/">Home</router-link>
    <router-link class="mr-4" to="/about">About</router-link>
    <router-link class="mr-4" to="/camera">Camera</router-link>
<<<<<<< HEAD
    <router-link class="mr-4" to="/create-case">Create Case</router-link>
=======
    <router-link to="/category">Category</router-link>
>>>>>>> cfc32e0f6299e7c84c0d7621e34c4e2ce536471f
  </div>
  <div class="p-2">
    <!-- eslint-disable-next-line vue/no-unused-vars -->
    <router-view v-slot="{ Component }">
      <transition name="slide">
        <component :is="Component" class="w-full min-h-screen" />
      </transition>
    </router-view>
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

.slide-enter-active,
.slide-leave-active {
  transition: all 0.4s linear;
}

.slide-enter-to {
  position: absolute;
  right: 0;
}

.slide-enter-from {
  position: absolute;
  right: -100vw;
}

.slide-leave-to {
  position: absolute;
  left: -100vw;
}

.slide-leave-from {
  position: absolute;
  left: 0;
}
</style>
