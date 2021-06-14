var role = sessionStorage.getItem('role');
var userId = sessionStorage.getItem('userId');
var token_string = sessionStorage.getItem("token");

$(document).ready(function () {
    $('#addPlaceButton').click(function(){
        showAddPlace();
    })

    $('#searchButton').click(function(){
        searchPlace();
    })

})

$.ajax({
    type : "GET",
    url: "https://localhost:5001/api/places/user/"+userId,
    dataType: "json",
    headers: {Authorization: 'Bearer '+ token_string},
    success : function(data){
        $.each(data, function (index, obj) {
            var name = obj.placeName;
            var title = obj.title;
            var info = obj.info;
            var image = obj.imageLink;
            var id = obj.placeId;

            var path = "detail.html?placeId="+id;

            loadModel(path,image,title,info,name,id);
        })
        clearFixModal();
    }
})


function loadModel(path, img, title, content, name, placeId){
    var container = document.getElementById("contentSection");

    var modal_stage1 = document.createElement("div");
    modal_stage1.setAttribute("class","col-md-3 blog-grid tempContent");

    var modal_stage2 = document.createElement("div");
    modal_stage2.setAttribute("class","blog-grid1");

    var image_href = document.createElement("a");
    image_href.setAttribute("href", path);

    var image = document.createElement("img");
    var srcPath = "data:image/png;base64,"+img;
    image.setAttribute("src", srcPath);
    image.setAttribute("height","191.25px");

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
    modal_readmore.setAttribute("class", "red-mre row");

    var row1 = document.createElement("div");
    row1.setAttribute("class","col-md-1");

    var edit = document.createElement("a");
    edit.setAttribute("href","formplace.html?edit=true&placeId="+placeId);

    var symbolEdit = document.createElement("i");
    symbolEdit.setAttribute("class","fas fa-edit");

    edit.appendChild(symbolEdit);
    row1.appendChild(edit);

    var row2 = document.createElement("div");
    row2.setAttribute("class","col-md-1");

    var deleteButton = document.createElement("a");
    deleteButton.addEventListener('click', function(){
        var result = confirm("Want to delete?");
        if(result){
            deletePlace(placeId);
        }
    });

    var symbolDelete = document.createElement("i");
    symbolDelete.setAttribute("class","fas fa-trash-alt");

    deleteButton.appendChild(symbolDelete);
    row2.appendChild(deleteButton);

    var row3 = document.createElement("div");
    row3.setAttribute("class","col-md-10");

    var readmore = document.createElement("a");
    readmore.setAttribute("href", path);
    readmore.setAttribute("style","width:185px;");
    readmore.innerHTML = "Read More";

    row3.appendChild(readmore);

    modal_readmore.appendChild(row1);
    modal_readmore.appendChild(row2);
    modal_readmore.appendChild(row3);

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
    element.setAttribute('id','clearFixContent');

    container.appendChild(element);
}

function showAddPlace(){
    
    if(role === 'Guide'){
        window.location.href = "formplace.html";
    } else {
        alert("You are not a Guider. Please sign in with Guide account to add new place.");
    }
}

function clearTempContent(){
    var list = $('.tempContent');
    for(i=0; i<list.length; i++){
        list[i].remove();
    }

    $('#clearFixContent').remove();
}

function searchPlace(){
    var search = $('#searchTxt').val();
    if(search != ""){
        clearTempContent();
        $.ajax({
            type : "GET",
            url: "https://localhost:5001/api/search/"+search,
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
    } 
}

function deletePlace(placeId){
    $.ajax({
        type : "DELETE",
        url: "https://localhost:5001/api/places/"+placeId,
        headers: {Authorization: 'Bearer '+ token_string},
        dataType: "text",
        success : function(data){
            window.location.reload();
        }
    })
}