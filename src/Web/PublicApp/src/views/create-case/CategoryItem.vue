<script setup lang="ts">
import ItemRow from "../../components/ItemRow.vue";
import TextField from "../../components/TextField.vue";
import { onMounted, computed, ref } from "vue";
import { useCreateCaseStore, useMainStore } from "@/stores";

const caseStore = useCreateCaseStore();
const mainStore = useMainStore();

onMounted(() => mainStore.fetchCategories());

const searchQuery = ref("");
const categories = computed(() =>
  mainStore.categories.filter(
    (c) => searchQuery.value.length == 0 || c.name.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
);

const isSelected = (category: any) => caseStore.category?.name === category.name;
const selectCategory = (category: any) => (caseStore.category = category);
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
