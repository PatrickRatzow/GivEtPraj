<script setup lang="ts">
import { useCaseHistory } from "@/compositions/case-history";
import { useLocationLookup } from "@/compositions/location-lookup";
import { useNetwork } from "@/compositions/network";

const caseHistory = useCaseHistory();
const router = useRouter();
const { t } = useI18n();
const locationLookup = useLocationLookup();
const network = useNetwork();

const cases = ref<SavedCase[]>([]);

const fetchAddressNames = async () => {
  cases.value = await Promise.all(
    caseHistory.cases.map(async (c) => {
      const address = await locationLookup.fetchAddress(c.geographicLocation);

      return {
        ...c,
        nearestCity: address?.zipCodeName,
      } as SavedCase;
    })
  );
};

const loading = ref(true);
onMounted(async () => {
  await fetchAddressNames();

  loading.value = false;
});

watch(
  () => network.status.value?.connected,
  (newValue: boolean | undefined, oldValue: boolean | undefined) => {
    if (!oldValue && newValue) {
      fetchAddressNames();
    }
  }
);
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button></ion-back-button>
      </ion-buttons>
      <ion-title>{{ t("my-cases.title") }} </ion-title>
    </ion-toolbar>
    <ion-content class="ion-padding">
      <div class="flex flex-col justify-between h-full">
        <ion-label v-if="loading">Loading...</ion-label>
        <ion-list v-else>
          <ion-item v-for="currentCase in cases" :key="currentCase.id" @click="router.push(`/praj/${currentCase.id}`)">
            <status-indicator :status="currentCase.status"> </status-indicator>
            <ion-label>
              <h3>{{ currentCase.nearestCity ?? t("my-cases.unable-to-fetch-closest-city-name") }}</h3>
              <p>{{ currentCase.category.name }}</p>
            </ion-label>
          </ion-item>
        </ion-list>
        <ion-button class="flex-row float-bottom" @click="caseHistory.syncWithAPI()">Sync</ion-button>
      </div>
    </ion-content>
  </ion-page>
</template>
