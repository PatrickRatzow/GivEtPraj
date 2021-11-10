<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";
import { useCases } from "@/compositions/cases";
import { useNetwork } from "@/compositions/network";
import { alertController, toastController } from "@ionic/vue";

const createCase = useCreateCaseStore();
const { t } = useI18n();
const cases = useCases();
const network = useNetwork();

// createCase.category = {
//   name: "Vejskade",
//   icon: "fas fa-road",
//   subCategories: [],
// };
//createCase.subCategories = [{ name: "Hul" }, { name: "XD" }, { name: "XD2" }];
const loading = ref(false);

const sendCase = async () => {
  cases.addCurrentCaseToQueue();

  if (!network.status.value?.connected) {
    const alert = await alertController.create({
      header: t("create-case.overview.send.offline-popup.header"),
      message: t("create-case.overview.send.offline-popup.message"),
      buttons: [t("create-case.overview.send.offline-popup.button")],
    });

    await alert.present();
    return;
  }

  loading.value = true;
  await cases.sendCases();
  loading.value = false;
  const toast = await toastController.create({
    position: "top",
    message: t("create-case.overview.send.success"),
    duration: 2000,
  });

  await toast.present();
};
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button default-href="/create-praj/pictures"></ion-back-button>
      </ion-buttons>
      <ion-title>{{ t("create-case.overview.title") }}</ion-title>
    </ion-toolbar>
    <ion-content class="ion-padding">
      <div class="flex flex-col justify-between h-full">
        <ion-list>
          <ion-list-header>{{ t("create-case.overview.category") }}</ion-list-header>
          <category-row :category="createCase.category!"></category-row>
        </ion-list>

        <ion-list v-if="createCase.subCategories.length >= 1">
          <ion-list-header>{{ t("create-case.overview.sub-categories") }}</ion-list-header>
          <ion-item v-for="(sub, idx) in createCase.subCategories" :key="idx">
            <ion-label>{{ sub.name }}</ion-label>
          </ion-item>
        </ion-list>
        <ion-list>
          <ion-list-header>{{ t("create-case.overview.pictures") }}</ion-list-header>
          <ion-item></ion-item>
        </ion-list>
        <ion-list v-if="createCase.description === undefined">
          <ion-list-header>{{ t("create-case.overview.comment.title") }}</ion-list-header>
          <ion-item>
            <ion-textarea
              autogrow="true"
              maxlength="200"
              :placeholder="t('create-case.overview.comment.placeholder')"
              @ionChange="createCase.comment = $event.target.textContent"
            ></ion-textarea>
          </ion-item>
        </ion-list>
        <ion-list v-else>
          <ion-list-header>{{ t("create-case.overview.description.title") }}</ion-list-header>
          <ion-item>
            <ion-textarea
              autogrow="true"
              maxlength="200"
              :value="createCase.description"
              @ionChange="createCase.description = $event.target.textContent"
            ></ion-textarea>
          </ion-item>
        </ion-list>
        <div class="w-full ion-padding">
          <ion-button expand="block" @click="sendCase">
            <template v-if="loading">
              <ion-spinner class="mr-2"> </ion-spinner>
              {{ t("create-case.overview.send.button.sending") }}
            </template>
            <template v-else>
              {{ t("create-case.overview.send.button.send") }}
            </template>
          </ion-button>
        </div>
      </div>
    </ion-content>
  </ion-page>
</template>
