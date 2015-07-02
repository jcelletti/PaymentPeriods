/// <binding BeforeBuild='clean' AfterBuild='copy' />
var gulp = require('gulp'),
	clean = require('gulp-clean'),
	concat = require('gulp-concat'),
	uglify = require('gulp-uglify'),
	less = require('gulp-less'),
	cssmin = require('gulp-cssmin'),
	wrapper = require('gulp-wrapper');

var project = require('./project.json');

var wwwroot = function (root) {
	var libRoot = root + '/lib/';
	return {
		base: root,
		scripts: root + 'Scripts/',
		contents: {
			base: root + 'Contents/',
			css: root + 'Contents/css/',
			images: root + 'Contents/images/',
		},
		ngTemplates: root + 'NgTemplates/',
		lib: {
			base: libRoot,
			scripts: libRoot + 'scripts/',
			contents: {
				base: libRoot + 'contents/',
				css: libRoot + 'contents/css/',
				images: libRoot + 'contents/images/',
				fonts: libRoot + 'contents/fonts/'
			}
		}
	}
}('./wwwroot/');

var bower = function (root) {
	var bowerBase = root + 'bower_components/';
	return {
		base: bowerBase,
		angular: {
			base: bowerBase + 'angular/',
			route: {
				base: bowerBase + 'angular-route/'
			}
		},
		bootstrap: {
			base: bowerBase + 'bootstrap/',
			dist: {
				base: bowerBase + 'bootstrap/dist/',
				css: bowerBase + 'bootstrap/dist/css/',
				js: bowerBase + 'bootstrap/dist/js/',
				fonts: bowerBase + 'bootstrap/dist/fonts/'
			},
			less: bowerBase + 'bootstrap/less/',
		},
		fontawesome: {
			base: bowerBase + 'font-awesome/',
			css: bowerBase + 'font-awesome/css/',
			fonts: bowerBase + 'font-awesome/fonts/',
			less: bowerBase + 'font-awesome/less/',
		},
		jquery: {
			base: bowerBase + 'jQuery/',
			dist: bowerBase + 'jQuery/dist/',
		}
	}
}('./');

var custom = function (root) {
	return {
		scripts: root + 'Scripts/',
		ngTemplates: root + 'NgTemplates/',
		contents: {
			base: root + 'Contents/',
			less: root + 'Contents/less/',
			images: root + 'Contents/images/'
		}
	}
}('./');

gulp.task('clean', ['clean_lib', 'clean_contents', 'clean_scripts', 'clean_ng_templates']);

gulp.task('copy', ['copy_bower', 'copy_custom']);

gulp.task('copy_bower', ['copy_bower_scripts', 'copy_bower_css', 'copy_bower_fonts']);

gulp.task('copy_custom', ['copy_scripts', 'copy_less', 'copy_images', 'copy_ng_templates']);

gulp.task('clean_lib',
	function () {
		return gulp.src([
			wwwroot.lib.base
		])
			.pipe(clean());
	});

gulp.task('clean_contents',
	function () {
		return gulp.src([
			wwwroot.contents.base
		])
			.pipe(clean());
	});

gulp.task('clean_scripts',
	function () {
		return gulp.src([
			wwwroot.scripts
		])
			.pipe(clean());
	});

gulp.task('clean_ng_templates',
	function () {
		return gulp.src([
			wwwroot.ngTemplates
		])
			.pipe(clean());
	});

gulp.task('copy_bower_scripts',
	function () {
		return gulp.src([
			bower.angular.base + 'angular.js',
			bower.angular.route.base + 'angular-route.js',
			bower.bootstrap.dist.js + 'bootstrap.js',
			bower.jquery.dist + 'jquery.js'
		])
			//.pipe(uglify())
			.pipe(gulp.dest(wwwroot.lib.scripts));
	});

gulp.task('copy_bower_css',
	function () {
		return gulp.src([
			bower.bootstrap.dist.css + 'bootstrap.css',
			bower.bootstrap.dist.css + 'bootstrap-theme.css',
			bower.fontawesome.css + 'font-awesome.css'
		])
			.pipe(cssmin())
			.pipe(gulp.dest(wwwroot.lib.contents.css));
	});

gulp.task('copy_bower_fonts',
	function () {
		return gulp.src([
			bower.bootstrap.dist.fonts + '*',
			bower.fontawesome.fonts + '*'
		])
			.pipe(gulp.dest(wwwroot.lib.contents.fonts));
	});

gulp.task('copy_scripts',
	function () {
		return gulp.src([
			custom.scripts + '**/*.js'
		])
			.pipe(wrapper({
				header: '\n/* BEGIN ${filename} */\n',
				footer: '\n/* END   ${filename} */\n',
			}))
			.pipe(concat('SPA.js'))
			//.pipe(uglify())
			.pipe(gulp.dest(wwwroot.scripts));
	});

gulp.task('copy_less',
	function () {
		return gulp.src([
			custom.contents.less + '**/*.less'
		])
			.pipe(concat('spa.css'))
			.pipe(less({
				paths:[bower.bootstrap.less]
			}))
			.pipe(gulp.dest(wwwroot.contents.css));
	});

gulp.task('copy_images',
	function () {
		return gulp.src([
			custom.contents.images + '**/*'
		])
			.pipe(gulp.dest(wwwroot.contents.images));
	});

gulp.task('copy_ng_templates',
	function () {
		return gulp.src([
			custom.ngTemplates + '**/*.html'
		])
			.pipe(gulp.dest(wwwroot.ngTemplates));
	});
