import { createRouter, createWebHistory } from "@ionic/vue-router";
import { RouteRecordRaw } from "vue-router";

const cfgRoutes: RouteRecordRaw[] = [
	{
		path: "/",
		redirect: "/opret-praj/lokation",
	},
	{
		path: "/opret-praj/",
		component: () => import("@/layouts/tabs.vue"),
		children: [
			{
				path: "",
				redirect: "/opret-praj/lokation",
			},
			{
				path: "lokation",
				component: () => import("@/pages/create-case/map.vue"),
			},
			{
				path: "kategori",
				component: () => import("@/pages/create-case/category.vue"),
			},
			{
				path: "billeder",
				component: () => import("@/pages/create-case/pictures.vue"),
			},
			{
				path: "afslut",
				component: () => import("@/pages/create-case/finalize.vue"),
			},
		],
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
