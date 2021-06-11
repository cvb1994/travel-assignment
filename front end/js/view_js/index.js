$(document).ready(function () {


})

$.ajax({
    type : "GET",
    url: "https://localhost:49161/api/places",
    dataType: "json",
    success : function(data){
        $.each(data, function (index, obj) {
            var name = obj.placeName;
            var title = obj.title;
            var info = obj.info;
            var image = obj.imageLink;
            var id = obj.placeId;

            var path = "detail.html?placeId="+id;
            console.log(path);

            loadModel(path,image,title,info,name);
        })
        clearFixModal();
    }
})


function loadModel(path, img, title, content, name){
    var container = document.getElementById("contentSection");

    var modal_stage1 = document.createElement("div");
    modal_stage1.setAttribute("class","col-md-3 blog-grid");

    var modal_stage2 = document.createElement("div");
    modal_stage2.setAttribute("class","blog-grid1");

    var image_href = document.createElement("a");
    image_href.setAttribute("href", path);

    var image = document.createElement("img");
    var srcPath = "data:image/png;base64,"+img;
    image.setAttribute("src", srcPath);

    image_href.appendChild(image);

    var modal_content = document.createElement("div");
    modal_content.setAttribute("class","blog-grid1-info");

    var modal_title = document.createElement("div");
    modal_title.setAttribute("class","soluta");

    var titleModal = document.createElement("a");
    titleModal.setAttribute("href", path);
    titleModal.innerHTML = title;

    var place = document.createElement("span");
    place.innerHTML = name;

    modal_title.appendChild(titleModal);
    modal_title.appendChild(place);

    var contentModal = document.createElement("p");
    contentModal.innerHTML = content;

    var modal_readmore = document.createElement("div");
    modal_readmore.setAttribute("class", "red-mre");

    var readmore = document.createElement("a");
    readmore.setAttribute("href", path);
    readmore.innerHTML = "Read More";

    modal_readmore.appendChild(readmore);

    modal_content.appendChild(modal_title);
    modal_content.appendChild(contentModal);
    modal_content.appendChild(modal_readmore)
    modal_stage2.appendChild(image_href);
    modal_stage2.appendChild(modal_content);
    modal_stage1.appendChild(modal_stage2);
    container.appendChild(modal_stage1);
}

function clearFixModal(){
    var container = document.getElementById("contentSection");

    var element = document.createElement("div");
    element.setAttribute("class","clearfix");

    container.appendChild(element);
}