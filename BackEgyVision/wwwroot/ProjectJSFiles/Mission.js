﻿$(document).ready(function () {
    $('#summernote2').summernote('code', $("#ContentAr").val());
    $('#summernote1').summernote('code', $("#ContentEn").val());
    $("#btnSave").click(function () {
        $("#ContentEn").val($("#summernote1").summernote('code'));
        $("#ContentAr").val($("#summernote2").summernote('code'));
        $("#frmData").submit();
    });
});
