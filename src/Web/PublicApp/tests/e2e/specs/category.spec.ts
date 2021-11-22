import { test, expect } from "@playwright/test";
import { skipTutorial } from "../util";

test.describe("category", () => {
	test.beforeEach(async ({ page }) => {
		// Go to the starting url before each test.
		await page.goto("http://localhost:3000/create-praj/category");

		await skipTutorial(page);
	});
});
