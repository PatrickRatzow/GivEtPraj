import { createRouter, createWebHistory } from "@ionic/vue-router";
import { RouteRecordRaw } from "vue-router";

const cfgRoutes: RouteRecordRaw[] = [
	{
		path: "/",
		redirect: "/opret-praj/lokation",
	},
	{
		path: "/opret-praj/",
		alias: "/create-praj/",
		component: () => import("@/layouts/tabs.vue"),
		children: [
			{
				path: "",
				redirect: "/opret-praj/lokation",
			},
			{
				name: "location",
				path: "lokation",
				alias: "location",
				component: () => import("@/pages/create-case/map.vue"),
			},
			{
				name: "category",
				path: "kategori",
				alias: "category",
				component: () => import("@/pages/create-case/category.vue"),
			},
			{
				name: "pictures",
				path: "billeder",
				alias: "pictures",
				component: () => import("@/pages/create-case/pictures.vue"),
			},
			{
				name: "picture-preview",
				path: "billeder/:id",
				alias: "pictures/:id",
				component: () => import("@/pages/create-case/picture-preview.vue"),
			},
			{
				name: "checkout",
				path: "afslut",
				alias: "checkout",
				component: () => import("@/pages/create-case/finalize.vue"),
			},
		],
	},
	{
		name: "my-cases",
		path: "/mine-prajs",
		alias: "/my-prajs",
		component: () => import("@/pages/my-cases.vue"),
	},
	{
		name: "case",
		path: "/praj/:id",
		component: () => import("@/pages/case.vue"),
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
