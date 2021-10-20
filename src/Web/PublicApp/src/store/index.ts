import { Photo } from "@capacitor/camera";
import { InjectionKey } from "vue";
import { createStore, useStore as baseUseStore, Store } from "vuex";
import axios from "../utils/axios";

export const key: InjectionKey<Store<State>> = Symbol();

export const store = createStore<State>({
	state: {
		caseInCreation: null,
		cases: [],
		categories: [],
	} as State,
	mutations: {
		startCaseCreation(state: State) {
			state.caseInCreation ??= { subCategories: [], images: [] };
		},
		endCaseCreation(state: State) {
			state.caseInCreation = null;
		},
		setChosenCategory(state: State, payload: Category) {
			(state.caseInCreation as CaseCreation).category = payload;
		},
		setCategories(state: State, payload: Category[]) {
			state.categories = payload;
		},
		addImage(state: State, payload: Photo) {
			(state.caseInCreation as CaseCreation).images.push(payload);
		},
	},
	actions: {
		async fetchCategories({ commit }) {
			const resp = await axios.get<Category[]>("categories");
			commit("setCategories", resp.data);
		},
	},
});

export function useStore() {
	return baseUseStore(key);
}
