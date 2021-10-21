<script setup lang="ts">
const name = ref("");
const selected = ref(false);
</script>

<template>
  <div>
    <ion-toolbar>
      <ion-searchbar></ion-searchbar>
    </ion-toolbar>
    <TextField v-model:text="name" title="XD" />
    <br />
    Name is: {{ name }}
  </div>
  <div>
    <ItemRow title="Item1" icon="Very good icon" :selected="selected" @click="selected = !selected" />
  </div>
  <button type="button" @click="selected = !selected">FLIP</button>
</template>

<route lang="yaml">
meta:
  title: "About"
</route>
