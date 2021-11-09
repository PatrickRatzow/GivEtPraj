<script setup lang="ts">
import { useThemes } from "../compositions/themes";

const themes = useThemes();
</script>

<template>
  <ion-item>
    <ion-label>{{ "Dark mode" }}</ion-label>
    <ion-toggle
      slot="start"
      color="success"
      class="w-15 h-7.2"
      :checked="themes.activeTheme.value"
      @ionChange="themes.toggleTheme"
    >
    </ion-toggle>
  </ion-item>
</template>
