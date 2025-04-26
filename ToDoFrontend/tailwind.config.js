/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        primary: '#7699D4',
        secondary: '#63B4D1',
        accent: '#90FCF9',
        neutral: '#AAB8C2',
        info: '#3ABFF8',
        success: '#36D399',
        warning: '#FBBD23',
        error: '#F87272',
        background: '#EFF3FA',
        background_accent: '#E0E8F5',
      }
    },
  },
  plugins: [],
}
