import { Photo } from "@capacitor/camera";

declare global {
	interface SubCategory {
		name: string;
	}

	interface Category {
		name: string;
		icon: string;
		subCategories: SubCategory[];
	}

	interface Case {
		id: number;
		category: Category;
		subCategories: SubCategory[];
		images: string[];
		comment: string | null;
		status: Status;
		geographicLocation: GeographicLocation;
		createdAt: Date;
		updatedAt?: Date;
	}

	interface Status {
		color: string;
		name: string;
	}

	interface CaseCreation {
		category?: Category;
		subCategories: string[];
		images: Photo[];
		comment?: string;
	}

	interface GeographicLocation {
		latitude: number;
		longitude: number;
	}
}
