$(document).ready(function () {
    var url_string = window.location + "";
    var url = new URL(url_string);
    var placeId = url.searchParams.get("placeId");
    var user = localStorage.getItem("userId");

    $.ajax({
        type : "GET",
        url: "https://localhost:49161/api/places/"+placeId,
        dataType: "json",
        success : function(data){
            var title = obj.title;
            var info = obj.info;

            loadModel(title,info);
        }
    })

    $.ajax({
        type: "GET",
        url: "https://localhost:49161/api/images/"+placeId,
        dataType: "json",
        success : function(data){
            $.each(data, function (index, obj) {
                var imageUrl = obj.imageLink;
                loadImage(imageUrl);
            })
        }
    })

    loadComment();

    $('#sendComment').click(function(){
        var content = $('#content').val();


        var data = {
            Content : content,
            PlaceId : placeId,
            UserId : user
        }

        $.ajax({
            type : "POST",
            url: "https://localhost:49161/api/comment",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success : function(responseText){
                loadComment();
            }
        })
    });
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
    image.setAttribute("src", "../"+link);
    image.setAttribute("class","img-style row6");

    href.appendChild(image);
    element.appendChild(href);
    contain.appendChild(element);
}

function loadComment() {
    $.ajax({
        type : "GET",
        url: "https://localhost:49161/api/comment/"+placeId,
        dataType: "json",
        success : function(data){
            $.each(data, function (index, obj) {
                var name = obj.username;
                var content = obj.info;

                loadModel(path,image,title,info,name);
            })
        }
    })
}

function loadCommentModal(name, content){
    var contain = document.getElementById("contain");

    var element = document.createElement("li");
    element.setAttribute("class","media");

    var modal_image = document.createElement("div");
    modal_image.setAttribute("class","media-left");

    var image = document.createElement("img");
    image.setAttribute("class","media-object");
    image.setAttribute("src","images/11.jpg");

    modal_image.appendChild(image);

    var modal_body = document.createElement("div");
    modal_body.setAttribute("class","media-body");

    var nameUser = document.createElement("h4");
    nameUser.setAttribute("class","media-heading");
    nameUser.innerHTML = name;

    modal_body.appendChild(nameUser);
    modal_body.innerHTML = content;

    element.appendChild(modal_image);
    element.appendChild(modal_body);

    contain.appendChild(element);




}