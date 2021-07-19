const path = require('path');
const loaderUtils = require('loader-utils');

module.exports = class {
    constructor() {
        this.tagName = 'component';
        this.components = {};
    }

    consume(node) {
        let src = node.attrsMap.src;
        let parsed = path.parse(src);

        let name = node.attrsMap.name || parsed.name;

        this.components[name] = src;
    }

    finish(context) {
        var str = '';
        for (let key in this.components) {
            str += `,"${key}":require(${loaderUtils.stringifyRequest(context, this.components[key])}).default`;
        }
        return 'var components = {' + str.substring(1) + '}';
    }
}
