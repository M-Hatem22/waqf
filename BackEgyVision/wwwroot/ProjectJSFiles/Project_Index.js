$(document).ready(function () {
    // load datatable data
    $("#ItemsTable").dataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": false, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 3,
        "ajax": {
            "url": "/Projects/LoadProjects",
            "type": "POST",
            //"datatype": "json"
        },
        "columns":
            [
                { "data": "projectId", "name": "projectId", "autoWidth": true },
                { "data": "projectTitleAr", "name": "projectTitleAr", "autoWidth": true },
                { "data": "projectTitleEn", "name": "projectTitleEn", "autoWidth": true },
                {
                    data: null, render: function (data, type, row) {
                        return '<div>' + row.contentAr + '</div>'
                    }
                },
               /* { "data": "contentAr", "name": "contentAr", "autoWidth": true },*/
                { "data": "contentEn", "name": "contentEn", "autoWidth": true },
                {
                    data: null, render: function (data, type, row) {
                        return '<a href="#" data-toggle="modal" data-target="#notpopUp" onclick="DisplayImage(\'' + row.projectId + '\')"><img src="/Projects/DownProjectImage?projectId=' + row.projectId + '" alt="" /></a>'
                    },
                },
                {
                    data: null, render: function (data, type, row) {
                        return "<a href='#' class='btn btn-danger' onclick=DeleteProject('" + row.projectId + "'); > مسح </a>";
                    }
                },
                {
                    data: null, render: function (data, type, row) {
                        return "<button href='#'  class='btn btn-warning btn-xs Additme' onclick=ProjectDetails('" + row.projectId + "'); > <i class='fa fa-plus'></i>اضافة تفاصيل المشروع</button>";
                    }
                }
            ]
    });
});
function DisplayImage(ID) {
    var image = document.getElementById("ViewImage");
    image.src = "/Projects/DownProjectImage?projectId=" + ID;
}
function DeleteProject(projectId) {
    if (confirm("Are you want to delete this Project?")) {
        $.ajax({
            url: '/Projects/DeleteProject?projectId=' + projectId,
            type: 'POST',
            success: function (data) {
                if (data.Result == "Error") {
                    alert("لايمكنك حذف هذا السليدر");
                }
                else {
                    oTable = $('#ItemsTable').DataTable();
                    oTable.draw();
                }
            },
            error: function (err) {
                alert("Something Went Wrong!>>>>>>>>>");
            },
            complete: function (data) {
                //$("#btnCreateSlider").attr("disabled", false);
            }
        });
    }
    else {
        return false;
    }
}
function ProjectDetails(projectId) {
    var url = "/Projects/ProjectDetails?projectId=" + projectId;
    window.open(url, '_blank');
}


function resetAddSliderModel() {
    $('#TitleAr').val('');
    $('#TitleEn').val('');
    $('#ContentEn').val('');
    $('#ContentAr').val('');
    $('#SliderImg').val('');
}
function EditData(sliderId) {
    $.ajax({
        url: '/Slider/GetSliderById?sliderId=' + sliderId,
        type: 'POST',
        success: function (data) {
            if (data.slider !== null) {
                $('#ETitleAr').val(data.slider.sliderTitleAr);
                $('#EContentAr').val(data.slider.contentAr);
                $('#ETitleEn').val(data.slider.sliderTitleEn);
                $('#EContentEn').val(data.slider.contentEn);
                $('#EID').val(data.slider.sliderId);
                document.getElementById("ESliderImage").src = "data:image/png;base64," + data.slider.attachmentFile;
                $('#EditSlider').modal('show')
            }
        },
        error: function (err) {
            alert("Something Went Wrong!>>>>>>>>>");
        },
        complete: function (data) {
            $("#btnCreateSlider").attr("disabled", false);
        }
    });
}
function CreateSlider() {
    var SliderData = new FormData();
    var img = document.getElementById('SliderImg').files;
    // Check file selected or not
    if (img.length > 0) {
        SliderData.append("SliderImage", img[0]);
        if (img[0].size/1000 > 500) {
            alert("Invalid Image Size !");
            return;
        }
    }
    else {
        alert("Please Choose Image !");
        return;
    }
    SliderData.append("SliderTitleAr", $('#TitleAr').val());
    SliderData.append("SliderTitleEn", $('#TitleEn').val());
    SliderData.append("ContentEn", $('#ContentEn').val());
    SliderData.append("ContentAr", $('#ContentAr').val());
    if ($("#SlidersType option:selected").val() == 1)
        SliderData.append("MainSlider", "True");
    else
        SliderData.append("MainSlider", "False");

    $("#btnCreateSlider").attr("disabled", true);
    $.ajax({
        url: '/Slider/AddSlider',
        type: 'POST',
        data: SliderData,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.result === "OK") {
                $('.modal').modal('hide');
                oTable = $('#ItemsTable').DataTable();
                oTable.draw();
            }
        },
        error: function (err) {
            alert("Something Went Wrong!>>>>>>>>>");
        },
        complete: function (data) {
            $("#btnCreateSlider").attr("disabled", false);
            resetAddSliderModel();
        }
    });
}
function EditSlider() {
    var SliderData = new FormData();
    var img = document.getElementById('ESliderImg').files;
    // Check file selected or not
    if (img.length > 0) {
        SliderData.append("SliderImage", img[0]);
        if (img[0].size/1000 > 500) {
            alert("Invalid Image Size !");
            return;
        }
    }
    SliderData.append("SliderId", $('#EID').val());
    SliderData.append("SliderTitleAr", $('#ETitleAr').val());
    SliderData.append("SliderTitleEn", $('#ETitleEn').val());
    SliderData.append("ContentEn", $('#EContentEn').val());
    SliderData.append("ContentAr", $('#EContentAr').val());

    $("#btnCreateSlider").attr("disabled", true);
    $.ajax({
        url: '/Slider/EditSlider',
        type: 'POST',
        data: SliderData,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.result === "OK") {
                $('#EditSlider').modal('hide');
                oTable = $('#ItemsTable').DataTable();
                oTable.draw();
            }
        },
        error: function (err) {
            alert("Something Went Wrong!>>>>>>>>>");
        },
        complete: function (data) {
            $("#btnCreateSlider").attr("disabled", false);
        }
    });
}
