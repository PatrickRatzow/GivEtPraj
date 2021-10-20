<script setup lang="ts">
interface Props {
  title: string;
  icon: string;
  selected: boolean;
}
defineProps<Props>();
</script>

<template>
  <div class="relative">
    <button
      type="button"
      class="flex flex-row justify-between transition-all bg-[#FAF6F3] rounded-md p-[7px] text-xl focus:outline-none"
      :class="{ 'bg-green-300': selected }"
    >
      <span class="flex flex-row items-center">
        <i v-if="icon" class="ml-1 mr-2" :class="icon"></i>

        <span>{{ title }}</span>
      </span>
      <slot></slot>
    </button>
  </div>
</template>
