<script setup lang="ts">
import { useLocale } from "../compositions/locale";

const locale = useLocale();
</script>

<template>
  <ion-item>
    <ion-label>{{ "English" }}</ion-label>
    <ion-toggle
      slot="start"
      color="success"
      class="w-15 h-7.2"
      :checked="locale.isEnglish"
      @ionChange="locale.setLanguage($event.target.value)"
    >
    </ion-toggle>
  </ion-item>
</template>
