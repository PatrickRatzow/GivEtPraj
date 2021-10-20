import { defineStore } from "pinia";
import axios from "@/utils/axios";

export const useMainStore = defineStore("main", () => {
	const cases = ref<Case[]>([]);
	const categories = ref<Category[]>([]);

	const fetchCategories = async () => {
		if (categories.value.length > 0) return;

		const resp = await axios.get<Category[]>("categories");

		categories.value = resp.data;
	};

	return { cases, categories, fetchCategories };
});
