<script setup lang="ts">
defineProps<{ comment: string }>();
</script>

<template>
  <ion-item>{{ comment }}</ion-item>
</template>
