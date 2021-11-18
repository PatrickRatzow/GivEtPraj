import { PlaywrightTestConfig } from "@playwright/test";
const config: PlaywrightTestConfig = {
	use: {
		baseURL: "http://localhost:3000/create-praj/location",
		browserName: "chromium",
		headless: false,
		locale: "en-GB",
	},
};
export default config;