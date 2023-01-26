
$('.a-tag li a').filter(function () {
    return this.href === location.href;
}).addClass('active');

$(document).on("click", ".quuickview-btn", function (e) {
    e.preventDefault();

    var url = e.target.parentElement.href;
    console.log(url)

    fetch(url)
        .then(response => response.text())
        .then(data => {

            $('.modalProduct').html(data);
            console.log(data)
        })

    $("#quickModal").modal("show");
})