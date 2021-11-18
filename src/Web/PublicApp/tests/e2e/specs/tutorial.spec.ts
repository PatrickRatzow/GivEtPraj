import { test, expect } from "@playwright/test";
import { skipTutorial } from "../util";

test.describe("tutorial", () => {
	test.beforeEach(async ({ page }) => {
		// Go to the starting url before each test.
		await page.goto("http://localhost:3000/create-praj/location");
	});

	test("can skip tutorial", async ({ page }) => {
		const button = page.locator("text=skip tutorial");
		expect(await button.count()).toBeTruthy();

		await skipTutorial(page);

		expect(await button.count()).toBeFalsy();
	});
});
