import axios from "axios";

const instance = axios.create({
	baseURL: "https://localhost:5001/v1/",
});

export default instance;
