import { createApp } from "vue";
import App from "./App.vue";
import "./assets/main.css";
import router from "./router";
import { createPinia } from "pinia";
import ionic from "@/modules/ionic";

const pinia = createPinia();
pinia.state.value = {};

const app = createApp(App);
app.use(router);
app.use(pinia);
app.use(ionic);
app.mount("#app");
