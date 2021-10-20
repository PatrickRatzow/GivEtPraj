import { defineStore } from "pinia";
import axios from "../utils/axios";

export const useMainStore = defineStore("main", {
	state: () => ({
		cases: [] as Case[],
		categories: [] as Category[],
	}),
	actions: {
		async fetchCategories() {
			if (this.categories.length > 0) return;

			const resp = await axios.get<Category[]>("categories");

			this.categories = resp.data;
		},
	},
});
