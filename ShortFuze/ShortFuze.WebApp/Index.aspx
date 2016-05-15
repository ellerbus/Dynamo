<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ShortFuze.WebApp.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" ng-controller="MainController as main">
<head>
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title ng-bind="main.page.title"></title>

  <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" rel="stylesheet" />
  <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" rel="stylesheet" />
  <link href="//fonts.googleapis.com/css?family=Source+Code+Pro" rel="stylesheet" type="text/css" />
  <link href="//fonts.googleapis.com/css?family=Special+Elite" rel="stylesheet" type="text/css" />
  <link href="//cdnjs.cloudflare.com/ajax/libs/angularjs-toaster/2.0.0/toaster.min.css" rel="stylesheet" />
  <link href="//cdnjs.cloudflare.com/ajax/libs/angular-toastr/1.7.0/angular-toastr.min.css" rel="stylesheet" />
  <link href="//cdnjs.cloudflare.com/ajax/libs/angular-loading-bar/0.9.0/loading-bar.min.css" rel="stylesheet">

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
    <h2><i class="fa fa-fw fa-spin fa-spinner"></i>Loading ...</h2>
  </div>

  <div class="container">
    <navigation></navigation>
    <co-bread-crumbs></co-bread-crumbs>
    <ng-view></ng-view>
  </div>

  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
  <script type="text/javascript" src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-route.min.js"></script>
  <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-resource.min.js"></script>
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/angular-toastr/1.7.0/angular-toastr.min.js"></script>
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/angular-toastr/1.7.0/angular-toastr.tpls.min.js"></script>
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/angular-loading-bar/0.9.0/loading-bar.min.js"></script>
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.3/moment.min.js"></script>
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/numeral.js/1.4.5/numeral.min.js"></script>
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-hover-dropdown/2.2.1/bootstrap-hover-dropdown.min.js"></script>
  <script type="text/javascript">
    var deps = [
      'ngRoute', 'toastr', 'angular-loading-bar'
    ];

    angular.module('app', deps);

    var configuration = <%= GetVersion() %>;

    $(function () { angular.bootstrap(document, ['app']); });
  </script>
  <%= GetScripts() %>
</body>
</html>
