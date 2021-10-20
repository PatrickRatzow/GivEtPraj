import { defineConfig } from "vite";
import Vue from "@vitejs/plugin-vue";
import ESLintPlugin from "vite-plugin-eslint";
import { VitePWA } from "vite-plugin-pwa";
import { resolve } from "path";
import Components from "unplugin-vue-components/vite";
import AutoImport from "unplugin-auto-import/vite";
import Pages from "vite-plugin-pages";
import Layouts from "vite-plugin-vue-layouts";

// https://vitejs.dev/config/
export default defineConfig({
	resolve: {
		alias: {
			"@": resolve(__dirname, "./src"),
		},
	},
	plugins: [
		Vue(),
		ESLintPlugin(),
		VitePWA({
			mode: "development",
			base: "/",
			srcDir: "src",
			filename: "sw.ts",
			includeAssets: ["/favicon.png"],
			strategies: "injectManifest",
			manifest: {
				name: "Name of your app",
				short_name: "Short name of your app",
				description: "Description of your app",
				theme_color: "#ffffff",
				icons: [
					{
						src: "icon-192.png",
						sizes: "192x192",
						type: "image/png",
					},
					{
						src: "/icon-512.png",
						sizes: "512x512",
						type: "image/png",
					},
					{
						src: "icon-512.png",
						sizes: "512x512",
						type: "image/png",
						purpose: "any maskable",
					},
				],
			},
		}),
		Components({
			// allow auto load markdown components under `./src/components/`
			extensions: ["vue"],

			// allow auto import and register components used in markdown
			include: [/\.vue$/, /\.vue\?vue/],

			dts: "src/components.d.ts",
		}),
		AutoImport({
			imports: ["vue", "vue-router"],
			dts: "src/auto-imports.d.ts",
		}),
		Pages(),
		Layouts(),
	],
});
