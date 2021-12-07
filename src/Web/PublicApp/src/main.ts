import { createApp } from "vue";
import App from "./App.vue";
import "./assets/main.css";
import router from "./router";

const app = createApp(App);
app.use(router);

/* Load all modules */
const modules = Object.values(import.meta.globEager("./modules/*.ts")).map((i) => i.install?.({ app, router }));
await Promise.all(modules);

/* Load all compositions */
const compositions = Object.values(import.meta.globEager("./compositions/*.ts")).map((i) =>
	i.beforeAppMount?.({ app, router } as ModuleOptions)
);

router.isReady().then(async () => {
	await Promise.all(compositions);

	app.mount("#app");

	await Promise.all(
		Object.values(import.meta.globEager("./compositions/*.ts")).map((i) =>
			i.afterAppMount?.({ app, router } as ModuleOptions)
		)
	);
});
