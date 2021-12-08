import { Storage } from "@capacitor/storage";

type Theme = true | false;

const activeTheme = ref<Theme>(false);

export function useThemes() {
	async function saveTheme() {
		await Storage.set({
			key: "theme",
			value: activeTheme.value.toString(),
		});
	}

	async function loadTheme() {
		const { value } = await Storage.get({ key: "theme" });
		if (value !== null) return (activeTheme.value = value == "true");

		// Use matchMedia to check the user preference
		const prefersDark = window.matchMedia("(prefers-color-scheme: dark)");

		activeTheme.value = prefersDark.matches;

		// Listen for changes to the prefers-color-scheme media query
		prefersDark.addEventListener("change", (mediaQuery) => {
			activeTheme.value = mediaQuery.matches;
		});
	}

	function setTheme(theme: boolean) {
		activeTheme.value = theme;

		saveTheme();
	}

	return { loadTheme, setTheme, activeTheme: readonly(activeTheme) };
}

export const beforeAppMount: BeforeAppMount = async () => {
	const themes = useThemes();

	await themes.loadTheme();
};
