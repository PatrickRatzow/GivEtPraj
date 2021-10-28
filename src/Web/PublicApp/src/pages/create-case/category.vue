<script setup lang="ts">
import { useLocalizedRoutes } from "@/compositions/localizedRoutes";
import { useCreateCaseStore } from "@/stores/create-case";
import { useMainStore } from "@/stores/main";

const router = useRouter();
const createCase = useCreateCaseStore();
const main = useMainStore();
const localizedRoutes = useLocalizedRoutes();
const { t } = useI18n();

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

const goToNext = async () => {
  const route = await localizedRoutes.getPicturesUrl();

  router.push(route);
};

const backUrl = ref<string>();
onMounted(async () => {
  backUrl.value = await localizedRoutes.getLocationUrl();
});
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <back-button :url="backUrl"></back-button>
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

                <sub-categories v-if="createCase.category == cat"></sub-categories>
              </template>
            </ion-radio-group>
          </ion-list>
        </div>
        <ion-button class="flex flex-row my-6 mx-12 float-bottom" @click="goToNext">
          {{ t("navigation.next") }}
        </ion-button>
      </div>
    </ion-content>
  </ion-page>
</template>
