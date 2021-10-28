import { createPinia } from "pinia";

const pinia = createPinia();
pinia.state.value = {};

export const install: AppModule = (app) => {
	app.use(pinia);

	return Promise.resolve();
};
