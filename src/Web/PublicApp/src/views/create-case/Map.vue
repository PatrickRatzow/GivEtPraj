<script setup lang="ts">
import { computed, ref } from "vue";
import { useStore } from "../../store";
import { Geolocation } from "@capacitor/geolocation";

const store = useStore();
const caseInCreation = computed(() => store.state.caseInCreation);

const loc = ref<{
  lat: null | number;
  long: null | number;
}>({
  lat: null,
  long: null,
});

const getCurrentPosition = async () => {
  const pos = await Geolocation.getCurrentPosition();
  loc.value = {
    lat: pos.coords.latitude,
    long: pos.coords.longitude,
  };
};
</script>

<template>
  <div>
    <h1 v-if="caseInCreation">
      Case in creation!

      <br />

      {{ caseInCreation.category }}

      <div>
        <h1>Geolocation</h1>
        <p>Your location is:</p>
        <p>Latitude: {{ loc.lat }}</p>
        <p>Longitude: {{ loc.long }}</p>

        <button @click="getCurrentPosition">Get Current Location</button>
      </div>
    </h1>
    <div v-else>
      <button @click="store.commit('startCaseCreation')">start</button>
    </div>
  </div>
</template>
