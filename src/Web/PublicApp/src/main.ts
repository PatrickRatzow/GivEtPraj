import { createApp } from "vue";
import App from "./App.vue";
import "./assets/main.css";
import router from "./router";

const app = createApp(App);
app.use(router);

/* Load all modules */
const modules = Object.values(import.meta.globEager("./modules/*.ts")).map((i) => i.install?.(app));
await Promise.all(modules);

router.isReady().then(() => {
	app.mount("#app");
});
