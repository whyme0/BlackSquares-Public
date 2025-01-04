var gulp = require("gulp");
var del = require("del");

var paths = {
    scripts: ["wwwroot/js/ts/**/*.js", "wwwroot/js/ts/**/*.ts", "wwwroot/js/ts/**/*.map"]
};

gulp.task("clean", function () {
    return del(["wwwroot/js/ts-compiled/**/*"]);
});

gulp.task("default", function (done) {
    gulp.src(paths.scripts).pipe(gulp.dest("wwwroot/js/ts-compiled"));
    done();
});