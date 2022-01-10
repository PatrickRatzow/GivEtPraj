import { useNetwork } from "@/compositions/network";
import { useMainStore } from "@/stores/main";
import { useCreateCaseStore } from "@/stores/create-case";
import axios from "@/utils/axios";
import { useAuth } from "@/compositions/auth";
import { useCaseHistory } from "@/compositions/case-history";
import { Storage } from "@capacitor/storage";

export function useCases() {
	const network = useNetwork();
	const main = useMainStore();
	const createCase = useCreateCaseStore();
	const { isAuthorizated, removeAuthorization } = useAuth();
	const caseHistory = useCaseHistory();

	function addCurrentCaseToQueue() {
		if (!createCase.category) return;
		if (!createCase.geographicLocation) return;

		const newCase: BaseCase = {
			category: createCase.category,
			subCategories: createCase.subCategories,
			images: createCase.images.filter((photo) => photo).map((photo) => photo.base64String as string),
			comment: createCase.comment,
			description: createCase.description,
			status: { name: "not done", color: "#000000" },
			geographicLocation: createCase.geographicLocation,
		};

		main.caseQueue = [...main.caseQueue, newCase];

		saveCases();
	}

	function removeCaseFromQueue(index: number) {
		main.caseQueue = main.caseQueue.filter((c, idx) => idx != index);

		saveCases();
	}

	function emptyCaseQueue() {
		main.caseQueue = [];

		saveCases();
	}

	async function saveCases() {
		await Storage.set({
			key: "case_queue",
			value: JSON.stringify(main.caseQueue),
		});
	}

	async function loadCases() {
		const { value } = await Storage.get({ key: "case_queue" });
		if (value == null) return;

		main.caseQueue = JSON.parse(value);
	}

	interface CreateCaseRequestDto {
		cases: {
			description?: string;
			comment?: string;
			subCategoryIds: number[];
			images: string[];
			categoryId: number;
			longitude: number;
			latitude: number;
		}[];
	}

	async function sendCases(): Promise<boolean> {
		if (!network.status.value?.connected) return false;
		if (!isAuthorizated.value) return false;
		if (main.caseQueue.length <= 0) return false;

		try {
			const cases: CreateCaseRequestDto = {
				cases: main.caseQueue.map((c) => {
					return {
						description: c.description,
						comment: c.comment,
						subCategoryIds: c.subCategories.map((s) => s.id),
						images: c.images,
						categoryId: c.category.id,
						longitude: c.geographicLocation.longitude,
						latitude: c.geographicLocation.latitude,
					};
				}),
			};

			try {
				await axios.post("cases", cases);
				await caseHistory.syncWithAPI();
			} finally {
				await removeAuthorization();
				emptyCaseQueue();
			}

			return true;
		} catch {
			return false;
		}
	}

	watch(network.status, sendCases);

	return { addCurrentCaseToQueue, sendCases, removeCaseFromQueue, loadCases };
}

export const afterAppMount: BeforeAppMount = () => {
	const cases = useCases();

	// Don't await due to result not being critical to our app at startup
	new Promise<void>((resolve) => {
		cases
			.loadCases()
			.then(cases.sendCases)
			.then(() => resolve())
			.catch(() => resolve());
	});

	return Promise.resolve();
};
