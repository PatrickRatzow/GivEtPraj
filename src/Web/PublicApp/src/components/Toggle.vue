<script setup lang="ts">
interface Props {
  title: string;
  setting: boolean;
}

const props = defineProps<Props>();
const emit = defineEmits(["update:setting"]);
</script>

<template>
  <ion-item>
    <ion-label>{{ title }}</ion-label>
    <ion-toggle
      slot="start"
      color="success"
      class="w-15 h-7.2"
      @ionChange="emit('update:setting', $event.target.checked)"
    >
    </ion-toggle>
  </ion-item>
</template>
