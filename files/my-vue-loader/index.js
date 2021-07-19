const compiler = require('vue-template-compiler');
const transpile = require('vue-template-es2015-compiler');
const loaderUtils = require('loader-utils');
const path = require('path');

const extensions = require('./extensions');

module.exports = function (content) {
    let compiled = compiler.compile(content);
    let exts = extensions.map(e => new e());

    while (true) {
        let ext = exts.find(e => e.tagName == compiled.ast.tag);

        if (ext == null) break;

        ext.consume(compiled.ast);

        content = content.substring(content.indexOf('\n') + 1);
        compiled = compiler.compile(content);
    }

    let func = transpile(`var __render__func = function(_c) {${compiled.render}}`);

    let output = `// vue-template-loader
${exts.map(e => e.finish(this) + ";\n").join('')}
var wrap = require(${loaderUtils.stringifyRequest(this, path.join(__dirname, 'wrap.js'))});
${func};
var staticRenderFns = [${compiled.staticRenderFns.map(a => `function(){${a}}`).join(',')}];

module.exports = function(options) {
    options.render = wrap(style, __render__func);
    options.staticRenderFns = staticRenderFns;

    options.components = options.components || {};
    for (var key in components) options.components[key] = components[key];
    
    return options;
}`;

    return output;
}
