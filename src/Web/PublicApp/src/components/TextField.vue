<script setup lang="ts">
import { ref, computed } from "vue";
const props = defineProps<{ title: string; text: string }>();
const emit = defineEmits(["update:text"]);
const hasFocus = ref(false);

const isActive = computed(() => hasFocus.value || props.text.length > 0);
</script>

<template>
  <div class="relative">
    <span
      class="absolute ml-2 pointer-events-none text-base transition-all text-black"
      :class="[isActive ? ['-top-full', 'text-sm', 'mt-4'] : ['top-0', 'mt-2']]"
    >
      {{ title }}
    </span>
    <input
      :value="text"
      type="text"
      class="w-full p-2 rounded-md caret-orange bg-gray-100 border border-gray-300 focus:border-gray-700"
      @input="emit('update:text', ($event.target as HTMLInputElement).value)"
      @onfocus="hasFocus = true"
      @onblur="hasFocus = false"
    />
    <span class="absolute pointer-events-none top-2 right-2">
      <i class="fas fa-search"></i>
    </span>
  </div>
</template>
