import axios from "@/utils/axios";

interface AddressLookup {
	zipCodeName: string;
}

interface LookupResponse {
	postnr: number;
	postnrnavn: string;
}

interface ErrorMessage {
	type: string;
}

export function useLocationLookup() {
	async function fetchAddress(location: GeographicLocation): Promise<AddressLookup | null> {
		const response = await axios.get<LookupResponse | ErrorMessage>(
			`https://api.dataforsyningen.dk/adgangsadresser/reverse?x=${location.longitude}&y=${location.latitude}&struktur=mini`,
			{
				baseURL: undefined,
			}
		);
		if ((response.data as ErrorMessage).type) return null;

		const data: LookupResponse = response.data as LookupResponse;

		return {
			zipCodeName: data.postnrnavn,
		};
	}

	return { fetchAddress };
}
