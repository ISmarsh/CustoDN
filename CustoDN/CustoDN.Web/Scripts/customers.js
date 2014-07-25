

function read($http) {
    function edit(id) {
        $http.get("~/Customer/Edit/" + id).success(function (data) {
            document.getElementById(id).innerHTML = data;
        });
    }
}