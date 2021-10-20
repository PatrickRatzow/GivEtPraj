<script setup lang="ts">
import { useRouter } from "vue-router";

const router = useRouter();
</script>

<template>
  <h1>Category</h1>
  <button class="bg-blue-500 p-4 mt-2" @click="router.push('/create-case/sub-categories')">Next</button>
</template>
