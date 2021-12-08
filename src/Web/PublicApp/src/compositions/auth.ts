import axios from "@/utils/axios";
import { useNetwork } from "@/compositions/network";
import { Storage } from "@capacitor/storage";
import { useReCaptcha } from "vue-recaptcha-v3";
import { AxiosError } from "axios";

const authed = ref(false);
export function useAuth() {
	const captcha = useReCaptcha();
	const network = useNetwork();
	let executeRecaptcha: ((action: string) => Promise<string>) | undefined = undefined;
	let recaptchaLoaded: (() => Promise<boolean>) | undefined = undefined;

	function loadReCaptcha() {
		if (!executeRecaptcha || !recaptchaLoaded) {
			executeRecaptcha = captcha?.executeRecaptcha;
			recaptchaLoaded = captcha?.recaptchaLoaded;

			if (!executeRecaptcha || !recaptchaLoaded) return false;
		}

		return true;
	}

	async function removeAuthorization(): Promise<void> {
		if (!authed.value) throw new Error("Unable to consume key as there is none");

		authed.value = false;
		await Storage.remove({ key: "preAuthorized" });
	}

	// TODO: Clean up, this is a mess
	async function authorize(): Promise<boolean> {
		if (!network.status.value?.connected) return false;

		await loadCache();
		if (authed.value) return true;
		if (!loadReCaptcha()) return false;

		await recaptchaLoaded?.();

		const reCaptchaToken = await executeRecaptcha?.("pre_authorization");
		try {
			const resp = await axios.post(
				"auth",
				{},
				{
					headers: {
						["X-ReCAPTCHA-V3"]: reCaptchaToken as string,
					},
				}
			);

			if (resp.status != 204) return false;

			await Storage.set({
				key: "preAuthorized",
				value: "true",
			});

			authed.value = true;

			return true;
		} catch (e: unknown) {
			if (axios.isAxiosError(e)) {
				const err = e as AxiosError;

				if (err.response?.status != 409) throw e;

				await Storage.set({
					key: "preAuthorized",
					value: "true",
				});

				authed.value = true;

				return true;
			}

			throw e;
		}
	}

	async function loadCache() {
		const { value } = await Storage.get({ key: "preAuthorized" });
		if (value !== null) {
			authed.value = true;
		}
	}

	watch(network.status, authorize);

	return { authorize, loadCache, removeAuthorization, isAuthorizated: readonly(authed) };
}
