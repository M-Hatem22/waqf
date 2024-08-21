
$(document).ready(function () {
    LoadProjectsGallery();
});

function LoadProjectsGallery() {
    $.ajax({
        url: '/Home/LoadProjectGallery?projectId=' + $("#ProjectId").val(),
        type: 'POST',
        success: function (data) {
            for (var i = 0; i < data.images.length; i++) {
                var projectsDiv = document.createElement('div'); // is a node
                projectsDiv.className = 'col-md-2 col-sm-6 col-xs-12 thumb';
                projectsDiv.innerHTML = `<a class="pop thumbnail" href="#" data-image-id="" data-toggle="modal" data-title=""
                                                           data-image = 'data:image/png;base64,${data.images[0].attachmentFile}'
                                                           data-target="#image-gallery">
                                                            <figure class="snip1584">
                                                                <div class="border_img">
                                                                    <img class=""
                                                                         src = data:image/png;base64,${data.images[0].attachmentFile}
                                                                         alt="Another alt text">
                                                                </div>
                                                                <figcaption>
                                                                    <i class="fas fa-search-plus"></i>
                                                                </figcaption>
                                                            </figure>
                                                        </a>`
                document.getElementById("GalleryPics").appendChild(projectsDiv);
            }
        },
        error: function (err) {

        },
        complete: function (data) {

        }
    });
}
