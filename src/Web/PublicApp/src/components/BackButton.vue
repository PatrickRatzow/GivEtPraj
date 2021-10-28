<script setup lang="ts">
interface Props {
  url: string | undefined;
}

defineProps<Props>();

const { t } = useI18n();
</script>

<template>
  <ion-back-button :text="t('navigation.back')" :default-href="url"></ion-back-button>
</template>
