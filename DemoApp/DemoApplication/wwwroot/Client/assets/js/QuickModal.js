
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





let btns = document.querySelectorAll(".add-product-to-basket-btn")

btns.forEach(x => x.addEventListener("click", function (e) {
    e.preventDefault()
    fetch(e.target.parentElement.href)
        .then(response => response.text())
        .then(data => {
            $('.cart-block').html(data);
        })
}))

$(document).on("click", ".remove-product-to-basket-btn", function (e) {
    e.preventDefault();

    fetch(e.target.parentElement.href)
        .then(response => response.text())
        .then(data => {
            $('.cart-block').html(data);
        })
})

$(document).on("click", ".plus-btn", function (e) {
    e.preventDefault();

    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            $('.cartPageJs').html(data);

            fetch(e.target.nextElementSibling.href)
                .then(response => response.text())
                .then(data => {
                    $('.cart-block').html(data);
                })
        })
})

$(document).on("click", ".minus-btn", function (e) {
    e.preventDefault();

    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            $('.cartPageJs').html(data);

            fetch(e.target.nextElementSibling.href)
                .then(response => response.text())
                .then(data => {
                    $('.cart-block').html(data);
                })
        })
})