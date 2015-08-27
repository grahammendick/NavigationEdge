var browserify = require('browserify');
var gulp = require('gulp');
var source = require('vinyl-source-stream');

gulp.task('build', function(){
	return browserify('./node/StateInfo.js', { standalone: 'StateInfo'})
		.bundle()
		.pipe(source('StateInfo.js'))
		.pipe(gulp.dest('./NavigationEdge'))
});