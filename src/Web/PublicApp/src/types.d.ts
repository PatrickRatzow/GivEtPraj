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
		category: string;
		subCategories: string[];
		images: string[];
		comment: string | null;
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
