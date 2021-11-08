import { useLocale } from "@/compositions/locale";
import axios from "axios";

const conn = axios.create({
	baseURL: "https://localhost:5001/v1/",
});

const locale = useLocale();

conn.interceptors.request.use(async (config) => {
	const languageCode = await locale.getLanguageCode();

	config.headers ??= {};
	config.headers["X-Language"] = languageCode;

	return config;
});

export default conn;
