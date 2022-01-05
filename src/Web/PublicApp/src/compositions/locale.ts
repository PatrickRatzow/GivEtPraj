import { useMainStore } from "@/stores/main";
import { Device } from "@capacitor/device";
import { Storage } from "@capacitor/storage";

export function useLocale() {
	async function saveLanguage() {
		const main = useMainStore();

		if (!main.language) return;

		await Storage.set({
			key: "language",
			value: main.language.toString(),
		});
	}

	async function getLanguageCode(): Promise<Language> {
		const { value } = await Storage.get({ key: "language" });
		if (value !== null) return value as Language;

		const languageCodeResult = await Device.getLanguageCode();
		const languageCode = languageCodeResult.value.split("-")[0];

		return languageCode == "da" ? "da" : "en";
	}

	async function setLanguage(english: boolean) {
		const main = useMainStore();

		main.language = english ? "en" : "da";

		saveLanguage();
	}

	async function toggleLanguage() {
		const main = useMainStore();

		await setLanguage(main.language != "en");
	}

	return {
		getLanguageCode,
		setLanguage,
		toggleLanguage,
	};
}
