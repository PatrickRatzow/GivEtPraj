import { Photo } from "@capacitor/camera";
import { defineStore } from "pinia";

export const useCreateCaseStore = defineStore("createCase", () => {
	const category = ref<Category | undefined>();
	const subCategories = reactive<string[]>([]);
	const images = reactive<Photo[]>([]);
	const comment = ref<string | undefined>();

	return { category, subCategories, images, comment };
});
