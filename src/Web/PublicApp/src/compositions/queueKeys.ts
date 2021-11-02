import axios from "@/utils/axios";
import { Device } from "@capacitor/device";
import { useReCaptcha } from "vue-recaptcha-v3";

interface CreateQueueKeyRequest {
	id: string;
}

export function useQueueKeys() {
	// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
	const { executeRecaptcha, recaptchaLoaded } = useReCaptcha()!;
	const key = ref<QueueKey | undefined>();

	async function createKey(): Promise<QueueKey | undefined> {
		await recaptchaLoaded();

		const reCaptchaToken = await executeRecaptcha("queue_key");

		const id = (await Device.getId()).uuid;
		const data: CreateQueueKeyRequest = { id };
		const resp = await axios.post("queue-keys", data, {
			headers: {
				["X-ReCAPTCHA-V3"]: reCaptchaToken,
			},
		});

		return (resp.status == 200 && (resp.data as QueueKey)) || undefined;
	}

	return { key, createKey };
}
