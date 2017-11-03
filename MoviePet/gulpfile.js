'use strict';
let fs = require('fs');
let gulp = require('gulp');
let gulpApiDoc = require('gulp-apidoc');
let gulpDoc = require('gulp-documentation');
let runSequence = require('run-sequence');
let jshint = require('gulp-jshint');
let nodemon = require('gulp-nodemon');
let mocha = require('gulp-mocha');
let istanbul = require('gulp-istanbul');

/*
    JSHint - Server Side
    Called via gulp serve
*/
gulp.task('jshint-server', function () {
    return gulp.src([
        './server/*.js',
        './server/*/*.js',
        './server/*/*/*.js'
    ])
});

/*
    Starts the server via nodemon, 
    Called via gulp serve
*/
gulp.task('server', function () {
    nodemon({
        'script': 'bin/www',
        "ignore": ["public/*", "app/*"]
    });
});

gulp.task('serve', function (callback) {
    runSequence('jshint-server', 'server', callback);
});

/*
    This runs apiDoc and documentation for the service and api layer
    the api layer documentation is output as html for public viewing
    the service layer documentation is output in markdown for viewing on github
*/
gulp.task('document', function (callback) {
    let serviceFolders = getFolders('./server/services');
    let apiFolders = getFolders('./server/routes/api');

    gulpApiDoc({
        src: './server/routes/api/',
        dest: 'documentation/api/',
        config: './',
    }, callback);
    serviceFolders.map(function (folder) {
        return gulp.src([
            './server/services/' + folder + '/*.js'
        ])
            .pipe(gulpDoc('md', { shallow: true }))
            .pipe(rename(folder + '.md'))
            .pipe(gulp.dest('documentation/services'));
    });
});