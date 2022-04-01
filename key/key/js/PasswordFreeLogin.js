$(function () {
    $("text").click(function (){
        dd.runtime.permission.requestAuthCode({
            corpId: "ding22b125a2c7cb0a8e",
            onSuccess: function (result) {
                var result = result.code;
                document.getElementById("HiddenField1").nodeValue = result;
            },
            onFail: function (err) {
            }
        })
    })
});