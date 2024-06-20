const path = require('path');

module.exports = {
    content: ["./src/**/*.{html,js,jsx}"],
    entry: './src/index.css',
    output: {
        path: path.resolve(__dirname, 'public'),
        filename: 'bundle.css'
    },
    theme: {
        extend: {},
    },
    plugins: [],
}