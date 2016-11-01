/// <binding AfterBuild='default' ProjectOpened='watch' />
var gulp = require('gulp');
var del = require('del');
var paths = require('./gulp.config.json');
var plug = require('gulp-load-plugins')();
var execFile = require('child_process').execFile;

var log = plug.util.log;

gulp.task('help', plug.taskListing);

gulp.task('templatecache', function () {
    log('Creating an AngularJS $templateCache');

    return gulp.src(paths.htmltemplates)
               .pipe(plug.minifyHtml({
                   empty: true
               }))
               .pipe(plug.angularTemplatecache('templates.js', {
                   module: 'poker',
                   standalone: false,
                   root: '/components'
               }))
               .pipe(gulp.dest(paths.build));
});

gulp.task('js', ['templatecache', 'typescript'], function () {
    log('Bundling, minifying, and copying the app\'s JavaScript');

    return gulp.src(paths.js)
               .pipe(plug.concat('pokerApp.min.js'))
               .pipe(plug.ngAnnotate({
                   add: true,
                   single_quotes: true
               }))
               .pipe(plug.bytediff.start())
               .pipe(plug.uglify({
                   mangle: true
               }))
               .pipe(plug.bytediff.stop(bytediffFormatter))
               .pipe(gulp.dest(paths.build));
});

gulp.task("typescript", ['generateTypescript'], function () {
    var tsProject = plug.typescript.createProject("tsconfig.json");

    return tsProject.src()
        .pipe(plug.tslint({ formatter: "verbose" }))
        .pipe(plug.tslint.report())
        .pipe(tsProject())
        .on('error', function () { process.exit(1) })
        .js.pipe(gulp.dest(paths.tsbuild));
});

gulp.task("generateTypescript", function (cb) {
    execFile('C:\\Git\\PokerLeagueManager\\tools\\PokerLeagueManager.TypeScriptGenerator\\PokerLeagueManager.TypeScriptGenerator.exe',
        [__dirname + '\\..\\PokerLeagueManager.Common\\Queries',
            __dirname + '\\..\\PokerLeagueManager.Common\\DTO',
            __dirname + '\\services',
            __dirname + '\\dto'],
            cb);
});

gulp.task('vendorjs', ['js'], function () {
    log('Bundling, minifying, and copying the Vendor JavaScript');

    return gulp.src(paths.vendorjs)
               .pipe(plug.concat('vendor.min.js'))
               .pipe(plug.bytediff.start())
               .pipe(plug.uglify())
               .pipe(plug.bytediff.stop(bytediffFormatter))
               .pipe(gulp.dest(paths.build));
});

gulp.task('css', function () {
    log('Bundling, minifying, and copying the app\'s CSS');

    return gulp.src(paths.less)
               .pipe(plug.lesshint({ configPath: 'lesshintrc.json' }))
               .pipe(plug.lesshint.reporter())
               .pipe(plug.less())
               .pipe(plug.csslint('csslintrc.json'))
               .pipe(plug.csslint.formatter(require('csslint-stylish')))
               .pipe(plug.concat('pokerApp.min.css')) // Before bytediff or after
               .pipe(plug.autoprefixer('last 2 version', '> 5%'))
               .pipe(plug.bytediff.start())
               .pipe(plug.minifyCss({}))
               .pipe(plug.bytediff.stop(bytediffFormatter))
               .pipe(gulp.dest(paths.build));
});

gulp.task('vendorcss', function () {
    log('Compressing, bundling, copying vendor CSS');

    return gulp.src(paths.vendorcss)
               .pipe(plug.concat('vendor.min.css'))
               .pipe(plug.bytediff.start())
               .pipe(plug.minifyCss({}))
               .pipe(plug.bytediff.stop(bytediffFormatter))
               .pipe(gulp.dest(paths.build));
});

gulp.task('images', function () {
    log('Copying images to output dir');

    return gulp.src(paths.images)
               .pipe(gulp.dest(paths.build + "/images"));
});

gulp.task('favicon', function () {
    log('Copying favicon to root');

    return gulp.src(paths.favicon)
               .pipe(gulp.dest(paths.build));
});

gulp.task('build', ['js', 'vendorjs', 'css', 'vendorcss', 'images', 'favicon'], function () {
    log('Injecting dependencies into index.html and cache-busting');

    return gulp.src([paths.html])
               .pipe(inject('vendor.min.css', 'inject-vendor'))
               .pipe(inject('pokerApp.min.css'))
               .pipe(inject('vendor.min.js', 'inject-vendor'))
               .pipe(inject('pokerApp.min.js'))
               .pipe(gulp.dest(paths.build))
               .pipe(plug.cacheBust())
               .pipe(gulp.dest(paths.build));

    function inject(path, name) {
        var pathGlob = paths.build + path;
        var options = {
            ignorePath: paths.build.substring(1),
            read: false
        };
        if (name) {
            options.name = name;
        }
        return plug.inject(gulp.src(pathGlob), options);
    }
});

gulp.task('cleanIntermediates', function (cb) {
    del(paths.intermediateFiles, cb);
})

gulp.task('default', plug.sequence('clean', 'build', 'cleanIntermediates'))

gulp.task('clean', function (cb) {
    log('Cleaning: ' + plug.util.colors.blue(paths.build));

    del(paths.build, cb);
});

gulp.task('watch', function () {
    log('Watching all files');

    var tsConfig = require('./tsconfig.json');

    var ts = ['gulpfile.js'].concat(tsConfig.files);

    gulp.watch(ts, ['typescript'])
        .on('change', logWatch);

    function logWatch(event) {
        log('*** File ' + event.path + ' was ' + event.type + ', running tasks...');
    }
});

////////////////

/**
 * Formatter for bytediff to display the size changes after processing
 * @param  {Object} data - byte data
 * @return {String}      Difference in bytes, formatted
 */
function bytediffFormatter(data) {
    var difference = (data.savings > 0) ? ' smaller.' : ' larger.';
    return data.fileName + ' went from ' +
        (data.startSize / 1000).toFixed(2) + ' kB to ' + (data.endSize / 1000).toFixed(2) + ' kB' +
        ' and is ' + formatPercent(1 - data.percent, 2) + '%' + difference;
}

/**
 * Format a number as a percentage
 * @param  {Number} num       Number to format as a percent
 * @param  {Number} precision Precision of the decimal
 * @return {String}           Formatted percentage
 */
function formatPercent(num, precision) {
    return (num * 100).toFixed(precision);
}