<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";
import { useMainStore } from "@/stores/main";

const router = useRouter();
const createCase = useCreateCaseStore();
const category = computed(() => createCase.category as Category);
const main = useMainStore();

//const currentSelected = computed(() => createCase.category?.subCategories.filter);

const searchQuery = ref("");
const subCategories = computed(() =>
  category.value.subCategories.filter(
    (s) => searchQuery.value.length == 0 || s.name.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
);

onMounted(async () => {
  if (createCase.category) return;

  await main.fetchCategories();

  createCase.category = main.categories[0];
});

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
  <ion-item v-for="(subCat, index) in subCategories" :key="index" class="w-full px-4 mb-2">
    <ion-label>{{ subCat.name }}</ion-label>
    <ion-checkbox
      :disabled="createCase.subCategories.length == 3 && !isSelected(subCat)"
      @ionChange="toggleSubCategory(subCat)"
    ></ion-checkbox>
  </ion-item>
</template>

<route lang="yaml">
meta:
  title: "Vælg Subkategorier"
</route>
