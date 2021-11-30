import { useMainStore } from "@/stores/main";
import { Device } from "@capacitor/device";
import http from "@/utils/axios";

export function useCaseHistory() {
	const main = useMainStore();

	async function syncWithAPI(): Promise<void> {
		const deviceId = await Device.getId();

		const resp = await http.get<Case[]>(`cases/${deviceId.uuid}`);
		if (resp.status != 200) return;

		main.cases = resp.data;
	}

	syncWithAPI();

	return { cases: main.cases, syncWithAPI };
}
