import axios from "@/utils/axios";
import { Device } from "@capacitor/device";
import { useReCaptcha } from "vue-recaptcha-v3";
import { useNetwork } from "@/compositions/network";
import { Storage } from "@capacitor/storage";

interface CreateQueueKeyRequest {
	deviceId: string;
}

export function useQueueKeys() {
	const network = useNetwork();
	// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
	const { executeRecaptcha, recaptchaLoaded } = useReCaptcha()!;
	const key = ref<QueueKey | undefined>();

	function hasKey() {
		return key.value !== undefined;
	}

	async function consumeKey(): Promise<QueueKey> {
		if (!key.value) throw new Error("Unable to consume key as there is none");

		await Storage.remove({ key: "queueKey" });

		return key.value;
	}

	async function createKey(): Promise<QueueKey | undefined> {
		if (!network.status.value?.connected) return;

		const { value } = await Storage.get({ key: "queueKey" });
		if (value !== null) return JSON.parse(value) as QueueKey;

		await recaptchaLoaded();

		const reCaptchaToken = await executeRecaptcha("queue_key");

		const id = await Device.getId();
		const data: CreateQueueKeyRequest = { deviceId: id.uuid };
		const resp = await axios.post("queue-keys", data, {
			headers: {
				["X-ReCAPTCHA-V3"]: reCaptchaToken,
			},
		});

		if (resp.status != 200) return undefined;

		const queueKey = resp.data as QueueKey;

		await Storage.set({
			key: "queueKey",
			value: JSON.stringify(queueKey),
		});

		return queueKey;
	}

	watch(network.status, createKey);

	return { hasKey, consumeKey, createKey };
}
