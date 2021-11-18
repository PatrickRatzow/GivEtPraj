import { test, expect } from "@playwright/test";
import { skipTutorial, mapId } from "../util";

test.describe("map", () => {
	test.beforeEach(async ({ page }) => {
		// Go to the starting url before each test.
		await page.goto("http://localhost:3000/create-praj/location");

		await skipTutorial(page);
	});

	test("map container exists", async ({ page }) => {
		const map = await page.textContent(mapId);
		expect(map).toBeTruthy();
	});

	test("can click on point in denmark", async ({ page }) => {
		expect(await page.isEnabled("button")).toBeFalsy();

		await page.click(mapId);

		// Wait a bit due to mixing callbacks & promises in code (ref: JS eventloop)
		await page.waitForTimeout(250);

		expect(await page.isEnabled("button")).toBeTruthy();
	});

	test("can't click on grey area on map", async ({ page }) => {
		expect(await page.isEnabled("button")).toBeFalsy();

		await page.click(mapId, {
			position: {
				x: 100,
				y: 100,
			},
		});

		// Wait a bit due to mixing callbacks & promises in code (ref: JS eventloop)
		await page.waitForTimeout(250);

		expect(await page.isEnabled("button")).toBeFalsy();
	});
});
