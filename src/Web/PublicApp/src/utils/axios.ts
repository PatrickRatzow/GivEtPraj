import { useLocale } from "@/compositions/locale";
import axios from "axios";

const conn = axios.create({
	baseURL: "https://localhost:5001/v1/",
});

conn.interceptors.request.use(async (config) => {
	config.headers = {
		"x-language": await useLocale().getLanguageCode().toString(),
	};

	return config;
});

export default conn;
