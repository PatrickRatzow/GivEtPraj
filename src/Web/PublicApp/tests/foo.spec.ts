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

	test("click map", async ({ page }) => {
		await page.pause();
		const divMap = "#mapid";
		const button = page.locator("text=skip tutorial");

		await button.click();
		const map = await page.textContent(divMap);

		expect(map).toBeTruthy();
		await page.click(divMap);

		expect(await page.isEnabled("button")).toBeTruthy();
		await page.pause();
	});

	test("clicked grey area on map", async ({ page }) => {
		await page.pause();
		const divMap = "#mapid";
		const button = page.locator("text=skip tutorial");

		await button.click();
		const map = await page.textContent(divMap);

		expect(map).toBeTruthy();
		//await page.click(divMap) -- Try to click in the grey area;
		await page.click(divMap, {
			position: {
				x: 0,
				y: 0,
			},
		});

		expect(await page.isEnabled("button")).toBeFalsy();
		await page.pause();
	});
});
