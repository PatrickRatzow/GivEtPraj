import { useNetwork } from "@/compositions/network";
import { useMainStore } from "@/stores/main";
import { useCreateCaseStore } from "@/stores/create-case";
import axios from "@/utils/axios";
import { useQueueKeys } from "@/compositions/queue-keys";
import { useCaseHistory } from "@/compositions/case-history";
import { Device } from "@capacitor/device";

export function useCases() {
	const network = useNetwork();
	const main = useMainStore();
	const createCase = useCreateCaseStore();
	const queueKeys = useQueueKeys();
	const caseHistory = useCaseHistory();

	function addCurrentCaseToQueue() {
		if (!createCase.category) return;
		if (!createCase.geographicLocation) return;
		const newCase: BaseCase = {
			category: createCase.category,
			subCategories: createCase.subCategories,
			images: createCase.images.map((photo) => photo.base64String as string),
			comment: createCase.comment,
			description: createCase.description,
			status: { name: "not done", color: "#000000" },
			geographicLocation: createCase.geographicLocation,
		};

		main.caseQueue = [...main.caseQueue, newCase];
	}

	function removeCaseFromQueue(index: number) {
		main.caseQueue = main.caseQueue.filter((c, idx) => idx != index);
	}

	function emptyCaseQueue() {
		main.caseQueue = [];
	}

	interface CreateCaseRequestDto {
		deviceId: string;
		description: string;
		comment: string;
		subCategories: number[];
		images: string[];
		category: number;
		longitude: number;
		latitude: number;
	}

	async function sendCases(): Promise<boolean> {
		if (!network.status.value?.connected) return false;
		if (!queueKeys.hasKey()) return false;
		if (main.caseQueue.length <= 0) return false;

		const queueKey = await queueKeys.consumeKey();
		try {
			const deviceId = await Device.getId();
			const cases: CreateCaseRequestDto[] = main.caseQueue.map((c) => {
				return {
					deviceId: deviceId.uuid,
					description: c.description,
					comment: c.comment,
					subCategories: c.subCategories.map((s) => s.id),
					images: c.images,
					category: c.category.id,
					longitude: c.geographicLocation.longitude,
					latitude: c.geographicLocation.latitude,
				} as CreateCaseRequestDto;
			});
			await axios.post("cases", cases, {
				headers: {
					["X-QueueKey"]: queueKey.id,
				},
			});

			await caseHistory.syncWithAPI();
			emptyCaseQueue();
			return true;
		} catch {
			return false;
		}
	}

	watch(network.status, sendCases);

	return { addCurrentCaseToQueue, sendCases, removeCaseFromQueue };
}

export const afterAppMount: BeforeAppMount = () => {
	const cases = useCases();

	cases.sendCases();

	// Don't await due to result not being critical to our app at startup
	return Promise.resolve();
};
