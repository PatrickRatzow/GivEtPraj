<script setup lang="ts">
defineProps<{ pictures: string[] }>();
</script>

<template>
  <ion-item>{{ pictures }}</ion-item>
</template>
