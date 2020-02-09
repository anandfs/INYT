// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $(".wrapper-dropdown-3").on("click", function () {
        $(this).toggleClass("active")
    })
    $(".wrapper-dropdown-2").on("click", function () {
        $(this).toggleClass("active")
    })
})