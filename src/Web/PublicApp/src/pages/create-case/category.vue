<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";
import { useMainStore } from "@/stores/main";

const router = useRouter();
const createCase = useCreateCaseStore();
const main = useMainStore();

onMounted(() => main.fetchCategories());

const searchQuery = ref("");
const categories = computed(() =>
  main.categories.filter(
    (c) => searchQuery.value.length == 0 || c.name.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
);

const isSelected = (category: Category) => createCase.category?.name === category.name;
const selectCategory = (category: Category) => {
  createCase.category = category;

  router.push("/create-case/sub-categories");
};
</script>

<template>
  <div>
    <h1>About</h1>
    <TextField v-model:text="searchQuery" title="Søg" />
    <div class="mt-10 grid gap-y-4 grid-rows-1">
      <ItemRow
        v-for="(cat, index) in categories"
        :key="index"
        :title="cat.name"
        :icon="cat.icon"
        :selected="isSelected(cat)"
        @click="selectCategory(cat)"
      ></ItemRow>
    </div>
  </div>
</template>

<route lang="yaml">
meta:
  layout: create-case
</route>
