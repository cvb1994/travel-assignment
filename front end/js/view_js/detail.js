$(document).ready(function () {
    var url_string = window.location + "";
    var url = new URL(url_string);
    var placeId = url.searchParams.get("placeId");

    $.ajax({
        type : "GET",
        url: "https://localhost:49163/api/places/"+placeId,
        dataType: "json",
        success : function(data){
            var title = obj.title;
            var info = obj.info;

            loadModel(title,info);
        }
    })

    $.ajax({
        type: "GET",
        url: "https://localhost:49163/api/images/"+placeId,
        dataType: "json",
        success : function(data){
            var title = obj.title;
            var info = obj.info;

            loadModel(title,info);
        }
    })
})

function loadModal(title, info) {
    $(".sing-img-text1 h3").text(title);
    $(".est").text(info);
}

function loadImage(link) {
    var contain = document.getElementById("gallery");

    var element = document.createElement("div");
    element.setAttribute("class", "gallery-grid");

    var href = document.createElement("a");
    href.setAttribute("class","fancybox");
    href.setAttribute("href",link);
    href.setAttribute("data-fancybox-group","gallery");

    var image = document.createElement("img");
    image.setAttribute("src", link);
    image.setAttribute("class","img-style row6");
    image.setAttribute()

}