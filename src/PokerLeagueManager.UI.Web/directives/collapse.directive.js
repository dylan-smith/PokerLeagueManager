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

            // set the height as a data attr so we can use it again later
            // element.dataset.heightOld = angular.element(element).prop('offsetHeight');

            $scope.$watch(attributes.collapse, function (collapse) {
                //if (!collapse) {
                //    var oldVisibility = element.style.visibility;
                //    var oldDisplay = element.style.display;
                    
                //    element.style.visibility = 'hidden';
                //    element.style.display = 'block';
                //    element.style.height = 'auto';
                //    element.dataset.heightOld = angular.element(element).prop('offsetHeight');
                    
                //    element.style.visibility = oldVisibility;
                //    element.style.display = oldDisplay;

                //    ngElement.toggleClass('collapsed', collapse);
                //} else {
                //    element.style.height = '0px';
                //    ngElement.toggleClass('collapsed', collapse);
                //}

                //if (collapse) {
                //    element.style.max-height = '0px';
                //    ngElement.toggleClass('collapsed', collapse);
                //} else {
                //    element.style.max-height = '168px';
                //    ngElement.toggleClass('collapsed', collapse);
                //}
            });
        }
    }
}());