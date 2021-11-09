<script setup lang="ts">
import { useNetwork } from "@/compositions/network";
import { useLocale } from "@/compositions/locale";
import { useCreateCaseStore } from "@/stores/create-case";
import { Geolocation } from "@capacitor/geolocation";
import { presentAlert } from "@/compositions/geolocation-error-alert";
import * as turf from "@turf/turf";
import { streetMap, sateliteMap, bounderiesCoords } from "@/../leaflet.config";
import { map, tileLayer, marker, LeafletMouseEvent, Marker, control, Control, circle, Map } from "leaflet";
import { Position } from "@capacitor/geolocation/dist/esm/definitions";

const router = useRouter();
const createCase = useCreateCaseStore();
const locale = useLocale();
const { t } = useI18n();
const network = useNetwork();

const loadMap = (): Map => {
  let streets = tileLayer(
    "https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}",
    streetMap
  );

  let satellite = tileLayer(
    "https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}",
    sateliteMap
  );

  const myMap = map("mapid", { layers: [streets] }).setView([56.15674, 10.21076], 6);
  const addLayers = () => {
    const baseMaps = {} as Control.LayersObject;
    baseMaps[t("create-case.map.layers.street")] = streets;
    baseMaps[t("create-case.map.layers.satellite")] = satellite;

    control.layers(baseMaps).addTo(myMap);
  };

  let bounderies = turf.multiPolygon(bounderiesCoords);

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
    if (turf.booleanWithin(turf.point([e.latlng.lat, e.latlng.lng]), bounderies)) {
      setLocation({ latitude: e.latlng.lat, longitude: e.latlng.lng });
    }
  });
  return myMap;
};

const getUserPosition = async (map: Map): Promise<Position> => {
  const pos = await Geolocation.getCurrentPosition();

  const userPoint = circle([pos.coords.latitude, pos.coords.longitude], {
    color: "white",
    fillColor: "blue",
    weight: 4,
    fillOpacity: 1,
    radius: 21 - map.getZoom(),
  }).addTo(map);

  let userAccuracy = circle([pos.coords.latitude, pos.coords.longitude], {
    color: "blue",
    opacity: 1,
    weight: 0.5,
    fillColor: "#96c3eb",
    fillOpacity: 0.6,
    radius: pos.coords.accuracy,
  }).addTo(map);

  const watchPos = (newPos: Position | null) => {
    if (!newPos) return;

    userPoint.setLatLng([newPos.coords.latitude, newPos.coords.longitude]);
    userAccuracy.setLatLng([newPos.coords.latitude, newPos.coords.longitude]);
    userAccuracy.setRadius(newPos.coords.accuracy);
  };

  Geolocation.watchPosition({ enableHighAccuracy: true, timeout: 10000 }, watchPos);

  map.on("zoom", () => userPoint.setRadius(Math.pow(2, 20 - map.getZoom())));

  return pos;
};

onMounted(async () => {
  const map: Map = loadMap();
  //ake map fill whole div
  setInterval(async function () {
    map.invalidateSize();
  }, 100);

  const pos: Position = await getUserPosition(map);
  map.setView([pos.coords.latitude, pos.coords.longitude], 18);
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
