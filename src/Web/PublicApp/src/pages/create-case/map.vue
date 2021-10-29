<script setup lang="ts">
import { useLocalizedRoutes } from "@/compositions/localizedRoutes";
import { useNetwork } from "@/compositions/network";
import { useCreateCaseStore } from "@/stores/create-case";
import { Geolocation } from "@capacitor/geolocation";
import { map, tileLayer, marker, LeafletMouseEvent, Marker } from "leaflet";

const localizedRoutes = useLocalizedRoutes();
const router = useRouter();
const createCase = useCreateCaseStore();
const { t } = useI18n();
const network = useNetwork();

const loadMap = async () => {
  try {
    const pos = await Geolocation.getCurrentPosition();

    const myMap = map("mapid").setView([pos.coords.latitude, pos.coords.longitude], 18);
    tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(myMap);

    let m: Marker | undefined;
    const setLocation = (location: GeographicLocation) => {
      m ??= marker([location.latitude, location.longitude]).addTo(myMap);
      m.setLatLng({
        lat: location.latitude,
        lng: location.longitude,
      });

      createCase.geographicLocation = location;
    };
    myMap.on("click", (e: LeafletMouseEvent) => {
      setLocation({ latitude: e.latlng.lat, longitude: e.latlng.lng });
    });
  } catch (e: unknown) {
    const permissions = await Geolocation.checkPermissions();
    if (permissions.location == "denied") {
      alert("We needed permission :(");
    }
  }
};

onMounted(loadMap);

const getStatus = () => network.status.value?.connected;

watch(getStatus, loadMap);

const isPositionValid = computed(() => createCase.geographicLocation);

const nextPage = async () => {
  if (!isPositionValid.value) return;

  router.push(await localizedRoutes.getCategoryUrl());
};
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-title>{{ t("create-case.map.title") }}</ion-title>
    </ion-toolbar>
    <ion-content>
      <offline v-if="!getStatus()" class="h-full"></offline>
      <div v-else id="mapid" class="h-full"></div>
      <button
        v-if="getStatus()"
        class="
          absolute
          bottom-4
          left-1/2
          right-1/2
          transform
          -translate-x-1/2
          py-2
          px-4
          z-[99999]
          rounded-md
          w-max
          text-lg
          transition-all
        "
        type="button"
        :class="[isPositionValid ? ['bg-green-500 text-white'] : ['bg-gray-200 text-black border-red-500 opacity-90']]"
        @click="nextPage"
      >
        {{ isPositionValid ? t("create-case.map.confirm-button.valid") : t("create-case.map.confirm-button.invalid") }}
      </button>
    </ion-content>
  </ion-page>
</template>
