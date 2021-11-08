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

const area = () => {
  createCase.description = document.getElementById("description")?.textContent;
  console.log("area value:" + createCase.description);
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
    <ion-content>
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
                  <ion-textarea
                    v-if="createCase.category?.miscellaneous"
                    id="description"
                    placeholder="Enter your concern here"
                    maxlength="200"
                    class="description"
                  ></ion-textarea>
                  <sub-categories v-else></sub-categories>
                </template>
              </template>
            </ion-radio-group>
          </ion-list>
          <div></div>
        </div>
        <ion-button class="flex flex-row my-6 mx-12 float-bottom" @click="router.push('/create-praj/pictures')">
          {{ t("navigation.next") }}
          {{ area() }}
        </ion-button>
      </div>
    </ion-content>
  </ion-page>
</template>
