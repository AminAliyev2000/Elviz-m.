/// <binding BeforeBuild='build' />
var gulp = require('gulp'),
    sass = require('gulp-sass'),
    cssmin = require("gulp-cssmin"),
    rename = require("gulp-rename");

gulp.task('min:css', function () {
    return gulp.src('assets/scss/style.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(cssmin())
        .pipe(rename({
            suffix: ".min"
        }))
        .pipe(gulp.dest('wwwroot/'));
});
gulp.task('copy:img', function () {
    return gulp.src('assets/img/*.*')
        .pipe(gulp.dest('wwwroot/img_png'));
});
gulp.task('copy:fonts', function () {
    return gulp.src('assets/fonts/*.*')
        .pipe(gulp.dest('wwwroot'));
});

gulp.task('build', ['min:css', 'copy:img', 'copy:fonts']);