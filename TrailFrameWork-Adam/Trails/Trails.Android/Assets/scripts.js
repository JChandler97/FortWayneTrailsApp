
$('.menu_toggle').on('click', function(e) {
	$('.menu_toggle').toggleClass("close"); // Toggle class "close" on .menu_toggle when it is clicked
	$('nav').toggleClass("reveal"); // Toggle class "reveal" on nav when .menu_toggle is clicked
	e.preventDefault();
});

// Smooth scroll to top when .jump_to_top is clicked
$(".jump_to_top").click(function() {
    $('html, body').animate({
        scrollTop: $("#top").offset().top
    }, 500);
});