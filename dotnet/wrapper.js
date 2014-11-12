var fs = require('fs'),
    _ = require('underscore'),
    Handlebars = require('handlebars'),
    htmlToText = require('html-to-text'),
    pluralize = require('pluralize'),
    stripBom = require('strip-bom');

// Extensions

String.prototype.flatten = function() {
  return this.trim().split(/\r?\n/).map(function(x) { return x.trim(); }).join('');
};

String.prototype.initialCap = function () {
    return this.charAt(0).toUpperCase() + this.slice(1).toLowerCase();
}; 

String.prototype.initialLower = function () {
    return this.charAt(0).toLowerCase() + this.slice(1);
};

Array.prototype.pushMany = function (items) {
    this.push.apply(this, items);
};

Array.prototype.isEmpty = function () {
    return typeof this === "undefined" || 
       this === null || 
       this.length === null || 
       this.length === 0;
};

Array.prototype.any = function (predicate) {
    if (this.isEmpty()) return false;
    if (!predicate) return true;
    for (index = 0; index < this.length; ++index) {
        if (predicate(this[index])) return true;
    }
    return false;
};

Array.prototype.pathToTree = function (path) {
    var tree = {};
    this.forEach(function (item) {
        var parent;
        var node = tree;
        path(item).forEach(function (part, index, parts) {
            node.nodes = node.nodes || {};
            node = node.nodes[part] = node.nodes[part] || {
                name: part,
                path: parts.slice(0, index + 1),
                parent: parent
            };
            parent = node;
            if (index + 1 === parts.length) node.leaf = item;
        });
    });
    return tree;
};

Object.prototype.visitTree = function (children, visit, skip) {
    if (!skip) visit(this);
    var nodes = children(this);
    if (nodes) nodes.forEach(function (node) {
        node.visitTree(children, visit);
    });
};

// Generic helpers

Handlebars.registerHelper('htmlToText', function(context) {
    return context ? htmlToText.fromString(context.replace(/\r\n|\n|\r/g, "<br/>")) : '';
});

Handlebars.registerHelper('initialCap', function (context) {
    return _.isArray(context) ? context.map(function (x) { return x.initialCap(); }) : context.initialCap();
});

Handlebars.registerHelper('initialLower', function (context) {
    return context.initialLower();
});

Handlebars.registerHelper('join', function (context, delimiter) {
    return context.join(delimiter);
});

Handlebars.registerHelper('joinBlock', function (context, start, seed, delimiter, options) {
    if (!context) return '';
    var result = [];
    context.forEach(function (x) {
        result.push(options.fn(x));
    });
    result = result.splice(start);
    return (result.length > 0 ? seed : '') + result.join(delimiter);
});

Handlebars.registerHelper('split', function (context, options) {
    var result = "";

    context.split(/\r\n|\n|\r/).forEach(function(x) {
        result = result + options.fn(x);
    });

    return result;
});

Handlebars.registerHelper('env', function(name) {
    return process.env[name];
});

Handlebars.registerHelper('pathAsTree', function (items, field, delimiter, start, options) {
    var result = "";
    items.pathToTree(function (x) { return x[field].split(delimiter).splice(start); })
         .visitTree(function (x) { return _.values(x.nodes); },
                    function (x) { result = result + options.fn(x); }, true);
    return result;
});

Handlebars.registerHelper('visitTree', function () {
    var args = Array.prototype.slice.call(arguments);
    var root = args[0];
    var children = args.slice(1, args.length - 1).map(function(x) { return x.split("."); });
    var options = args[args.length - 1];
    var result = "";
    root.visitTree(
        function (node) {
            return Array.prototype.concat.apply([], children
                .map(function(child) {
                    var field = node[child[0]];
                    if (!field) return;
                    return _.isArray(field) ? field.map(function(x) { return x[child[1]] }) : field[child[1]];
                })
                .filter(function(x) { return x; }));
        },
        function (x) { result = result + options.fn(x); });
    return result;
});

Handlebars.registerHelper('children', function (options) {
    var result = "";
    if (this.nodes) _.values(this.nodes).forEach(function (node) {
        result = result + options.fn(node);
    });
    return result;
});

Handlebars.registerHelper('removeWhitespace', function (options) {
    return options.fn(this).replace(/\s/g, '');
});

Handlebars.registerHelper('any', function (context, options) {
    return context && context.any() ? options.fn(this) : options.inverse();
});

Handlebars.registerHelper('pluralize', function (word) {
    return pluralize(word);
});

// Swank helpers

function getEndpointName(endpoint, excluding) {
    var params = endpoint.UrlParameters ? endpoint.UrlParameters
        .filter(function (x) { return !_.contains(excluding, x.Name); }) : [];
    return endpoint.Method.initialCap() + (params.isEmpty() ? '' : "By" +
        params.map(function (x) { return x.Name; }).join("And"));
}

function supportsStream(data) {
    return _.contains(data.MimeTypes, "*/*") ||
           _.contains(data.MimeTypes, "application/octet-stream");
}

Handlebars.registerHelper('endpointName', function () {
    var args = Array.prototype.slice.call(arguments);
    return getEndpointName(this, args.slice(0, args.length - 1));
});

Handlebars.registerHelper('hasRequest', function (options) {
    if (supportsStream(this.Request)) return options.fn(_.extend(this, { stream: true }));
    if (this.Request.Body.Type) return options.fn(_.extend(this, { stream: false }));
    return options.inverse();
});

Handlebars.registerHelper('hasResponse', function (options) {
    if (supportsStream(this.Response)) return options.fn(_.extend({ stream: true }, this));
    if (this.Response.Body.Type) return options.fn(_.extend({ stream: false }, this));
    return options.inverse();
});

Handlebars.registerHelper('parameters', function () {
    var args = Array.prototype.slice.call(arguments);
    var endpoint = this;
    var parameters = []; 
    var delimiter = args[0];
    var excluding = args.slice(1, args.length - 1);
    var options = args[args.length - 1];
    if (endpoint.UrlParameters)
        parameters.pushMany(endpoint.UrlParameters
            .map(function (x) { return { name: x.Name.initialLower(), type: x.Type }; }));
    if (endpoint.Request.Body.Type || supportsStream(endpoint.Request))
        parameters.push({ name: "request", stream: supportsStream(endpoint.Request) });
    if (this.QuerystringParameters)
        parameters.pushMany(endpoint.QuerystringParameters.map(function (x) {
            return { name: x.Name.initialLower(), type: x.Type, optional: true };
        }));
    return parameters
        .filter(function (x) { return !_.contains(excluding, x.name); })
        .map(function (x) { return options.fn(_.extend(x, endpoint)); })
        .join(delimiter);
});

// .NET Specific

function getSimpleClrType(type, optional, isEnum) {
    var nullable = optional ? "?" : "";
    if (isEnum) return type + nullable;
    switch (type) {
        case "string": return "String";
        case "boolean": return "Boolean" + nullable;
        case "decimal": return "Decimal" + nullable;
        case "double": return "Double" + nullable;
        case "float": return "Single" + nullable;
        case "unsignedByte": return "Byte" + nullable;
        case "byte": return "SByte" + nullable;
        case "short": return "Int16" + nullable;
        case "unsignedShort": return "UInt16" + nullable;
        case "int": return "Int32" + nullable;
        case "unsignedInt": return "UInt32" + nullable;
        case "long": return "Int64" + nullable;
        case "unsignedLong": return "UInt64" + nullable;
        case "dateTime": return "DateTime" + nullable;
        case "duration": return "TimeSpan" + nullable;
        case "uuid": return "Guid" + nullable;
        case "char": return "Char" + nullable;
        default: return type;
    }
}

Handlebars.registerHelper('simpleClrType', function (name, optional, isEnum) {
    return getSimpleClrType(name, optional, isEnum);
});

// Module

module.exports = function(jsonPath, templatePath, outputPath) {
    var json = JSON.parse(stripBom(fs.readFileSync(jsonPath, { encoding: 'UTF8' })));
    var template = Handlebars.compile(templatePath, { encoding: 'UTF8' });
    fs.writeFileSync(outputPath, tempate(json));
}
