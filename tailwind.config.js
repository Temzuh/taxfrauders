/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml'
    ],
    theme: {
        extend: {
            animation: {
                typewriter: "typewriter 2s steps(23) forwards",
                typewriter1: "typewriter 6s steps(61) 2s forwards",
                typewriter2: "typewriter 1500ms steps(16) 8s forwards"
            },
            keyframes: {
                typewriter: {
                    to: {
                        left: "100%"
                    }
                }
            }
        },
    },
    plugins: [require('tailwindcss-animated')],
}
