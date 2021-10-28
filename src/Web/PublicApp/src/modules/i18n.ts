import { createI18n } from "vue-i18n";
import { useLocale } from "@/compositions/locale";

const locale = useLocale();

// Import i18n resources
// https://vitejs.dev/guide/features.html#glob-import
const messages = Object.fromEntries(
	Object.entries(import.meta.globEager("../../locales/*.y(a)?ml")).map(([key, value]) => {
		const yaml = key.endsWith(".yaml");
		return [key.slice(14, yaml ? -5 : -4), value.default];
	})
);

export const install: AppModule = async (app) => {
	const languageCode = await locale.getLanguageCode();
	const i18n = createI18n({
		legacy: false,
		locale: languageCode,
		messages,
	});

	app.use(i18n);
};
