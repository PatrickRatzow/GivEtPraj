import { defineStore } from "pinia";
import axios from "@/utils/axios";

export const useMainStore = defineStore("main", () => {
	const caseQueue = ref<BaseCase[]>([]);
	const cases = ref<Case[]>([]);
	const categories = ref<Category[]>([]);
	const hasSeenTutorial = ref(false);
	const language = ref<Language>("en");

	const fetchCategories = async () => {
		if (categories.value.length > 0) return;

		const resp = await axios.get<Category[]>("categories");

		categories.value = resp.data;
	};

	return { hasSeenTutorial, cases, categories, fetchCategories, caseQueue, language };
});
