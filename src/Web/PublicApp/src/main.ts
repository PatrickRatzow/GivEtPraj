import { createApp } from "vue";
import App from "./App.vue";
import "./assets/main.css";
import router from "./router";
import { createPinia } from "pinia";
import ionic from "@/modules/ionic";
import i18n from "@/modules/i18n";

const pinia = createPinia();
pinia.state.value = {};

const app = createApp(App);
app.use(ionic);
app.use(router);
app.use(pinia);
app.use(i18n);

router.isReady().then(() => {
	app.mount("#app");
});
