$(document).ready(function () {
    $("#submitButton").click(function () {
        $("#imageSubmit").ajaxForm({url: 'https://localhost:49157/api/images', type: 'post'});
        location.reload();
    })

})



function handleFileSelect(evt) {
    var f = evt.target.files[0]; // FileList object
    var reader = new FileReader();
    // Closure to capture the file information.
    reader.onload = (function(theFile) {
        return function(e) {
            var binaryData = e.target.result;
            //Converting Binary Data to base 64
            var base64String = window.btoa(binaryData);
            //showing file converted to base64
            console.log(base64String);
        };
    })(f);
    // Read in the image file as a data URL.
    reader.readAsBinaryString(f);
}

