const path = require('path');
const withImages = require('next-images')
module.exports = withImages({
    exclude: path.resolve(__dirname, './svg'),
    webpack(config, options) {
        return config
    }
})


module.exports = {
    webpack(config) {
        config.module.rules.push({
            test: /\.svg$/,
            use: ["@svgr/webpack"]
        });

        return config;
    },
    images: {
        disableStaticImages: false
    }
}
