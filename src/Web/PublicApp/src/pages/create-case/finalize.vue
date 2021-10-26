<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";
import { useMainStore } from "@/stores/main";

const createCase = useCreateCaseStore();
const main = useMainStore();

createCase.category = {
  name: "Vejskade",
  icon: "fas fa-road",
  subCategories: [],
};
createCase.subCategories = [{ name: "Hul" }, { name: "XD" }, { name: "XD2" }];
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button default-href="/opret-praj/billeder"></ion-back-button>
      </ion-buttons>
      <ion-title>Afslut</ion-title>
    </ion-toolbar>
    <ion-content>
      <ion-list>
        <ion-list-header>Kategori</ion-list-header>
        <ion-item>
          <i class="ml-1 mr-2" :class="createCase.category?.icon"></i>
          <ion-label>{{ createCase.category?.name }}</ion-label>
        </ion-item>
      </ion-list>
      <ion-list>
        <ion-list-header>Underkategorier</ion-list-header>
        <ion-item v-for="(sub, idx) in createCase.subCategories" :key="idx">
          <ion-label>{{ sub.name }}</ion-label>
        </ion-item>
      </ion-list>
      <ion-list>
        <ion-list-header>Billeder</ion-list-header>
        <ion-item>I'm an image</ion-item>
      </ion-list>
      <ion-list>
        <ion-list-header>Kommentar</ion-list-header>
        <ion-item>
          <ion-textarea placeholder="Skriv her"></ion-textarea>
        </ion-item>
      </ion-list>
    </ion-content>
  </ion-page>
</template>
