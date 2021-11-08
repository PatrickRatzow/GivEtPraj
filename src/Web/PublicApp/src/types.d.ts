import { Photo } from "@capacitor/camera";
import { App } from "vue";
import { Router } from "vue-router";

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
		comment: string | undefined;
		status: Status;
		geographicLocation: GeographicLocation;
		createdAt: Date;
		updatedAt?: Date;
		deviceId: string;
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

	interface ModuleOptions {
		app: App<unknown>;
		router: Router;
	}

	interface QueueKey {
		id: string;
		createdAt: Date;
		expiresAt: Date;
	}

	export type AppModule = (options: ModuleOptions) => Promise<void>;
	export type Language = "en" | "da";
}
