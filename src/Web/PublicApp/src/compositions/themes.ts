import { Storage } from "@capacitor/storage";

type Theme = true | false;

export function useThemes() {
	const activeTheme = ref<Theme>(false);

	async function saveTheme() {
		await Storage.set({
			key: "theme",
			value: activeTheme.value.toString(),
		});
	}

	async function loadTheme() {
		const { value } = await Storage.get({ key: "theme" });
		if (value !== null) return setTheme(value == "true");

		// Use matchMedia to check the user preference
		const prefersDark = window.matchMedia("(prefers-color-scheme: dark)");

		setTheme(prefersDark.matches);

		// Listen for changes to the prefers-color-scheme media query
		prefersDark.addListener((mediaQuery) => setTheme(mediaQuery.matches));
	}

	function setTheme(theme: Theme) {
		activeTheme.value = theme;

		document.body.classList.toggle("dark", theme);
	}

	async function toggleTheme() {
		setTheme(!activeTheme.value);
		await saveTheme();
	}

	return { activeTheme, loadTheme, toggleTheme };
}
