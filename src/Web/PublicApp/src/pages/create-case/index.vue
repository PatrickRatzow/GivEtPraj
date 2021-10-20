<script setup lang="ts">
import { ref } from "vue";
import { Geolocation, Position } from "@capacitor/geolocation";

const position = ref<Position | null>(null);

const getCurrentPosition = async () => {
  position.value = await Geolocation.getCurrentPosition();
};

getCurrentPosition();
</script>

<template>
  <div>
    <h1>Geolocation</h1>
    <div v-if="position">
      <p>Your location is:</p>
      <p>Latitude: {{ position?.coords.latitude }}</p>
      <p>Longitude: {{ position?.coords.longitude }}</p>
    </div>

    <button class="mt-2">
      <router-link class="bg-green-500 py-2 px-4" to="/create-case/category">Next Page</router-link>
    </button>
  </div>
</template>

<route lang="yaml">
meta:
  layout: create-case
</route>
