import { Device } from "@capacitor/device";
import { Storage } from "@capacitor/storage";

const language = ref<Language>();

export function useLocale() {
	async function saveLanguage() {
		if (!language.value) return;

		await Storage.set({
			key: "language",
			value: language.value.toString(),
		});
	}

	async function getLanguageCode(): Promise<Language> {
		const { value } = await Storage.get({ key: "language" });
		if (value !== null) return value as Language;

		const languageCodeResult = await Device.getLanguageCode();
		const languageCode = languageCodeResult.value.split("-")[0];
		language.value = languageCode == "da" ? "da" : "en";

		return language.value;
	}

	async function setLanguage(english: boolean) {
		language.value = english ? "en" : "da";

		saveLanguage();
	}

	return {
		getLanguageCode,
		setLanguage,
		language: readonly(language),
	};
}
