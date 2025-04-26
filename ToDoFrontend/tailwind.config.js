/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        primary: '#1DA1F2',
        secondary: '#14171A',
        accent: '#657786',
        neutral: '#AAB8C2',
        info: '#3ABFF8',
        success: '#36D399',
        warning: '#FBBD23',
        error: '#F87272'
      }
    },
  },
  plugins: [],
}
