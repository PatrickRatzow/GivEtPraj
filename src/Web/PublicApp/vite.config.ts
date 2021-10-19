import { defineConfig } from "vite";
import vue, { parseVueRequest } from "@vitejs/plugin-vue";
import eslintPlugin from "vite-plugin-eslint";
import { VitePWA as pwa } from "vite-plugin-pwa";

// https://vitejs.dev/config/
export default defineConfig({
	// server: {
	// 	fs: {
	// 		allow: [],
	// 	},
	// },
	plugins: [
		vue(),
		eslintPlugin(),
		pwa({
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
	],
});
