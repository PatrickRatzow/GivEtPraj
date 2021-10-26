import { createRouter, createWebHistory } from "@ionic/vue-router";
import { RouteRecordRaw } from "vue-router";

const cfgRoutes: RouteRecordRaw[] = [
	{
		path: "/",
		redirect: "/opret-praj",
	},
	{
		path: "/opret-praj",
		component: () => import("@/pages/create-case/index.vue"),
	},
	{
		path: "/mine-prajs",
		component: () => import("@/pages/about.vue"),
	},
	{
		path: "/:pathMatch(.*)*",
		component: () => import("@/pages/404.vue"),
	},
];

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: cfgRoutes,
});

export default router;
