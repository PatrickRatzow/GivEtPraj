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

  //router.push("/create-case/sub-categories");
};
</script>

<template>
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
  <ion-button class="float-right">GG GO NEXT</ion-button>
</template>

<route lang="yaml">
meta:
  title: "Vælg kategori"
</route>
