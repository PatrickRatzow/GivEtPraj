import { VueReCaptcha } from "vue-recaptcha-v3";

export const install: AppModule = ({ app }) => {
	app.use(VueReCaptcha, { siteKey: "6LftYP8cAAAAANp4eYjBuN8Yf1HUL1kOi1OwvRd3" });

	return Promise.resolve();
};
