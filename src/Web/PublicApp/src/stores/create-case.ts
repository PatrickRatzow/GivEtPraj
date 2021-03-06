import { Photo } from "@capacitor/camera";
import { defineStore } from "pinia";

export const useCreateCaseStore = defineStore("createCase", () => {
	const category = ref<Category | null>(null);
	const subCategories = ref<SubCategory[]>([]);
	const images = ref<Photo[]>([]);
	const comment = ref<string>();
	const description = ref<string>();
	const geographicLocation = ref<GeographicLocation>();

	return { category, subCategories, images, comment, geographicLocation, description };
});
