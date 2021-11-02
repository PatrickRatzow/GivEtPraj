<script setup lang="ts">
import { useNetwork } from "@/compositions/network";
import { useLocale } from "@/compositions/locale";
import { useCreateCaseStore } from "@/stores/create-case";
import { Geolocation } from "@capacitor/geolocation";
import { map, tileLayer, marker, LeafletMouseEvent, Marker, control, Control, circle, point } from "leaflet";

const router = useRouter();
const createCase = useCreateCaseStore();
const locale = useLocale();
const { t } = useI18n();
const network = useNetwork();

const loadMap = async () => {
  try {
    const pos = await Geolocation.getCurrentPosition();

    let streets = tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}", {
      attribution:
        'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
      maxZoom: 18,
      id: "mapbox/streets-v11",
      tileSize: 512,
      zoomOffset: -1,
      accessToken: "pk.eyJ1Ijoic2ltb25uaWVzZSIsImEiOiJja3ZnZG9yZm0wMWxzMnVwM3Nlcjk3YmpiIn0.p1TDwifWqx1kcYFnBqI_fg",
    });
    const satellite = tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}", {
      attribution:
        'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
      maxZoom: 18,
      id: "mapbox/satellite-streets-v11",
      tileSize: 512,
      zoomOffset: -1,
      accessToken: "pk.eyJ1Ijoic2ltb25uaWVzZSIsImEiOiJja3ZnZG9yZm0wMWxzMnVwM3Nlcjk3YmpiIn0.p1TDwifWqx1kcYFnBqI_fg",
    });

    const myMap = map("mapid", { layers: [streets] }).setView([pos.coords.latitude, pos.coords.longitude], 18);

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

    circle([pos.coords.latitude, pos.coords.longitude], {
      color: "blue",
      opacity: 1,
      weight: 0.5,
      fillColor: "#96c3eb",
      fillOpacity: 0.6,
      radius: pos.coords.accuracy,
    }).addTo(myMap);

    circle([pos.coords.latitude, pos.coords.longitude], {
      color: "white",
      fillColor: "blue",
      weight: 2,
      fillOpacity: 1,
      radius: 1.5,
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
        @click="router.push('/create-praj/category')"
      >
        {{ isPositionValid ? t("create-case.map.confirm-button.valid") : t("create-case.map.confirm-button.invalid") }}
      </button>
    </ion-content>
  </ion-page>
</template>
