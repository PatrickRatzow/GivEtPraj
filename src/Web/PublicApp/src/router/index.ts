import { createRouter, createWebHistory } from "vue-router";
import Home from "../views/Home.vue";
import About from "../views/About.vue";
import Map from "../views/create-case/Map.vue";
import Camera from "../views/Camera.vue";
import Category from "../views/create-case/CategoryItem.vue";

const routes = [
	{ path: "/", name: "Home", component: Home },
	{ path: "/about", name: "About", component: About },
	{ path: "/create-case/map", name: "Map", component: Map },
	{ path: "/camera", name: "Camera", component: Camera },
	{ path: "/category", name: "Category", component: Category },
];

const router = createRouter({
	history: createWebHistory(),
	routes,
});

export default router;
