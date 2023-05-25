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