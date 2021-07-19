const path = require('path');
const { DefinePlugin } = require('webpack');

const config = {
    entry: './src/index.js',
    output: {
        path: path.join(__dirname, "dist"),
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
    },
    plugins: []
}

module.exports = function (env) {
    let args = JSON.parse(process.env.DEPLOY_ARGS || '{}');

    let service = args.api_host || 'https://api.mfro.me/avalon';

    console.log('weback build args:');
    console.log('  SERVICE_URL:', service);

    config.plugins.push(new DefinePlugin({
        'SERVICE_URL': JSON.stringify(service),
    }));

    return config;
}
