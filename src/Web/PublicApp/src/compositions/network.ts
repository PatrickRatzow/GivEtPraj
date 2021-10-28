import { Network } from "@capacitor/network";

export function useNetwork() {
	function getStatus() {
		return Network.getStatus();
	}

	return { getStatus };
}
