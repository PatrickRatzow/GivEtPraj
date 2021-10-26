<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";
import { useMainStore } from "@/stores/main";

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

  router.push("/create-case/billeder");
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
          <ion-item v-for="(cat, index) in categories" :key="index" @click="selectCategory(cat)">
            <i class="ml-1 mr-2" :class="cat.icon"></i>
            <ion-label>{{ cat.name }}</ion-label>
            <ion-radio slot="end" color="success" :value="cat.name"></ion-radio>
          </ion-item>
        </ion-radio-group>
      </ion-list>
    </ion-content>
  </ion-page>
</template>
