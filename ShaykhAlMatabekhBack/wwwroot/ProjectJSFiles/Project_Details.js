var preview;
var counter = 0;
var NewGalleryPics = [], oldGalleryPics = [], NewCoverPic = [];
$(document).ready(function () {
    $('#summernote1').summernote('code', $("#ContentEn").val());
    $('#summernote2').summernote('code', $("#ContentAr").val());

    $("#btnSubmit").click(function () {
        SubmitFormData();
    });

    if ($("#ProjectId").val() > 0) {
        // display cover and gallery images
        DrowCoverPicture();
        DrowGalleryPictures();
    }
});
function DrowCoverPicture() {
    $.ajax({
        url: '/Projects/LoadProjectAttachments?keyTypeId=3&projectId=' + $("#ProjectId").val(),
        type: 'POST',
        success: function (data) {
            var img = new Image();
            img.style.width = "200px";
            img.style.height = "190px";
            img.style.border = "3px solid white !important";
            img.style.padding = "5px";
            //img.style.borderRadius = "30%";
            img.src = "data:image/jpeg;base64," + data.images[0].attachmentFile;
            document.querySelector('#previewCoverFile').appendChild(img);
        },
        error: function (err) {

        },
        complete: function (data) {

        }
    });
}
function DrowGalleryPictures() {
    $.ajax({
        url: '/Projects/LoadProjectAttachments?keyTypeId=4&projectId=' + $("#ProjectId").val(),
        type: 'POST',
        success: function (data) {
            // Check file selected or not
            for (var i = 0; i < data.images.length; i++) {
                oldGalleryPics.push(data.images[i]);
                var div2 = document.createElement("div");   // Create a <button> element
                div2.setAttribute("id", "div" + data.images[i].attachmentId);
                div2.classList.add('col-sm-3');
                //div2.setAttribute('width', "440");
                //div2.setAttribute('height', "390");
                div2.innerHTML = '<div class="success callout "><button style="color: red; background: black;" class="close-button" onclick="DeleteGalleryPic(' + data.images[i].attachmentId + ')" type="button"><span aria-hidden="true">&times;</span></button></div >'
                var img = new Image();
                img.style.width = "200px";
                img.style.height = "200px";
                img.style.border = "3px solid white !important";
                img.style.padding = "5px";
                img.style.borderRadius = "30%";

                img.src = "data:image/jpeg;base64," + data.images[i].attachmentFile;
                div2.appendChild(img);
                document.querySelector('#previewGalleryFiles').appendChild(div2);
            }
        },
        error: function (err) {

        },
        complete: function (data) {

        }
    });
}
function DeleteGalleryPic(attachId) {
    if (confirm('Are you Sure !')) {
        if (oldGalleryPics.length <= 1 && NewGalleryPics.length > 0) {
            alert('Please Save the new picture before delete this !');
            return;
        }
        if (oldGalleryPics.length <= 1 && NewGalleryPics.length == 0) {
            alert('Can not Delete Last Image !');
            return;
        }
        $.ajax({
            url: '/Projects/DeleteAttachment?attachmentId=' + attachId,
            type: 'POST',
            success: function (data) {
                if (data.result == "OK") {
                    oldGalleryPics = $.grep(oldGalleryPics, function (item) {
                        return item.attachmentId !== attachId;
                    });
                    $("#div" + attachId).hide();
                    alert('Image Deleted ');
                }
            },
            error: function (err) {
                alert('Can Not Delete Image');
            },
            complete: function (data) {

            }
        });
    }
}
function DeleteNewGalleryPic(fileName, index) {
    if (confirm('Are you Sure !')) {
        NewGalleryPics = $.grep(NewGalleryPics, function (item) {
            return item.name !== fileName;
        });
        $("#div" + index).hide();
    }
}

function previewImages(index) {
    var img;
    // if image of cover
    if (index == 1) {
        preview = document.querySelector('#previewCoverFile');

        img = document.getElementById('CoverFile').files;
    }
    else {
        preview = document.querySelector('#previewGalleryFiles');
        img = document.getElementById('GalleryFiles').files;
    }

    // Check file selected or not
    for (var i = 0; i < img.length; i++) {
        // Make sure `file.name` matches our extensions criteria
        if (!/\.(jpe?g|png|gif)$/i.test(img[i].name)) {
            alert(img[i].name + " is not an image");
            continue;
        }
        if (index == 1) {
            CoverPic = [];
            readAndPreviewCover(img[i]);
        }
        else {
            if ((oldGalleryPics.length + NewGalleryPics.length) >= 10) {
                alert("Can Not Add More Than 10 Picture For Project !");
                return;
            }
            //NewGalleryPics.push(img[i]);
            readAndPreviewGallery(img[i]);
        }
    }

}
function readAndPreviewGallery(file) {
    var tempCount = ++counter;
    // check file size 
    if (Math.round(file.size / 1024) > 500) {
        alert("File too Big, please select a file less than 500kb");
        return;
    }
    else {
        var reader = new FileReader();
        reader.addEventListener("load", function () {
            var div = document.createElement("div");   // Create a <button> element
            div.setAttribute("id", "div" + tempCount);
            div.classList.add('col-sm-3');
            div.innerHTML = `<div class="success callout"><button style="color: red; background: black;" class="close-button" onClick="DeleteNewGalleryPic('${file.name}', ${tempCount})" type="button" ><span aria-hidden="true">&times;</span></button></div >`
            var image = new Image();
            image.title = file.name;
            image.src = this.result;
            // check original width & height
            if (image.height > 1200 || image.width > 900) {
                alert(`Sorry, this image doesn't look like the size we wanted. It's 
                        ${image.width} x ${image.height} but we require 1200 x 900 size image.`);
            }
            else {
                NewGalleryPics.push(file);
                image.style.width = "200px";
                image.style.height = "200px";
                image.style.border = "3px solid white !important";
                image.style.padding = "5px";
                image.style.borderRadius = "30%";
                div.appendChild(image);
                preview.appendChild(div);
            }
        });
        reader.readAsDataURL(file);
    }
}
function readAndPreviewCover(file) {

    // check file size
    if (file.size / 1024 > 500) {
        alert("File too Big, please select a file less than 500kb");
        $('#CoverFile').val('')
        return;
    }
    var reader = new FileReader();
    reader.addEventListener("load", function () {
        var image = new Image();

        image.title = file.name;
        image.src = this.result;
        // check original width & height
        if (image.height > 900 || image.width > 900) {
            alert(`Sorry, this image doesn't look like the size we wanted. It's 
                        ${image.width} x ${image.height} but we require 900 x 900 size image.`);
            $('#CoverFile').val('')
            return;
        }
        preview.innerHTML = '';
        NewCoverPic.push(file);
        image.height = 10;
        image.style.width = "150px";
        image.style.border = "3px solid white !important";
        //image.style.padding = "5px";
        preview.appendChild(image);
    });
    reader.readAsDataURL(file);
}
function SubmitFormData() {

    // check if the new project has images
    if ($("#ProjectId").val() <= 0) {
        if (NewCoverPic.length <= 0) {
            alert("Choose At Least One Picture For Project Cover !");
            return;
        }
        if (NewGalleryPics.length <= 0) {
            alert("Choose At Least One Picture For Project Gallery !");
            return;
        }
    }
    var ProjectData = new FormData();
    ProjectData.append("ProjectId", $('#ProjectId').val());
    ProjectData.append("ProjectTitleAr", $('#ProjectTitleAr').val());
    ProjectData.append("ProjectTitleEn", $('#ProjectTitleEn').val());
    ProjectData.append("ContentAr", $("#summernote2").summernote('code'));
    ProjectData.append("ContentEn", $("#summernote1").summernote('code'));
    NewGalleryPics.forEach(function (image, i) {
        ProjectData.append('GalleryFiles' , image);
    });
    NewCoverPic.forEach(function (image, i) {
        ProjectData.append('CoverFile', image);
    });
    //ProjectData.append("GalleryFiles", NewGalleryPics);
    //ProjectData.append("CoverFile", NewCoverPic);
    $("#btnSubmit").prop('disabled', true);
    $.ajax({
        url: '/Projects/UpdateProject',
        type: 'POST',
        data: ProjectData,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.result === "OK") {
                alert("Project Updated");
                location.reload();
            }
        },
        error: function (err) {
            alert("Something Went Wrong!>>>>>>>>>");
        },
        complete: function (data) {
            $("#btnSubmit").prop('disabled', false);
        }
    });
}