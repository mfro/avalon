function map(name, style) {
    let hash = style[name];
    if (!hash) return name;

    return hash;
}

function apply(data, key, style) {
    let value = data[key];
    if (!value) return;

    if (typeof value == 'string') {
        data[key] = value.split(/\s/)
            .filter(a => a)
            .map(name => map(name, style))
            .join(' ');

        return;
    }

    if (value instanceof Array) {
        for (let i = 0; i < value.length; i++) {
            apply(value, i, style);
        }

        return;
    }

    let mapped = {};
    for (let name in value) {
        mapped[map(name, style)] = value[name];
    }
    data[key] = mapped;
}

function wrapCreateElement(original, style) {
    if (!style) return original;

    return function (tagName, data) {
        if (data) {
            apply(data, 'class', style);
            apply(data, 'staticClass', style);
        }

        return original.apply(this, arguments);
    };
}

module.exports = function (style, renderFunction) {
    return function (createElement) {
        createElement = wrapCreateElement(createElement, style);

        this._c = createElement;

        return renderFunction.call(this, createElement);
    }
};
