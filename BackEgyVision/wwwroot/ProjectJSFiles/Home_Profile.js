var preview;
$(document).ready(function () {
    // load logo image
    DrowLogo();
    $("#btnSave").click(function () {
        var Data = new FormData();
        var img = document.getElementById('LogoFile').files;
        // Check file selected or not
        if (img.length > 0) {
            // Make sure `file.name` matches our extensions criteria
            if (!/\.(jpe?g|png|gif)$/i.test(img[0].name)) {
                $('#LogoFile').val('')
                return alert(img[0].name + " is not an image");
               
            }
            Data.append("LogoFile", img[0]);
        }
        $("#btnSave").prop('disabled', true);
        $.ajax({
            url: '/Home/EditLogo',
            type: 'POST',
            data: Data,
            contentType: false,
            processData: false,
            success: function (data) {
                alert("Logo Updated ");
            },
            error: function (err) {
                alert("Something Went Wrong!>>>>>>>>>");
            },
            complete: function (data) {
                $("#btnSave").prop('disabled', false);
            }
        });
    });
});
function previewImages() {
    var img;
    // if image of cover
    preview = document.querySelector('#previewlogoFile');
    img = document.getElementById('LogoFile').files;

    // Check file selected or not
    for (var i = 0; i < img.length; i++) {
        //NewGalleryPics.push(img[i]);
        readAndPreview(img[i]);
    }
}
function readAndPreview(file) {

    // check file size
    var reader = new FileReader();
    reader.addEventListener("load", function () {
        var image = new Image();
        image.title = file.name;
        image.src = this.result;
        // check original width & height
        preview.innerHTML = '';
        //image.height = 10;
        image.style.width = "150px";
        image.style.border = "3px solid white !important";
        image.style.padding = "5px";
        preview.appendChild(image);
    });
    reader.readAsDataURL(file);
}

function DrowLogo() {
    $.ajax({
        url: '/Home/LoadLogoAttachments',
        type: 'POST',
        success: function (data) {
            var img = new Image();
            img.style.width = "200px";
            //img.style.height = "200px";
            img.style.border = "3px solid white !important";
            img.style.padding = "5px";
            //img.style.borderRadius = "30%";
            img.src = "data:image/jpeg;base64," + data.logo.attachmentFile;
            document.querySelector('#previewlogoFile').appendChild(img);
        },
        error: function (err) {

        },
        complete: function (data) {

        }
    });
}

