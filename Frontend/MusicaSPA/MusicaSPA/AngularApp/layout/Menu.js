//document.addEventListener('DOMContentLoaded', function () {
//    var elems = document.querySelectorAll('.sidenav');
//    var instances = M.Sidenav.init(elems, options);
//});

// Initialize collapsible (uncomment the lines below if you use the dropdown variation)
// var collapsibleElem = document.querySelector('.collapsible');
// var collapsibleInstance = M.Collapsible.init(collapsibleElem, options);

// Or with jQuery

$(document).ready(function () {
    $('.side-nav').sidenav();
});

$(document).ready(function () {
  $('.button-collapse').sideNav({
    menuWidth: 300, // Default is 300
    edge: 'left', // Choose the horizontal origin
    closeOnClick: false, // Closes side-nav on <a> clicks, useful for Angular/Meteor
    draggable: true // Choose whether you can drag to open on touch screens
  }
  );
  // START OPEN
  $('.button-collapse').sideNav('hide');
});
