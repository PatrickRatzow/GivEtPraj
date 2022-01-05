import { createApp } from "vue";
import App from "./App.vue";
import "./assets/main.css";
import router from "./router";

const app = createApp(App);
app.use(router);

/* Load all modules */
const modules = Object.values(import.meta.globEager("./modules/*.ts")).map((i) => i.install?.({ app, router }));
Promise.all(modules).then(async () => {
	await router.isReady();
	const compositions = Object.values(import.meta.globEager("./compositions/*.ts"));

	await Promise.all(compositions.map((i) => i.beforeAppMount?.({ app, router } as ModuleOptions)));

	app.mount("#app");

	await Promise.all(compositions.map((i) => i.afterAppMount?.({ app, router } as ModuleOptions)));
});
