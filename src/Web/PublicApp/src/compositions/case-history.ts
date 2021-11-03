import { useMainStore } from "@/stores/main";
import { Device } from "@capacitor/device";
import axios from "@/utils/axios";
import { useNetwork } from "@/compositions/network";
import { Storage } from "@capacitor/storage";

export function useCaseHistory() {
	const main = useMainStore();
	const network = useNetwork();

	async function syncWithAPI(disableCache = false): Promise<void> {
		if (!disableCache) {
			const { value } = await Storage.get({ key: "case-history" });
			if (value !== null) {
				const cacheCases = JSON.parse(value) as Case[];
				main.cases = cacheCases;

				return;
			}
		}

		if (network.status.value?.connected !== true) return;

		const deviceId = await Device.getId();

		const resp = await axios.get<Case[]>(`cases/${deviceId.uuid}`);
		if (resp.status != 200) return;

		main.cases = resp.data;
		await Storage.set({
			key: "case-history",
			value: JSON.stringify(main.cases),
		});
	}

	syncWithAPI();

	return { cases: main.cases, syncWithAPI };
}
