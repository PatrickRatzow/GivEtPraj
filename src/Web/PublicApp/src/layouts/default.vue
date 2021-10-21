<script setup lang="ts">
const router = useRouter();
</script>

<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <!-- eslint-disable-next-line vue/no-deprecated-slot-attribute -->
        <ion-buttons slot="start">
          <ion-back-button></ion-back-button>
        </ion-buttons>
        <ion-title>{{ router.currentRoute.value.meta.title ?? "Unknown title" }}</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content class="ion-padding">
      <ion-router-outlet />
    </ion-content>
  </ion-page>
</template>

<style>
.router-link-exact-active {
  color: rgb(59, 130, 246);
}
</style>
