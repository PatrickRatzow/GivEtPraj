<script setup lang="ts">
import { useMainStore } from "@/stores/main";
import { useThemes } from "@/compositions/themes";

const themes = useThemes();
const main = useMainStore();
</script>

<template>
  <ion-item>
    <ion-label>{{ "Dark mode" }}</ion-label>
    <ion-toggle
      slot="start"
      color="success"
      class="w-15 h-7.2"
      :checked="main.activeTheme"
      @ionChange="themes.setTheme($event.target.checked)"
    >
    </ion-toggle>
  </ion-item>
</template>
