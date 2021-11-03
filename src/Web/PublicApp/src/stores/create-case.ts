import { Photo } from "@capacitor/camera";
import { defineStore } from "pinia";
import { Device } from "@capacitor/device";

export const useCreateCaseStore = defineStore("createCase", () => {
	const category = ref<Category | null>(null);
	const subCategories = ref<SubCategory[]>([]);
	const images = ref<Photo[]>([]);
	const comment = ref<string | undefined>();
	const geographicLocation = ref<GeographicLocation>();
	const deviceId = Device.getId();

	return { category, subCategories, images, comment, geographicLocation, deviceId };
});
