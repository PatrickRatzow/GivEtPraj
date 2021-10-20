<script setup lang="ts">
import { ref } from "vue";

const name = ref("");
const selected = ref(false);
</script>

<template>
  <div>
    <h1>About</h1>
    <TextField v-model:text="name" title="XD" />
    <br />
    Name is: {{ name }}
  </div>
  <div>
    <ItemRow title="Item1" icon="Very good icon" :selected="selected" @click="selected = !selected" />
  </div>
  <button type="button" @click="selected = !selected">FLIP</button>
</template>
