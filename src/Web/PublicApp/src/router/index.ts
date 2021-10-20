import { createRouter, createWebHistory } from "vue-router";

const routes = [
	{
		path: "/",
		name: "Home",
		component: () => import("@/views/Home.vue"),
	},
	{
		path: "/about",
		name: "About",
		component: () => import("@/views/About.vue"),
	},
	{
		path: "/create-case",
		name: "Category",
		component: () => import("@/views/create-case/Category.vue"),
	},
	{
		path: "/create-case/sub-categories",
		name: "Sub Categories",
		component: () => import("@/views/create-case/SubCategories.vue"),
	},
	{
		path: "/create-case/map",
		name: "Map",
		component: () => import("@/views/create-case/Map.vue"),
	},
	{
		path: "/camera",
		name: "Camera",
		component: () => import("@/views/Camera.vue"),
	},
];

const router = createRouter({
	history: createWebHistory(),
	routes,
});

export default router;
