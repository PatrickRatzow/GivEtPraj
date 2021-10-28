import { ConnectionStatus, Network } from "@capacitor/network";

export function useNetwork() {
	const status = ref<ConnectionStatus | undefined>();

	Network.addListener("networkStatusChange", (newStatus) => {
		status.value = newStatus;
	});

	Network.getStatus().then((connectionStatus) => (status.value = connectionStatus));

	return { status };
}
