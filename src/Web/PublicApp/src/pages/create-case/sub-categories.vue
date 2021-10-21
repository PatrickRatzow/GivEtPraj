<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";

const router = useRouter();
const createCase = useCreateCaseStore();
const category = computed(() => createCase.category as Category);

//const currentSelected = computed(() => createCase.category?.subCategories.filter);

const searchQuery = ref("");
const subCategories = computed(() =>
  category.value.subCategories.filter(
    (s) => searchQuery.value.length == 0 || s.name.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
);

const addSubCategory = (subCategory: SubCategory) => {
  if (createCase.subCategories.length >= 3) return;

  createCase.$patch((state) => {
    state.subCategories.push(subCategory);
  });
};
const removeSubCategory = (subCategory: SubCategory) => {
  createCase.$patch((state) => {
    state.subCategories = state.subCategories.filter((s) => s.name !== subCategory.name);
  });
};

const isSelected = (subCategory: SubCategory) => createCase.subCategories.some((s) => s.name === subCategory.name);
const toggleSubCategory = (subCategory: SubCategory) => {
  if (isSelected(subCategory)) {
    return removeSubCategory(subCategory);
  }

  addSubCategory(subCategory);
};
</script>

<template>
  <h1>Sub Categories {{ category.name }}</h1>
  <TextField v-model:text="searchQuery" title="Søg" />
  <div class="mt-10 grid gap-y-4 grid-rows-1">
    <ItemRow
      v-for="(subCat, i) in subCategories"
      :key="i"
      :title="subCat.name"
      :icon="category.icon"
      :selected="isSelected(subCat)"
      @click="toggleSubCategory(subCat)"
    ></ItemRow>
  </div>
  <div class="mt-10 gap-y-4">
    <button class="bg-green-500 py-2 px-4 float-right" type="button">Gg go next</button>
  </div>
</template>

<route lang="yaml">
meta:
  layout: create-case
</route>
