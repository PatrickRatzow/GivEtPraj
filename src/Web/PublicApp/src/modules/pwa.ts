export const install: AppModule = ({ router }) => {
	return router.isReady().then(async () => {
		console.log("pepeg");
		const { registerSW } = await import("virtual:pwa-register");
		registerSW({ immediate: true });
	});
};
