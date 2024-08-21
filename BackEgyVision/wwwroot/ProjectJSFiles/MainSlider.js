$(document).ready(function () {
    // load datatable data
    $("#ItemsTable").dataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": false, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 3,
        "ajax": {
            "url": "/Slider/LoadSliders",
            "type": "POST",
        },
        "columns":
            [
                { "data": "sliderId", "name": "sliderId", "autoWidth": true },
                {
                    data: null, render: function (data, type, row) {
                        if (row.mainSlider)
                            return '<label>Main Slider</label>'
                        else
                            return '<label>Sub Slider</label>'
                    },
                },
                { "data": "sliderTitleAr", "name": "sliderTitleAr", "autoWidth": true },
                { "data": "sliderTitleEn", "name": "sliderTitleEn", "autoWidth": true },
                { "data": "contentAr", "name": "contentAr", "autoWidth": true },
                { "data": "contentEn", "name": "contentEn", "autoWidth": true },
                {
                    data: null, render: function (data, type, row) {
                        return '<a href="#" data-toggle="modal" data-target="#notpopUp" onclick="DisplayImage(\'' + row.sliderId + '\')"><img src="/Slider/DownSliderImage?sliderId=' + row.sliderId + '" alt="" /></a>'
                    },
                },
                {
                    data: null, render: function (data, type, row) {
                        return '<button  class="btn btn-info" onclick=EditData(' + row.sliderId + ');>Edit</button>';
                    }
                },
                {
                    data: null, render: function (data, type, row) {
                        return "<a href='#' class='btn btn-danger' onclick=DeleteSlider('" + row.sliderId + "'); >Delete</a>";
                    }
                }
            ]
    });

    // create slider button
    $("#btnCreateSlider").click(function () {
        CreateSlider();
    });
    // Edit slider button
    $("#EbtnEditSlider").click(function () {
        EditSlider();
    });
    // close model create slider
    $('#CloseModel').on('click', function () {
        $('.modal').modal('hide')
    });
    // close model create slider
    $('#ECloseModel').on('click', function () {
        $('#EditSlider').modal('hide')
    });

});
function DisplayImage(ID) {
    var image = document.getElementById("ViewImage");
    image.src = "/Slider/DownSliderImage?sliderId=" + ID;
}
function DeleteSlider(sliderId) {
    if (confirm("Are you want to delete this Slider?")) {
        $.ajax({
            url: '/Slider/DeleteSlider?sliderId=' + sliderId,
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
                $("#btnCreateSlider").attr("disabled", false);
            }
        });
    }
    else {
        return false;
    }
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
                if (data.slider.mainSlider == true)
                    $("#ESlidersType").prop('selectedIndex', 0);
                else
                    $("#ESlidersType").prop('selectedIndex', 1);
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
    if (img.length <= 0) {
        alert("Please Choose Image !");
        return;
    }
    SliderData.append("SliderTitleAr", $('#TitleAr').val());
    SliderData.append("SliderTitleEn", $('#TitleEn').val());
    SliderData.append("ContentEn", $('#ContentEn').val());
    SliderData.append("ContentAr", $('#ContentAr').val());
    SliderData.append("SliderImage", img[0]);
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
        // Make sure `file.name` matches our extensions criteria
        if (!/\.(jpe?g|png|gif)$/i.test(img[0].name)) {
            return alert(img[0].name + " is not an image");
            $('#ESliderImg').val('')
        }
        if (img[0].size/1024 > 500) {
            alert("Invalid Image Size !");
            $('#ESliderImg').val('')
            return;
        }
        SliderData.append("SliderImage", img[0]);
    }
    SliderData.append("SliderId", $('#EID').val());
    SliderData.append("SliderTitleAr", $('#ETitleAr').val());
    SliderData.append("SliderTitleEn", $('#ETitleEn').val());
    SliderData.append("ContentEn", $('#EContentEn').val());
    SliderData.append("ContentAr", $('#EContentAr').val());
    if ($("#ESlidersType option:selected").val() == 1)
        SliderData.append("MainSlider", "True");
    else
        SliderData.append("MainSlider", "False");

    $("#EbtnEditSlider").prop('disabled', true);
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
                $('#ESliderImg').val('');
            }
        },
        error: function (err) {
            alert("Something Went Wrong!>>>>>>>>>");
        },
        complete: function (data) {
            $("#EbtnEditSlider").prop('disabled', false);
        }
    });
}
function resetAddSliderModel() {
    $('#TitleAr').val('');
    $('#TitleEn').val('');
    $('#ContentEn').val('');
    $('#ContentAr').val('');
    $('#SliderImg').val('');
}
function Filevalidation() {
    debugger;
    var fi = document.getElementById('ESliderImg');
    // Check if any file is selected.
    if (fi.files.length > 0) {
        for (var i = 0; i < fi.files.length; i++) {
            // Make sure `file.name` matches our extensions criteria
            if (!/\.(jpe?g|png|gif)$/i.test(fi.files[i].name)) {
                alert(fi.files[i].name + " is not an image");
                $('#ESliderImg').val('')
                continue;
            }
            var fsize = fi.files.item(i).size;
            var file = Math.round((fsize / 1024));
            // check image width and hieght
            //Read the contents of Image File.
            var reader = new FileReader();
            //Read the contents of Image File.
            debugger;

            reader.readAsDataURL(fi.files[i]);
            reader.onload = function (e) {
                //Initiate the JavaScript Image object.
                var image = new Image();
                //Set the Base64 string return from FileReader as source.
                image.src = e.target.result;
                //Validate the File Height and Width.
                image.onload = function () {
                    if ($('#ESlidersType').val() == 1) {
                        if (this.width > 1920 || this.height > 1080) {
                            alert(`Sorry, this image doesn't look like the size we wanted. It's 
                        ${this.width} x ${this.height} but we require 1920 x 1080 size image.`);
                            $('#ESliderImg').val('')
                            return;
                        }
                    }
                    else {
                        if (this.width > 300 || this.height > 500) {
                            alert(`Sorry, this image doesn't look like the size we wanted. It's 
                        ${this.width} x ${this.height} but we require 300 x 500 size image.`);
                            $('#ESliderImg').val('')
                            return;
                        }
                    }
                };
            };

            if ($('#ESlidersType').val() == 1) // main slider
            {
                if (file > 500) {
                    alert("File too Big, please select a file less than 500kb");
                    $('#ESliderImg').val('')
                    return;
                }
            }
            else // secondary slider
            {
                if (file > 500) {
                    alert("File too Big, please select a file less than 500kb");
                    $('#ESliderImg').val('')
                    return;
                }
            }
        }
    }
}
function CreateFilevalidation() {

    const fi = document.getElementById('SliderImg');
    // Check if any file is selected.
    if (fi.files.length > 0) {
        for (var i = 0; i < fi.files.length; i++) {
            // Make sure `file.name` matches our extensions criteria
            if (!/\.(jpe?g|png|gif)$/i.test(fi.files[i].name)) {
                alert(fi.files[i].name + " is not an image");
                $('#SliderImg').val('')

                continue;
            }
            var fsize = fi.files.item(i).size;
            var file = Math.round((fsize / 1024));
            if ($('#SlidersType').val() == 1) // main slider
            {
                if (file > 500) {
                    alert("File too Big, please select a file less than 500kb");
                    $('#SliderImg').val('')
                }
            }
            else // secondary slider
            {
                if (file > 500) {
                    alert("File too Big, please select a file less than 500kb");
                    $('#SliderImg').val('')
                }
            }
        }
    }
}