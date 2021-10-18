<script setup lang="ts">
import { ref } from "vue";
import TextField from "../components/TextField.vue";

const name = ref("");
</script>

<template>
  <div>
    <h1>About</h1>
    <TextField v-model:text="name" title="XD" />
    <br />
    Name is: {{ name }}
  </div>
</template>
