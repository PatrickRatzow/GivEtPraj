<script setup lang="ts">
import { ref } from "vue";
import { Geolocation, Position } from "@capacitor/geolocation";

const position = ref<Position | null>(null);

const getCurrentPosition = async () => {
  position.value = await Geolocation.getCurrentPosition();
};
</script>

<template>
  <div>
    <h1 v-if="position">
      <div>
        <h1>Geolocation</h1>
        <p>Your location is:</p>
        <p>Latitude: {{ position?.coords.latitude }}</p>
        <p>Longitude: {{ position?.coords.longitude }}</p>

        <button @click="getCurrentPosition">Get Current Location</button>
      </div>
    </h1>
  </div>
</template>
