import axios from "@/utils/axios";
import { useNetwork } from "@/compositions/network";
import { Storage } from "@capacitor/storage";
import { useReCaptcha } from "vue-recaptcha-v3";

const authed = ref(false);
export function useAuth() {
	const network = useNetwork();
	let executeRecaptcha: ((action: string) => Promise<string>) | undefined = undefined;
	let recaptchaLoaded: (() => Promise<boolean>) | undefined = undefined;

	function loadReCaptcha() {
		if (!executeRecaptcha || !recaptchaLoaded) {
			const captcha = useReCaptcha();
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

	async function authorize(): Promise<boolean> {
		if (!network.status.value?.connected) return false;

		const { value } = await Storage.get({ key: "preAuthorized" });
		if (value !== null) return true;
		if (!loadReCaptcha()) return false;

		await recaptchaLoaded?.();

		const reCaptchaToken = await executeRecaptcha?.("pre_authorization");
		const resp = await axios.post("auth", undefined, {
			headers: {
				["X-ReCAPTCHA-V3"]: reCaptchaToken as string,
			},
		});

		if (resp.status != 200) return false;

		await Storage.set({
			key: "preAuthorized",
			value: Boolean(true).toString(),
		});

		return true;
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
