<script setup lang="ts">
defineProps<{ category: Category }>();
</script>

<template>
  <ion-item>
    <i class="ml-1 mr-2" :class="category.icon"></i>
    <ion-label>{{ category.name }}</ion-label>
  </ion-item>
</template>
