var gutil = require('../../../node_modules/gulp_nuget/node_modules/gulp-util');

module.exports = function() {
	if(!arguments) {
		return;
	}

	var args = Array.prototype.slice.call(arguments);
	args.map(function(output) {
		if(output) {
			gutil.log(output);
		}
	});
};