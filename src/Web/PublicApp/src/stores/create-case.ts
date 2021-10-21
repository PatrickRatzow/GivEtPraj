import { Photo } from "@capacitor/camera";
import { defineStore } from "pinia";

export const useCreateCaseStore = defineStore("createCase", () => {
	const category = ref<Category | null>(null);
	const subCategories = ref<SubCategory[]>([]);
	const images = reactive<Photo[]>([]);
	const comment = ref<string | undefined>(undefined);

	return { category, subCategories, images, comment };
});
