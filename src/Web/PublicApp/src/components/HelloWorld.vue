<script setup lang="ts">
import { ref } from "vue";

defineProps<{ msg: string }>();

const count = ref(0);

const capitalize = (input: string) => input.toUpperCase();
</script>

<template>
  <h1>Test {{ capitalize("capitalisation") }}</h1>

  <h1>{{ msg }}</h1>

  <p>
    Recommended IDE setup:
    <a href="https://code.visualstudio.com/" target="_blank">VSCode</a>
    +
    <a href="https://github.com/johnsoncodehk/volar" target="_blank">Volar</a>
  </p>
  <p>See <code>README.md</code> for more information.</p>
  <p>
    <a href="https://vitejs.dev/guide/features.html" target="_blank"> Vite Docs </a>
    |
    <a href="https://v3.vuejs.org/" target="_blank">Vue 3 Docs</a>
  </p>
  <button class="py-2 px-4 bg-blue-500 rounded-md text-white" type="button" @click="count++">
    count is: {{ count }}
  </button>
  <p>Edit<code>components/HelloWorld.vue</code> to test hot module replacement.</p>
</template>

<style scoped>
a {
  color: #42b983;
}

label {
  margin: 0 0.5em;
  font-weight: bold;
}

code {
  background-color: #eee;
  padding: 2px 4px;
  border-radius: 4px;
  color: #304455;
}
</style>
