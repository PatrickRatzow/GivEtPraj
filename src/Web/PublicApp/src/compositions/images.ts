import { Camera, CameraResultType, Photo } from "@capacitor/camera";
import { useCreateCaseStore } from "@/stores/create-case";

export function useImages() {
	const createCase = useCreateCaseStore();

	function base64ToDataUrl(base64: string) {
		return `data:image/png;base64,${base64}`;
	}

	function getImageAsDataUrl(index: number): string | undefined {
		const image = createCase.images[index];
		if (image?.base64String === undefined) return;

		return base64ToDataUrl(image.base64String);
	}

	function addPicture(index: number, photo: Photo) {
		const images = [...createCase.images];
		images[index] = photo;
		createCase.images = images;
	}

	async function takePicture(index: number) {
		const image = await Camera.getPhoto({
			quality: 90,
			allowEditing: true,
			resultType: CameraResultType.Base64,
		});

		addPicture(index, image);
	}

	function removePicture(index: number) {
		const images = [...createCase.images];
		delete images[index];
		createCase.images = images;
	}

	return { base64ToDataUrl, getImageAsDataUrl, takePicture, removePicture, addPicture };
}
