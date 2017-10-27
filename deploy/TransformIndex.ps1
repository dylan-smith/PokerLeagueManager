param(
    [string]$SourcePath,
    [string]$TargetPath
)

Write-Output "SourcePath: $SourcePath"
Write-Output "TargetPath: $TargetPath"

$indexContent = [IO.File]::ReadAllText($SourcePath) 

$topContent = '@{' + "`n"
$topContent += '  var appInsightsKey = System.Configuration.ConfigurationManager.AppSettings["AppInsightsKey"];' + "`n"
$topContent += '  var queryServiceUrl = System.Configuration.ConfigurationManager.AppSettings["QueryServiceUrl"];' + "`n"
$topContent += '  var commandServiceUrl = System.Configuration.ConfigurationManager.AppSettings["CommandServiceUrl"];' + "`n"
$topContent += '  var googleAnalyticsId = System.Configuration.ConfigurationManager.AppSettings["GoogleAnalyticsId"];' + "`n"
$topContent += '}' + "`n"

$newContent = "`n"
$newContent += '  <script type="text/javascript">' + "`n"
$newContent += '    var pokerConfig = {' + "`n"
$newContent += '      appInsightsKey: "@appInsightsKey",' + "`n"
$newContent += '      queryServiceUrl: "@queryServiceUrl",' + "`n"
$newContent += '      commandServiceUrl: "@commandServiceUrl",' + "`n"
$newContent += '      googleAnalyticsId: "@googleAnalyticsId"' + "`n"
$newContent += '    }' + "`n"
$newContent += '  </script>' + "`n"
$newContent += "  `n"
$newContent += '  @if (!string.IsNullOrWhiteSpace(appInsightsKey))' + "`n"
$newContent += '  {' + "`n"
$newContent += '    <script type="text/javascript">' + "`n"
$newContent += '      var appInsights = window.appInsights || function (config) {' + "`n"
$newContent += '        function i(config) { t[config] = function () { var i = arguments; t.queue.push(function () { t[config].apply(t, i) }) } } var t = { config: config }, u = document, e = window, o = "script", s = "AuthenticatedUserContext", h = "start", c = "stop", l = "Track", a = l + "Event", v = l + "Page", y = u.createElement(o), r, f; y.src = config.url || "https://az416426.vo.msecnd.net/scripts/a/ai.0.js"; u.getElementsByTagName(o)[0].parentNode.appendChild(y); try { t.cookie = u.cookie } catch (p) { } for (t.queue = [], t.version = "1.0", r = ["Event", "Exception", "Metric", "PageView", "Trace", "Dependency"]; r.length;) i("track" + r.pop()); return i("set" + s), i("clear" + s), i(h + a), i(c + a), i(h + v), i(c + v), i("flush"), config.disableExceptionTracking || (r = "onerror", i("_" + r), f = e[r], e[r] = function (config, i, u, e, o) { var s = f && f(config, i, u, e, o); return s !== !0 && t["_" + r](config, i, u, e, o), s }), t' + "`n"
$newContent += '      }({' + "`n"
$newContent += '        instrumentationKey: pokerConfig.appInsightsKey' + "`n"
$newContent += '      });' + "`n"
$newContent += "      `n"
$newContent += '      window.appInsights = appInsights;' + "`n"
$newContent += '      appInsights.trackPageView();' + "`n"
$newContent += '    </script>' + "`n"
$newContent += '  }' + "`n"
$newContent += "  `n"
$newContent += '  @if (!string.IsNullOrWhiteSpace(googleAnalyticsId))' + "`n"
$newContent += '  {' + "`n"
$newContent += '    <script type="text/javascript">' + "`n"
$newContent += '      (function (i, s, o, g, r, a, m) {' + "`n"
$newContent += '        i["GoogleAnalyticsObject"] = r; i[r] = i[r] || function () {' + "`n"
$newContent += '          (i[r].q = i[r].q || []).push(arguments)' + "`n"
$newContent += '        }, i[r].l = 1 * new Date(); a = s.createElement(o),' + "`n"
$newContent += '        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)' + "`n"
$newContent += '      })(window, document, "script", "https://www.google-analytics.com/analytics.js", "ga");' + "`n"
$newContent += "      `n"
$newContent += '      ga("create", pokerConfig.googleAnalyticsId, "auto");' + "`n"
$newContent += '      ga("send", "pageview");' + "`n"
$newContent += '    </script>' + "`n"
$newContent += '  }' + "`n"

$indexContent = $topContent + $indexContent
$indexContent = $indexContent -replace '(?<=<!--Start-->)(.|\n)*?(?=<!--End-->)', $newContent

$indexContent | Set-Content $TargetPath