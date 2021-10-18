import { InjectionKey } from "vue";
import { createStore, useStore as baseUseStore, Store } from "vuex";

interface Case {
	category: string;
	subCategories: string[];
	images: string[];
	comment: string | null;
}

interface CaseCreation {
	category?: string;
	subCategories: string[];
	images: string[];
	comment?: string;
}

interface State {
	caseInCreation: CaseCreation | null;
	cases: Case[];
}

export const key: InjectionKey<Store<State>> = Symbol();

export const store = createStore<State>({
	state: {
		caseInCreation: null,
		cases: [],
	} as State,
	mutations: {
		startCaseCreation(state: State) {
			state.caseInCreation = { category: "fuck", subCategories: [], images: [] };
		},
		endCaseCreation(state: State) {
			state.caseInCreation = null;
		},
	},
});

export function useStore() {
	return baseUseStore(key);
}
