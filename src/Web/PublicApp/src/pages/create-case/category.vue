<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";
import { useMainStore } from "@/stores/main";
import SubCategories from "./sub-categories.vue";

const router = useRouter();
const createCase = useCreateCaseStore();
const main = useMainStore();

main.fetchCategories();

const searchQuery = ref("");
const categories = computed(() =>
  main.categories.filter(
    (c) => searchQuery.value.length == 0 || c.name.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
);

const selectCategory = (category: Category) => {
  createCase.category = category;
  createCase.subCategories = [];
};

const ConfirmCatagories = () => {
  router.push("/create-case/pictures");
};
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button></ion-back-button>
      </ion-buttons>
      <ion-title>Vælg Kategori</ion-title>
    </ion-toolbar>
    <ion-content>
      <ion-searchbar
        placeholder="Søg"
        :value="searchQuery"
        @ionInput="searchQuery = $event.target.value"
        @ionClear="searchQuery = ''"
      ></ion-searchbar>
      <ion-list>
        <ion-radio-group :value="createCase.category?.name">
          <template v-for="(cat, index) in categories" :key="index">
            <ion-item @click="selectCategory(cat)">
              <i class="ml-1 mr-2" :class="cat.icon"></i>
              <ion-label>{{ cat.name }}</ion-label>
              <ion-radio slot="end" color="success" :value="cat.name"> </ion-radio>
            </ion-item>

            <sub-categories v-if="createCase.category == cat"></sub-categories>
          </template>
        </ion-radio-group>
      </ion-list>
      <ion-button class="flex flex-row my-6 mx-12" @click="ConfirmCatagories()">GG GO NEXT</ion-button>
    </ion-content>
  </ion-page>
</template>
