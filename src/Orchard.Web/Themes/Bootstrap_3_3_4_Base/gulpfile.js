var gulp = require('gulp')
    less = require('gulp-less');

var paths = {

    bower : './bower_components/',
    styles : './styles/',
    scripts : '/scripts/',
    app : './app_components/'

};

gulp.task('default', function() {

    

});

gulp.task('styles', function () {

    var stream = gulp.src(paths.app + '/app.less')
        .pipe(less())
        .pipe(gulp.dest(paths.styles));

});
