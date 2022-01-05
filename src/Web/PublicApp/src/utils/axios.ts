import { Storage } from "@capacitor/storage";
import axios, { AxiosRequestConfig } from "axios";
import { useNetwork } from "@/compositions/network";
import { Device } from "@capacitor/device";
import { useMainStore } from "@/stores/main";

const network = useNetwork();
const baseURL = "https://localhost:5001/v1/";

interface HttpResponse<T> {
	data: T;
	status: number;
	fromCache: boolean;
}

class HttpClient {
	private conn = axios.create({ baseURL });

	constructor() {
		this.conn.interceptors.request.use(async (config) => {
			const main = useMainStore();

			const languageCode = main.language;
			const deviceId = await Device.getId();

			config.headers ??= {};
			config.headers["X-Language"] = languageCode;
			config.headers["X-DeviceId"] = deviceId.uuid;

			return config;
		});
	}

	public async get<T = unknown, D = unknown>(url: string, config?: AxiosRequestConfig<D>): Promise<HttpResponse<T>> {
		if (!network.status.value?.connected) {
			const cache = await this.getResponseFromCache<T, D>(url, config);
			if (cache != null) {
				return cache;
			}
		}

		const response = await this.conn.get<T>(url, config);
		const httpResponse: HttpResponse<T> = {
			data: response.data,
			status: response.status,
			fromCache: false,
		};

		await this.saveResponseToCache(url, httpResponse, config);

		return httpResponse;
	}

	public async post<T = unknown>(
		url: string,
		data?: unknown,
		config?: AxiosRequestConfig<T>
	): Promise<HttpResponse<T>> {
		const response = await this.conn.post<T>(url, data, config);
		const httpResponse: HttpResponse<T> = {
			data: response.data,
			status: response.status,
			fromCache: false,
		};

		return httpResponse;
	}

	public isAxiosError(err: unknown): boolean {
		return axios.isAxiosError(err);
	}

	private async saveResponseToCache<T, D>(url: string, httpResponse: HttpResponse<T>, config?: AxiosRequestConfig<D>) {
		const requestUrl = this.getRequestUrl(config?.baseURL ?? baseURL, url);

		httpResponse.fromCache = true;

		await Storage.set({
			key: `cache_${requestUrl}`,
			value: JSON.stringify(httpResponse),
		});
	}

	private async getResponseFromCache<T, D>(url: string, config?: AxiosRequestConfig<D>) {
		const requestUrl = this.getRequestUrl(config?.baseURL ?? baseURL, url);
		const { value } = await Storage.get({ key: `cache_${requestUrl}` });
		if (value == null) return;

		return JSON.parse(value) as HttpResponse<T>;
	}

	private getRequestUrl(baseUrl?: string, url?: string): string {
		baseUrl ??= "";
		url ??= "";

		if (url.startsWith("http")) return url;

		return baseUrl + url;
	}
}

export default new HttpClient();
