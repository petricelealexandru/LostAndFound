function createItem() {
    debugger

    var name = $("#text-id")[0].value;

    var postModel = {
        TypeId: "FF9E110D-68F8-435C-A082-EFD709FE1A66",
        Name: name,

    }

    var url = "api/Item/CreateItem";

    ajaxHelper.post(url, postModel,
        function (result) {
            debugger
        },
        function (err) {
            debugger
        });
}

// Get the modal
var modal = document.getElementById("myModal");

// Get the button that opens the modal
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on the button, open the modal
btn.onclick = function () {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}