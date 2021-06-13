$(document).ready(function () {
    $('#passConfirm').change(function(){
        checkPass();
    })
    $('#pass').change(function(){
        checkPass();
    })
    
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
            url: "https://localhost:5001/api/Users",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success : function(responseText){
                if(responseText === "success"){
                    window.location.href = 'index.html'
                } else {
                    $("#message").removeClass("hiddenField");
                    $("#message").text("Username Not Available");
                }
            }
        })
    });
})

function checkPass(){
    var password = $('#pass').val();
    var confirm = $('#passConfirm').val();
    console.log(password);
    if(password != confirm){
        $('#message').text('Password Confirm Is Not Correct!');
        $('#message').removeClass('hiddenField');
        $('#register').addClass('disabled');
    } else {
        $('#message').text('');
        $('#message').addClass('hiddenField');
        $('#register').removeClass('disabled');

    }
}