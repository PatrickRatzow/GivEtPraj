import { alertController } from "@ionic/vue";

// TODO: i18n
export async function presentAlert(error: GeolocationPositionError) {
	const alert = await alertController.create({});

	if (error.code == GeolocationPositionError.PERMISSION_DENIED) {
		alert.header = "Access denied";
		alert.message = "You have to enable geolocation for the application to get your position";
		alert.buttons = ["OK"];
	} else if (error.code == GeolocationPositionError.POSITION_UNAVAILABLE) {
		alert.header = "Geolocation unavaliable";
		alert.message = "Geolocation is not accessible on this device when offline";
		alert.buttons = ["OK"];
	} else if (error.code == GeolocationPositionError.TIMEOUT) {
		alert.header = "Timeout";
		alert.message = "Request has times out";
		alert.buttons = ["OK"];
	}

	await alert.present();
}
