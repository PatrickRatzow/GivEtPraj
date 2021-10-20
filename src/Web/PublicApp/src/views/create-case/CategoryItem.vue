<script setup lang="ts">
import ItemRow from "../../components/ItemRow.vue";
import TextField from "../../components/TextField.vue";
import { onMounted, computed, ref } from "vue";
import { useStore } from "../../store";

const store = useStore();
store.commit("startCaseCreation");
onMounted(() => store.dispatch("fetchCategories"));
const searchQuery = ref("");
const categories = computed(() =>
  store.state.categories.filter((c) =>
    searchQuery.value.length == 0 ? true : c.name.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
);

const isSelected = (category: any) => store.state.caseInCreation?.category?.name === category.name;
const selectCategory = (category: any) => store.commit("setChosenCategory", category);
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
