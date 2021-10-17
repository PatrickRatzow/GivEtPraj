// eslint-disable-next-line no-undef
module.exports = {
    mode: "jit",
    purge: {
        // eslint-disable-next-line no-undef
        enabled: process.env.NODE_ENV === "production",
        // classes that are generated dynamically, e.g. `rounded-${size}` and must
        // be kept
        safeList: [],
        content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {
            colors: {
                orange: "#FFCA98",
                gray: {
                    step: "#7B7B7B",
                },
            },
        },
    },
    variants: {
        extend: {},
    },
    plugins: [],
};
