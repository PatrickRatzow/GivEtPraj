<script setup lang="ts">
import { useCreateCaseStore } from "@/stores/create-case";
import { useMainStore } from "@/stores/main";

const router = useRouter();
const createCase = useCreateCaseStore();
const main = useMainStore();
const { t } = useI18n();

main.fetchCategories();

const searchQuery = ref("");
const categories = computed(() =>
  main.categories
    .filter((c) => searchQuery.value.length == 0 || c.name.toLowerCase().includes(searchQuery.value.toLowerCase()))
    .sort((a) => (a.miscellaneous ? 1 : -1))
);

const selectCategory = (category: Category) => {
  createCase.category = category;
  createCase.subCategories = [];
};
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <back-button url="/create-praj/location"></back-button>
      </ion-buttons>
      <ion-title>{{ t("create-case.category.title") }}</ion-title>
    </ion-toolbar>
    <ion-content class="ion-padding">
      <div class="flex flex-col justify-between h-full">
        <div>
          <ion-searchbar
            :placeholder="t('create-case.category.search-placeholder')"
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

                <template v-if="createCase.category == cat">
                  <sub-categories></sub-categories>
                </template>
              </template>
              <ion-item v-if="createCase.category?.miscellaneous">
                <ion-textarea
                  placeholder="Enter your concern here"
                  autogrow="true"
                  maxlength="200"
                  class="border"
                  @IonChange="createCase.description = $event.target.textContent"
                ></ion-textarea>
              </ion-item>
            </ion-radio-group>
          </ion-list>
          <div></div>
        </div>
        <ion-button class="flex-row float-bottom" @click="router.push('/create-praj/pictures')">
          {{ t("navigation.next") }}
        </ion-button>
      </div>
    </ion-content>
  </ion-page>
</template>
