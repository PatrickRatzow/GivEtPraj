import { Page } from "@playwright/test";

export async function skipTutorial(page: Page) {
	const skipButton = page.locator("text=skip tutorial");
	await skipButton.click();
}

export const mapId = "#mapid";
