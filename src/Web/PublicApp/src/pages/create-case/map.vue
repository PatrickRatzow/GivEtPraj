<script setup lang="ts">
import { useNetwork } from "@/compositions/network";
import { useLocale } from "@/compositions/locale";
import { useCreateCaseStore } from "@/stores/create-case";
import { Geolocation, WatchPositionCallback } from "@capacitor/geolocation";
import { presentAlert } from "@/compositions/GeolocationErrorAlert";
import * as turf from "@turf/turf";
import {
  map,
  tileLayer,
  marker,
  LeafletMouseEvent,
  Marker,
  control,
  Control,
  circle,
  point,
  LeafletEvent,
  LayerGroup,
  Map,
} from "leaflet";

const router = useRouter();
const createCase = useCreateCaseStore();
const locale = useLocale();
const { t } = useI18n();
const network = useNetwork();

const loadMap = () => {
  let streets = tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}", {
    attribution:
      'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    maxZoom: 18,
    minZoom: 6,
    id: "mapbox/streets-v11",
    tileSize: 512,
    zoomOffset: -1,
    accessToken: "pk.eyJ1Ijoic2ltb25uaWVzZSIsImEiOiJja3ZnZG9yZm0wMWxzMnVwM3Nlcjk3YmpiIn0.p1TDwifWqx1kcYFnBqI_fg",
    bounds: [
      [57.765923, 7.482181],
      [54.54649, 15.455954],
    ],
  });

  let satellite = tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}", {
    attribution:
      'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    maxZoom: 18,
    minZoom: 6,
    id: "mapbox/satellite-streets-v11",
    tileSize: 512,
    zoomOffset: -1,
    accessToken: "pk.eyJ1Ijoic2ltb25uaWVzZSIsImEiOiJja3ZnZG9yZm0wMWxzMnVwM3Nlcjk3YmpiIn0.p1TDwifWqx1kcYFnBqI_fg",
    bounds: [
      [57.765923, 7.482181],
      [54.54649, 15.455954],
    ],
  });

  const myMap = map("mapid", { layers: [streets] }).setView([56.15674, 10.21076], 6);
  const addLayers = () => {
    const baseMaps = {} as Control.LayersObject;
    baseMaps[t("create-case.map.layers.street")] = streets;
    baseMaps[t("create-case.map.layers.satellite")] = satellite;

    control.layers(baseMaps).addTo(myMap);
  };

  addLayers();
  watch(
    () => locale.language,
    () => {
      myMap.eachLayer((layer) => myMap.removeLayer(layer));

      addLayers();
    }
  );

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

  return myMap;
};

const drawPosition = async (myMap: Map) => {
  try {
    const pos = await Geolocation.getCurrentPosition();
    myMap.setView([pos.coords.latitude, pos.coords.longitude], 18);
    const userPoint = circle([pos.coords.latitude, pos.coords.longitude], {
      color: "white",
      fillColor: "blue",
      weight: 4,
      fillOpacity: 1,
      radius: 21 - myMap.getZoom(),
    }).addTo(myMap);

    myMap.on("zoom", () => userPoint.setRadius(Math.pow(2, 20 - myMap.getZoom())));
  } catch {
    //If user denies to share their location
  }
};

onMounted(async () => {
  const map = loadMap();

  //ake map fill whole div
  setInterval(function () {
    map.invalidateSize();
  }, 100);
  const pos = await drawPosition(map);
});

const getStatus = () => network.status.value?.connected;

const isPositionValid = computed(() => createCase.geographicLocation);

const sendInCurrentLocation = async () => {
  try {
    var pos = await Geolocation.getCurrentPosition({ enableHighAccuracy: true });
    createCase.geographicLocation = { latitude: pos.coords.latitude, longitude: pos.coords.longitude };
    router.push("/create-praj/category");
  } catch (e: unknown) {
    if (e instanceof GeolocationPositionError) {
      if (e.code == 1) {
        presentAlert(e.code);
      } else if (e.code == 2) {
        presentAlert(e.code);
      } else if (e.code == 3) {
        presentAlert(e.code);
      }
    }

    console.log("Unknown error occurred");
  }
};
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-title>{{ t("create-case.map.title") }}</ion-title>
    </ion-toolbar>
    <ion-content>
      <div id="mapid" class="h-full w-full"></div>
      <template v-if="!getStatus()">
        <div class="flex flex-col justify-between items-center text-black dark:text-white text-center h-full">
          <div class="flex flex-col items-center mt-8">
            <i class="fas fa-exclamation-circle text-red-500 text-6xl mb-4"></i>
            <h1 class="text-2xl">{{ t("create-case.map.internet-error.no-internet") }}</h1>
            <p class="mx-8">{{ t("create-case.map.internet-error.no-internet-explanation") }}</p>
          </div>

          <ion-button class="mb-8" @click="sendInCurrentLocation">
            {{ t("create-case.map.confirm-button-offline.valid") }}
          </ion-button>
        </div>
      </template>
      <template v-else>
        <button
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
          :class="[
            isPositionValid ? ['bg-green-500 text-white'] : ['bg-gray-200 text-black border-red-500 opacity-90'],
          ]"
          @click="router.push('/create-praj/category')"
        >
          {{
            isPositionValid ? t("create-case.map.confirm-button.valid") : t("create-case.map.confirm-button.invalid")
          }}
        </button>
      </template>
    </ion-content>
  </ion-page>
</template>
