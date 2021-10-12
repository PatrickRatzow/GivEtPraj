module.exports = {
  mode: 'jit',
  purge: [
      './**/*.{razor,html}'
  ],
  darkMode: false, // or 'media' or 'class'
  theme: {
      extend: {
          colors: {
              orange: "#FFCA98",
              gray: "#7B7B7B"
          }
      },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
