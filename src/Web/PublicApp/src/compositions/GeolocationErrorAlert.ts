import { alertController } from "@ionic/vue";
enum GeoLocationError {
	AccessDenied = 1,
	Unavaliable = 2,
	Timeout = 3,
}

export async function presentAlert(errorCode: number) {
	const alert = await alertController.create({});

	if (errorCode == GeoLocationError.AccessDenied) {
		alert.header = "Access denied";
		alert.message = "You have to enable geolocation for the application to get your position";
		alert.buttons = ["OK"];
	} else if (errorCode == GeoLocationError.Unavaliable) {
		alert.header = "Geolocation unavaliable";
		alert.message = "Geolocaiton is not accessible on this device when offline";
		alert.buttons = ["OK"];
	} else if (errorCode == GeoLocationError.Timeout) {
		alert.header = "Timeout";
		alert.message = "Request has times out";
		alert.buttons = ["OK"];
	}

	await alert.present();
}
