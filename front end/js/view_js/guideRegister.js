$(document).ready(function () {
    console.log("co chay");
    $('#register').click(function(){
        var name = $('#username').val();
        var password = $('#pass').val();
        var role = 1;

        var data = {
            UserName : name,
            UserPass : password,
            RoleId : role
        }

        $.ajax({
            type : "POST",
            url: "https://travelservice2021.azurewebsites.net/api/Users",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success : function(responseText){
                if(responseText === "success"){
                    window.location.href = 'index.html'
                } else {
                    $("#Message").removeClass("hiddenField");
                    $("#Message").text("Username Not Available");
                }
            }
        })
    });
})