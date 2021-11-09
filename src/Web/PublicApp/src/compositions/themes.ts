import { useMainStore } from "@/stores/main";
import { Storage } from "@capacitor/storage";

export function useThemes() {
	const main = useMainStore();

	async function saveTheme() {
		await Storage.set({
			key: "theme",
			value: main.activeTheme.toString(),
		});
	}

	async function loadTheme() {
		const { value } = await Storage.get({ key: "theme" });
		if (value !== null) return (main.activeTheme = value == "true");

		// Use matchMedia to check the user preference
		const prefersDark = window.matchMedia("(prefers-color-scheme: dark)");

		main.activeTheme = prefersDark.matches;

		// Listen for changes to the prefers-color-scheme media query
		prefersDark.addEventListener("change", (mediaQuery) => {
			main.activeTheme = mediaQuery.matches;
		});
	}

	function setTheme(theme: boolean) {
		main.activeTheme = theme;

		saveTheme();
	}

	return { loadTheme, setTheme };
}
