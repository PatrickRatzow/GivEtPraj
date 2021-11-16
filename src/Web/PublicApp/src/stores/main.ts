import { defineStore } from "pinia";
import axios from "@/utils/axios";

type Theme = true | false;

export const useMainStore = defineStore("main", () => {
	const caseQueue = ref<BaseCase[]>([
		{
			category: { id: 1, name: "Vejskade", icon: "fas fa-road", miscellaneous: false, subCategories: [] },
			subCategories: [{ id: 1, name: "Hul" }],
			images: [],
			comment: undefined,
			description: undefined,
			status: { color: "##FF0000", name: "Ikke færdig" },
			geographicLocation: { longitude: 9.34, latitude: 52.0 },
		},
		{
			category: { id: 2, name: "Vejskade", icon: "fas fa-road", miscellaneous: false, subCategories: [] },
			subCategories: [{ id: 1, name: "Hul" }],
			images: [],
			comment: undefined,
			description: undefined,
			status: { color: "##FF0000", name: "Ikke færdig" },
			geographicLocation: { longitude: 9.34, latitude: 52.0 },
		},
	]);
	const cases = ref<Case[]>([
		{
			id: 1,
			category: { id: 1, name: "Vejskade", icon: "fas fa-road", miscellaneous: false, subCategories: [] },
			subCategories: [{ id: 1, name: "Hul" }],
			images: [],
			comment: undefined,
			description: undefined,
			status: { color: "#00ff00", name: "Færdig" },
			geographicLocation: { longitude: 9.34, latitude: 52.0 },
			createdAt: new Date(),
			deviceId: "0001",
		},
		{
			id: 2,
			category: { id: 2, name: "Vejskade", icon: "fas fa-road", miscellaneous: false, subCategories: [] },
			subCategories: [
				{ id: 2, name: "Hul" },
				{ id: 2, name: "Isskade" },
			],
			images: [],
			comment: undefined,
			description: undefined,
			status: { color: "#00ff00", name: "Færdig" },
			geographicLocation: { longitude: 9.88387122, latitude: 57.01991472 },
			createdAt: new Date(2018, 11, 15),
			deviceId: "001",
		},
	]);
	const categories = ref<Category[]>([]);
	const activeTheme = ref<Theme>(false);
	const hasSeenTutorial = ref(false);
	const queueKey = ref<QueueKey>();

	const fetchCategories = async () => {
		if (categories.value.length > 0) return;

		const resp = await axios.get<Category[]>("categories");

		categories.value = resp.data;
	};

	return { hasSeenTutorial, cases, categories, activeTheme, fetchCategories, caseQueue, queueKey };
});
