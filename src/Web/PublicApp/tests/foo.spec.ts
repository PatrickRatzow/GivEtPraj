import { test, expect } from "@playwright/test";

test.describe("start up", () => {
	test.beforeEach(async ({ page }) => {
		// Go to the starting url before each test.
		await page.goto("http://localhost:3000/create-praj/location");
	});

	test("skips tutorial", async ({ page }) => {
		const button = page.locator("text=skip tutorial");
		expect(await button.count()).toBeTruthy();
		await button.click();
		expect(await button.count()).toBeFalsy();
	});

	test("click skip", async ({ page }) => {
		await page.click("text=skip tutorial");
	});
});
