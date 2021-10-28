import { Device } from "@capacitor/device";

export function useLocale() {
	async function getLanguageCode(): Promise<Language> {
		const languageCode = await Device.getLanguageCode();
		if (languageCode.value == "da") return "da";

		return "en";
	}

	return { getLanguageCode };
}