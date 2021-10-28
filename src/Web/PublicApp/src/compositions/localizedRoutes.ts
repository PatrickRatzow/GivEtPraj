import { useLocale } from "@/compositions/locale";

const locale = useLocale();

export function useLocalizedRoutes() {
	const language = computed(() => locale.getLanguageCode);
	const routes = {
		location: {
			da: "/opret-praj/lokation",
			en: "/create-praj/location",
		},
		category: {
			da: "/opret-praj/kategori",
			en: "/create-praj/category",
		},
		pictures: {
			da: "/opret-praj/billeder",
			en: "/create-praj/pictures",
		},
		picturePreview: {
			da: "/opret-praj/billeder/:id",
			en: "/create-praj/pictures/:id",
		},
		checkout: {
			da: "/opret-praj/afslut",
			en: "/create-praj/checkout",
		},
		cases: {
			da: "/mine-prajs",
			en: "/my-prajs",
		},
	};

	async function getLocationUrl() {
		return routes.location[await language.value()];
	}

	async function getCategoryUrl() {
		return routes.category[await language.value()];
	}

	async function getPicturesUrl() {
		return routes.pictures[await language.value()];
	}

	async function getPicturePreviewUrl(id: number) {
		const route = routes.picturePreview[await language.value()];

		return route.replace(":id", id.toString());
	}

	async function getCheckoutUrl() {
		return routes.checkout[await language.value()];
	}

	async function getCasesUrl() {
		return routes.cases[await language.value()];
	}

	return { getLocationUrl, getCategoryUrl, getPicturesUrl, getPicturePreviewUrl, getCheckoutUrl, getCasesUrl };
}
