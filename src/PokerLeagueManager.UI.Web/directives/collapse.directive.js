(function () {
    'use strict';

    angular.module('poker').directive('collapse', collapseDirective);

    function collapseDirective() {
        return {
            restrict: 'A',
            link: link
        };

        function link($scope, ngElement, attributes) {
            var element = ngElement[0];

            $scope.$watch(attributes.collapse, function (collapse) {
                if (collapse) {
                    element.style.maxHeight = '0px';
                } else {
                    element.style.maxHeight = element.dataset.expandedHeight + 'px';
                }
            });
        }
    }
}());