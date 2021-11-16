<script setup lang="ts">
import { closeCircleOutline } from "ionicons/icons";
import { useCaseHistory } from "@/compositions/case-history";
import { useLocationLookup } from "@/compositions/location-lookup";
import { useNetwork } from "@/compositions/network";
import { useMainStore } from "../stores/main";
import { useCases } from "@/compositions/cases";
import { alertController } from "@ionic/vue";

const caseHistory = useCaseHistory();
const router = useRouter();
const { t } = useI18n();
const locationLookup = useLocationLookup();
const network = useNetwork();
const main = useMainStore();
const cases = useCases();

const savedCases = ref<SavedCase[]>([]);

const fetchAddressNames = async () => {
  savedCases.value = await Promise.all(
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

const getQueueCaseId = (queueCase: BaseCase): string => {
  if (queueCase.category.miscellaneous) {
    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
    return `${queueCase.category.id}-${queueCase.description!.length}`;
  }

  return `${queueCase.category.id}-${queueCase.subCategories.map((s) => s.id).join(",")}`;
};

const deleteQueueCase = async (idx: number) => {
  const alert = await alertController.create({
    header: t("my-cases.cached.delete-alert.header"),
    message: t("my-cases.cached.delete-alert.message"),
    buttons: [
      {
        text: t("my-cases.cached.delete-alert.delete"),
        role: "destructive",
        handler: () => {
          cases.removeCaseFromQueue(idx);
        },
      },
      {
        text: t("my-cases.cached.delete-alert.cancel"),
        role: "cancel",
      },
    ],
  });
  await alert.present();
  return;
};
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
          <ion-list v-if="main.caseQueue.length > 0">
            <ion-list-header>{{ t("my-cases.cached.title") }}</ion-list-header>
            <ion-item v-for="(queueCase, idx) in main.caseQueue" :key="getQueueCaseId(queueCase)">
              <ion-icon class="text-black" :icon="closeCircleOutline" @click="deleteQueueCase(idx)"></ion-icon>
              {{ queueCase.category.id }}
            </ion-item>
          </ion-list>
          <ion-item
            v-for="currentCase in savedCases"
            :key="currentCase.id"
            @click="router.push(`/praj/${currentCase.id}`)"
          >
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
