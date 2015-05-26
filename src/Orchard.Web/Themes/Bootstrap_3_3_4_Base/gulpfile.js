var gulp = require('gulp');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');
var less = require('gulp-less');

var paths = {

    bower : './bower_components/',
    styles : './Styles/',
    scripts : './Scripts/',
    app : './app_components/'

};

gulp.task('default', ['styles','scripts'], function() {

    

});

gulp.task('styles', function () {

    var stream = gulp.src(paths.app + '/base.less')
        .pipe(less())
        .pipe(gulp.dest(paths.styles));

    return stream;

});

gulp.task('scripts', function () {

    var stream = gulp.src(
        [
            paths.bower + 'jquery/dist/jquery.js',
            paths.bower + 'bootstrap/dist/js/bootstrap.js'
        ])
        .pipe(concat('base.js'))
        .pipe(uglify())
        .pipe(gulp.dest(paths.scripts));

    return stream;

});
