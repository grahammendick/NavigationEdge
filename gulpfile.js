var browserify = require('browserify');
var gulp = require('gulp');
var react = require('gulp-react');
var source = require('vinyl-source-stream');

gulp.task('transform', function(){
	return gulp.src('./NavigationEdgeApi/node/Component.jsx')
		.pipe(react())
		.pipe(gulp.dest('./NavigationEdgeApi/node'))
});

gulp.task('build', ['transform'], function(){
	return browserify('./NavigationEdgeApi/node/StateInfo.js', { standalone: 'StateInfo'})
		.bundle()
		.pipe(source('app.js'))
		.pipe(gulp.dest('./NavigationEdgeApi'))
});