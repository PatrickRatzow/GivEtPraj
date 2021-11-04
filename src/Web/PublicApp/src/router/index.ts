import { createRouter, createWebHistory } from "@ionic/vue-router";
import { RouteRecordRaw } from "vue-router";

const cfgRoutes: RouteRecordRaw[] = [
	{
		path: "/",
		redirect: "/create-praj/location",
	},
	{
		path: "/create-praj/",
		component: () => import("@/layouts/tabs.vue"),
		children: [
			{
				path: "",
				redirect: "/create-praj/location",
			},
			{
				name: "location",
				path: "location",
				component: () => import("@/pages/create-case/map.vue"),
			},
			{
				name: "category",
				path: "category",
				component: () => import("@/pages/create-case/category.vue"),
			},
			{
				name: "pictures",
				path: "pictures",
				component: () => import("@/pages/create-case/pictures.vue"),
			},
			{
				name: "picture-preview",
				path: "pictures/:id",
				component: () => import("@/pages/create-case/picture-preview.vue"),
			},
			{
				name: "checkout",
				path: "checkout",
				component: () => import("@/pages/create-case/finalize.vue"),
			},
		],
	},
	{
		name: "my-cases",
		path: "/my-prajs",
		component: () => import("@/pages/my-cases.vue"),
	},
	{
		name: "case",
		path: "/praj/:id",
		component: () => import("@/pages/case.vue"),
	},
	{
		name: "settings",
		path: "/settings",
		component: () => import("@/pages/settings.vue"),
	},
	{
		name: "404",
		path: "/:pathMatch(.*)*",
		component: () => import("@/pages/404.vue"),
	},
];

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: cfgRoutes,
});

export default router;
