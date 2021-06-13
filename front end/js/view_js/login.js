$(document).ready(function () {
    var user = sessionStorage.getItem('userId');
    var token_string = sessionStorage.getItem('token');
    var username = sessionStorage.getItem('username');
    var userRole = sessionStorage.getItem('role');

    // $.ajax({
    //     type : "GET",
    //     url: "https://localhost:5001/api/users/"+user,
    //     //dataType: "json",
    //     headers: {Authorization: 'Bearer '+ token_string},
    //     success : function(data){
    //         var name = data.userName;
    //         logined(name);
    //     }
    // })
    if(username != null){
        logined(username);
    }
    

    $("#logoutbutton").click(function () {
        console.log("logout");
        logout();
    })

})

function logined(name){
    var contain = document.getElementById("navBar");
    var register = document.getElementById("registerForm");
    register.remove();

    var login = document.getElementById("loginForm");
    login.remove();

    var user = document.createElement("li");
    var user_href = document.createElement("a");
    user_href.innerHTML = name;

    var logoutButton = document.createElement('li');
    logoutButton.setAttribute("id","logoutbutton");
    var logout_href = document.createElement("a");
    logout_href.setAttribute('href','');
    logout_href.innerHTML = 'Logout';

    user.appendChild(user_href);
    logoutButton.appendChild(logout_href);
    contain.appendChild(user);
    contain.appendChild(logoutButton);
}

function logout() {
    sessionStorage.clear();
    window.location.reload();

}