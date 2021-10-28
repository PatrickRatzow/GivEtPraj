<script setup lang="ts">
defineProps<{ status: Status }>();
</script>

<template>
  <span class="w-8 h-8 mr-2 rounded-full" :style="`background: ${status.color};`" :title="status.name"> </span>
</template>
