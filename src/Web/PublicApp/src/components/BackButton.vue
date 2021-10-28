<script setup lang="ts">
interface Props {
  url: string;
}

defineProps<Props>();

const { t } = useI18n();
</script>

<template>
  <ion-back-button :text="t('title-bar.back')" :default-href="url"></ion-back-button>
</template>
