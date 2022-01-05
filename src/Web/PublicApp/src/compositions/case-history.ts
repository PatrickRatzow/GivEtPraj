import { useMainStore } from "@/stores/main";
import http from "@/utils/axios";

export function useCaseHistory() {
	const main = useMainStore();

	async function syncWithAPI(): Promise<void> {
		const resp = await http.get<Case[]>("cases/mine");
		if (resp.status != 200) return;

		main.cases = resp.data.map((c) => {
			return {
				...c,
				// TODO: Change this, it's a hack
				status: {
					color: "#ffffff",
					name: "Under behandling",
				} as Status,
			};
		});
	}

	syncWithAPI();

	return { syncWithAPI };
}
