var url_string = window.location + "";
var url = new URL(url_string);
var placeId = url.searchParams.get("placeId");
var user = sessionStorage.getItem("userId");
var role = sessionStorage.getItem('role');
var token_string = sessionStorage.getItem('token');
var rateId = 0;

$(document).ready(function () {
    
    checkRoleForComment();
    loadComment();

    $('.rating').click(function(){
        if(rateId == 0){
            console.log('add new');
            newRating();
        } else {
            console.log('update');
            updateRating();
        }
        
    })

    $('#sendComment').click(function(){
        var contentData = $('#content').val();

        var data = {
            Content : contentData,
            PlaceId : Number.parseInt(placeId),
            UserId : Number.parseInt(user)
        }

        $.ajax({
            type : "POST",
            url: "https://localhost:5001/api/comment",
            headers: {Authorization: 'Bearer '+ token_string},
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success : function(responseText){
                console.log(responseText);
                loadComment();
            }
            
        })
    });

    $('#addImageButton').click(function(){
        if(role = "Guide"){
            window.location.href="formimage.html?placeId="+placeId;
        } else {
            alert("Please sign in with an Guide account.")
        }
        
    })
})

loadContent();
loadRating();

function loadRating() { 
    $.ajax({
        type : "GET",
        url: "https://localhost:5001/api/rating/place/"+placeId+"/"+user,
        headers: {Authorization: 'Bearer '+ token_string},
        dataType: "json",
        success : function(data){
            var point = data.rating1;
            showRating(point);
            rateId = data.ratingId;
        }
    })
 }


function loadContent() {
    $.ajax({
        type : "GET",
        url: "https://localhost:5001/api/places/"+placeId,
        dataType: "json",
        success : function(data){
            var title = data.title;
            var info = data.info;
            var imageCover = data.imageLink;
            $("#banner").attr("style","background: url(data:image/png;base64,"+imageCover+"); background-repeat: no-repeat; background-size: cover;");
            var imageList = data.imageList;

            loadModal(title,info);
            $.each(imageList, function (index, obj) {
                var imageUrl = obj;
                loadImageModal(imageUrl);
            })
            clearFixModalImage();
        }
    })
}

function loadImage() {
    $.ajax({
        type: "GET",
        url: "https://localhost:5001/api/images/place/"+placeId,
        dataType: "json",
        success : function(data){
            $.each(data, function (index, obj) {
                var imageUrl = obj.imageLink;
                loadImageModal(imageUrl);
            })

            clearFixModalImage();
        },
        error: function (errormessage){
            console.log(errormessage.responseText);
        }
    })
}


function loadComment() {
    $.ajax({
        type : "GET",
        url: "https://localhost:5001/api/comment/place/"+placeId,
        dataType: "json",
        success : function(data){
            $.each(data, function (index, obj) {
                var name = obj.username;
                var content = obj.info;
                var commentId = obj.commnetId;

                loadCommentModal(name,content);
            })
        },
        error: function (errormessage){
            console.log(errormessage.responseText);
        }
    })
}

function loadModal(title, info) {
    $(".sing-img-text1 #title").text(title);
    $(".est")[0].setAttribute("style","white-space: pre-line");
    $(".est").text(info);
}

function loadImageModal(link) {
    var contain = document.getElementById("gallery");

    var element = document.createElement("div");
    element.setAttribute("class", "gallery-grid");
    var srcPath = "data:image/png;base64,"+link;

    var href = document.createElement("a");
    href.setAttribute("class","fancybox");
    href.setAttribute("href",srcPath);
    href.setAttribute("data-fancybox-group","gallery");

    var image = document.createElement("img");
    image.setAttribute("src", srcPath);
    image.setAttribute("class","img-style row6");
    image.setAttribute("height","250px");

    href.appendChild(image);
    element.appendChild(href);
    contain.appendChild(element);
}

function loadCommentModal(name, content){
    var contain = document.getElementById("contain");

    var element = document.createElement("li");
    element.setAttribute("class","media");

    var modal_image = document.createElement("div");
    modal_image.setAttribute("class","media-left");

    var image = document.createElement("img");
    image.setAttribute("class","media-object");
    image.setAttribute("src","images/user.jpg");

    modal_image.appendChild(image);

    var modal_body = document.createElement("div");
    modal_body.setAttribute("class","media-body");

    var nameUser = document.createElement("h4");
    nameUser.setAttribute("class","media-heading");
    nameUser.innerHTML = name;

    var commentContent = document.createElement("p");
    commentContent.innerHTML = content;

    modal_body.appendChild(nameUser);
    modal_body.appendChild(commentContent);

    element.appendChild(modal_image);
    element.appendChild(modal_body);

    contain.appendChild(element);
}

function checkRoleForComment() {
    
    if(role != 'Traveller'){
        $('#commentText').remove();
        var mess = document.createElement('h3');
        mess.innerHTML = "Please sign in to comment.";

        var contain = document.getElementById('commentContain');
        contain.appendChild(mess);

        $('#ratingForm').remove();
        $('#ratingContainer h4').text('Please sign in to rate.');

    } 
}

function newRating(){
    
    console.log(token_string);
    var form = new FormData($("#ratingForm")[0]);
    
    form.append('placeId',Number.parseInt(placeId));
    form.append('userId',Number.parseInt(user));

    $.ajax({
        url: "https://localhost:5001/api/rating/place",
        type: "POST",
        dataType: 'text',
        headers: {Authorization: 'Bearer '+ token_string},
        data: form,
        processData: false,
        contentType: false,
        success: function(result){
            console.log("success");
        },
        error: function(er){
            console.log("failed");
        }
    });
}

function updateRating(){
    var form = new FormData($("#ratingForm")[0]);
    
    form.append('placeId',Number.parseInt(placeId));
    form.append('userId',Number.parseInt(user));

    $.ajax({
        url: "https://localhost:5001/api/rating/"+rateId,
        type: "PUT",
        dataType: 'text',
        headers: {Authorization: 'Bearer '+ token_string},
        data: form,
        processData: false,
        contentType: false,
        success: function(result){
            console.log("success");
        },
        error: function(er){
            console.log("failed");
        }
    });
}

function clearFixModalImage(){
    var container = document.getElementById("gallery");

    var element = document.createElement("div");
    element.setAttribute("class","clearfix");
    element.setAttribute('id','clearFixImage');

    container.appendChild(element);
}

function showRating(point){
    var list = $('.rating');
    
    for(i =0; i <list.length; i++){
        if(list[i].hasAttribute("checked")){
            list[i].removeAttribute("checked");
        }
    }

    for(i =0; i < list.length; i++){
        var val = list[i].value;
        if(val == point){
            list[i].setAttribute("checked","checked");
        }
    }
}