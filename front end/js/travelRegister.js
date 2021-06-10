$(document).ready(function () {
    $('#register').click(function(){
        var name = $('#username').val();
        var password = $('#pass').val();
        var role = 2;

        var data = {
            UserName : name,
            UserPass : password,
            RoleId : role
        }

        $.ajax({
            type : "POST",
            url: "https://localhost:49155/api/Users",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success : function(responseText){
                if(responseText === "success"){
                    window.location.href = 'index.html'
                } else {
                    $("#Message").removeClass("hiddenField");
                    $("#Message").val("Username Not Available");
                }
            }
        })
    });
})