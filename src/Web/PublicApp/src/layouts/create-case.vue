<template>
  <!-- eslint-disable-next-line vue/no-unused-vars -->
  <router-view v-slot="{ Component }">
    <transition name="slide">
      <component :is="Component" class="w-full min-h-screen" />
    </transition>
  </router-view>
</template>

<style>
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
