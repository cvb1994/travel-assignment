$(document).ready(function () {
    var user = localStorage.getItem('userId');
    var token_string = localStorage.getItem('token');

    $.ajax({
        type : "GET",
        url: "https://localhost:49155/api/users/"+user,
        //dataType: "json",
        headers: {Authorization: 'Bearer '+ token_string},
        success : function(data){
            var name = data.userName;
            logined(name);
        }
    })

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
    user.setAttribute("id","logoutbutton");
    var href = document.createElement("a");
    href.innerHTML = name;

    user.appendChild(href);
    contain.appendChild(user);
}

function logout() {
    var contain = document.getElementById("navBar");

    var register = document.createElement("li");
    register.setAttribute("id","registerForm");
    var href1 = document.createElement("a");
    href1.setAttribute("href","travellerregister.html");
    href1.innerHTML = "REGISTER";
    register.appendChild(href1);

    var login = document.createElement("li");
    login.setAttribute("id","loginForm");
    var href2 = document.createElement("a");
    href2.setAttribute("href","login.html");
    href2.innerHTML = "LOGIN";
    login.appendChild(href2);

    var user = document.getElementById("logout");
    user.remove();

    contain.appendChild(register);
    contain.appendChild(login);

}