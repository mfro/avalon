const loaderUtils = require('loader-utils');

module.exports = class {
    constructor() {
        this.tagName = 'style';
        this.files = [];
    }

    consume(node) {
        this.files.push(node.attrsMap.src);
    }

    finish(context) {
        if (this.files.length == 0) {
            return "var style = undefined;";
        } else if (this.files.length == 1) {
            return `var style = require(${loaderUtils.stringifyRequest(context, "!style-loader!css-loader?modules!less-loader?strictMath!" + this.files[0])})`;
        } else {
            throw new Error('uh oh');
        }
    }
}
