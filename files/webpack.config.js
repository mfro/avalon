const path = require('path');

module.exports = {
    entry: './src/index.js',
    output: {
        path: path.join(__dirname, "build"),
        filename: "bundle.js"
    },
    resolve: {
        modules: [
            "node_modules",
            path.join(__dirname, "src"),
        ],
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                loader: 'babel-loader',
                options: { presets: ['es2015'] },
            },
            {
                test: /\.html$/,
                loader: 'my-vue-loader'
            },
            {
                test: /\.css$/,
                loader: 'style-loader!css-loader'
            },
            {
                test: /\.png$/,
                loader: 'file-loader',
                options: { name: 'images/[hash].[ext]' },
            }
        ]
    }
}
