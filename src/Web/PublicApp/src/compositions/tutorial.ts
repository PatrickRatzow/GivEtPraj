import { useMainStore } from "@/stores/main";
import { Storage } from "@capacitor/storage";

export function useTutorial() {
	const main = useMainStore();

	async function loadCache() {
		const { value } = await Storage.get({ key: "seenTutorial" });
		main.hasSeenTutorial = value == "true";
	}

	async function setTutorialSeen(seen: boolean) {
		main.hasSeenTutorial = seen;

		await Storage.set({
			key: "seenTutorial",
			value: seen.toString(),
		});
	}

	return { setTutorialSeen, loadCache };
}
