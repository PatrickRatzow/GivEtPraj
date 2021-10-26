<script setup lang="ts">
import { Geolocation } from "@capacitor/geolocation";
import { map, tileLayer } from "leaflet";

onMounted(async () => {
  try {
    const pos = await Geolocation.getCurrentPosition();

    const myMap = map("mapid").setView([pos.coords.latitude, pos.coords.longitude], 18);
    tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(myMap);
  } catch (e: unknown) {
    const permissions = await Geolocation.checkPermissions();
    if (permissions.location == "denied") {
      alert("We needed permission :(");
    }
  }
});
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <!-- eslint-disable-next-line vue/no-deprecated-slot-attribute -->
      <ion-buttons slot="start">
        <ion-back-button></ion-back-button>
      </ion-buttons>
      <ion-title>Vælg Lokation</ion-title>
    </ion-toolbar>
    <div id="mapid" class="h-full"></div>
  </ion-page>
</template>
