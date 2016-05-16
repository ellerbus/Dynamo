<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ShortFuze.WebApp.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head data-ng-controller="HeadController as head">
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title data-ng-bind="head.page.title"></title>
  <link href="//ajax.googleapis.com/ajax/libs/angular_material/1.1.0-rc2/angular-material.min.css" rel="stylesheet" type="text/css" />
  <link href="//fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css" />
  <link href="//fonts.googleapis.com/css?family=Source+Code+Pro" rel="stylesheet" type="text/css" />
  <link href="//fonts.googleapis.com/css?family=Special+Elite" rel="stylesheet" type="text/css" />
  <link href="//cdnjs.cloudflare.com/ajax/libs/angular-loading-bar/0.9.0/loading-bar.min.css" rel="stylesheet" type="text/css" />

  <!-- link href="Content/Site.min.css" rel="stylesheet" / -->

  <style type="text/css">
    [ng\:cloak], [ng-cloak], [data-ng-cloak], [x-ng-cloak], .ng-cloak, .x-ng-cloak {
      display: none !important;
    }

    .splash {
      padding-top: 64px;
      display: none;
      position: absolute;
      z-index: 9999;
      top: 0;
      left: 0;
      height: 100%;
      width: 100%;
      filter: alpha(opacity=60);
      opacity: 0.6;
      background: #000;
    }

    [ng-cloak].splash {
      display: block !important;
    }

    .splash h2 {
      text-align: center;
      font-size: 2em;
      color: #808080;
    }
  </style>
</head>
<body>
  <div class="splash" ng-cloak="">
    <h2>Loading ...</h2>
  </div>

  <div layout="column" ng-cloak="" layout-fill="">
    <section layout="row" flex="">
      <md-sidenav class="md-sidenav-left md-whiteframe-z2" md-component-id="left" md-is-locked-open="$mdMedia('gt-md')">
        <md-toolbar class="md-theme-indigo">
          <h1 class="md-toolbar-tools">Sidenav Left</h1>
        </md-toolbar>
        <md-content layout-padding="">
          <md-button ng-click="close()" class="md-primary" hide-gt-md="">
            Close Sidenav Left
          </md-button>
          <p hide-md="" show-gt-md="">
            <md-icon aria-label="Menu" class="material-icons">menu</md-icon>
            This sidenav is locked open on your device. To go back to the default behavior,
            narrow your display.
          </p>
        </md-content>
      </md-sidenav>

      <md-content flex="" layout-padding="">
        <div layout="column" layout-fill="" layout-align="top center">
          <p>
          The left sidenav will 'lock open' on a medium (>=960px wide) device.
          </p>
        </div>
        <div flex=""></div>
      </md-content>

    </section>

  </div>

  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-animate.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-aria.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-cookies.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-messages.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-route.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-resource.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-sanitize.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angular_material/1.1.0-rc2/angular-material.min.js"></script>
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/angular-loading-bar/0.9.0/loading-bar.min.js"></script>
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.3/moment.min.js"></script>
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/numeral.js/1.4.5/numeral.min.js"></script>
  <script type="text/javascript">
    var deps = [
      'ngAnimate', 'ngMessages', 'ngSanitize', 'ngRoute', 'ngMaterial', 'angular-loading-bar'
    ];

    angular.module('app', deps);

    var configuration = <%= GetVersion() %>;

    $(function () { angular.bootstrap(document, ['app']); });
  </script>
  <%= GetScripts() %>
</body>
</html>
