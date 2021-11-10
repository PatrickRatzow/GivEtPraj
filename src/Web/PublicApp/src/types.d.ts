import { Photo } from "@capacitor/camera";
import { App } from "vue";
import { Router } from "vue-router";

declare global {
	interface SubCategory {
		id: number;
		name: string;
	}

	interface Category {
		id: number;
		name: string;
		icon: string;
		miscellaneous: boolean;
		subCategories: SubCategory[];
	}

	interface BaseCase {
		category: Category;
		subCategories: SubCategory[];
		images: string[];
		comment: string | undefined;
		description: string | undefined;
		status: Status;
		geographicLocation: GeographicLocation;
	}
	interface Case extends BaseCase {
		id: number;
		createdAt: Date;
		updatedAt?: Date;
		deviceId: string;
	}

	interface SavedCase extends Case {
		nearestCity?: string;
	}

	interface Status {
		color: string;
		name: string;
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
