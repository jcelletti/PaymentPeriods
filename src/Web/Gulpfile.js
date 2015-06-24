/// <binding BeforeBuild='clean' AfterBuild='copy' />
var gulp = require('gulp');
var clean = require('gulp-clean');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var less = require('gulp-less');

var project = require('./project.json');

var libRoot = project.webroot + '/lib/'
var scriptLibRoot = libRoot + 'scripts/'
var contentsLibRoot = libRoot + 'contents/'

var scriptsRoot = project.webroot + '/scripts/';
var contentsRoot = project.webroot + '/contents/';
var angularTemplatesRoot = project.webroot + '/NgTemplates/'


var bowerBase = './bower_components/';

gulp.task('clean_lib', function () {
	return gulp.src(libRoot)
		.pipe(clean());
});

gulp.task('clean_contents', function () {
	return gulp.src(contentsRoot)
		.pipe(clean());
});

gulp.task('clean_scripts', function () {
	return gulp.src(scriptsRoot)
		.pipe(clean());
});

gulp.task('clean', ['clean_lib', 'clean_contents', 'clean_scripts']);

var getJs = function (src, dest) {
	return gulp.src(src)
		.pipe(uglify())
		.pipe(gulp.dest(dest));
};

gulp.task('copy_angular', function () {
	return getJs([
		bowerBase + 'angular/angular.js',
		bowerBase + 'angular-route/angular-route.js'
	], scriptLibRoot);
});

gulp.task('copy_jquery', function () {
	return getJs([
		bowerBase + 'jQuery/dist/jquery.js'
	], scriptLibRoot);
});

gulp.task('copy_bootstrap_js', function () {
	return getJs([
		bowerBase + 'bootstrap/dist/js/bootstrap.js'
	], scriptLibRoot);
});

gulp.task('copy_bootstrap_css', function () {
	return gulp.src([
		bowerBase + 'bootstrap/less/*.less'
	])
		.pipe(concat('bootstrap.css'))
		.pipe(less())
		.pipe(gulp.dest(contentsLibRoot + 'css/'));
});

gulp.task('copy_scripts', function () {
	return gulp.src([
		'Library/Scripts/**/*.js'
	])
		.pipe(concat('SPA.js'))
		.pipe(uglify())
		.pipe(gulp.dest(scriptsRoot));
});

gulp.task('copy_templates', function () {
	return gulp.src([
		'Library/NgTemplates/**/*.html'
	])
	.pipe(gulp.dest(angularTemplatesRoot));
});

gulp.task('copy', ['copy_jquery', 'copy_angular', 'copy_bootstrap_js', 'copy_bootstrap_css', 'copy_scripts', 'copy_templates']);
