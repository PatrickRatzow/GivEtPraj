import { createApp } from "vue";
import App from "./App.vue";
import "./assets/main.css";
import router from "./router";
import { store, key } from "./store";
import { defineCustomElements } from "@ionic/pwa-elements/loader";

createApp(App).use(router).use(store, key).mount("#app");

//To enable PWA, right now it give errors;
//defineCustomElements(window);
