import { defineStore } from "pinia";
import axios from "@/utils/axios";

type Theme = true | false;

export const useMainStore = defineStore("main", () => {
	const cases = ref<Case[]>([
		{
			id: 1,
			category: { name: "Vejskade", icon: "fas fa-road", miscellaneous: false, subCategories: [] },
			subCategories: [{ name: "Hul" }],
			images: [],
			comment: undefined,
			description: null,
			status: { color: "#00ff00", name: "Færdig" },
			geographicLocation: { longitude: 9.34, latitude: 52.0 },
			createdAt: new Date(),
			deviceId: "0001",
		},
		{
			id: 2,
			category: { name: "Vejskade", icon: "fas fa-road", miscellaneous: false, subCategories: [] },
			subCategories: [{ name: "Hul" }, { name: "Isskade" }],
			images: [],
			comment: undefined,
			description: null,
			status: { color: "#00ff00", name: "Færdig" },
			geographicLocation: { longitude: 9.88387122, latitude: 57.01991472 },
			createdAt: new Date(2018, 11, 15),
			deviceId: "001",
		},
	]);
	const categories = ref<Category[]>([]);
	const activeTheme = ref<Theme>(false);
	const hasSeenTutorial = ref(false);

	const fetchCategories = async () => {
		if (categories.value.length > 0) return;

		const resp = await axios.get<Category[]>("categories");

		categories.value = resp.data;
	};

	return { hasSeenTutorial, cases, categories, activeTheme, fetchCategories };
});
