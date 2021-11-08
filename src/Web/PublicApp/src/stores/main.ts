import { defineStore } from "pinia";
import axios from "@/utils/axios";

export const useMainStore = defineStore("main", () => {
	const cases = ref<Case[]>([
		{
			id: 1,
			category: { name: "Vejskade", icon: "fas fa-road", subCategories: [] },
			subCategories: [{ name: "Hul" }],
			images: [],
			comment: undefined,
			status: { color: "#00ff00", name: "Færdig" },
			geographicLocation: { latitude: 0, longitude: 0 },
			createdAt: new Date(),
			deviceId: "0001",
		},
		{
			id: 2,
			category: { name: "Vejskade", icon: "fas fa-road", subCategories: [] },
			subCategories: [{ name: "Hul" }, { name: "Isskade" }],
			images: [],
			comment: undefined,
			status: { color: "#00ff00", name: "Færdig" },
			geographicLocation: { latitude: 0, longitude: 0 },
			createdAt: new Date(2018, 11, 15),
			deviceId: "001",
		},
	]);
	const categories = ref<Category[]>([]);

	const fetchCategories = async () => {
		if (categories.value.length > 0) return;

		const resp = await axios.get<Category[]>("categories");

		categories.value = resp.data;
	};

	return { cases, categories, fetchCategories };
});
