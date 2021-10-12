module.exports = {
  mode: 'jit',
  purge: {
    enabled: true,
    content: [
        './**/*.html',
        './**/*.razor'
    ]
  },
  darkMode: false, // or 'media' or 'class'
  theme: {
      extend: {
          colors: {
              orange: "#FFCA98"
          }
      },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
