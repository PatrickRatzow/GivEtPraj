import { useLocale } from "@/compositions/locale";
import { Storage } from "@capacitor/storage";
import axios, { AxiosRequestConfig } from "axios";
import { useNetwork } from "@/compositions/network";

const network = useNetwork();
const locale = useLocale();
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
			const languageCode = locale.language.value ?? "en";

			config.headers ??= {};
			config.headers["X-Language"] = languageCode;

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

	public async post<T = unknown>(url: string, config?: AxiosRequestConfig<T>): Promise<HttpResponse<T>> {
		const response = await axios.post<T>(url, config);
		const httpResponse: HttpResponse<T> = {
			data: response.data,
			status: response.status,
			fromCache: false,
		};

		return httpResponse;
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
