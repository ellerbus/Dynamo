'use strict';

var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var cssmin = require('gulp-cssmin');
var less = require('gulp-less');
var rename = require('gulp-rename');
var templateCache = require('gulp-angular-templatecache');

gulp.task('ng-templates', function () {

    var opts = {
        templateHeader: 'define(['require', 'exports', 'angular'], function (require, exports, angular) {    angular.module('app.templates', []).run(['$templateCache', function ($templateCache) {',
        templateBody: '$templateCache.put('App/<%= url %>', '<%= contents %>');',
        templateFooter: '}]);});'
    };

    return gulp.src('App/**/*.html')
        .pipe(templateCache('templates.js', opts))
        .pipe(gulp.dest('App'));
});

gulp.task('ng-requirejs', ['ng-templates'], function (cb) {
    rjs.optimize(cfg, function (resp) { cb(); }, cb);
});


gulp.task('compile-less', function () {

    return gulp.src('Content/*.less')
        .pipe(less())	//.on('error', function(e){console.log(e)})
        .pipe(cssmin())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('Content/'));

});