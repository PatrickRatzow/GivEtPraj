import { defineStore } from "pinia";

export const useCreateCaseStore = defineStore("main", {
	state: () =>
		({
			subCategories: [],
			images: [],
		} as CaseCreation),
});
