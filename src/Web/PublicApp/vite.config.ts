import { defineConfig } from "vite";
import Vue from "@vitejs/plugin-vue";
import ESLintPlugin from "vite-plugin-eslint";
import { VitePWA } from "vite-plugin-pwa";
import { resolve } from "path";
import Components from "unplugin-vue-components/vite";
import AutoImport from "unplugin-auto-import/vite";
import { ComponentResolver } from "unplugin-vue-components/dist/types";
import VueI18n from "@intlify/vite-plugin-vue-i18n";

function IonicResolver(): ComponentResolver {
	return (name: string) => {
		if (name.match(/^Ion[A-Z]/)) return { importName: name, path: "@ionic/vue" };
	};
}

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
			includeAssets: ["favicon.svg", "favicon.ico", "robots.txt", "apple-touch-icon.png"],
			strategies: "injectManifest",
			manifest: {
				name: "Giv Et Praj",
				short_name: "Giv Et Praj",
				description: "Report road related damages to the Danish authorities",
				theme_color: "#2ecc71",
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
			resolvers: [IonicResolver()],

			dts: "src/components.d.ts",
		}),
		AutoImport({
			imports: ["vue", "vue-router", "vue-i18n", "@vueuse/head"],
			dts: "src/auto-imports.d.ts",
		}),
		VueI18n({
			runtimeOnly: true,
			compositionOnly: true,
			include: [resolve(__dirname, "./locales/**")],
		}),
	],
});
